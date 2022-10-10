using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DBManageSystem.IntegrationTests.Services.DbServerManageServiceTests;

[Collection("GetDbServerTest")]
public class GetDbServerTest:IClassFixture<DbServerManageServiceTestFixture>
{
  private DbServerManageServiceTestFixture _;
  public GetDbServerTest(DbServerManageServiceTestFixture fixture)
  {
    _ = fixture;
  }

  [Fact]
  public async Task GetDbServer()
  {
    var result = await _.databaseServerService.GetDatabaseServerWithPasswordDecrptedById(_.TestGetDbServerId);
    Assert.True(result.IsSuccess);
    Assert.Equal(_.OrdinaryPassword, result.Value.Password);
  }

  [Fact]
  public async Task ListDbServer()
  {
    var result = await _.databaseServerService.GetServerList();
    Assert.True(result.IsSuccess);

    var dblist = result.Value;
    Assert.Contains(dblist, t => t.Name == _.TestDeleteDbServer.Name);
    Assert.Contains(dblist, t => t.Name == _.TestUpdateDbServer.Name);
    Assert.Contains(dblist, t => t.Id == _.TestGetDbServerId);

  }

}
