using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DBManageSystem.IntegrationTests.Services.UserServiceTests;
public class DeleteUser : IClassFixture<BaseIdentityTestFixture>
{
  public BaseIdentityTestFixture Fixture { get; }
  public DeleteUser(BaseIdentityTestFixture fixture)
  {
    Fixture = fixture;
  }
}
