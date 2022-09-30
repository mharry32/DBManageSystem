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
    await _client.TestWithoutAuthorize<object>(null, $"/users/menusbyrole/{DbManageSysDBSeed.testRoleId}", HttpMethod.Get);

    var data = await _client.TestWithAuthorizeUsingJWT<object>(null, $"/users/menusbyrole/{DbManageSysDBSeed.testRoleId}", HttpMethod.Get, token);

    var json = await data.Content.ReadAsStringAsync();
    var menus = JsonSerializer.Deserialize<List<int>>(json, Constants.DefaultJsonOptions);

    Assert.Contains(menus, m => m == DbManageSysDBSeed.subMenuId);

  }

  [Fact]
  public async Task GetMenusForUser()
  {
    await _client.TestWithoutAuthorize<object>(null, "/admin/menus", HttpMethod.Get);

    var data = await _client.TestWithAuthorizeUsingJWT<object>(null, "/admin/menus", HttpMethod.Get, token);

    var json = await data.Content.ReadAsStringAsync();
    var menus = JsonSerializer.Deserialize<List<MainMenuDTO>>(json, Constants.DefaultJsonOptions);

    Assert.Contains(menus, m => m.Name == DbManageSysDBSeed.testMainMenu);
  }


  [Fact]
  public async Task ModifyMenus()
  {
    //first,we need to acquire menus to modify 
    var data = await _client.TestWithAuthorizeUsingJWT<object>(null, "/users/allmenus", HttpMethod.Get, token);

    var json = await data.Content.ReadAsStringAsync();
    var menus = JsonSerializer.Deserialize<List<MainMenuDTO>>(json, Constants.DefaultJsonOptions);

    //then,make the request
    ModifyMenusRequest request = new ModifyMenusRequest();
    request.RoleId = DbManageSysDBSeed.testRoleId;
    request.MenuIds = new List<int>();

    foreach(var mainMenu in menus)
    {
      foreach(var subMenu in mainMenu.SubMenus)
      {
        if(subMenu.Name == DbManageSysDBSeed.testSubMenu||subMenu.Name==DbManageSysDBSeed.testModifySubMenu)
        {
          request.MenuIds.Add(subMenu.Id);
        }

      }
    }

    //test without authorize
    await _client.TestWithoutAuthorize<ModifyMenusRequest>(request, ModifyMenusRequest.Route, HttpMethod.Put);

    //test under authorize
    var result = await _client.TestWithAuthorizeUsingJWT<ModifyMenusRequest>(request, ModifyMenusRequest.Route, HttpMethod.Put, token);

    Assert.True(result.IsSuccessStatusCode);

    //get menus after modified
    data = await _client.TestWithAuthorizeUsingJWT<object>(null, $"/users/menusbyrole/{DbManageSysDBSeed.testRoleId}", HttpMethod.Get, token);

    json = await data.Content.ReadAsStringAsync();
    var menusAfterModified = JsonSerializer.Deserialize<List<int>>(json, Constants.DefaultJsonOptions);

    //check main menus
    Assert.Contains(menusAfterModified, t =>t== request.MenuIds[0]);
    Assert.Contains(menusAfterModified, t => t == request.MenuIds[1]);

  }

}
