using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DBManageSystem.IntegrationTests.Services.DbServerManageServiceTests;
public class DeleteDbServerTest:IClassFixture<DbServerManageServiceTestFixture>
{
  private DbServerManageServiceTestFixture _;
  public DeleteDbServerTest(DbServerManageServiceTestFixture fixture)
  {
    _ = fixture;
  }

  [Fact]
  public async Task Delete()
  {
    var result = await _.databaseServerService.DeleteDatabaseServer(_.TestDeleteDbServer.Id);
    Assert.True(result.IsSuccess);

    var dbdeleted = _.dbContext.databaseServers.FirstOrDefault(t => t.Name == _.TestDeleteDbServer.Name);
    Assert.Null(dbdeleted);
  }
}
