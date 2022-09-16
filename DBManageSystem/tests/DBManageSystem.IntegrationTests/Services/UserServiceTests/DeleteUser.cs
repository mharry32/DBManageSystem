using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBManageSystem.Core.Constants;
using DBManageSystem.Core.Entities;
using DBManageSystem.Core.Interfaces;
using DBManageSystem.Infrastructure.Services;
using Xunit;

namespace DBManageSystem.IntegrationTests.Services.UserServiceTests;
[Collection("DeleteUser")]
public class DeleteUser : IClassFixture<BaseIdentityTestFixture>
{
  public BaseIdentityTestFixture Fixture { get; }
  public DeleteUser(BaseIdentityTestFixture fixture)
  {
    Fixture = fixture;
  }

  [Fact]
  public async Task DeleteExistingUser()
  {
    User user = new User();
    user.UserName = "testerUser";
   await Fixture.userManager.CreateAsync(user, UserConstants.DefaultPassword);

    var userInDb = await Fixture.userManager.FindByNameAsync(user.UserName);

    UserService userService = new UserService(Fixture.userManager, null, default, default);
    var result =await userService.DeleteUser(userInDb.Id);
    Assert.True(result.IsSuccess);

    var userDeleted = await Fixture.userManager.FindByNameAsync(user.UserName);
    Assert.Null(userDeleted);


  }

  [Fact]
  public async Task DeleteNonExistingUser()
  {
    User user = new User();
    user.UserName = "DeleteNonExistingUser";
    await Fixture.userManager.CreateAsync(user, UserConstants.DefaultPassword);

    var userInDb = await Fixture.userManager.FindByNameAsync(user.UserName);
    int deletedUserId = userInDb.Id;
    await Fixture.userManager.DeleteAsync(userInDb);

    UserService userService = new UserService(Fixture.userManager, null, default, default);
    var result = await userService.DeleteUser(deletedUserId);

    Assert.False(result.IsSuccess);
    Assert.NotEmpty(result.Errors);

  }

}
