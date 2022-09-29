using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Specification;

namespace DBManageSystem.Core.Entities.MenuAggregate.Specifications;
public class MainMenusOrderSpec:Specification<MainMenu>
{
  public MainMenusOrderSpec()
  {
    Query.OrderBy(m => m.Order);
  }
}
