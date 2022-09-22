using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Ardalis.HttpClientTestExtensions;
using DBManageSystem.FunctionalTests.ApiEndpoints.AuthEndpoints;
using DBManageSystem.ManageWebAPI;
using DBManageSystem.ManageWebAPI.Endpoints.RoleManageEndpoints;
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
    CreateRoleRequest createRoleRequest = new CreateRoleRequest();
    createRoleRequest.RoleName = "testRole";
    await _client.TestWithoutAuthorize<CreateRoleRequest>(createRoleRequest, "/role", HttpMethod.Post);

    var createResult = await _client.TestWithAuthorizeUsingJWT<CreateRoleRequest>(createRoleRequest, "/role", HttpMethod.Post, token);
    Assert.True(createResult.IsSuccessStatusCode);

    await _client.TestWithoutAuthorize<object>(null, "/users/roles", HttpMethod.Get);

    var getResult = await _client.TestWithAuthorizeUsingJWT<object>(null, "/users/roles", HttpMethod.Get, token);
    var json = await getResult.Content.ReadAsStringAsync();
    var roles = JsonSerializer.Deserialize<List<RoleDTO>>(json, Constants.DefaultJsonOptions);
    Assert.Contains(roles, r => r.Name == createRoleRequest.RoleName);

    ModifyRoleForUserRequest modifyRoleForUserRequest = new ModifyRoleForUserRequest();
    modifyRoleForUserRequest.RoleId = roles.FirstOrDefault(r => r.Name == createRoleRequest.RoleName).Id;
    modifyRoleForUserRequest.UserId = IdentityTestingConstants.TestModifyPassWordUserId;
    await _client.TestWithoutAuthorize<ModifyRoleForUserRequest>(modifyRoleForUserRequest, ModifyRoleForUserRequest.Route, HttpMethod.Put);

    var putResult = await _client.TestWithAuthorizeUsingJWT<ModifyRoleForUserRequest>(
      modifyRoleForUserRequest, ModifyRoleForUserRequest.Route, HttpMethod.Put, token);

    Assert.True(putResult.IsSuccessStatusCode);

    var deleteRoleId = roles.FirstOrDefault(r => r.Name == createRoleRequest.RoleName).Id;
    await _client.TestWithoutAuthorize<int>(deleteRoleId, "/role", HttpMethod.Delete);

    var deleteResult = await _client.TestWithAuthorizeUsingJWT<int>(deleteRoleId, "/role", HttpMethod.Delete, token);
    Assert.True(deleteResult.IsSuccessStatusCode);


    getResult = await _client.TestWithAuthorizeUsingJWT<object>(null, "/users/roles", HttpMethod.Get, token);
    json = await getResult.Content.ReadAsStringAsync();
    roles = JsonSerializer.Deserialize<List<RoleDTO>>(json, Constants.DefaultJsonOptions);
    Assert.DoesNotContain(roles, r => r.Name == createRoleRequest.RoleName);
  }
}
