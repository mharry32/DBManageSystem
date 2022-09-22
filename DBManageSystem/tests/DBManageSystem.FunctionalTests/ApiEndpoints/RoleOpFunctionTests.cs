using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBManageSystem.ManageWebAPI;
using Xunit;

namespace DBManageSystem.FunctionalTests.ApiEndpoints;
public class RoleOpFunctionTests:IClassFixture<FunctionalTestWebApplicationFactory<ManageWebMarker>>
{
  private readonly HttpClient _client;

  private readonly string token;

  public RoleOpFunctionTests(FunctionalTestWebApplicationFactory<ManageWebMarker> factory)
  {
    _client = factory.CreateClient();
    var tokenTask = factory.GetAuthToken(_client);
    tokenTask.Wait();
    token = tokenTask.Result;
  }

  [Fact]
  public async Task BasicRoleFunctionTest()
  {
     
  }
}
