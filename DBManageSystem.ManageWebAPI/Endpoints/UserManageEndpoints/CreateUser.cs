using Ardalis.ApiEndpoints;
using AutoMapper;
using DBManageSystem.Core.Entities;
using DBManageSystem.Core.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DBManageSystem.ManageWebAPI.Endpoints.UserManageEndpoints
{
    public class CreateUser:EndpointBaseAsync.WithRequest<CreateUserRequest>.WithActionResult
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        public CreateUser(IUserService userService,IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }


        [HttpPost(CreateUserRequest.Route)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public override async Task<ActionResult> HandleAsync(CreateUserRequest request, CancellationToken cancellationToken = default)
        {
            var user = _mapper.Map<User>(request);
            var result = await _userService.CreateUser(user);
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
