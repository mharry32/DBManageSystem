using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Specification;

namespace DBManageSystem.Core.Entities.Specifications;
public class UserByIdSpec:Specification<User>,ISingleResultSpecification
{
  public UserByIdSpec(int userId)
  {
    Query.Where(x => x.Id == userId);
  }
}
