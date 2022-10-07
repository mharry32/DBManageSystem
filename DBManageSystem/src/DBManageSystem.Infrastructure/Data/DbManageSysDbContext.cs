using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using DBManageSystem.Core.Constants;
using DBManageSystem.Core.Entities;
using DBManageSystem.Core.Entities.MenuAggregate;
using Microsoft.EntityFrameworkCore;
using Ardalis.SmartEnum.EFCore;
using SmartEnum.EFCore;

namespace DBManageSystem.Infrastructure.Data;
public class DbManageSysDbContext : DbContext
{
  public DbSet<MainMenu> mainMenus { get; set; }

  public DbSet<SubMenu> subMenus { get; set; }

  public DbSet<RoleMenu> roleMenus { get; set; }

  public DbSet<DatabaseServer> databaseServers { get; set; }
  public DbManageSysDbContext(DbContextOptions<DbManageSysDbContext> options) : base(options)
  {

  }


  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    MenusBuilder builder = new MenusBuilder();
    var dbManageMenus = builder.AddMainMenu(MenuConstants.DB_MANAGE_MAIN_NAME)
      .AddSubMenu(MenuConstants.DB_MONITOR_SUB_NAME, MenuConstants.DB_MONITOR_SUB_PATH)
      .AddSubMenu(MenuConstants.DB_REPORT_SUB_NAME, MenuConstants.DB_REPORT_SUB_PATH)
      .Build();

    var accountManageMenus = builder.AddMainMenu(MenuConstants.ACCOUNT_MANAGE_MAIN_NAME)
      .AddSubMenu(MenuConstants.ACCOUNT_USER_SUB_NAME, MenuConstants.ACCOUNT_USER_SUB_PATH)
      .AddSubMenu(MenuConstants.ACCOUNT_ROLE_SUB_NAME, MenuConstants.ACCOUNT_ROLE_SUB_PATH)
      .AddSubMenu(MenuConstants.ACCOUNT_PERMISSION_SUB_NAME, MenuConstants.ACCOUNT_PERMISSION_SUB_PATH)
      .Build();


    int roleMenuId = 1;
    foreach (var menu in dbManageMenus.Item2)
    {
      modelBuilder.Entity<SubMenu>().HasData(new
      {
        Path = menu.Path,
      MainMenuId = menu.MainMenu.Id,
      Name = menu.Name,
      Order = menu.Order,
      Id = menu.Id
    });
      modelBuilder.Entity<RoleMenu>().HasData(new RoleMenu(RoleConstants.ADMINISTRATOR, menu.Id) { Id = roleMenuId});
      roleMenuId++;
    }

    foreach(var menu in accountManageMenus.Item2)
    {
      modelBuilder.Entity<SubMenu>().HasData(new
      {
        Path = menu.Path,
        MainMenuId = menu.MainMenu.Id,
        Name = menu.Name,
        Order = menu.Order,
        Id = menu.Id
      });
      modelBuilder.Entity<RoleMenu>().HasData(new RoleMenu(RoleConstants.ADMINISTRATOR, menu.Id) { Id = roleMenuId });
      roleMenuId++;
    }

   
    modelBuilder.Entity<MainMenu>().HasData(new
    {
      Id = dbManageMenus.Item1.Id,
      Name = dbManageMenus.Item1.Name,
      Order = dbManageMenus.Item1.Order
    });
    modelBuilder.Entity<MainMenu>().HasData(new
    {
      Id = accountManageMenus.Item1.Id,
      Name = accountManageMenus.Item1.Name,
      Order = accountManageMenus.Item1.Order
    });

    modelBuilder.ConfigureSmartEnum();
  }
}
