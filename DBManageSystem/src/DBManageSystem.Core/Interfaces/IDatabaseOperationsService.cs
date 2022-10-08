using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Result;
using DBManageSystem.Core.Entities;
using DBManageSystem.Core.Entities.SchemaAggregate;
using DBManageSystem.Core.Enums;

namespace DBManageSystem.Core.Interfaces;
public interface IDatabaseOperationsService
{
  /// <summary>
  /// Get the Newest Status of DatabaseServer
  /// </summary>
  /// <param name="server">The Server needs to check status</param>
  /// <returns>The Status</returns>
  Task<Result<DatabaseStatusEnum>> CheckStatus(DatabaseServer server);

  Task<Result<List<DatabaseSchema>>> GetDatabaseSchemas(int dbServerId);

  Task<Result<List<TableSchema>>> GetTableSchemas(int dbServerId, string databaseName);

  Task<Result<List<ColumnSchema>>> GetColumnSchemas(int dbServerId, string TableName);

  Task<SqlExecuteResult> ExecuteSql(DatabaseServer server, string databaseName, string sqlText);
}
