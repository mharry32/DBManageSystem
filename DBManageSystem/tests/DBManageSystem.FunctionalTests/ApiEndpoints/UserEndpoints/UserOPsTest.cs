using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using DBManageSystem.Core.Entities;
using DBManageSystem.FunctionalTests.ApiEndpoints.AuthEndpoints;
using DBManageSystem.ManageWebAPI;
using DBManageSystem.ManageWebAPI.Endpoints.UserManageEndpoints;
using Xunit;

namespace DBManageSystem.FunctionalTests.ApiEndpoints.UserEndpoints;
[Collection("UserOPsTest")]
public class UserOPsTest:IClassFixture<FunctionalTestWebApplicationFactory<ManageWebMarker>>
{
  private readonly HttpClient _client;

  private readonly string token;


  public UserOPsTest(FunctionalTestWebApplicationFactory<ManageWebMarker> factory)
  {
    _client = factory.CreateClient();
    var tokenTask = factory.GetAuthToken(_client);
    tokenTask.Wait();
    token = tokenTask.Result;



  }

  [Fact]
  public async Task PerformCreateUser()
  {
    CreateUserRequest user = new CreateUserRequest();
    user.UserName = "testCreate";
    await _client.TestWithoutAuthorize<CreateUserRequest>(user, CreateUserRequest.Route, HttpMethod.Post);

    var result = await _client.TestWithAuthorizeUsingJWT<CreateUserRequest>(user, CreateUserRequest.Route, HttpMethod.Post, token);
    Assert.True(result.IsSuccessStatusCode);
  }

  [Fact]
  public async Task PerformDeleteUser()
  {
    await _client.TestWithoutAuthorize<int>(IdentityTestingConstants.TestModifyPassWordUserId, "/users", HttpMethod.Delete);

    var result = await _client.TestWithAuthorizeUsingJWT<int>(IdentityTestingConstants.TestModifyPassWordUserId, "/users", HttpMethod.Delete, token);
    Assert.True(result.IsSuccessStatusCode);
  }

  
}
