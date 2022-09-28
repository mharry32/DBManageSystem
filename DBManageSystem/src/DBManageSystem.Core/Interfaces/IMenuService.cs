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
  public Task<Result<List<SubMenu>>> GetMenusForRole(int roleId);

  public Task<Result<List<SubMenu>>> GetAllMenus();

  public Task<Result> ModifyMenusForRole(List<RoleMenu> roleMenus);
}
