using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Specification;

namespace DBManageSystem.Core.Entities.MenuAggregate.Specifications;
public class SubMenusByMainMenuIdSpec:Specification<SubMenu>
{
  public SubMenusByMainMenuIdSpec(int mainMenuId)
  {
    Query.Where(m => m.MainMenu.Id == mainMenuId).OrderBy(s => s.Order);
  }
}
