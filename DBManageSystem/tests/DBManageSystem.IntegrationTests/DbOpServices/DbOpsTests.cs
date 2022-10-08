using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBManageSystem.Core.Entities;
using DBManageSystem.Core.Enums;
using DBManageSystem.Core.Interfaces;
using DBManageSystem.IntegrationTests.CryptoService;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace DBManageSystem.IntegrationTests.DbOpServices;
public class DbOpsTests:IClassFixture<DbOpServicesTestFixture>
{
  IServiceProvider _serviceProvider { get; }
  public DbOpsTests(DbOpServicesTestFixture fixture)
  {
    _serviceProvider = fixture._serviceProvider;
  }

  [Fact]
  public async Task TestMySqlExecuteSql()
  {
    using (var scope = _serviceProvider.CreateScope())
    {
      DatabaseServer mysql = new DatabaseServer();
      mysql.ConnectUrl = "localhost";
      mysql.DatabaseType = DatabaseTypeEnum.MySQL;
      mysql.UserName = "root";
      mysql.Password = "1995072132Mh.";
      var strategy = scope.ServiceProvider.GetRequiredService<IDbServiceStrategy>();
      var service = strategy.Decide(mysql.DatabaseType);

      DbContextOptionsBuilder builder = new DbContextOptionsBuilder();
      DbConnectionStringBuilder stringbuilder = new DbConnectionStringBuilder
    {
      { "Server", mysql.ConnectUrl },
      { "Database", "TestMySqlExecuteSql" },
      { "Uid", mysql.UserName },
      { "Pwd", mysql.Password },
      { "charset", "UTF8" }
    };
      builder.UseMySql(stringbuilder.ConnectionString, MySqlServerVersion.LatestSupportedServerVersion);
      TestContext dbcontext = new TestContext(builder.Options);
      dbcontext.Database.EnsureDeleted();
      dbcontext.Database.EnsureCreated();


      var result = await service.ExecuteSql(mysql, "TestMySqlExecuteSql", "select nulldate from testEntities");

      Assert.NotNull(result);
    }
  }
}
