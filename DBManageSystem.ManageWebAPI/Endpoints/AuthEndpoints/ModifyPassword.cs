using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;

namespace DBManageSystem.ManageWebAPI.Endpoints.AuthEndpoints
{
    public class ModifyPassword : EndpointBaseAsync.WithRequest<ModifyPasswordRequest>.WithActionResult
    {
        public override Task<ActionResult> HandleAsync(ModifyPasswordRequest request, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
