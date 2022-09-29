using Ardalis.ApiEndpoints;
using Ardalis.Result.AspNetCore;
using AutoMapper;
using DBManageSystem.Core.Entities;
using DBManageSystem.Core.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DBManageSystem.ManageWebAPI.Endpoints.MenuEndpoints
{
    public class ModifyMenus:EndpointBaseAsync.WithRequest<ModifyMenusRequest>.WithActionResult
    {
        private IMenuService _menuService;

        public ModifyMenus(IMenuService menuService)
        {
            _menuService = menuService;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut(ModifyMenusRequest.Route)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public override async Task<ActionResult> HandleAsync(ModifyMenusRequest request, CancellationToken cancellationToken = default)
        {
            List<RoleMenu> roleMenus = new List<RoleMenu>();
            foreach(var menuId in request.MenuIds)
            {
                roleMenus.Add(new RoleMenu(request.RoleId, menuId));
            }
            return (await _menuService.ModifyMenusForRole(request.RoleId,roleMenus)).ToActionResult(this);
        }
    }
}
