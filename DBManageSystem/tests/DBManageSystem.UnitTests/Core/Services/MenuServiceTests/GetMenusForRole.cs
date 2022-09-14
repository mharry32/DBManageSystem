using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBManageSystem.Core.Entities;
using DBManageSystem.Core.Entities.MenuAggregate;
using DBManageSystem.SharedKernel.Interfaces;
using Moq;
using Xunit;

namespace DBManageSystem.UnitTests.Core.Services.MenuServiceTests;
public class GetMenusForRole
{
  private readonly Mock<IRepository<SubMenu>> _mockSubMenuRepo = new ();

  private readonly Mock<IRepository<RoleMenu>> _mockRoleMenuRepo = new();
  [Fact]
  public void GetMenusForRoleExpected()
  {

  }
}
