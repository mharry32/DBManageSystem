using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Specification;
using DBManageSystem.Core.Constants;

namespace DBManageSystem.Core.Entities.Specifications;
public class RolesExceptForAdmin:Specification<Role>
{
  public RolesExceptForAdmin()
  {
    Query.Where(r => r.Id != RoleConstants.ADMINISTRATOR);
  }
}
