using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Specification;

namespace DBManageSystem.Core.Entities.MenuAggregate.Specifications;
public class SubMenuWithMainMenuByIdSpec:Specification<SubMenu>,ISingleResultSpecification
{
  public SubMenuWithMainMenuByIdSpec(int menuId)
  {
    Query.Where(s => s.Id == menuId).Include(m => m.MainMenu);
  }
}
