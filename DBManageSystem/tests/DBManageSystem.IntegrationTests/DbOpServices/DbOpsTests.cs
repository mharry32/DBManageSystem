using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using DBManageSystem.Core.Entities;
using DBManageSystem.Core.Enums;
using DBManageSystem.Core.Interfaces;
using DBManageSystem.IntegrationTests.CryptoService;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using Ardalis.HttpClientTestExtensions;

namespace DBManageSystem.IntegrationTests.DbOpServices;

[Collection("DbOpsTests")]
public class DbOpsTests:IClassFixture<DbOpServicesTestFixture>
{
  IServiceProvider _serviceProvider { get; }

  DbOpServicesTestFixture _fixture { get; }
  public DbOpsTests(DbOpServicesTestFixture fixture)
  {
    _fixture = fixture;
    _serviceProvider = fixture._serviceProvider;
  }

  

  [Fact]
  public async Task TestMySqlExecuteSql()
  {
    using (var scope = _serviceProvider.CreateScope())
    {

      var strategy = scope.ServiceProvider.GetRequiredService<IDbServiceStrategy>();
      var service = strategy.Decide(_fixture.mysql.DatabaseType);
      var result = await service.ExecuteSql(_fixture.mysql, "TestMySqlExecuteSql", "select * from testEntities");
      List<TestEntityDTO> testEntities = JsonSerializer.Deserialize<List<TestEntityDTO>>(result.JsonData);

      List<string> testEntitieHeaders = JsonSerializer.Deserialize<List<string>>(result.JsonHeader);

      Assert.True(result.IsSuccess);
      Assert.True(result.HasData);

      Assert.NotEmpty(testEntities);
      Assert.NotEmpty(testEntitieHeaders);

      Assert.Contains(testEntities, t => t.Name == "test!2");
    }
  }

  [Fact]
  public async Task TestSqlServerExecuteSql()
  {
    using (var scope = _serviceProvider.CreateScope())
    {

      var strategy = scope.ServiceProvider.GetRequiredService<IDbServiceStrategy>();
      var service = strategy.Decide(_fixture.sqlserver.DatabaseType);
      var result = await service.ExecuteSql(_fixture.sqlserver, "TestMySqlExecuteSql", "select * from testEntities");
      List<TestEntityDTO> testEntities = JsonSerializer.Deserialize<List<TestEntityDTO>>(result.JsonData);

      List<string> testEntitieHeaders = JsonSerializer.Deserialize<List<string>>(result.JsonHeader);

      Assert.True(result.IsSuccess);
      Assert.True(result.HasData);

      Assert.NotEmpty(testEntities);
      Assert.NotEmpty(testEntitieHeaders);

      Assert.Contains(testEntities, t => t.Name == "test!2");
    }
  }



  [Fact]
  public async Task TestSqlServerGetDatabaseSchema()
  {
    using (var scope = _serviceProvider.CreateScope())
    {
      var strategy = scope.ServiceProvider.GetRequiredService<IDbServiceStrategy>();
      var service = strategy.Decide(_fixture.sqlserver.DatabaseType);

      var result = await service.GetDatabaseSchemas(_fixture.sqlserver);
      Assert.True(result.IsSuccess);
      Assert.Contains(result.Value, t => t.Name == _fixture.DatabaseName);
    }
  }

  [Fact]
  public async Task TestMySqlGetDatabaseSchema()
  {
    using (var scope = _serviceProvider.CreateScope())
    {
      var strategy = scope.ServiceProvider.GetRequiredService<IDbServiceStrategy>();
      var service = strategy.Decide(_fixture.mysql.DatabaseType);

      var result = await service.GetDatabaseSchemas(_fixture.mysql);
      Assert.True(result.IsSuccess);
      Assert.Contains(result.Value, t => t.Name == _fixture.DatabaseName.ToLower());
    }
  }


