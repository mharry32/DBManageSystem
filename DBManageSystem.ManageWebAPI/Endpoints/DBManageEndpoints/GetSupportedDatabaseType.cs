using Ardalis.ApiEndpoints;
using AutoMapper;
using DBManageSystem.Core.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DBManageSystem.ManageWebAPI.Endpoints.DBManageEndpoints
{
    public class GetSupportedDatabaseType : EndpointBaseAsync.WithoutRequest.WithActionResult<List<DatabaseServerTypeDTO>>
    {
        private readonly IDatabaseServerService _databaseServerService;
        private readonly IMapper _mapper;
        public GetSupportedDatabaseType(IDatabaseServerService databaseServerService, IMapper mapper)
        {
            _databaseServerService = databaseServerService;
            _mapper = mapper;
        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("/dbmanage/dbtypes")]
        public override async Task<ActionResult<List<DatabaseServerTypeDTO>>> HandleAsync(CancellationToken cancellationToken = default)
        {
            var result = _databaseServerService.GetDatabaseTypes();
            List<DatabaseServerTypeDTO> dtos = new List<DatabaseServerTypeDTO>();
            foreach (var item in result.Value)
            {
                var dto = _mapper.Map<DatabaseServerTypeDTO>(item);
                dtos.Add(dto);
            }
            return Ok(dtos);
        }
    }
}
