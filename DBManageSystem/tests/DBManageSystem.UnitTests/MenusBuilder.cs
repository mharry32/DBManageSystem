using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBManageSystem.Core.Entities.MenuAggregate;
using Moq;

namespace DBManageSystem.UnitTests;
public class MenusBuilder
{
  private List<SubMenu> _subMenus = new List<SubMenu>();
  private MainMenu _mainMenu = null;
  private int _subMenuId = 0;
  private int _mainMenuId = 0;

  public MenusBuilder AddMainMenu(string name)
  {
      _mainMenuId++;
    _mainMenu = new MainMenu(name, _mainMenuId);
    _mainMenu.Id = _mainMenuId;
    return this;
  }

  public MenusBuilder AddSubMenu(string name)
  {
    _subMenuId++;
    var subMenu = new SubMenu($"/{name}", _mainMenu, name, _subMenuId);
    subMenu.Id=_subMenuId;
    _subMenus.Add(subMenu);
    _mainMenu.AddSubMenu(subMenu);
    return this;
  }

  public Tuple<MainMenu,List<SubMenu>> Build()
  {
    return new Tuple<MainMenu, List<SubMenu>>(_mainMenu, _subMenus);
  }
}
