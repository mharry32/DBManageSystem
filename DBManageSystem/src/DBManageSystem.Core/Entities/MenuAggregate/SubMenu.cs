using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBManageSystem.Core.Entities.MenuAggregate;
public class SubMenu:MenuBase
{
  private SubMenu() { }

  public SubMenu(string path, MainMenu mainMenu,string name,int order)
  {
    Path = path;
    MainMenu = mainMenu;
    Name = name;
    Order = order;
  }

  public string Path { get;private set; }

    public MainMenu MainMenu { get;private set; }
}
