namespace DBManageSystem.ManageWebAPI.Endpoints.AuthEndpoints
{
    public class ModifyPasswordRequest
    {
        public const string Route = "/users/password";
        public int UserId { get; set; }

        public string OldPassword { get; set; }

        public string NewPassword { get; set; }
    }
}
