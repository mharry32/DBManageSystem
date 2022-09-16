using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Result;
using DBManageSystem.Core.Entities;
using DBManageSystem.Core.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace DBManageSystem.Infrastructure.Services;
public class RoleService : IRoleService
{
  private readonly UserManager<User> _userManager;
  private readonly IAppLogger<UserService> _logger;
  private readonly RoleManager<Role> _roleManager;
  public RoleService(UserManager<User> userManager,RoleManager<Role> roleManager, IAppLogger<UserService> logger)
  {
    _userManager = userManager;
    _logger = logger;
    _roleManager = roleManager;
  }
  public Task<Result> CreateRole(string roleName)
  {
    throw new NotImplementedException();
  }

  public Task<Result> DeleteRole(int roleId)
  {
    throw new NotImplementedException();
  }

  public Task<Result<Role>> GetRoleByName(string roleName)
  {
    throw new NotImplementedException();
  }

  public Task<Result<List<Role>>> GetRoleExceptForAdminAndDefault()
  {
    throw new NotImplementedException();
  }

  public Task<Result> SetRoleForUser(int roleId, int userId)
  {
    throw new NotImplementedException();
  }
}
