using Ardalis.ApiEndpoints;
using Ardalis.Result.AspNetCore;
using DBManageSystem.Core.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DBManageSystem.ManageWebAPI.Endpoints.RoleManageEndpoints
{
    public class ModifyRoleForUser:EndpointBaseAsync.WithRequest<ModifyRoleForUserRequest>.WithActionResult
    {
        private readonly IRoleService _roleService;
        public ModifyRoleForUser(IRoleService roleService)
        {
            _roleService = roleService;
        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [HttpPut(ModifyRoleForUserRequest.Route)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public override async Task<ActionResult> HandleAsync(ModifyRoleForUserRequest request, CancellationToken cancellationToken = default)
        {
            return this.ToActionResult(await _roleService.SetRoleForUser(request.RoleId, request.UserId));
        }
    }
}
