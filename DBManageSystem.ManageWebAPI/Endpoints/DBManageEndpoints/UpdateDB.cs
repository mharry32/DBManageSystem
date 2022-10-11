using Ardalis.ApiEndpoints;
using Ardalis.Result.AspNetCore;
using AutoMapper;
using DBManageSystem.Core.Entities;
using DBManageSystem.Core.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DBManageSystem.ManageWebAPI.Endpoints.DBManageEndpoints
{
    public class UpdateDB : EndpointBaseAsync.WithRequest<DatabaseServerDTO>.WithActionResult
    {
        private readonly IDatabaseServerService _databaseServerService;
        private readonly IMapper _mapper;
        public UpdateDB(IDatabaseServerService databaseServerService, IMapper mapper)
        {
            _databaseServerService = databaseServerService;
            _mapper = mapper;
        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut(CreateDBRequest.Route)]
        public override async Task<ActionResult> HandleAsync(DatabaseServerDTO request, CancellationToken cancellationToken = default)
        {
            var server = _mapper.Map<DatabaseServer>(request);
            var result = await _databaseServerService.UpdateDatabaseServer(server);
            return result.ToActionResult(this);
        }
    }
}
