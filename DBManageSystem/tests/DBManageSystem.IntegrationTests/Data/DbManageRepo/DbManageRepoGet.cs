using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBManageSystem.Core.Entities.MenuAggregate;
using DBManageSystem.Core.Entities;
using DBManageSystem.Infrastructure.Data;
using Xunit;

namespace DBManageSystem.IntegrationTests.Data.DbManageRepo;

[Collection("DbManageRepoGet")]
public class DbManageRepoGet : IClassFixture<BaseDbManageRepoTestFixture>
{
  private DbManageSysRepository<MainMenu> _mainMenuRepo;

  private DbManageSysRepository<SubMenu> _subMenuRepo;

  private DbManageSysRepository<RoleMenu> _roleMenuRepo;
  public DbManageRepoGet(BaseDbManageRepoTestFixture fixture)
  {
    _mainMenuRepo = new DbManageSysRepository<MainMenu>(fixture._dbContext);
    _subMenuRepo = new DbManageSysRepository<SubMenu>(fixture._dbContext);
    _roleMenuRepo = new DbManageSysRepository<RoleMenu>(fixture._dbContext);
  }

  [Fact]
  public async Task GetAllSubMenu()
  {
    MainMenu mainMenu = new MainMenu("m1", 1);
    mainMenu.Id = 1;
    SubMenu subMenu = new SubMenu("/m1/s1", mainMenu, "s1", 1);
    subMenu.Id = 1;
    mainMenu.AddSubMenu(subMenu);

    await _mainMenuRepo.AddAsync(mainMenu);

    var subMenus = await _subMenuRepo.ListAsync();

    Assert.Equal(subMenu.Id, subMenus.FirstOrDefault().Id);
    Assert.NotNull(subMenus.FirstOrDefault().MainMenu);
    Assert.Equal(subMenu.MainMenu.Id, subMenus.FirstOrDefault().MainMenu.Id);
  }
}
