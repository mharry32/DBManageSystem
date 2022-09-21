using Ardalis.ApiEndpoints;
using Ardalis.Result.AspNetCore;
using AutoMapper;
using DBManageSystem.Core.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DBManageSystem.ManageWebAPI.Endpoints.RoleManageEndpoints
{
    public class CreateRole:EndpointBaseAsync.WithRequest<CreateRoleRequest>.WithActionResult
    {
        private readonly IRoleService _roleService;
        public CreateRole(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [HttpPost(CreateRoleRequest.Route)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public override async Task<ActionResult> HandleAsync(CreateRoleRequest request, CancellationToken cancellationToken = default)
        {
            var result = await _roleService.CreateRole(request.RoleName);
            return result.ToActionResult(this);
        }
    }
}
