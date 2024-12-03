using DAL.ORM;
using Ninject.Modules;

namespace BLL.Modules;

public class UnitWorkModule : NinjectModule
{
    public override void Load()
    {
        Bind<IUnitWork>().To<EFUnitWork>().InSingletonScope();
    }
}