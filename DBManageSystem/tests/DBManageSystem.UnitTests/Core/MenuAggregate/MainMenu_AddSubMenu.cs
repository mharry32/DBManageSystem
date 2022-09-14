using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DBManageSystem.UnitTests.Core.MenuAggregate;
public class MainMenu_AddSubMenu
{
  [Fact]
  public void AddSubMenuWithNoException()
  {
    MenusBuilder menusBuilder =new MenusBuilder();
    var menu = menusBuilder.AddMainMenu("main").AddSubMenu("sub").Build();
    Assert.NotNull(menu.Item1);
    Assert.NotNull(menu.Item2);
    Assert.Equal("sub", menu.Item2[0].Name);
  }


  [Fact]
  public void AddMenuWithNullSubMenu()
  {
    MenusBuilder menusBuilder = new MenusBuilder();
    var menu = menusBuilder.AddMainMenu("main").AddSubMenu("sub").Build();
    Assert.Throws<ArgumentNullException>(() =>
    {
      menu.Item1.AddSubMenu(new DBManageSystem.Core.Entities.MenuAggregate.SubMenu("test", null, "test", 1));

    });
  }


  [Fact]
  public void AddSubMenuWithWrongMainMenu()
  {
    MenusBuilder menusBuilder = new MenusBuilder();
    var menu = menusBuilder.AddMainMenu("main").AddSubMenu("sub").Build();
    Assert.Throws<ArgumentException>(() =>
    {
      var main = new DBManageSystem.Core.Entities.MenuAggregate.MainMenu("testmain", 2);
      main.Id = 2;
      menu.Item1.AddSubMenu(new DBManageSystem.Core.Entities.MenuAggregate.SubMenu("test",main, "test", 1));

    });
  } 
  
  [Fact]
  public void AddSubMenuWithDuplicateOrder()
  {
    MenusBuilder menusBuilder = new MenusBuilder();
    var menu = menusBuilder.AddMainMenu("main").AddSubMenu("sub").Build();
    Assert.Throws<ArgumentException>(() =>
    {
      menu.Item1.AddSubMenu(new DBManageSystem.Core.Entities.MenuAggregate.SubMenu("test",menu.Item1, "test", 1));

    });
  }
}
