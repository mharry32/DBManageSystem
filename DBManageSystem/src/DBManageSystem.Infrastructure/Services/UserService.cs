using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Result;
using DBManageSystem.Core.Constants;
using DBManageSystem.Core.Entities;
using DBManageSystem.Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace DBManageSystem.Infrastructure.Services;
public class UserService : IUserService
{
  private readonly SignInManager<User> _signInManager;
  private readonly UserManager<User> _userManager;
  private readonly IAppLogger<UserService> _logger;

  public UserService(UserManager<User> userManager,
        SignInManager<User> signInManager, IAppLogger<UserService> logger)
  {
    _userManager = userManager;
    _signInManager = signInManager;
    _logger = logger;
  }

  public async Task<Result> CreateUser(User user)
  {
    var result = await _userManager.CreateAsync(user, UserConstants.DefaultPassword);
    if (result.Succeeded)
    {
      return Result.Success();
    }
    else
    {
      foreach (var error in result.Errors)
      {
        _logger?.LogInformation(error.Description);
      }
      return Result.Error(result.Errors.First().Description);
    }
  }

  public Task<Result> DeleteUser(int userId)
  {
    throw new NotImplementedException();
  }

  public Task<Result<List<User>>> GetAllUsersExceptForCurrent(int currentUserId)
  {
    throw new NotImplementedException();
  }

  public Task<Result<Role>> GetRoleByUserId(int userId)
  {
    throw new NotImplementedException();
  }

  public Task<Result<User>> GetUserById(int userId)
  {
    throw new NotImplementedException();
  }

  public Task<Result<User>> GetUserByName(string userName)
  {
    throw new NotImplementedException();
  }

  public Task<Result<string>> Login(string username, string password)
  {
    throw new NotImplementedException();
  }

  public Task<Result> ModifyPassword(User user)
  {
    throw new NotImplementedException();
  }
}
