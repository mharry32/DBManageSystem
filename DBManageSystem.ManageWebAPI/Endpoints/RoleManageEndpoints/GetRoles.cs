using Ardalis.ApiEndpoints;
using Ardalis.Result;
using Ardalis.Result.AspNetCore;
using AutoMapper;
using DBManageSystem.Core.Interfaces;
using DBManageSystem.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DBManageSystem.ManageWebAPI.Endpoints.RoleManageEndpoints
{
    public class GetRoles:EndpointBaseAsync.WithoutRequest.WithActionResult<IEnumerable<RoleDTO>>
    {
        private readonly IRoleService _roleService;
        private readonly IMapper _mapper;
        public GetRoles(IRoleService roleService, IMapper mapper)
        {
            _roleService = roleService;
            _mapper = mapper;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("/users/roles")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public override async Task<ActionResult<IEnumerable<RoleDTO>>> HandleAsync(CancellationToken cancellationToken = default)
        {
            var roles = await _roleService.GetRoleExceptForAdminAndDefault();
            return roles.Map(t => t.Select(r => _mapper.Map<RoleDTO>(r))).ToActionResult(this);
 
        }
    }
}
