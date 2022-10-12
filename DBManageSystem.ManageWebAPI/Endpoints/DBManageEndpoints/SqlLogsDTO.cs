using Ardalis.Result;
using DBManageSystem.Core.Entities;

namespace DBManageSystem.ManageWebAPI.Endpoints.DBManageEndpoints
{
    public class SqlLogsDTO
    {
        public PagedInfo PagedInfo { get; set; }

        public List<SqlLogDTO> SqlLogs { get; set; }
    }
}
