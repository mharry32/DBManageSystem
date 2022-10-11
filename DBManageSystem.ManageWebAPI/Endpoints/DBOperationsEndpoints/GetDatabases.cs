using Ardalis.ApiEndpoints;
using Ardalis.Result;
using Ardalis.Result.AspNetCore;
using AutoMapper;
using DBManageSystem.Core.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DBManageSystem.ManageWebAPI.Endpoints.DBOperationsEndpoints
{
    public class GetDatabases : EndpointBaseAsync.WithRequest<int>.WithActionResult<List<DatabaseSchemaDTO>>
    {
        private readonly IMapper _mapper;
        private readonly IDbServiceStrategy _dbServiceStrategy;
        private readonly IDatabaseServerService _databaseServerService;
        public GetDatabases(IDbServiceStrategy strategy, IDatabaseServerService manageService,IMapper mapper)
        {
            _mapper = mapper;
            _dbServiceStrategy = strategy;
            _databaseServerService = manageService;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("/dbschema/databases/{request}")]
        public override async Task<ActionResult<List<DatabaseSchemaDTO>>> HandleAsync(int request, CancellationToken cancellationToken = default)
        {
            var result = await _databaseServerService.GetDatabaseServerWithPasswordDecrptedById(request);
            if(result.IsSuccess == false)
            {
                return UnprocessableEntity();
            }

            var dbServer = result.Value;
            var schemaService = _dbServiceStrategy.Decide(dbServer.DatabaseType);
            var databasesResult = await schemaService.GetDatabaseSchemas(dbServer);
            if(databasesResult.IsSuccess == false)
            {
                return UnprocessableEntity();
            }

            return databasesResult.Map(t =>
            {
                List<DatabaseSchemaDTO> databaseSchemas = new List<DatabaseSchemaDTO>();
                t.ForEach(dbschema =>
                {
                    var dto = _mapper.Map<DatabaseSchemaDTO>(dbschema);
                    dto.ServerId = dbServer.Id;
                    databaseSchemas.Add(dto);
                });
                return databaseSchemas;
            }).ToActionResult(this);
        }
    }
}
