using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBManageSystem.Core.Entities;
using DBManageSystem.Core.Entities.MenuAggregate;
using DBManageSystem.Core.Entities.MenuAggregate.Specifications;
using DBManageSystem.Infrastructure.Data;
using Xunit;

namespace DBManageSystem.IntegrationTests.Data.DbManageRepo;

[Collection("DbManageRepoAdd")]
public class DbManageRepoAdd:IClassFixture<BaseDbManageRepoTestFixture>
{
  private DbManageSysRepository<MainMenu> _mainMenuRepo;

  private DbManageSysRepository<SubMenu> _subMenuRepo;

  private DbManageSysRepository<RoleMenu> _roleMenuRepo;

  public DbManageRepoAdd(BaseDbManageRepoTestFixture fixture)
  {
    _mainMenuRepo = new DbManageSysRepository<MainMenu>(fixture._dbContext);
    _subMenuRepo = new DbManageSysRepository<SubMenu>(fixture._dbContext);
    _roleMenuRepo = new DbManageSysRepository<RoleMenu>(fixture._dbContext);
  }

  [Fact]
  public async Task MenuAdd()
  {
    MainMenu mainMenu = new MainMenu("m1", 1);
    mainMenu.Id = 1;
    SubMenu subMenu = new SubMenu("/m1/s1", mainMenu, "s1", 1);
    subMenu.Id = 1;
    mainMenu.AddSubMenu(subMenu);

   await _mainMenuRepo.AddAsync(mainMenu);

    var mainMenuInDB = (await _mainMenuRepo.ListAsync()).FirstOrDefault();
    Assert.Equal("m1", mainMenuInDB.Name);

    SubMenuWithMainMenuByIdSpec spec = new SubMenuWithMainMenuByIdSpec(subMenu.Id);
    var subMenuInDB = (await _subMenuRepo.ListAsync(spec)).FirstOrDefault();

    Assert.NotNull(subMenuInDB.MainMenu);
    Assert.Equal(subMenu.Path, subMenuInDB.Path);
    Assert.Equal(mainMenu.Name,subMenuInDB.MainMenu.Name);
    Assert.Equal(mainMenu.Id, subMenuInDB.MainMenu.Id);

  }


  [Fact]
  public async Task RoleMenuAdd()
  {
    RoleMenu roleMenu = new RoleMenu(1, 1);
    await _roleMenuRepo.AddAsync(roleMenu);

    var roleMenuInDB = (await _roleMenuRepo.ListAsync()).FirstOrDefault();

    Assert.Equal(roleMenu.RoleId, roleMenuInDB.RoleId);
    Assert.Equal(roleMenu.SubMenuId, roleMenuInDB.SubMenuId);

  }

}
