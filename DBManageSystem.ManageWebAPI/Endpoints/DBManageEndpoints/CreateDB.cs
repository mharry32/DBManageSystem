using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DBManageSystem.ManageWebAPI.Endpoints.DBManageEndpoints
{
    
    public class CreateDB : EndpointBaseAsync.WithoutRequest.WithActionResult
    {
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("/dbmanage/create")]
        public override async Task<ActionResult> HandleAsync(CancellationToken cancellationToken = default)
        {
            return Ok();
        }
    }
}
