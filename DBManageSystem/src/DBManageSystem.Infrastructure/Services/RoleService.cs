using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Ardalis.Result;
using Ardalis.Specification.EntityFrameworkCore;
using DBManageSystem.Core.Entities;
using DBManageSystem.Core.Entities.Specifications;
using DBManageSystem.Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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
  public async Task<Result> CreateRole(string roleName)
  {
    Role role = new Role();
    role.Name = roleName;
   var result = await _roleManager.CreateAsync(role);
    if (result.Succeeded)
    {
      return Result.Success();
    }
    else
    {
      List<string> errors = new List<string>();
      foreach (var error in result.Errors)
      {
        errors.Add(error.Description);
        _logger?.LogInformation(error.Description);

      }
      return Result.Error(errors.FirstOrDefault());
    }
  }

  public async Task<Result> DeleteRole(int roleId)
  {
    var spec = new RoleByIdSpec(roleId);
    var roleToDelete = await _roleManager.Roles.WithSpecification(spec).FirstOrDefaultAsync();
    if(roleToDelete == null)
    {
      return Result.Error($"The Role With RoleId {roleId} is Not Found");
    }
    else
    {
      var result = await _roleManager.DeleteAsync(roleToDelete);
      if (result.Succeeded)
      {
        return Result.Success();
      }
      else
      {
        List<string> errors = new List<string>();
        foreach (var error in result.Errors)
        {
          errors.Add(error.Description);
          _logger?.LogInformation(error.Description);

        }
        return Result.Error(errors.FirstOrDefault());
      }
    }
  }

  public async Task<Result<Role>> GetRoleByName(string roleName)
  {
    var spec = new RolesByNameSpec(roleName);
    var role = await _roleManager.Roles.WithSpecification(spec).FirstOrDefaultAsync();
    if (role == null)
    {
      return Result<Role>.NotFound();
    }
    else
    {
      return Result<Role>.Success(role);
    }
  }

  public async Task<Result<List<Role>>> GetRoleExceptForAdminAndDefault()
  {
    var spec = new RolesExceptForAdmin();
    var roles = await _roleManager.Roles.WithSpecification(spec).ToListAsync();
    return new Result<List<Role>>(roles);
  }

  public async Task<Result> SetRoleForUser(int roleId, int userId)
  {
    var roleSpec = new RoleByIdSpec(roleId);
    var userSpec = new UserByIdSpec(userId);
    var role = await _roleManager.Roles.WithSpecification(roleSpec).FirstOrDefaultAsync();
    var user = await _userManager.Users.WithSpecification(userSpec).FirstOrDefaultAsync();
    
    if(role == null)
    {
      return Result.Error($"Role Not Found,RoleId:{roleId}");
    }

    if(user == null)
    {
      return Result.Error($"User Not Found,UserId:{userId}");
    }

    var rolesInDb = await _userManager.GetRolesAsync(user);
    await _userManager.RemoveFromRolesAsync(user, rolesInDb);
    var result = await _userManager.AddToRoleAsync(user, role.Name);
    if (result.Succeeded)
    {
      return Result.Success();
    }
    else
    {
      List<string> errors = new List<string>();
      foreach (var error in result.Errors)
      {
        errors.Add(error.Description);
        _logger?.LogInformation(error.Description);

      }
      return Result.Error(errors.FirstOrDefault());
    }
  }
}
