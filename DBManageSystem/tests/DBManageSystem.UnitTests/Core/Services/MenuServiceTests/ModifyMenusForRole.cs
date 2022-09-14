using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBManageSystem.Core.Entities;
using DBManageSystem.Core.Entities.MenuAggregate;
using DBManageSystem.Core.Entities.Specifications;
using DBManageSystem.Core.Services;
using DBManageSystem.SharedKernel.Interfaces;
using Moq;
using Xunit;

namespace DBManageSystem.UnitTests.Core.Services.MenuServiceTests;
public class ModifyMenusForRole
{
  private readonly Mock<IRepository<RoleMenu>> _mockRoleMenuRepo = new();

  private readonly Mock<IRepository<SubMenu>> _mockSubMenuRepo = new();
  [Fact]
  public async Task ModifyMenusForRole_NullRole()
  {
    var menuService = new MenuService(_mockSubMenuRepo.Object,_mockRoleMenuRepo.Object);
    var result_null =await menuService.ModifyMenusForRole(null);
    Assert.NotEmpty(result_null.ValidationErrors);
  }

  [Fact]
  public async Task ModifyMenusForRole_Success()
  {

     var roleMenusModify = new List<RoleMenu>()
      {
         new RoleMenu(2,2)
      };

    var roleMenusOrdinary = new List<RoleMenu>()
      {
         new RoleMenu(1,1)
      };

    _mockRoleMenuRepo.Setup(x => x.ListAsync(It.IsAny<RoleMenusByRoleIdSpec>(), default))
      .ReturnsAsync(roleMenusOrdinary);



    var menuService = new MenuService(_mockSubMenuRepo.Object, _mockRoleMenuRepo.Object);
    var result = await menuService.ModifyMenusForRole(roleMenusModify);
    _mockRoleMenuRepo.Verify(x => x.DeleteRangeAsync(roleMenusOrdinary, default));
    _mockRoleMenuRepo.Verify(x => x.AddRangeAsync(roleMenusModify, default));
    Assert.True(result.IsSuccess);

  }

}
