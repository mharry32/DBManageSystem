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
[Collection("ModifyPassword")]
public class ModifyPassword : IClassFixture<BaseIdentityTestFixture>
{

  public BaseIdentityTestFixture Fixture { get; }
  public ModifyPassword(BaseIdentityTestFixture fixture)
  {
    Fixture = fixture;
  }



  [Fact]
  public async Task ModifyPasswordInvalid()
  {
    User user = new User();
    user.UserName = "testerUser";
    var newPassword = "aaa";
    await Fixture.userManager.CreateAsync(user, UserConstants.DefaultPassword);
    user = await Fixture.userManager.FindByNameAsync(user.UserName);
    var userService = new UserService(Fixture.userManager, Fixture.signInManager, Fixture.roleManager, null, Fixture.defaultPassword, null);

    var result = await userService.ModifyPassword(user.Id, UserConstants.DefaultPassword, newPassword);
    Assert.False(result.IsSuccess);
    Assert.NotEmpty(result.Errors);
  }

}
