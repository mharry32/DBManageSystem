using System.ComponentModel.DataAnnotations;

namespace DBManageSystem.ManageWebAPI.Endpoints.AuthEndpoints
{
    public class LoginRequest
    {
        public const string Routes = "/admin/login";

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
