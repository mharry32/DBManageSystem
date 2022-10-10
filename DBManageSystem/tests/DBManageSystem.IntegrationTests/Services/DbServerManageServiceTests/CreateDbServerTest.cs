using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBManageSystem.Core.Entities;
using DBManageSystem.Core.Enums;
using Xunit;

namespace DBManageSystem.IntegrationTests.Services.DbServerManageServiceTests;

[Collection("CreateDbServerTest")]
public class CreateDbServerTest:IClassFixture<DbServerManageServiceTestFixture>
{
  private DbServerManageServiceTestFixture _;
  public CreateDbServerTest(DbServerManageServiceTestFixture fixture)
  {
    _ = fixture;
  }

  [Fact]
  public async Task CreateEmptyName()
  {
    DatabaseServer emptyNameServer = new DatabaseServer()
    {
      ConnectUrl = "emptyNameServer",
      Name = "",
      DatabaseType = DatabaseTypeEnum.SQLServer,
      IsMonitored = true
    };
   var result = await _.databaseServerService.CreateDatabaseServer(emptyNameServer);
    Assert.False(result.IsSuccess);
  }


  [Fact]
  public async Task CreateEmptyPassword()
  {
    DatabaseServer emptyPasswordServer = new DatabaseServer()
    {
      ConnectUrl = "emptyPasswordServer",
      Name = "emptyPasswordServer",
      DatabaseType = DatabaseTypeEnum.SQLServer,
      IsMonitored = true
    };

    var result = await _.databaseServerService.CreateDatabaseServer(emptyPasswordServer);
    Assert.True(result.IsSuccess);
    var dbcreated = _.dbContext.databaseServers.FirstOrDefault(s => s.Name == emptyPasswordServer.Name);
    Assert.NotNull(dbcreated);
    Assert.Equal(emptyPasswordServer.DatabaseType, dbcreated.DatabaseType);
    Assert.Null(dbcreated.Password);
    
  }


  [Fact]
  public async Task CreateHasPassword()
  {
    DatabaseServer hasPasswordServer = new DatabaseServer()
    {
      ConnectUrl = "PasswordServer",
      Name = "PasswordServer",
      UserName = "sa",
      Password = "pppppppp",
      DatabaseType = DatabaseTypeEnum.SQLServer,
      IsMonitored = true
    };

    var result = await _.databaseServerService.CreateDatabaseServer(hasPasswordServer);
    Assert.True(result.IsSuccess);
    var dbcreated = _.dbContext.databaseServers.FirstOrDefault(s => s.Name == hasPasswordServer.Name);
    Assert.NotNull(dbcreated);
    Assert.Equal(hasPasswordServer.DatabaseType, dbcreated.DatabaseType);
    Assert.Equal(hasPasswordServer.UserName, dbcreated.UserName);
    Assert.NotNull(dbcreated.Password);

  }
}
