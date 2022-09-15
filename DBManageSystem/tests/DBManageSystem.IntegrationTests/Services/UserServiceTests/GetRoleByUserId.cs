using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

  }
}
