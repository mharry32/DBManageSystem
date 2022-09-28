using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBManageSystem.Core.Entities.MenuAggregate;
using DBManageSystem.Core.Entities;
using DBManageSystem.Infrastructure.Data;
using Xunit;
using DBManageSystem.SharedKernel.Interfaces;
using DBManageSystem.Core.Entities.MenuAggregate.Specifications;

namespace DBManageSystem.IntegrationTests.Data.DbManageRepo;

[Collection("DbManageRepoGet")]
public class DbManageRepoGet : IClassFixture<BaseDbManageRepoTestFixture>
{
  private IRepository<MainMenu> _mainMenuRepo;

  private IRepository<SubMenu> _subMenuRepo;

  private IRepository<RoleMenu> _roleMenuRepo;

  private DbManageSysDbContext _context;
  public DbManageRepoGet(BaseDbManageRepoTestFixture fixture)
  {
    _mainMenuRepo = new DbManageSysRepository<MainMenu>(fixture._dbContext);
    _subMenuRepo = new DbManageSysRepository<SubMenu>(fixture._dbContext);
    _roleMenuRepo = new DbManageSysRepository<RoleMenu>(fixture._dbContext);
    _context = fixture._dbContext;
  }

  [Fact]
  public async Task GetAllSubMenu()
  {
    MainMenu mainMenu = new MainMenu("m1", 1);
    SubMenu subMenu = new SubMenu("/m1/s1", mainMenu, "s1", 1);
    mainMenu.AddSubMenu(subMenu);
    _context.ChangeTracker.QueryTrackingBehavior = Microsoft.EntityFrameworkCore.QueryTrackingBehavior.NoTracking;

    _context.Add(mainMenu);
    _context.SaveChanges();
    //await _mainMenuRepo.AddAsync(mainMenu);
    var subMenus = await _subMenuRepo.ListAsync(new SubMenusWithMainMenuSpec());

    var targetSubMenu = subMenus.FirstOrDefault(t => t.Name == subMenu.Name);
    Assert.NotNull(targetSubMenu);
    Assert.NotNull(targetSubMenu.MainMenu);
    Assert.Equal(targetSubMenu.MainMenu.Name, mainMenu.Name);
  }
}
