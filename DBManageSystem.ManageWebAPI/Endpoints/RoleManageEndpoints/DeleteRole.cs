using Ardalis.ApiEndpoints;
using Ardalis.Result.AspNetCore;
using DBManageSystem.Core.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DBManageSystem.ManageWebAPI.Endpoints.RoleManageEndpoints
{
    public class DeleteRole:EndpointBaseAsync.WithRequest<int>.WithActionResult
    {
        private readonly IRoleService _roleService;
        public DeleteRole(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [HttpDelete("/role/{request}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public override async Task<ActionResult> HandleAsync(int request, CancellationToken cancellationToken = default)
        {
            return this.ToActionResult(await _roleService.DeleteRole(request));
        }
    }
}
