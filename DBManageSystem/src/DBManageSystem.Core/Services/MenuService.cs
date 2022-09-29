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
using DBManageSystem.Core.Interfaces;
using DBManageSystem.SharedKernel.Interfaces;

namespace DBManageSystem.Core.Services;
public class MenuService : IMenuService
{
  private readonly IRepository<SubMenu> _subMenuRepository;

  private readonly IRepository<RoleMenu> _roleMenuRepository;

  private readonly IRepository<MainMenu> _mainMenuRepository;
  public MenuService(IRepository<SubMenu> subMenuRepository, 
    IRepository<RoleMenu> roleMenuRepository,
    IRepository<MainMenu> mainMenuRepository
    )
  {
    _subMenuRepository = subMenuRepository;
    _roleMenuRepository = roleMenuRepository;
    _mainMenuRepository = mainMenuRepository;
  }


  public async Task<Result<List<MainMenu>>> GetMenusForRole(int roleId)
  {
    List<SubMenu> subMenus = new List<SubMenu>();
    Dictionary<int, MainMenu> mainMenusDic = new Dictionary<int, MainMenu>();
    RoleMenusByRoleIdSpec spec = new RoleMenusByRoleIdSpec(roleId);
    var roleMenus = await _roleMenuRepository.ListAsync(spec);
    foreach(var roleMenu in roleMenus)
    {
      SubMenuWithMainMenuByIdSpec subMenuSpec = new SubMenuWithMainMenuByIdSpec(roleMenu.SubMenuId);
      var subMenu = await _subMenuRepository.FirstOrDefaultAsync(subMenuSpec);
      mainMenusDic[subMenu.MainMenu.Id] = subMenu.MainMenu;
    }

    return new Result<List<MainMenu>>(mainMenusDic.Values.ToList());
  }


  public async Task<Result<List<int>>> GetsubMenuIdsForRole(int roleId)
  {
    List<int> subMenuIds = new List<int>();
    RoleMenusByRoleIdSpec spec = new RoleMenusByRoleIdSpec(roleId);
    var roleMenus = await _roleMenuRepository.ListAsync(spec);
    roleMenus.ForEach(r =>
    {
      subMenuIds.Add(r.SubMenuId);
    });
    return new Result<List<int>>(subMenuIds);
  }

  public async Task<Result> ModifyMenusForRole(int roleId,List<RoleMenu> roleMenus)
  {
    try
    {
      Guard.Against.Null(roleMenus);
      var roleMenuSpec = new Entities.Specifications.RoleMenusByRoleIdSpec(roleId);
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

  public async Task<Result<List<MainMenu>>> GetAllMenus()
  {
    List<MainMenu> mainMenus = new List<MainMenu>();
    MainMenusOrderSpec mainMenuSpec = new MainMenusOrderSpec();
    var mainMenusInDb = await _mainMenuRepository.ListAsync(mainMenuSpec);
    foreach(var mainMenu in mainMenusInDb)
    {
      SubMenusByMainMenuIdSpec subMenusSpec = new SubMenusByMainMenuIdSpec(mainMenu.Id);
      var subMenusInDb = await _subMenuRepository.ListAsync(subMenusSpec);
      mainMenus.Add(mainMenu);
    }
    return new Result<List<MainMenu>>(mainMenus);
  }
}
