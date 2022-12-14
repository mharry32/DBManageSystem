using Ardalis.ApiEndpoints;
using AutoMapper;
using DBManageSystem.Core.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DBManageSystem.ManageWebAPI.Endpoints.MenuEndpoints
{
    public class GetAllMenu : EndpointBaseAsync.WithoutRequest.WithActionResult<List<MainMenuDTO>>
    {
        private IMenuService _menuService;
        private IMapper _mapper;
        public GetAllMenu(IMenuService menuService,IMapper mapper)
        {
            _menuService = menuService;
            _mapper = mapper;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("/users/allmenus")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public override async Task<ActionResult<List<MainMenuDTO>>> HandleAsync(CancellationToken cancellationToken = default)
        {
            var mainMenus = (await _menuService.GetAllMenus()).Value;
            var result = _mapper.Map<List<MainMenuDTO>>(mainMenus);
            return new OkObjectResult(result);

        }
    }
}
