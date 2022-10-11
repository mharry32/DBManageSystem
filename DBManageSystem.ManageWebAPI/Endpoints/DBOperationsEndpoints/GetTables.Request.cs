namespace DBManageSystem.ManageWebAPI.Endpoints.DBOperationsEndpoints
{
    public class GetTablesRequest
    {
        public int ServerId { get; set; }
        public string DatabaseName { get; set; }
    }
}
