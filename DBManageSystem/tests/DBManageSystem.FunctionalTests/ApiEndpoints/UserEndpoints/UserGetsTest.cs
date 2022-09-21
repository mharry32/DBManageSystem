using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Ardalis.HttpClientTestExtensions;
using DBManageSystem.FunctionalTests.ApiEndpoints.AuthEndpoints;
using DBManageSystem.ManageWebAPI;
using DBManageSystem.ManageWebAPI.Endpoints.UserManageEndpoints;
using Xunit;

namespace DBManageSystem.FunctionalTests.ApiEndpoints.UserEndpoints;
[Collection("UserGetsTest")]
public class UserGetsTest : IClassFixture<FunctionalTestWebApplicationFactory<ManageWebMarker>>
{
  private readonly HttpClient _client;

  private readonly string token;


  public UserGetsTest(FunctionalTestWebApplicationFactory<ManageWebMarker> factory)
  {
    _client = factory.CreateClient();
    var tokenTask = factory.GetAuthToken(_client);
    tokenTask.Wait();
    token = tokenTask.Result;
  }

  [Fact]
  public async Task PerformGetAllUsers()
  {
    await _client.TestWithoutAuthorize<object>(null, "/users", HttpMethod.Get);
    var result = await _client.TestWithAuthorizeUsingJWT<object>(null, "/users", HttpMethod.Get, token);
    Assert.True(result.IsSuccessStatusCode);
    var json = await result.Content.ReadAsStringAsync();
    var users = JsonSerializer.Deserialize<List<UserDTO>>(json,Constants.DefaultJsonOptions);
    Assert.NotEmpty(users);
    Assert.DoesNotContain(users, t => t.UserName == IdentityTestingConstants.TestLoginUserName);
    Assert.Equal(IdentityTestingConstants.TestModifyPassWordUserId, users[0].Id);
  }

  [Fact]
  public async Task PerformGetCurrentUser()
  {
    await _client.TestWithoutAuthorize<object>(null, "/users/current", HttpMethod.Get);
    var result = await _client.TestWithAuthorizeUsingJWT<object>(null, "/users/current", HttpMethod.Get, token);
    Assert.True(result.IsSuccessStatusCode);
    var json = await result.Content.ReadAsStringAsync();
    var user = JsonSerializer.Deserialize<UserDTO>(json, Constants.DefaultJsonOptions);
  }

}
