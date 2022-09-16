using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBManageSystem.Infrastructure.Services;
using Xunit;

namespace DBManageSystem.IntegrationTests.Services.RoleServiceTests;

[Collection("RoleOperationsTests")]
public class RoleOperationsTests : IClassFixture<BaseIdentityTestFixture>
{
  private string testRoleName = "testRoleName";
  public BaseIdentityTestFixture Fixture { get; }

  private RoleService _roleService;
  public RoleOperationsTests(BaseIdentityTestFixture fixture)
  {
  }
}
