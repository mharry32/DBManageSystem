using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBManageSystem.Core.Constants;
using DBManageSystem.Core.Entities;
using DBManageSystem.Infrastructure.Services;
using Xunit;

namespace DBManageSystem.IntegrationTests.Services.UserServiceTests;

[Collection("GetRoleByUserId")]

public class GetRoleByUserId : IClassFixture<BaseIdentityTestFixture>
{
  public BaseIdentityTestFixture Fixture { get; }
  public GetRoleByUserId(BaseIdentityTestFixture fixture)
  {
    Fixture = fixture;
  }

  [Fact]
  public async Task GetRoleByExistingUserId()
  {
    User user = new User();
    user.UserName = "testUserRole";

    Role role = new Role();
    role.Name = "testRole";

    await Fixture.userManager.CreateAsync(user, UserConstants.DefaultPassword);
    await Fixture.roleManager.CreateAsync(role);

    user = await Fixture.userManager.FindByNameAsync(user.UserName);
    await Fixture.userManager.AddToRoleAsync(user, role.Name);

    var roleInDb = await Fixture.roleManager.FindByNameAsync(role.Name);
    int roleId = roleInDb.Id;

    UserService userService = new UserService(Fixture.userManager, null, Fixture.roleManager, null, Fixture.defaultPassword, null);
    var result =await userService.GetRoleByUserId(user.Id);
    Assert.Equal(roleId, result.Value.Id);
  }

  [Fact]
  public async Task GetRoleByUserId_HaveNoRole()
  {
    User user = new User();
    user.UserName = "testUserRole2";

    await Fixture.userManager.CreateAsync(user, UserConstants.DefaultPassword);
    user = await Fixture.userManager.FindByNameAsync(user.UserName);

    UserService userService = new UserService(Fixture.userManager, null, Fixture.roleManager,null, Fixture.defaultPassword, null);
    var result = await userService.GetRoleByUserId(user.Id);
    Assert.Equal(RoleConstants.DEFAULT,result.Value.Id);
    Assert.Equal(RoleConstants.DEFAULT_ROLENAME, result.Value.Name);
  }
}
