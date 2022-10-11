namespace DBManageSystem.ManageWebAPI.Endpoints.DBOperationsEndpoints
{
    public class GetColumnsRequest
    {
        public int ServerId { get; set; }

        public string TableName { get; set; }

        public string DatabaseName { get; set; }

    }
}
