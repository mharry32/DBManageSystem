using Autofac;
using DBManageSystem.Core.Interfaces;
using DBManageSystem.Core.Services;

namespace DBManageSystem.Core;
public class DefaultCoreModule : Module
{
  protected override void Load(ContainerBuilder builder)
  {
    builder.RegisterType<MenuService>()
        .As<IMenuService>().InstancePerLifetimeScope();
  }
}
