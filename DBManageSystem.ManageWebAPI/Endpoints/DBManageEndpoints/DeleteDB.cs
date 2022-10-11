using Ardalis.ApiEndpoints;
using Ardalis.Result.AspNetCore;
using AutoMapper;
using DBManageSystem.Core.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DBManageSystem.ManageWebAPI.Endpoints.DBManageEndpoints
{
    public class DeleteDB : EndpointBaseAsync.WithRequest<int>.WithActionResult
    {
        private readonly IDatabaseServerService _databaseServerService;
        private readonly IMapper _mapper;
        public DeleteDB(IDatabaseServerService databaseServerService, IMapper mapper)
        {
            _databaseServerService = databaseServerService;
            _mapper = mapper;
        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpDelete("/dbmanage/db/{request}")]
        public override async Task<ActionResult> HandleAsync(int request, CancellationToken cancellationToken = default)
        {
           return (await _databaseServerService.DeleteDatabaseServer(request)).ToActionResult(this);
        }
    }
}
