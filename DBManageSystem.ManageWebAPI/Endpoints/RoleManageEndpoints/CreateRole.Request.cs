namespace DBManageSystem.ManageWebAPI.Endpoints.RoleManageEndpoints
{
    public class CreateRoleRequest
    {
        public const string Route = "/role";
        public string RoleName { get; set; }
    }
}
