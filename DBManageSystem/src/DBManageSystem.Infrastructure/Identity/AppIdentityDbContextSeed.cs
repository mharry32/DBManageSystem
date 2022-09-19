using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBManageSystem.Core.Constants;
using DBManageSystem.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DBManageSystem.Infrastructure.Identity;
public class AppIdentityDbContextSeed
{
  public static async Task SeedAsync(AppIdentityDbContext identityDbContext, UserManager<User> userManager, RoleManager<Role> roleManager)
  {


      identityDbContext.Database.Migrate();

    Role adminRole = new Role();
    adminRole.Id = RoleConstants.ADMINISTRATOR;
    adminRole.Name = RoleConstants.ADMINISTRATOR_ROLENAME;
    await roleManager.CreateAsync(adminRole);

    string adminUserName = "admin";
    var adminUser = new User { UserName = adminUserName };
    await userManager.CreateAsync(adminUser, UserConstants.DefaultPassword);

    adminUser = await userManager.FindByNameAsync(adminUserName);
    await userManager.AddToRoleAsync(adminUser,RoleConstants.ADMINISTRATOR_ROLENAME);
  }
}
