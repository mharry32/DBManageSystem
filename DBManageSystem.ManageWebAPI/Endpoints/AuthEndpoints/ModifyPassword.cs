using Ardalis.ApiEndpoints;
using Ardalis.Result;
using DBManageSystem.Core.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ardalis.Result;
namespace DBManageSystem.ManageWebAPI.Endpoints.AuthEndpoints
{
    public class ModifyPassword : EndpointBaseAsync.WithRequest<ModifyPasswordRequest>.WithActionResult
    {
        private readonly IUserService _userService;
        public ModifyPassword(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost(ModifyPasswordRequest.Route)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public override async Task<ActionResult> HandleAsync(ModifyPasswordRequest request, CancellationToken cancellationToken = default)
        {
            var result = await _userService.ModifyPassword(request.UserId, request.OldPassword, request.NewPassword);
            if (result.IsSuccess)
            {
                return Ok();
            }
            else
            {
                return BadRequest(result.Errors.FirstOrDefault());
            }
        }
    }
}
