using Ardalis.ApiEndpoints;
using Ardalis.Result;
using AutoMapper;
using DBManageSystem.Core.Entities;
using DBManageSystem.Core.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.Xml;

namespace DBManageSystem.ManageWebAPI.Endpoints.DBManageEndpoints
{
    public class GetSqlLogs : EndpointBaseAsync.WithRequest<GetSqlLogsRequest>.WithActionResult<SqlLogsDTO>
    {
        private readonly IDatabaseServerService _databaseServerService;
        private readonly IMapper _mapper;
        public GetSqlLogs(IDatabaseServerService databaseServerService,IMapper mapper)
        {
            _databaseServerService = databaseServerService;
            _mapper = mapper;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut("/sqllogs")]
        public override async Task<ActionResult<SqlLogsDTO>> HandleAsync(GetSqlLogsRequest request, CancellationToken cancellationToken = default)
        {
            var result = await _databaseServerService.GetExecutedSqls(request.PageIndex, request.PageSize);
            if(result.IsSuccess == false)
            {
                return UnprocessableEntity();
            }
            SqlLogsDTO sqlLogsDTO = new SqlLogsDTO();
            sqlLogsDTO.SqlLogs = _mapper.Map<List<SqlLogDTO>>(result.Value);
            sqlLogsDTO.PagedInfo = result.PagedInfo;
            return Ok(sqlLogsDTO);
        }
    }
}
