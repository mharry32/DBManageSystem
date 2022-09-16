using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBManageSystem.Core.Constants;
using DBManageSystem.Core.Entities;
using DBManageSystem.Infrastructure.Services;
using Xunit;

namespace DBManageSystem.IntegrationTests.Services.RoleServiceTests;

[Collection("GetRoleTests")]
public class GetRoleTests : IClassFixture<BaseIdentityTestFixture>
{
  private string testRoleName = "testRoleName";
  public BaseIdentityTestFixture Fixture { get; }

  private RoleService _roleService;
  public GetRoleTests(BaseIdentityTestFixture fixture)
  {
    Fixture = fixture;
    Role admin = new Role();
    admin.Name = RoleConstants.ADMINISTRATOR_ROLENAME;

    Role testRole = new Role();
    testRole.Name = testRoleName;

    var t1 = fixture.roleManager.CreateAsync(admin);
    var t2 = fixture.roleManager.CreateAsync(testRole);
    Task.WaitAll(t1, t2);
    _roleService = new RoleService(Fixture.userManager,Fixture.roleManager,null);

  }

  [Fact]
  public async Task GetRoleExceptForAdminAndDefault()
  {
    var result = await _roleService.GetRoleExceptForAdminAndDefault();
    Assert.True(result.IsSuccess);
    Assert.DoesNotContain(result.Value, t => t.Id == RoleConstants.ADMINISTRATOR);
    Assert.NotEmpty(result.Value);
  }

  [Fact]
  public async Task GetRoleByName()
  {
    var result = await _roleService.GetRoleByName(testRoleName);
    Assert.Equal(testRoleName, result.Value.Name);
  }

  [Fact]
  public async Task GetRoleNonExist()
  {
    var result = await _roleService.GetRoleByName("NON EXISIT");
    Assert.True(result.Status == Ardalis.Result.ResultStatus.NotFound);
    Assert.Null(result.Value);
  }
}
