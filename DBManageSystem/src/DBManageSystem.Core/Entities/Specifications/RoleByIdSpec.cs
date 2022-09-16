using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Specification;

namespace DBManageSystem.Core.Entities.Specifications;
public class RoleByIdSpec:Specification<Role>,ISingleResultSpecification
{
  public RoleByIdSpec(int roleId)
  {
    Query.Where(r => r.Id == roleId);
  }
}
