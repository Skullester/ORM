using BLL;
using BLL.Cache;
using BLL.Distributors;
using BLL.DTO;
using BLL.Mapping;
using BLL.Providers;
using BLL.Providers.Container;
using BLL.Modules;
using Ninject;
using Ninject.Modules;

namespace UI;

public class Program
{
    private class InitializeModule : NinjectModule
    {
        public override void Load()
        {
            Kernel!.Bind<IManager>()
                .To<Manager>()
                .InSingletonScope();
            Kernel.Bind<IMapper>()
                .To<Mapper>()
                .InSingletonScope();
            Kernel.Bind<ICloser>()
                .To<Closer>()
                .InSingletonScope();
            BindDistributors();
            BindProviders();
            BindCacher();
        }

        private void BindCacher()
        {
            Kernel!.Bind<ICacher>()
                .To<Cacher>()
                .InSingletonScope();
        }

        private void BindDistributors()
        {
            Kernel!.Bind<IDistributor>()
                .To<Distributor>()
                .InSingletonScope();
            Kernel.Bind<DTODistributor<GroupDTO>>()
                .To<GroupDTODistributor>()
                .InSingletonScope();
            Kernel.Bind<DTODistributor<TeacherDTO>>()
                .To<TeacherDTODistributor>()
                .InSingletonScope();
            Kernel.Bind<DTODistributor<SemesterDTO>>()
                .To<SemesterDTODistributor>()
                .InSingletonScope();
        }

        private void BindProviders()
        {
            Kernel!.Bind<IProviderContainer>()
                .To<ProviderContainer>()
                .InSingletonScope();
            Kernel!.Bind<IGradeProvider>()
                .To<GradeProvider>()
                .InSingletonScope();
            Kernel.Bind<IStudentProvider>()
                .To<StudentProvider>()
                .InSingletonScope();
            Kernel.Bind<IDisciplineProvider>()
                .To<DisciplineProvider>()
                .InSingletonScope();
            Kernel.Bind<IGradeStudentDisciplineProvider>()
                .To<GradeStudentDisciplineProvider>()
                .InSingletonScope();
        }
    }

    private static void Main()
    {
        var kernel = GetKernel();
        var manager = kernel.Get<IManager>();
        manager.Start();
    }

    private static IKernel GetKernel() => new StandardKernel(new InitializeModule(), new UnitWorkModule());
}