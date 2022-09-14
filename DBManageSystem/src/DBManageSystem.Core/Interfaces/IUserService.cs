using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Result;
using DBManageSystem.Core.Entities;

namespace DBManageSystem.Core.Interfaces;
public interface IUserService
{
    Task<Result<string>> Login(string username, string password);

  Task<Result<List<User>>> GetAllUsersExceptForCurrent(User currentUser);

  Task<Result> CreateUser(User user);

  Task<Result> DeleteUser(User user);

  Task<Result> ModifyPassword(User user);

  Task<Result<User>> GetUserById(int userId);

  Task<Result<Role>> GetRole(User user);


}
