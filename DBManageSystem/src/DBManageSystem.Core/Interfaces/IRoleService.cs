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

  Task<Result> CreateRole(string roleName);

  Task<Result> DeleteRole(int roleId);

  Task<Result> SetRoleForUser(int roleId, int userId);

  Task<Result<Role>> GetRoleByName(string roleName);



}
