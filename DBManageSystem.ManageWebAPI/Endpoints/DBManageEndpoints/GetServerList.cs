using Ardalis.ApiEndpoints;
using Ardalis.Result.AspNetCore;
using AutoMapper;
using DBManageSystem.Core.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DBManageSystem.ManageWebAPI.Endpoints.DBManageEndpoints
{
    public class GetServerList : EndpointBaseAsync.WithoutRequest.WithActionResult<List<DatabaseServerDTO>>
    {
        private readonly IDatabaseServerService _databaseServerService;
        private readonly IMapper _mapper;
        public GetServerList(IDatabaseServerService databaseServerService, IMapper mapper)
        {
            _databaseServerService = databaseServerService;
            _mapper = mapper;
        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("/dbmanage/db")]
        public override async Task<ActionResult<List<DatabaseServerDTO>>> HandleAsync(CancellationToken cancellationToken = default)
        {
            var result = await _databaseServerService.GetServerList();
            if(result.IsSuccess)
            {
                var data = _mapper.Map<List<DatabaseServerDTO>>(result.Value);
                return Ok(data);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
