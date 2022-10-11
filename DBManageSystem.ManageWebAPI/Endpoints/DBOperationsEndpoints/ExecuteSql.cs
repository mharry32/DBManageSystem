using Ardalis.ApiEndpoints;
using AutoMapper;
using DBManageSystem.Core.Entities;
using DBManageSystem.Core.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DBManageSystem.ManageWebAPI.Endpoints.DBOperationsEndpoints
{
    public class ExecuteSql : EndpointBaseAsync.WithRequest<ExecuteSqlRequest>.WithActionResult<SqlExecuteResult>
    {
        private readonly IMapper _mapper;
        private readonly IDbServiceStrategy _dbServiceStrategy;
        private readonly IDatabaseServerService _databaseServerService;
        public ExecuteSql(IDbServiceStrategy strategy, IDatabaseServerService manageService, IMapper mapper)
        {
            _mapper = mapper;
            _dbServiceStrategy = strategy;
            _databaseServerService = manageService;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("/dbschema/execsql")]
        public override async Task<ActionResult<SqlExecuteResult>> HandleAsync(ExecuteSqlRequest request, CancellationToken cancellationToken = default)
        {
            var dbserverResult = await _databaseServerService.GetDatabaseServerWithPasswordDecrptedById(request.ServerId);
            if(dbserverResult.IsSuccess == false)
            {
                return new UnprocessableEntityResult();
            }

            var dbserver = dbserverResult.Value;

            var service = _dbServiceStrategy.Decide(dbserver.DatabaseType);

            var execResult = await service.ExecuteSql(dbserver, request.DatabaseName, request.SqlText);

            return execResult;
        }
    }
}
