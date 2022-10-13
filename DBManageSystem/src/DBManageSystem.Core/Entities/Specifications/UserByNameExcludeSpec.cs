using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Specification;

namespace DBManageSystem.Core.Entities.Specifications;
public class UserByNameExcludeSpec: Specification<User>
{
  public UserByNameExcludeSpec(string name)
  {
    Query.Where(x => x.UserName != name);
  }
}
