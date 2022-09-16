using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBManageSystem.Core.Entities;
using DBManageSystem.Infrastructure.Services;
using Xunit;

namespace DBManageSystem.IntegrationTests.Services.RoleServiceTests;

[Collection("RoleOperationsTests")]
public class RoleOperationsTests : IClassFixture<BaseIdentityTestFixture>
{
  private Role roleForCreate;

  private Role roleForDelete;

  private Role roleForSet;

  private int roleIdNonExists = 100;

  private int userIdNonExists = 100;
  public BaseIdentityTestFixture Fixture { get; }

  private RoleService _roleService;
  public RoleOperationsTests(BaseIdentityTestFixture fixture)
  {
    Fixture = fixture;
    _roleService = new RoleService(Fixture.userManager, Fixture.roleManager, null);
    roleForCreate = new Role();
    roleForCreate.Name = "roleForCreate";

    roleForDelete = new Role();
    roleForDelete.Name = "roleForDelete";

    roleForSet = new Role();
    roleForSet.Name = "roleForSet";


    Fixture.roleManager.CreateAsync(roleForDelete).Wait();
   Fixture.roleManager.CreateAsync(roleForSet).Wait();

 

  }


  [Fact]
  public async Task CreateRole()
  {
    var result = await _roleService.CreateRole(roleForCreate.Name);
    var roleInDb = await Fixture.roleManager.FindByNameAsync(roleForCreate.Name);
    Assert.Equal(roleInDb.Name, roleForCreate.Name);
  }

  [Fact]
  public async Task DeleteRole()
  {
    var roleIdToDelete = await Fixture.roleManager.FindByNameAsync(roleForDelete.Name);
    var result = await _roleService.DeleteRole(roleIdToDelete.Id);
    Assert.True(result.IsSuccess);
    var roleDeleted = await Fixture.roleManager.FindByNameAsync(roleForDelete.Name);
    Assert.Null(roleDeleted);

  }

  [Fact]
  public async Task DeleteRoleDoNotExist()
  {
    var result = await _roleService.DeleteRole(roleIdNonExists);
    Assert.False(result.IsSuccess);
    Assert.NotEmpty(result.Errors);
  }

  [Fact]
  public async Task SetRoleForUser()
  {
    User user = new User();
    user.UserName = "test";
    await Fixture.userManager.CreateAsync(user);

    user =await Fixture.userManager.FindByNameAsync(user.UserName);
    var role = await Fixture.roleManager.FindByNameAsync(roleForSet.Name);

    var result = await _roleService.SetRoleForUser(role.Id, user.Id);
    Assert.True(result.IsSuccess);
    var userRole = await Fixture.userManager.GetRolesAsync(user);

    Assert.Contains(userRole, t => t == role.Name);
  }


  [Fact]
  public async Task SetRoleForUser_RoleNotExist()
  {
    User user = new User();
    user.UserName = "test2";
    await Fixture.userManager.CreateAsync(user);

    user = await Fixture.userManager.FindByNameAsync(user.UserName);
    var result = await _roleService.SetRoleForUser(roleIdNonExists, user.Id);
    Assert.False(result.IsSuccess);
    Assert.NotEmpty(result.Errors);
  }


  [Fact]
  public async Task SetRoleForUser_UserNotExist()
  {
    var role = await Fixture.roleManager.FindByNameAsync(roleForSet.Name);
    var result = await _roleService.SetRoleForUser(role.Id, userIdNonExists);
    Assert.False(result.IsSuccess);
    Assert.NotEmpty(result.Errors);
  }
}
