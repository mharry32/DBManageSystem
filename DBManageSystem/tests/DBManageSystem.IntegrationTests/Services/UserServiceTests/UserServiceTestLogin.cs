using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBManageSystem.Core.Constants;
using DBManageSystem.Core.Entities;
using DBManageSystem.Infrastructure.Services;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using Xunit;

namespace DBManageSystem.IntegrationTests.Services.UserServiceTests;

[Collection("UserServiceTestLogin")]
public class UserServiceTestLogin : IClassFixture<BaseIdentityTestFixture>
{

  public BaseIdentityTestFixture Fixture { get; }
  public UserServiceTestLogin(BaseIdentityTestFixture fixture)
  {
    Fixture = fixture;
  }

  [Fact]
  public async Task LoginSuccess()
  {
    User user = new User();
    user.UserName = "testerUser";
    await Fixture.userManager.CreateAsync(user, UserConstants.DefaultPassword);
    var userService = new UserService(Fixture.userManager, Fixture.signInManager, Fixture.roleManager, null);
    var result = await userService.Login(user.UserName, UserConstants.DefaultPassword);
    Assert.True(result.IsSuccess);

    var token = result.Value;
    var tokenHandler = new JwtSecurityTokenHandler();
    SecurityToken securityToken = null;

    try
    {
      tokenHandler.ValidateToken(token, new TokenValidationParameters
      {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(AuthConstants.KEY)),
        ValidateIssuer = false,
        ValidateAudience = false
      }, out securityToken);
    }
    catch(Exception ex)
    {
      Assert.Fail(ex.Message);
    }

  }

  [Fact]
  public async Task LoginWithWrongPassword()
  {
    User user = new User();
    user.UserName = "testerUser2";
    await Fixture.userManager.CreateAsync(user, UserConstants.DefaultPassword);
    var userService = new UserService(Fixture.userManager, Fixture.signInManager, Fixture.roleManager, null);
    var result = await userService.Login(user.UserName, UserConstants.DefaultPassword+"a");
    Assert.False(result.IsSuccess);
    Assert.Null(result.Value);
  }

}
