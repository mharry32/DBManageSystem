using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBManageSystem.Core.Entities;
using DBManageSystem.Core.Entities.MenuAggregate;
using Microsoft.EntityFrameworkCore;

namespace DBManageSystem.Infrastructure.Data;
public class DbManageSysDbContext : DbContext
{
  public DbSet<MainMenu> mainMenus { get; set; }

  public DbSet<SubMenu> subMenus { get; set; }

  public DbSet<RoleMenu> roleMenus { get; set; }
  public DbManageSysDbContext(DbContextOptions<DbManageSysDbContext> options) : base(options)
  {

  }
}
