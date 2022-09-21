using Ardalis.ApiEndpoints;
using Ardalis.Result;
using Ardalis.Result.AspNetCore;
using AutoMapper;
using DBManageSystem.Core.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DBManageSystem.ManageWebAPI.Endpoints.UserManageEndpoints
{
    public class GetCurrentUser:EndpointBaseAsync.WithoutRequest.WithActionResult<UserDTO>
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public GetCurrentUser(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("/users/current")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public override async Task<ActionResult<UserDTO>> HandleAsync(CancellationToken cancellationToken = default)
        {
            var userName = Request.HttpContext.User.Claims.FirstOrDefault(t => t.Type == ClaimTypes.Name)?.Value;
            var userResult = await _userService.GetUserByName(userName);
            return userResult.Map(ur => _mapper.Map<UserDTO>(ur)).ToActionResult(this);
        }
    }
}