  [Fact]
  public async Task TestMySqlGetTableSchema()
  {
    using (var scope = _serviceProvider.CreateScope())
    {
      var strategy = scope.ServiceProvider.GetRequiredService<IDbServiceStrategy>();
      var service = strategy.Decide(_fixture.mysql.DatabaseType);

      var result = await service.GetTableSchemas(_fixture.mysql,_fixture.DatabaseName);
      Assert.True(result.IsSuccess);

      var tableName = nameof(TestContext.testEntities).ToLower();
      Assert.Contains(result.Value, t => t.Name == tableName);

    }
  }


  [Fact]
  public async Task TestSqlServerGetTableSchema()
  {
    using (var scope = _serviceProvider.CreateScope())
    {
      var strategy = scope.ServiceProvider.GetRequiredService<IDbServiceStrategy>();
      var service = strategy.Decide(_fixture.sqlserver.DatabaseType);

      var result = await service.GetTableSchemas(_fixture.sqlserver, _fixture.DatabaseName);
      Assert.True(result.IsSuccess);
      var tableName = nameof(TestContext.testEntities);
      Assert.Contains(result.Value, t => t.Name == tableName);

    }
  }


  [Fact]
  public async Task TestSqlServerGetColumnSchema()
  {
    using (var scope = _serviceProvider.CreateScope())
    {
      var strategy = scope.ServiceProvider.GetRequiredService<IDbServiceStrategy>();
      var service = strategy.Decide(_fixture.sqlserver.DatabaseType);
      var tableName = nameof(TestContext.testEntities);

      var result = await service.GetColumnSchemas(_fixture.sqlserver, tableName, _fixture.DatabaseName);
      Assert.True(result.IsSuccess);

      var columnName = nameof(TestEntity.LongNum);
      Assert.Contains(result.Value, t => t.Name == columnName);

    }
  }


  [Fact]
  public async Task TestMySqlGetColumnSchema()
  {
    using (var scope = _serviceProvider.CreateScope())
    {
      var strategy = scope.ServiceProvider.GetRequiredService<IDbServiceStrategy>();
      var service = strategy.Decide(_fixture.mysql.DatabaseType);
      var tableName = nameof(TestContext.testEntities);

      var result = await service.GetColumnSchemas(_fixture.mysql, tableName, _fixture.DatabaseName);
      Assert.True(result.IsSuccess);

      var columnName = nameof(TestEntity.LongNum);
      Assert.Contains(result.Value, t => t.Name == columnName);

    }
  }

  [Fact]
  public async Task TestMySqlCheckStatusOnline()
  {
    using (var scope = _serviceProvider.CreateScope())
    {
      var strategy = scope.ServiceProvider.GetRequiredService<IDbServiceStrategy>();
      var service = strategy.Decide(_fixture.mysql.DatabaseType);

      var result = await service.CheckStatus(_fixture.mysql);
      Assert.True(result.IsSuccess);

      Assert.Equal(DatabaseStatusEnum.Online, result.Value);

    }
  }

    [Fact]
    public async Task TestMySqlCheckStatusOffline()
    {
      using (var scope = _serviceProvider.CreateScope())
      {
        var strategy = scope.ServiceProvider.GetRequiredService<IDbServiceStrategy>();
        var service = strategy.Decide(_fixture.mysql.DatabaseType);

      DatabaseServer errorServer = new DatabaseServer()
      {
        ConnectUrl = "xx",
        UserName = "xx",
        Password = "xx",
        DatabaseType = DatabaseTypeEnum.MySQL
      };
        var result = await service.CheckStatus(errorServer);
       

        Assert.Equal(DatabaseStatusEnum.Offline, result.Value);

      DatabaseServer errorAuth = new DatabaseServer()
      {
        ConnectUrl = _fixture.DbUrl,
        UserName = "xx",
        Password = "xx",
        DatabaseType = DatabaseTypeEnum.MySQL

      };

      result = await service.CheckStatus(errorAuth);
      Assert.Equal(DatabaseStatusEnum.Offline, result.Value);

    }

  }

}
