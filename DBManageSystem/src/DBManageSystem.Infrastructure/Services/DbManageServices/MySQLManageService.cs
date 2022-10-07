using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Result;
using DBManageSystem.Core.Entities;
using DBManageSystem.Core.Entities.SchemaAggregate;
using DBManageSystem.Core.Enums;
using DBManageSystem.Core.Interfaces;

namespace DBManageSystem.Infrastructure.Services.DbManageServices;
public class MySQLManageService : IDatabaseServerService
{
  public Task<Result<DatabaseStatusEnum>> CheckStatus(DatabaseServer server)
  {
    throw new NotImplementedException();
  }

  public Task<Result> CreateDatabaseServer(DatabaseServer databaseServer)
  {
    throw new NotImplementedException();
  }

  public Task<Result> DeleteDatabaseServer(int dbServerId)
  {
    throw new NotImplementedException();
  }

  public Task<SqlExecuteResult> ExecuteSql(DatabaseServer server, string databaseName, string sqlText)
  {
    throw new NotImplementedException();
  }

  public Task<Result<List<ColumnSchema>>> GetColumnSchemas(int dbServerId, string TableName)
  {
    throw new NotImplementedException();
  }

  public Task<Result<List<DatabaseSchema>>> GetDatabaseSchemas(int dbServerId)
  {
    throw new NotImplementedException();
  }

  public Task<Result<List<DatabaseServer>>> GetServerList()
  {
    throw new NotImplementedException();
  }

  public Task<Result<List<TableSchema>>> GetTableSchemas(int dbServerId, string databaseName)
  {
    throw new NotImplementedException();
  }

  public Task<Result> UpdateDatabaseServer(DatabaseServer databaseServer)
  {
    throw new NotImplementedException();
  }
}
