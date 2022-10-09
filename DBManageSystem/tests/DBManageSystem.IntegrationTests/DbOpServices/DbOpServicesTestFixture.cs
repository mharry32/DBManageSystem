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
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using System.Security.Policy;
using DBManageSystem.Core.Entities;
using DBManageSystem.Core.Enums;

namespace DBManageSystem.IntegrationTests.DbOpServices;
public class DbOpServicesTestFixture
{
  public IServiceProvider _serviceProvider { get; private set; }
  public string DbUrl { get => dbUrl; set => dbUrl = value; }
  public string MysqlUserName { get => mysqlUserName; set => mysqlUserName = value; }
  public string SqlServerUserName { get => sqlServerUserName; set => sqlServerUserName = value; }
  public string Password { get => password; set => password = value; }
  public string DatabaseName { get => databaseName; set => databaseName = value; }
  public TestContext SqlserverContext { get => sqlserverContext; set => sqlserverContext = value; }
  public TestContext MysqlContext { get => mysqlContext; set => mysqlContext = value; }

  string dbUrl = "localhost";
  string mysqlUserName = "root";
  string sqlServerUserName = "sa";
  string password = "testPurpose123";
  string databaseName = "TestMySqlExecuteSql";

  TestContext sqlserverContext;

  TestContext mysqlContext;

  public DatabaseServer mysql;

  public DatabaseServer sqlserver;
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
    PrepareMySqlContext();
    PrepareSqlServerContext();

    InitMySqlDatabaseServerEntity();
    InitSqlServerDatabaseServerEntity();
  }

  private void PrepareMySqlContext()
  {
    DbContextOptionsBuilder builder = new DbContextOptionsBuilder();
    DbConnectionStringBuilder stringbuilder = new DbConnectionStringBuilder
    {
      { "Server", dbUrl },
      { "Database", databaseName },
      { "Uid", mysqlUserName },
      { "Pwd", password },
      { "charset", "UTF8" }
    };
    builder.UseMySql(stringbuilder.ConnectionString, MySqlServerVersion.LatestSupportedServerVersion);
    mysqlContext = new TestContext(builder.Options);
    mysqlContext.Database.EnsureDeleted();
    mysqlContext.Database.EnsureCreated();
  }


  private void InitMySqlDatabaseServerEntity()
  {
    mysql = new DatabaseServer();
    mysql.ConnectUrl = DbUrl;
    mysql.DatabaseType = DatabaseTypeEnum.MySQL;
    mysql.UserName = MysqlUserName;
    mysql.Password = Password;
  }

  private void InitSqlServerDatabaseServerEntity()
  {
    sqlserver = new DatabaseServer();
    sqlserver.ConnectUrl = DbUrl;
    sqlserver.DatabaseType = DatabaseTypeEnum.SQLServer;
    sqlserver.UserName = SqlServerUserName;
    sqlserver.Password = Password;
  }


  private void PrepareSqlServerContext()
  {
    DbContextOptionsBuilder builder = new DbContextOptionsBuilder();
    DbConnectionStringBuilder stringbuilder = new DbConnectionStringBuilder
    {
      { "Data Source", dbUrl },
      { "Initial Catalog", databaseName },
      { "User Id", sqlServerUserName },
      { "Password", password }
    };
    builder.UseSqlServer(stringbuilder.ConnectionString);
    sqlserverContext = new TestContext(builder.Options);
    sqlserverContext.Database.EnsureDeleted();
    sqlserverContext.Database.EnsureCreated();
  }
}
