namespace DBManageSystem.ManageWebAPI.Endpoints.RoleManageEndpoints
{
    public class ModifyRoleForUserRequest
    {
        public const string Route = "/users/modifyrole";

        public int UserId { get; set; }

        public int RoleId { get; set; }
    }
}
