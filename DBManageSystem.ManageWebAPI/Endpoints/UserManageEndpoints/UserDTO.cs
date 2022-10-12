using DBManageSystem.ManageWebAPI.Endpoints.RoleManageEndpoints;

namespace DBManageSystem.ManageWebAPI.Endpoints.UserManageEndpoints
{
    public class UserDTO
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string? LastLoginIP { get; set; }
        public RoleDTO Role { get; set; }
    }
}
