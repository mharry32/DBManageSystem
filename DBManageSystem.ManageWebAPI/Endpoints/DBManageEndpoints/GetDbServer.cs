using Ardalis.ApiEndpoints;
using Ardalis.Result.AspNetCore;
using AutoMapper;
using DBManageSystem.Core.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DBManageSystem.ManageWebAPI.Endpoints.DBManageEndpoints
{
    public class GetDbServer : EndpointBaseAsync.WithRequest<int>.WithActionResult<DatabaseServerDTO>
    {
        private readonly IDatabaseServerService _databaseServerService;
        private readonly IMapper _mapper;
        public GetDbServer(IDatabaseServerService databaseServerService, IMapper mapper)
        {
            _databaseServerService = databaseServerService;
            _mapper = mapper;
        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("/dbmanage/db/{request}")]
        public override async Task<ActionResult<DatabaseServerDTO>> HandleAsync(int request, CancellationToken cancellationToken = default)
        {
            var dbserver = await _databaseServerService.GetDatabaseServerWithPasswordDecrptedById(request);
            if(dbserver.IsSuccess == true)
            {
                return Ok(_mapper.Map<DatabaseServerDTO>(dbserver.Value));
            }
            else
            {
                return UnprocessableEntity();
            }
        }
    }
}
