namespace DBManageSystem.ManageWebAPI.Endpoints.DBOperationsEndpoints
{
    public class ExecuteSqlRequest
    {
        public int ServerId { get; set; }
        public string DatabaseName { get; set; }

        public string SqlText { get; set; }
    }
}
