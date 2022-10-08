using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac.Extensions.DependencyInjection;
using Autofac;
using DBManageSystem.Core.Constants;
using DBManageSystem.Core;
using DBManageSystem.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DBManageSystem.IntegrationTests.DbOpServices;
public class DbOpServicesTestFixture
{
  public IServiceProvider _serviceProvider { get; private set; }

  public DbOpServicesTestFixture()
  {
    var serviceCollection = new ServiceCollection();
    serviceCollection.AddLogging(t => t.AddConsole());
    var containerBuilder = new ContainerBuilder();
    containerBuilder.Populate(serviceCollection);

    containerBuilder.RegisterModule(new DefaultCoreModule());
    containerBuilder.RegisterModule(new DefaultInfrastructureModule(true));
    var container = containerBuilder.Build();
    _serviceProvider = new AutofacServiceProvider(container);
  }
}
