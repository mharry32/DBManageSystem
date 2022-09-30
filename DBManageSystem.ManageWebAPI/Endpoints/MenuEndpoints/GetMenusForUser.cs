using Ardalis.ApiEndpoints;
using AutoMapper;
using DBManageSystem.Core.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DBManageSystem.ManageWebAPI.Endpoints.MenuEndpoints
{

    public class GetMenusForUser : EndpointBaseAsync.WithoutRequest.WithActionResult<List<MainMenuDTO>>
    {
        private IMenuService _menuService;
        private IMapper _mapper;
        private readonly IUserService _userService;
        public GetMenusForUser(IMenuService menuService, IMapper mapper,IUserService userService)
        {
            _menuService = menuService;
            _mapper = mapper;
            _userService = userService;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("/admin/menus")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public override async Task<ActionResult<List<MainMenuDTO>>> HandleAsync(CancellationToken cancellationToken = default)
        {
            var userName = Request.HttpContext.User.Claims.FirstOrDefault(t => t.Type == ClaimTypes.Name)?.Value;
            var userResult = await _userService.GetUserByName(userName);
            var role = await _userService.GetRoleByUserId(userResult.Value.Id);
            var menu = await _menuService.GetMenusForRole(role.Value.Id);

            var result = _mapper.Map<List<MainMenuDTO>>(menu.Value);
            foreach(var main in result)
            {
               main.SubMenus = main.SubMenus.OrderBy(t => t.Order).ToList();
            }

            return new OkObjectResult(result);
        }
    }
}
