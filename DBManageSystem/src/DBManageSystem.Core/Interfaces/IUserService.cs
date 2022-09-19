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
    Task<Result<string>> Login(string username, string password,string loginIP = null);

  Task<Result<List<User>>> GetAllUsersExceptForCurrent(int currentUserId);

  Task<Result> CreateUser(User user);

  Task<Result> DeleteUser(int userId);

  Task<Result> ModifyPassword(int userId,string currentPassword,string newPassword);

  Task<Result<User>> GetUserById(int userId);

  Task<Result<User>> GetUserByName(string userName);

  Task<Result<Role>> GetRoleByUserId(int userId);


}
