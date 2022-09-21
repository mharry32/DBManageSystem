using Ardalis.ApiEndpoints;
using AutoMapper;
using DBManageSystem.Core.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DBManageSystem.ManageWebAPI.Endpoints.UserManageEndpoints
{
    public class DeleteUser:EndpointBaseAsync.WithRequest<int>.WithActionResult
    {
        private readonly IUserService _userService;
        public DeleteUser(IUserService userService)
        {
            _userService = userService;
        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("/users/{request}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public override async Task<ActionResult> HandleAsync(int request, CancellationToken cancellationToken = default)
        {
           var result = await _userService.DeleteUser(request);
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
