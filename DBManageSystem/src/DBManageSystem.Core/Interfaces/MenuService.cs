using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Ardalis.Result;
using DBManageSystem.Core.Entities;
using DBManageSystem.Core.Entities.MenuAggregate;
using DBManageSystem.Core.Entities.MenuAggregate.Specifications;
using DBManageSystem.Core.Entities.Specifications;
using DBManageSystem.SharedKernel.Interfaces;

namespace DBManageSystem.Core.Interfaces;
public class MenuService : IMenuService
{
  private readonly IRepository<SubMenu> _subMenuRepository;

  private readonly IRepository<RoleMenu> _roleMenuRepository;
  public MenuService(IRepository<SubMenu> subMenuRepository, IRepository<RoleMenu> roleMenuRepository)
  {
    _subMenuRepository = subMenuRepository;
    _roleMenuRepository = roleMenuRepository;
  }
  public async Task<Result<List<SubMenu>>> GetAllMenus()
  {
    return new Result<List<SubMenu>>(await _subMenuRepository.ListAsync());
  }

  public async Task<Result<List<SubMenu>>> GetMenusForRole(Role role)
  {
    var roleMenuSpec = new RoleMenusByRoleIdSpec(role.Id);
    var roleMenus = await _roleMenuRepository.ListAsync(roleMenuSpec);
    var subMenus = new List<SubMenu>();

    foreach (var roleMenu in roleMenus)
    {
      var SubMenuSpec = new SubMenuWithMainMenuByIdSpec(roleMenu.SubMenuId);
      var subMenu = await _subMenuRepository.FirstOrDefaultAsync(SubMenuSpec);
      if (subMenu == null)
      {
        return Result<List<SubMenu>>.Error($"SubMenuNotFound,MenuId:{roleMenu.SubMenuId}");
      }
      subMenus.Add(subMenu);
    }

    return new Result<List<SubMenu>>(subMenus);
  }

  public async Task<Result> ModifyMenusForRole(List<RoleMenu> roleMenus)
  {
    try
    {
      Guard.Against.NullOrEmpty(roleMenus);
      var roleMenuSpec = new RoleMenusByRoleIdSpec(roleMenus[0].RoleId);
      var roleMenusInStore = await _roleMenuRepository.ListAsync(roleMenuSpec);
      await _roleMenuRepository.DeleteRangeAsync(roleMenusInStore);
      await _roleMenuRepository.AddRangeAsync(roleMenus);
      return Result.Success();
    }
    catch (ArgumentException argex)
    {
      var errors = new List<ValidationError>
      {
        new() { Identifier = nameof(roleMenus), ErrorMessage = argex.Message }
      };
      return Result.Invalid(errors);
    }


  }
}
