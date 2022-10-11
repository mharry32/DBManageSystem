using Ardalis.ApiEndpoints;
using AutoMapper;
using DBManageSystem.Core.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DBManageSystem.ManageWebAPI.Endpoints.DBOperationsEndpoints
{
    public class GetTables : EndpointBaseAsync.WithRequest<GetTablesRequest>.WithActionResult<List<TableSchemaDTO>>
    {
        private readonly IMapper _mapper;
        private readonly IDbServiceStrategy _dbServiceStrategy;
        private readonly IDatabaseServerService _databaseServerService;
        public GetTables(IDbServiceStrategy strategy, IDatabaseServerService manageService, IMapper mapper)
        {
            _mapper = mapper;
            _dbServiceStrategy = strategy;
            _databaseServerService = manageService;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut("/dbschema/tables")]
        public override async Task<ActionResult<List<TableSchemaDTO>>> HandleAsync(GetTablesRequest request, CancellationToken cancellationToken = default)
        {
            var result = await _databaseServerService.GetDatabaseServerWithPasswordDecrptedById(request.ServerId);
            if (result.IsSuccess == false)
            {
                return UnprocessableEntity();
            }

            var dbServer = result.Value;
            var schemaService = _dbServiceStrategy.Decide(dbServer.DatabaseType);

            var tablesResult = await schemaService.GetTableSchemas(dbServer, request.DatabaseName);
            if(tablesResult.IsSuccess == false)
            {
                return UnprocessableEntity();
            }

            return Ok(_mapper.Map<List<TableSchemaDTO>>(tablesResult.Value));

        }
    }
}
