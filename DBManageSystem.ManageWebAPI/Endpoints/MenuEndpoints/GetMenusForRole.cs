using Ardalis.ApiEndpoints;
using Ardalis.Result.AspNetCore;
using AutoMapper;
using DBManageSystem.Core.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DBManageSystem.ManageWebAPI.Endpoints.MenuEndpoints
{
    public class GetMenusForRole:EndpointBaseAsync.WithRequest<int>.WithActionResult<List<int>>
    {
        private IMenuService _menuService;
        private IMapper _mapper;
        public GetMenusForRole(IMenuService menuService, IMapper mapper)
        {
            _menuService = menuService;
            _mapper = mapper;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("/users/menusbyrole/{request}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public override async Task<ActionResult<List<int>>> HandleAsync(int request,CancellationToken cancellationToken = default)
        {
           return (await _menuService.GetsubMenuIdsForRole(request)).ToActionResult(this);
           

        }
    }
}
