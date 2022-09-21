using Ardalis.ApiEndpoints;
using Ardalis.Result;
using AutoMapper;
using DBManageSystem.Core.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DBManageSystem.ManageWebAPI.Endpoints.UserManageEndpoints
{
    public class GetAllUsers:EndpointBaseAsync.WithoutRequest.WithActionResult<List<UserDTO>>
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public GetAllUsers(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("/users")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public override async Task<ActionResult<List<UserDTO>>> HandleAsync(CancellationToken cancellationToken = default)
        {
            var userName = Request.HttpContext.User.Claims.FirstOrDefault(t => t.Type == ClaimTypes.Name)?.Value;
           var userResult = await _userService.GetUserByName(userName);
            if(userResult.Status == Ardalis.Result.ResultStatus.NotFound)
            {
                return BadRequest($"Cannot Find Current User with name {userName}.");
            }
            var users = await _userService.GetAllUsersExceptForCurrent(userResult.Value.Id);

            var response = new List<UserDTO>();
            foreach(var user in users.Value)
            {
                var userdto = _mapper.Map<UserDTO>(user);
                response.Add(userdto);
            }

            return new OkObjectResult(response);


        }
    }
}
