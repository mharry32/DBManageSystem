using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using DBManageSystem.ManageWebAPI;
using DBManageSystem.ManageWebAPI.Endpoints.RoleManageEndpoints;
using Xunit;
using Ardalis.HttpClientTestExtensions;
using DBManageSystem.ManageWebAPI.Endpoints.MenuEndpoints;
using DBManageSystem.Core.Constants;

namespace DBManageSystem.FunctionalTests.ApiEndpoints;
[Collection("MenuOpFunctionTests")]
public class MenuOpFunctionTests : IClassFixture<FunctionalTestWebApplicationFactory<ManageWebMarker>>
{
  private readonly HttpClient _client;

  private readonly string token;

  public MenuOpFunctionTests(FunctionalTestWebApplicationFactory<ManageWebMarker> factory)
  {
    _client = factory.CreateClient();
    var tokenTask = factory.GetAuthToken(_client);
    tokenTask.Wait();
    token = tokenTask.Result;
  }

  [Fact]
  public async Task GetAllMenus()
  {
    await _client.TestWithoutAuthorize<object>(null, "/users/allmenus", HttpMethod.Get);

    var data = await _client.TestWithAuthorizeUsingJWT<object>(null, "/users/allmenus", HttpMethod.Get, token);

    var json = await data.Content.ReadAsStringAsync();
    var menus = JsonSerializer.Deserialize<List<MainMenuDTO>>(json, Constants.DefaultJsonOptions);

    Assert.Contains(menus, m => m.Name == MenuConstants.ACCOUNT_MANAGE_MAIN_NAME);
    Assert.Contains(menus, m => m.Name == MenuConstants.DB_MANAGE_MAIN_NAME);

    Assert.Equal(MenuConstants.DB_MONITOR_SUB_NAME,menus[0].SubMenus[0].Name);
    Assert.Equal(MenuConstants.DB_REPORT_SUB_NAME, menus[0].SubMenus[1].Name);
    Assert.Equal(MenuConstants.DB_REPORT_SUB_PATH, menus[0].SubMenus[1].Path);

  }

  [Fact]
  public async Task GetMenusForRole()
  {
    await _client.TestWithoutAuthorize<object>(null, $"/users/menusbyrole/{RoleConstants.ADMINISTRATOR}", HttpMethod.Get);

    var data = await _client.TestWithAuthorizeUsingJWT<object>(null, $"/users/menusbyrole/{RoleConstants.ADMINISTRATOR}", HttpMethod.Get, token);

    var json = await data.Content.ReadAsStringAsync();
    var menus = JsonSerializer.Deserialize<List<MainMenuDTO>>(json, Constants.DefaultJsonOptions);

    Assert.Contains(menus, m => m.Name == MenuConstants.ACCOUNT_MANAGE_MAIN_NAME);
    Assert.Contains(menus, m => m.Name == MenuConstants.DB_MANAGE_MAIN_NAME);

    Assert.Equal(MenuConstants.DB_MONITOR_SUB_NAME, menus[0].SubMenus[0].Name);
    Assert.Equal(MenuConstants.DB_REPORT_SUB_NAME, menus[0].SubMenus[1].Name);
    Assert.Equal(MenuConstants.DB_REPORT_SUB_PATH, menus[0].SubMenus[1].Path);
  }

}
