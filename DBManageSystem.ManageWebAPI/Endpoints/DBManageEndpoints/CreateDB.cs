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
    
    public class CreateDB : EndpointBaseAsync.WithRequest<CreateDBRequest>.WithActionResult
    {
        private readonly IDatabaseServerService _databaseServerService;
        private readonly IMapper _mapper;
        public CreateDB(IDatabaseServerService databaseServerService, IMapper mapper)
        {
            _databaseServerService = databaseServerService;
            _mapper = mapper;
        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost(CreateDBRequest.Route)]
        public override async Task<ActionResult> HandleAsync(CreateDBRequest request, CancellationToken cancellationToken = default)
        {
            var server = _mapper.Map<DatabaseServer>(request);
            var result = await _databaseServerService.CreateDatabaseServer(server);
            return result.ToActionResult(this);
        }
    }
}
