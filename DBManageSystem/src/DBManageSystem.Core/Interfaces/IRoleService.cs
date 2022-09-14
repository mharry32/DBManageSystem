using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Result;
using DBManageSystem.Core.Entities;

namespace DBManageSystem.Core.Interfaces;
public interface IRoleService
{
  Task<Result<List<Role>>> GetRoleExceptForAdminAndDefault();

  Task<Result> CreateRole(Role role);

  Task<Result> DeleteRole(Role role);

  Task<Result> SetRoleForUser(Role role, User user);



}
