using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBManageSystem.Core.Entities;
using DBManageSystem.Infrastructure.Services;
using Xunit;

namespace DBManageSystem.IntegrationTests.Services.UserServiceTests;

[Collection("CreateUser")]
public class CreateUser:IClassFixture<BaseIdentityTestFixture>
{
  public BaseIdentityTestFixture Fixture { get; }
   public CreateUser(BaseIdentityTestFixture fixture)
  {
    Fixture = fixture;
  }

  [Fact]
  public async Task CreateOneUser()
  {
    User user = new User();
    user.UserName = "testerUser";
    UserService userService = new UserService(Fixture.userManager, null, null, null, Fixture.defaultPassword, null);
   var result = await userService.CreateUser(user);
    Assert.True(result.IsSuccess);
    var userInDb = Fixture.identityContext.Users.FirstOrDefault(t=>t.UserName == user.UserName);
    Assert.NotNull(userInDb);
    Assert.Equal(user.UserName, userInDb.UserName);

  }

  [Fact]
  public async Task CreateDuplicateUser()
  {
    User user = new User();
    user.UserName = "testerUser";
    UserService userService = new UserService(Fixture.userManager, null, null, null, Fixture.defaultPassword, null);
   await userService.CreateUser(user);


    User userDup = new User();
    userDup.UserName = "testerUser";
    
    var result = await userService.CreateUser(userDup);
    Assert.False(result.IsSuccess);
    Assert.NotEmpty(result.Errors);
  }

  [Fact]
  public async Task CreateTwoDifferentUsers()
  {
    User user = new User();
    user.UserName = "testerUser";
    UserService userService = new UserService(Fixture.userManager, null, null, null, Fixture.defaultPassword, null);
    await userService.CreateUser(user);


    User userDup = new User();
    userDup.UserName = "testerUser2";

    var result = await userService.CreateUser(userDup);
    Assert.True(result.IsSuccess);
  }
}
