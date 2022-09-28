using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBManageSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using DBManageSystem.Core.Entities;
using DBManageSystem.Core.Entities.MenuAggregate;

namespace DBManageSystem.FunctionalTests.ApiEndpoints;
public class DbManageSysDBSeed
{
  public static async Task SeedAsync(DbManageSysDbContext context)
  {
    context.ChangeTracker.QueryTrackingBehavior = Microsoft.EntityFrameworkCore.QueryTrackingBehavior.NoTracking;

    MainMenu tmm = new MainMenu(testMainMenu, 100);
    tmm.AddSubMenu(new SubMenu(testSubMenu, tmm, testSubMenu, 1));
    context.mainMenus.Add(tmm);

    MainMenu tmdm = new MainMenu(testModifyMainMenu, 101);
    tmdm.AddSubMenu(new SubMenu(testModifySubMenu, tmdm, testModifySubMenu, 1));
    context.mainMenus.Add(tmdm);

    context.SaveChanges();

    var submenu = context.subMenus.FirstOrDefault(t => t.Name == testSubMenu);
    context.roleMenus.Add(new RoleMenu(testRoleId,submenu.Id));
    context.SaveChanges();

  }

  public const int testRoleId = 3;

  public const string testMainMenu = "testMainMenu";

  public const string testSubMenu = "testSubMenu";

  public const string testModifyMainMenu = "testModifyMainMenu";

  public const string testModifySubMenu = "testModifySubMenu";

}
