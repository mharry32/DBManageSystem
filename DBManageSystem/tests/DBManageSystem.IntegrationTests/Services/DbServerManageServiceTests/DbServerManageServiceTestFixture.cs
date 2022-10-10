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
using Microsoft.AspNetCore.DataProtection;
using DBManageSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using DBManageSystem.Core.Entities;
using DBManageSystem.Core.Enums;
using DBManageSystem.Core.Interfaces;
using Serilog;

namespace DBManageSystem.IntegrationTests.Services.DbServerManageServiceTests;
public class DbServerManageServiceTestFixture
{
  public IServiceProvider _serviceProvider { get; private set; }

  public DatabaseServer TestUpdateDbServer { get; private set; }

  public DatabaseServer TestDeleteDbServer { get; private set; }

  public int TestGetDbServerId { get; private set; }

  public string OrdinaryPassword { get; private set; }

  public DbManageSysDbContext dbContext { get; private set; }

  public IDatabaseServerService databaseServerService { get; private set; }

  public DbServerManageServiceTestFixture()
  {
    var serviceCollection = new ServiceCollection();
    var logger = new LoggerConfiguration()
    .WriteTo.File("log-.txt", rollingInterval: RollingInterval.Day).CreateLogger();

    serviceCollection.AddLogging(logBuilder => logBuilder.AddSerilog(logger));
    serviceCollection.AddDataProtection().DisableAutomaticKeyGeneration()
      .SetApplicationName(ApplicationConstants.APP_NAME)
      .PersistKeysToFileSystem(new DirectoryInfo(AppContext.BaseDirectory));

    serviceCollection.AddDbContext<DbManageSysDbContext>(op =>
    {
      op.UseMySql(TestConstants.DbConnectString, MySqlServerVersion.LatestSupportedServerVersion);
    });
    var containerBuilder = new ContainerBuilder();
    containerBuilder.Populate(serviceCollection);

    containerBuilder.RegisterModule(new DefaultCoreModule());
    containerBuilder.RegisterModule(new DefaultInfrastructureModule(true));
    var container = containerBuilder.Build();
    _serviceProvider = new AutofacServiceProvider(container);
    PrepareTestDatas();
  }

  private void PrepareTestDatas()
  {
      var scope = _serviceProvider.CreateScope();
      dbContext = scope.ServiceProvider.GetRequiredService<DbManageSysDbContext>();
      dbContext.Database.EnsureDeleted();
      dbContext.Database.EnsureCreated();
      var cryptoService = scope.ServiceProvider.GetRequiredService<IDataProtectionProvider>();
      databaseServerService = scope.ServiceProvider.GetRequiredService<IDatabaseServerService>();
      var protector = cryptoService.CreateProtector("DbPasswordCrypto");
      DatabaseServer serverForTestUpdate = new DatabaseServer()
      {
        ConnectUrl = "testUrl",
        UserName = "test",
        Password = "testPassword",
        Name = "test",
        DatabaseType = DatabaseTypeEnum.MySQL,
        IsMonitored = true
      };

      DatabaseServer serverForTestDelete = new DatabaseServer()
      {
        ConnectUrl = "testUrl",
        UserName = "test",
        Password = "testPassword",
        Name = "testDelete",
        DatabaseType = DatabaseTypeEnum.SQLServer,
        IsMonitored = true
      };

      OrdinaryPassword = "testPurpose123";
     
      DatabaseServer serverForTestGet = new DatabaseServer()
      {
        ConnectUrl = "testUrl",
        UserName = "test",
        Password = protector.Protect(OrdinaryPassword),
        Name = "testGet",
        DatabaseType = DatabaseTypeEnum.SQLServer,
        IsMonitored = true
      };

      dbContext.Add(serverForTestUpdate);
      dbContext.Add(serverForTestDelete);
      dbContext.Add(serverForTestGet);
      dbContext.SaveChanges();

      TestUpdateDbServer = dbContext.databaseServers.FirstOrDefault(t => t.Name == serverForTestUpdate.Name);
      TestDeleteDbServer = dbContext.databaseServers.FirstOrDefault(t => t.Name == serverForTestDelete.Name);
      TestGetDbServerId = dbContext.databaseServers.FirstOrDefault(t => t.Name == serverForTestGet.Name).Id;
    
  }

}
