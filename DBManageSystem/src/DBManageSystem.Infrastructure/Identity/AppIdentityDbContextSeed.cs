using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Specification.EntityFrameworkCore;
using DBManageSystem.Core.Constants;
using DBManageSystem.Core.Entities;
using DBManageSystem.Core.Entities.Specifications;
using DBManageSystem.Infrastructure.Configs;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DBManageSystem.Infrastructure.Identity;
public class AppIdentityDbContextSeed
{
  public static async Task SeedAsync(AppIdentityDbContext identityDbContext, UserManager<User> userManager, RoleManager<Role> roleManager,
    DefaultPassword defaultPassword)
  {

      
      identityDbContext.Database.Migrate();

    var rolespec = new RoleByIdSpec(RoleConstants.ADMINISTRATOR);
    var roleInDb = await identityDbContext.Roles.WithSpecification(rolespec).FirstOrDefaultAsync();
    if (roleInDb == null)
    {
      Role adminRole = new Role();
      adminRole.Id = RoleConstants.ADMINISTRATOR;
      adminRole.Name = RoleConstants.ADMINISTRATOR_ROLENAME;
      await roleManager.CreateAsync(adminRole);
    }

    string adminUserName = "admin";
    var userInDb = await userManager.FindByNameAsync(adminUserName);
    if (userInDb == null)
    {
      var adminUser = new User { UserName = adminUserName };
      await userManager.CreateAsync(adminUser, defaultPassword.Value);
      adminUser = await userManager.FindByNameAsync(adminUserName);
      await userManager.AddToRoleAsync(adminUser, RoleConstants.ADMINISTRATOR_ROLENAME);
    }

  }
}
