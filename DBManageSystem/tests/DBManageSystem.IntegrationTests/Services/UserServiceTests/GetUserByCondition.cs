using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using DBManageSystem.Core.Constants;
using DBManageSystem.Core.Entities;
using DBManageSystem.Infrastructure.Services;
using Xunit;

namespace DBManageSystem.IntegrationTests.Services.UserServiceTests;

[Collection("GetUserByCondition")]
public class GetUserByCondition:IClassFixture<BaseIdentityTestFixture>
{
  public BaseIdentityTestFixture Fixture { get; }
  public GetUserByCondition(BaseIdentityTestFixture fixture)
  {
    Fixture = fixture;
  }

  [Fact]
  public async Task GetUsersExceptCurrent()
  {
    User user = new User();
    user.UserName = "testerUser";

    User user2 = new User();
    user2.UserName = "testerUser2";

    User user3 = new User();
    user3.UserName = "testerUser3";//current user
    await Fixture.userManager.CreateAsync(user, UserConstants.DefaultPassword);
    await Fixture.userManager.CreateAsync(user2, UserConstants.DefaultPassword);
    await Fixture.userManager.CreateAsync(user3, UserConstants.DefaultPassword);

    var currentUser = await Fixture.userManager.FindByNameAsync(user3.UserName);
    UserService userService = new UserService(Fixture.userManager, default, default);

    var result =await userService.GetAllUsersExceptForCurrent(currentUser.Id);

    Assert.True(result.IsSuccess);
    Assert.DoesNotContain(result.Value, t => t.UserName == user3.UserName);
  }


  [Fact]
  public async Task GetUserById()
  {
    User user = new User();
    user.UserName = "testerUser4";
    await Fixture.userManager.CreateAsync(user, UserConstants.DefaultPassword);

    var currentUser = await Fixture.userManager.FindByNameAsync(user.UserName);
    UserService userService = new UserService(Fixture.userManager, default, default);

    var result = await userService.GetUserById(currentUser.Id);
    Assert.True(result.IsSuccess);
    Assert.Equal(user.UserName, result.Value.UserName);

  }

  [Fact]
  public async Task GetNonExistUserById()
  {
    User user = new User();
    user.UserName = "testerUser5";
    await Fixture.userManager.CreateAsync(user, UserConstants.DefaultPassword);

    var currentUser = await Fixture.userManager.FindByNameAsync(user.UserName);
    await Fixture.userManager.DeleteAsync(currentUser);

    UserService userService = new UserService(Fixture.userManager, default, default);

    var result = await userService.GetUserById(currentUser.Id);
    Assert.True(result.IsSuccess);
    Assert.Null(result.Value);

  }

  [Fact]
  public async Task GetUserByName()
  {
    User user = new User();
    user.UserName = "testerUser6";
    await Fixture.userManager.CreateAsync(user, UserConstants.DefaultPassword);

    var currentUser = await Fixture.userManager.FindByNameAsync(user.UserName);
    UserService userService = new UserService(Fixture.userManager, default, default);

    var result = await userService.GetUserByName(user.UserName);
    Assert.True(result.IsSuccess);
    Assert.Equal(currentUser.Id, result.Value.Id);

  }

  [Fact]
  public async Task GetNonExistUserByName()
  {
    User user = new User();
    user.UserName = "testerUser7";
    await Fixture.userManager.CreateAsync(user, UserConstants.DefaultPassword);

    var currentUser = await Fixture.userManager.FindByNameAsync(user.UserName);
    await Fixture.userManager.DeleteAsync(currentUser);

    UserService userService = new UserService(Fixture.userManager, default, default);

    var result = await userService.GetUserByName(user.UserName);
    Assert.True(result.IsSuccess);
    Assert.Null(result.Value);

  }

}
