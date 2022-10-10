using DBManageSystem.Core.Enums;

namespace DBManageSystem.ManageWebAPI.Endpoints.DBManageEndpoints
{
    public class CreateDBRequest
    {
        public const string Route = "/dbmanage/db";
        public string Name { get; set; }
        public string ConnectUrl { get; set; }

        public string? UserName { get; set; }

        public string? Password { get; set; }

        public DatabaseServerTypeDTO DatabaseType { get; set; }

        public bool IsMonitored { get; set; }
    }
}
