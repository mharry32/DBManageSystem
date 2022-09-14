using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Specification;

namespace DBManageSystem.Core.Entities.Specifications;
public class RoleMenusByRoleIdSpec:Specification<RoleMenu>
{
   public RoleMenusByRoleIdSpec(int roleId)
  {
    Query.Where(r => r.RoleId == roleId);
  }
}
