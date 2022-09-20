namespace DBManageSystem.ManageWebAPI.Endpoints.UserManageEndpoints
{
    public class CreateUserRequest
    {
        public const string Route = "/users";

        public string UserName { get; set; }
    }
}
