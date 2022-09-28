namespace DBManageSystem.ManageWebAPI.Endpoints.MenuEndpoints
{
    public class ModifyMenusRequest
    {
        public const string Route = "/users/modifypermission";
        public int RoleId { get; set; }

        public List<int> MenuIds { get; set; }
    }
}
