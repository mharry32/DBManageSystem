using Ardalis.ApiEndpoints;
using AutoMapper;
using DBManageSystem.Core.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DBManageSystem.ManageWebAPI.Endpoints.DBOperationsEndpoints
{
    public class GetColumns : EndpointBaseAsync.WithRequest<GetColumnsRequest>.WithActionResult<List<ColumnSchemaDTO>>
    {
        private readonly IMapper _mapper;
        private readonly IDbServiceStrategy _dbServiceStrategy;
        private readonly IDatabaseServerService _databaseServerService;
        public GetColumns(IDbServiceStrategy strategy, IDatabaseServerService manageService, IMapper mapper)
        {
            _mapper = mapper;
            _dbServiceStrategy = strategy;
            _databaseServerService = manageService;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut("/dbschema/columns")]
        public override async Task<ActionResult<List<ColumnSchemaDTO>>> HandleAsync(GetColumnsRequest request, CancellationToken cancellationToken = default)
        {
            var result = await _databaseServerService.GetDatabaseServerWithPasswordDecrptedById(request.ServerId);
            if (result.IsSuccess == false)
            {
                return UnprocessableEntity();
            }

            var dbServer = result.Value;
            var schemaService = _dbServiceStrategy.Decide(dbServer.DatabaseType);

            var columnsResult = await schemaService.GetColumnSchemas(dbServer, request.TableName ,request.DatabaseName);
            if (columnsResult.IsSuccess == false)
            {
                return UnprocessableEntity();
            }

            List<ColumnSchemaDTO> columns = new List<ColumnSchemaDTO>();
            foreach (var column in columnsResult.Value) 
            {
                string nullText = column.IsNullable ? "空" : "非空";
                ColumnSchemaDTO dto = new ColumnSchemaDTO()
                {
                    Name = $"{column.Name},{column.DataType},{nullText}"
                };
                columns.Add(dto);
            }
            return Ok(columns);
        }
    }
}
