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

namespace DBManageSystem.IntegrationTests.Data.DbManageRepo;

[Collection("DbManageRepoDelete")]
public class DbManageRepoDelete : IClassFixture<BaseDbManageRepoTestFixture>
{
  private DbManageSysRepository<MainMenu> _mainMenuRepo;

  private DbManageSysRepository<SubMenu> _subMenuRepo;

  private DbManageSysRepository<RoleMenu> _roleMenuRepo;
  public DbManageRepoDelete(BaseDbManageRepoTestFixture fixture)
  {
    _mainMenuRepo = new DbManageSysRepository<MainMenu>(fixture._dbContext);
    _subMenuRepo = new DbManageSysRepository<SubMenu>(fixture._dbContext);
    _roleMenuRepo = new DbManageSysRepository<RoleMenu>(fixture._dbContext);
  }


  [Fact]
  public async Task DeleteRoleMenu()
  {
    RoleMenu roleMenu = new RoleMenu(100, 100);
    await _roleMenuRepo.AddAsync(roleMenu);
    await _roleMenuRepo.DeleteAsync(roleMenu);

    Assert.DoesNotContain(await _roleMenuRepo.ListAsync(),
       rm => rm.RoleId == roleMenu.RoleId);
  }
}
