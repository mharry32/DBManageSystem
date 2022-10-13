using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Ardalis.Result;
using Ardalis.Specification.EntityFrameworkCore;
using DBManageSystem.Core.Constants;
using DBManageSystem.Core.Entities;
using DBManageSystem.Core.Entities.Specifications;
using DBManageSystem.Core.Interfaces;
using DBManageSystem.Infrastructure.Configs;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace DBManageSystem.Infrastructure.Services;
public class UserService : IUserService
{
  private readonly SignInManager<User> _signInManager;
  private readonly UserManager<User> _userManager;
  private readonly IAppLogger<UserService> _logger;
  private readonly RoleManager<Role> _roleManager;
  private readonly DefaultPassword _defaultPassword;
  private readonly JwtSecret _jwtSecret;

  public UserService(UserManager<User> userManager,
        SignInManager<User> signInManager, RoleManager<Role> roleManager,IAppLogger<UserService> logger,
        DefaultPassword defaultPassword,
        JwtSecret jwtSecret)
  {
    _userManager = userManager;
    _signInManager = signInManager;
    _logger = logger;
    _roleManager = roleManager;
    _jwtSecret = jwtSecret;
    _defaultPassword = defaultPassword;
  }

  public async Task<Result> CreateUser(User user)
  {
    var result = await _userManager.CreateAsync(user, _defaultPassword.Value);
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

  public async Task<Result> DeleteUser(int userId)
  {
    var spec = new UserByIdSpec(userId);
    var userToDelete =await _userManager.Users.WithSpecification(spec).FirstOrDefaultAsync();
    if(userToDelete == null)
    {
      _logger?.LogInformation($"User Not Found,userId:{userId}");
      return Result.Error($"User Not Found,userId:{userId}");
    }
    else
    {
      var result = await _userManager.DeleteAsync(userToDelete);
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
  }

  public async Task<Result<List<User>>> GetAllUsersExceptForCurrent(int currentUserId)
  {
    var spec = new UserByIdExcludeSpec(currentUserId);
    var excludeadmin = new UserByNameExcludeSpec("admin");
    var users = await _userManager.Users.WithSpecification(spec).WithSpecification(excludeadmin).ToListAsync();
    return new Result<List<User>>(users);
  }

  public async Task<Result<Role>> GetRoleByUserId(int userId)
  {
    try
    {
      var userSpec = new UserByIdSpec(userId);
      var user = await _userManager.Users.WithSpecification(userSpec).FirstOrDefaultAsync();
      Guard.Against.Null(user,$"User Not Found.UserId:{userId}");
      var roles = await _userManager.GetRolesAsync(user);
      if(roles.Count == 0)
      {
        Role role = new Role();
        role.Id = RoleConstants.DEFAULT;
        role.Name = RoleConstants.DEFAULT_ROLENAME;
        return new Result<Role>(role);
      }
      else
      {
        var role =await _roleManager.FindByNameAsync(roles[0]);
        return new Result<Role>(role);
      }
    }
    catch(ArgumentNullException ex)
    {
      _logger?.LogInformation(ex.ToString());
      return Result.Error(ex.Message);
    }
  }

  public async Task<Result<User>> GetUserById(int userId)
  {
    var spec = new UserByIdSpec(userId);
    var user =await _userManager.Users.WithSpecification(spec).FirstOrDefaultAsync();
    if(user == null)
    {
      return Result<User>.NotFound();
    }
    return new Result<User>(user);
  }

  public async Task<Result<User>> GetUserByName(string userName)
  {
    var user = await _userManager.FindByNameAsync(userName);
    if (user == null)
    {
      return Result<User>.NotFound();
    }
    else
    {
      return new Result<User>(user);
    }
  }

  public async Task<Result<string>> Login(string username, string password,string loginIP = null)
  {
    var result = await _signInManager.PasswordSignInAsync(username, password, false, false);
    if (result.Succeeded)
    {
      var tokenHandler = new JwtSecurityTokenHandler();
      var key = Encoding.ASCII.GetBytes(_jwtSecret.Value);
      var user = await _userManager.FindByNameAsync(username);
      user.LastLoginIP = loginIP;
     await _userManager.UpdateAsync(user);
      var roles = await _userManager.GetRolesAsync(user);
      var claims = new List<Claim> { new Claim(ClaimTypes.Name, username) };

      foreach (var role in roles)
      {
        claims.Add(new Claim(ClaimTypes.Role, role));
      }

      var tokenDescriptor = new SecurityTokenDescriptor
      {
        Subject = new ClaimsIdentity(claims.ToArray()),
        Expires = DateTime.UtcNow.AddDays(7),
        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
      };
      var token = tokenHandler.CreateToken(tokenDescriptor);
      return new Result<string>(tokenHandler.WriteToken(token));
    }
    else
    {
      _logger?.LogInformation($"User Login Failed.Reason:{result.ToString()}");
      return Result.Error($"User Login Failed.Reason:{result.ToString()}");
    }
  }

  public async Task<Result> ModifyPassword(int userId, string currentPassword, string newPassword)
  {
    try
    {
      var spec = new UserByIdSpec(userId);
      var user = await _userManager.Users.WithSpecification(spec).FirstOrDefaultAsync();
      Guard.Against.Null(user, $"user Not Found,userId:{userId}");
      
      var result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
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
    catch(ArgumentNullException ex)
    {
      _logger?.LogInformation(ex.ToString());
      return Result.Error(ex.Message);
    }
  }
}
