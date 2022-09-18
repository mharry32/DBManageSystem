using Ardalis.ApiEndpoints;
using Ardalis.Result;
using DBManageSystem.Core.Entities;
using DBManageSystem.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace DBManageSystem.ManageWebAPI.Endpoints.AuthEndpoints
{
    public class Login : EndpointBaseAsync.WithRequest<LoginRequest>.WithActionResult<LoginResponse>
    {
        private readonly IUserService _userService;
        public Login(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost(LoginRequest.Routes)]
        [SwaggerOperation(
  Summary = "Perform a login request",
  Description = "Perform a login request",
  OperationId = "Auth.Login")
]
        public override async Task<ActionResult<LoginResponse>> HandleAsync(LoginRequest request, CancellationToken cancellationToken = default)
        {
            var result = await _userService.Login(request.UserName, request.Password);
            if (result.IsSuccess)
            {
                LoginResponse response = new LoginResponse(result.Value);
                return Ok(response);
            }
            else
            {
                return Unauthorized(result.Errors.FirstOrDefault());
            }
        }
    }
}
