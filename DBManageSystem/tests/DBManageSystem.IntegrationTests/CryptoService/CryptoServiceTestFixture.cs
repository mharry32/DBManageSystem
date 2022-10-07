using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using DBManageSystem.Core;
using DBManageSystem.Core.Constants;
using DBManageSystem.Infrastructure;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DBManageSystem.IntegrationTests.CryptoService;
public class CryptoServiceTestFixture
{
  public IServiceProvider _serviceProvider { get; private set; }

  public CryptoServiceTestFixture()
  {
    var serviceCollection = new ServiceCollection();
    serviceCollection.AddLogging(t=>t.AddConsole());
    serviceCollection.AddDataProtection().SetDefaultKeyLifetime(TimeSpan.FromDays(365 * 100))
      .SetApplicationName(ApplicationConstants.APP_NAME)
      .PersistKeysToFileSystem(new DirectoryInfo(AppContext.BaseDirectory)); ;


    var containerBuilder = new ContainerBuilder();
    containerBuilder.Populate(serviceCollection);

    containerBuilder.RegisterModule(new DefaultCoreModule());
    containerBuilder.RegisterModule(new DefaultInfrastructureModule(true));
    var container = containerBuilder.Build();
    _serviceProvider = new AutofacServiceProvider(container);
  }

}
