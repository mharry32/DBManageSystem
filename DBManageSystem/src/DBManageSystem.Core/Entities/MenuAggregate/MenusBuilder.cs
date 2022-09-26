using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBManageSystem.Core.Entities.MenuAggregate;
public class MenusBuilder
{
  private List<SubMenu> _subMenus;
  private MainMenu _mainMenu = null;
  private int _subMenuId = 0;
  private int _mainMenuId = 0;

  public MenusBuilder AddMainMenu(string name)
  {
    _mainMenuId++;
    _mainMenu = new MainMenu(name, _mainMenuId);
    _subMenus = new List<SubMenu>();
    _mainMenu.Id = _mainMenuId;
    return this;
  }

  public MenusBuilder AddSubMenu(string name,string path=null)
  {
    if(path == null)
    {
      path = name;
    }
    _subMenuId++;
    var subMenu = new SubMenu($"{path}", _mainMenu, name, _subMenuId);
    subMenu.Id = _subMenuId;
    _subMenus.Add(subMenu);
    _mainMenu.AddSubMenu(subMenu);
    return this;
  }

  public Tuple<MainMenu, List<SubMenu>> Build()
  {
    return new Tuple<MainMenu, List<SubMenu>>(_mainMenu, _subMenus);
  }
}
