using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBManageSystem.Core.Enums;
using Xunit;

namespace DBManageSystem.IntegrationTests.Services.DbServerManageServiceTests;

[Collection("UpdateDbServerTest")]
public class UpdateDbServerTest : IClassFixture<DbServerManageServiceTestFixture>
{
  private DbServerManageServiceTestFixture _;
  public UpdateDbServerTest(DbServerManageServiceTestFixture fixture)
  {
    _ = fixture;
  }

  [Fact]
  public async Task UpdateEmptyPassword()
  {
    _.TestUpdateDbServer.Password = null;
    _.TestUpdateDbServer.ConnectUrl = "updatedurl";

    var result = await _.databaseServerService.UpdateDatabaseServer(_.TestUpdateDbServer);
    Assert.True(result.IsSuccess);

    var dbserver = _.dbContext.databaseServers.FirstOrDefault(t => t.ConnectUrl == "updatedurl");
    Assert.NotNull(dbserver);
    Assert.Null(dbserver.Password);
    Assert.Equal(_.TestUpdateDbServer.Name, dbserver.Name);
  }

  [Fact]
  public async Task UpdateWithPassword()
  {
    _.TestUpdateDbServer.Password = "aaaaa";
    _.TestUpdateDbServer.ConnectUrl = "updatedurl";

    var result = await _.databaseServerService.UpdateDatabaseServer(_.TestUpdateDbServer);
    Assert.True(result.IsSuccess);

    var dbserver = _.dbContext.databaseServers.FirstOrDefault(t => t.ConnectUrl == "updatedurl");
    Assert.NotNull(dbserver);
    Assert.NotNull(dbserver.Password);
    Assert.Equal(_.TestUpdateDbServer.Name, dbserver.Name);
  }

  [Fact]
  public async Task UpdateStatus()
  {
    _.TestUpdateDbServer.UpdateStatus(DatabaseStatusEnum.Online);
    var result = await _.databaseServerService.UpdateDatabaseServer(_.TestUpdateDbServer);
    Assert.True(result.IsSuccess);

    var dbserver = _.dbContext.databaseServers.FirstOrDefault(t => t.ConnectUrl == _.TestUpdateDbServer.ConnectUrl);
    Assert.Equal(DatabaseStatusEnum.Online, dbserver.Status);

  }
}
