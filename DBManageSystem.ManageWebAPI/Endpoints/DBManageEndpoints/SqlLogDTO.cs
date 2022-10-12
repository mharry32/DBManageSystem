namespace DBManageSystem.ManageWebAPI.Endpoints.DBManageEndpoints
{
    public class SqlLogDTO
    {
        public string ConnectUrl { get;  set; }

        public string ExecuteTime { get;  set; }

        public string SqlText { get; set; }
    }
}
