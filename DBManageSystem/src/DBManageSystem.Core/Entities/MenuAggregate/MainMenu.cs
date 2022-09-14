using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.GuardClauses;

namespace DBManageSystem.Core.Entities.MenuAggregate;
public class MainMenu:MenuBase
{
   private MainMenu() { }

  public MainMenu(string name,int order)
  {
    Name = name;
    Order = order;
  }


  public void AddSubMenu(SubMenu subMenu)
  {
    Guard.Against.Null(subMenu.MainMenu);
    if(subMenu.MainMenu.Id!=this.Id)
    {
      throw new ArgumentException($"SubMenu doesn't belong to MainMenu,MainMenuId:{this.Id},SubMenuId:{subMenu.Id}");
    }

    if (!SubMenus.Any(i => i.Order == subMenu.Order))
    {
      _subMenus.Add(subMenu);
      return;
    }
    else
    {
      throw new ArgumentException($"Have Duplicate Order in SubMenu,Order:{subMenu.Order}");
    }
   
  }

  private readonly List<SubMenu> _subMenus = new List<SubMenu>();

  // Using List<>.AsReadOnly() 
  // This will create a read only wrapper around the private list so is protected against "external updates".
  // It's much cheaper than .ToList() because it will not have to copy all items in a new collection. (Just one heap alloc for the wrapper instance)
  //https://msdn.microsoft.com/en-us/library/e78dcd75(v=vs.110).aspx 
  public IReadOnlyCollection<SubMenu> SubMenus => _subMenus.AsReadOnly();
}
