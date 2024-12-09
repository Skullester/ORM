using BLL;
using BLL.Cache;
using BLL.Distributors;
using BLL.DTO;
using BLL.Mapping;
using BLL.Services;
using BLL.Services.Container;
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
            Bind<IManager>()
                .To<Manager>()
                .InSingletonScope();
            Bind<IMapper>()
                .To<Mapper>()
                .InSingletonScope();
            Bind<ICloser>()
                .To<Closer>()
                .InSingletonScope();
            BindDistributors();
            BindServices();
            BindCacher();
        }

        private void BindCacher()
        {
            Bind<ICacher>()
                .To<Cacher>()
                .InSingletonScope();
        }

        private void BindDistributors()
        {
            Bind<IDistributor>()
                .To<Distributor>()
                .InSingletonScope();
            Bind<DTODistributor<GroupDTO>>()
                .To<GroupDTODistributor>()
                .InSingletonScope();
            Bind<DTODistributor<TeacherDTO>>()
                .To<TeacherDTODistributor>()
                .InSingletonScope();
            Bind<DTODistributor<SemesterDTO>>()
                .To<SemesterDTODistributor>()
                .InSingletonScope();
        }

        private void BindServices()
        {
            Bind<IServiceContainer>()
                .To<ServiceContainer>()
                .InSingletonScope();
            Bind<IGradeService>()
                .To<GradeService>()
                .InSingletonScope();
            Bind<IStudentService>()
                .To<StudentService>()
                .InSingletonScope();
            Bind<IDisciplineService>()
                .To<DisciplineService>()
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