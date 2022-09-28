using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBManageSystem.Core.Entities;
using DBManageSystem.Core.Entities.MenuAggregate;
using DBManageSystem.Core.Entities.MenuAggregate.Specifications;
using DBManageSystem.Core.Entities.Specifications;
using DBManageSystem.Core.Interfaces;
using DBManageSystem.Core.Services;
using DBManageSystem.SharedKernel.Interfaces;
using Moq;
using Xunit;

namespace DBManageSystem.UnitTests.Core.Services.MenuServiceTests;
public class GetMenusForRole
{
  private readonly Mock<IRepository<SubMenu>> _mockSubMenuRepo = new();

  private readonly Mock<IRepository<RoleMenu>> _mockRoleMenuRepo = new();
  [Fact]
  public async Task GetMenusForRoleExpected()
  {
    Role role = new Role();
    role.Id = 1;

    List<RoleMenu> roleMenus = new List<RoleMenu>();
    MenusBuilder menusBuilder = new MenusBuilder();
    var menuForRole1 = menusBuilder.AddMainMenu("m1").AddSubMenu("s1").AddSubMenu("s2").Build();
    var subMenu = menuForRole1.Item2;
    foreach (var menu in menuForRole1.Item2)
    {
      RoleMenu m = new RoleMenu(1, menu.Id);
      roleMenus.Add(m);
    }

    _mockRoleMenuRepo.Setup(x => x.ListAsync(It.IsAny<RoleMenusByRoleIdSpec>(), default)).ReturnsAsync(roleMenus);
    _mockSubMenuRepo.SetupSequence(x => x.FirstOrDefaultAsync(It.IsAny<SubMenuWithMainMenuByIdSpec>(), default)).
      ReturnsAsync(subMenu[0]).ReturnsAsync(subMenu[1]);

    var menuService = new MenuService(_mockSubMenuRepo.Object, _mockRoleMenuRepo.Object);
    var result = await menuService.GetMenusForRole(role.Id);
    Assert.NotNull(result.Value);
    Assert.Equal<int>(1, result.Value[0].Id);
    Assert.Equal<int>(2, result.Value[1].Id);
    Assert.Equal("m1", result.Value[0].MainMenu.Name);

  }

  [Fact]
  public async Task GetMenusForRole_SubMenuNotFound()
  {
    Role role = new Role();
    role.Id = 1;

    List<RoleMenu> roleMenus = new List<RoleMenu>();
    MenusBuilder menusBuilder = new MenusBuilder();
    var menuForRole1 = menusBuilder.AddMainMenu("m1").AddSubMenu("s1").AddSubMenu("s2").Build();
    var subMenu = menuForRole1.Item2;
    foreach (var menu in menuForRole1.Item2)
    {
      RoleMenu m = new RoleMenu(1, menu.Id);
      roleMenus.Add(m);
    }

    _mockRoleMenuRepo.Setup(x => x.ListAsync(It.IsAny<RoleMenusByRoleIdSpec>(), default)).ReturnsAsync(roleMenus);
    _mockSubMenuRepo.SetupSequence(x => x.FirstOrDefaultAsync(It.IsAny<SubMenuWithMainMenuByIdSpec>(), default)).
      ReturnsAsync(subMenu[0]).ReturnsAsync(default(SubMenu));

    var menuService = new MenuService(_mockSubMenuRepo.Object, _mockRoleMenuRepo.Object);
    var result = await menuService.GetMenusForRole(role.Id);
    Assert.True(!result.IsSuccess);
    Assert.NotEmpty(result.Errors);
  }
}
