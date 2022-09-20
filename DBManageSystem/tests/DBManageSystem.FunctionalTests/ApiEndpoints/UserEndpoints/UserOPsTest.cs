using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using DBManageSystem.Core.Entities;
using DBManageSystem.ManageWebAPI;
using DBManageSystem.ManageWebAPI.Endpoints.UserManageEndpoints;
using Xunit;

namespace DBManageSystem.FunctionalTests.ApiEndpoints.UserEndpoints;
[Collection("UserOPsTest")]
public class UserOPsTest:IClassFixture<FunctionalTestWebApplicationFactory<ManageWebMarker>>
{
  private readonly HttpClient _client;

  private readonly string token;

  private readonly AuthenticationHeaderValue _authenticationHeader;


  public UserOPsTest(FunctionalTestWebApplicationFactory<ManageWebMarker> factory)
  {
    _client = factory.CreateClient();
    var tokenTask = factory.GetAuthToken(_client);
    tokenTask.Wait();
    token = tokenTask.Result;
    _authenticationHeader = new AuthenticationHeaderValue("Bearer", token);


  }

  [Fact]
  public async Task PerformCreateUser()
  {
    CreateUserRequest user = new CreateUserRequest();
    user.UserName = "testCreate";
    await _client.TestWithoutAuthorize<CreateUserRequest>(user, CreateUserRequest.Route, HttpMethod.Post);



  }
  
}
