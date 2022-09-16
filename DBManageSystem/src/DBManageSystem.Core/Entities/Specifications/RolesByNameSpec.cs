using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Specification;

namespace DBManageSystem.Core.Entities.Specifications;
public class RolesByNameSpec:Specification<Role>,ISingleResultSpecification
{
  public RolesByNameSpec(string name)
  {
    Query.Where(x => x.Name == name);
  }
}
