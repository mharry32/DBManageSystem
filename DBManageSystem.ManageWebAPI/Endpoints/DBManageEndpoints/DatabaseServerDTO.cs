using DBManageSystem.Core.Enums;

namespace DBManageSystem.ManageWebAPI.Endpoints.DBManageEndpoints
{
    public class DatabaseServerDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ConnectUrl { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string LastUpdatedTime { get; set; }

        public int Status { get;  set; }

        public DatabaseServerTypeDTO DatabaseType { get; set; }

        public bool IsMonitored { get; set; }
    }
}
