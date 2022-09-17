using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Specification;

namespace DBManageSystem.Core.Entities.Specifications;
public class UserByIdExcludeSpec:Specification<User>
{
  public UserByIdExcludeSpec(int userId)
  {
    Query.Where(x => x.Id != userId);
  }
}
