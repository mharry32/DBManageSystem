using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Result;
using DBManageSystem.Core.Entities;
using DBManageSystem.Core.Entities.MenuAggregate;

namespace DBManageSystem.Core.Interfaces;
public interface IMenuService
{
  public Task<Result<List<int>>> GetsubMenuIdsForRole(int roleId);

  public Task<Result<List<MainMenu>>> GetAllMenus();
  
  public Task<Result<List<MainMenu>>> GetMenusForRole(int roleId);

  public Task<Result> ModifyMenusForRole(int roleId,List<RoleMenu> roleMenus);
}
