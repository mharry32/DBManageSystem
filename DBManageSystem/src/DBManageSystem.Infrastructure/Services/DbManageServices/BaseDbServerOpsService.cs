using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Ardalis.Result;
using DBManageSystem.Core.Entities;
using DBManageSystem.Core.Entities.SchemaAggregate;
using DBManageSystem.Core.Enums;
using DBManageSystem.Core.Interfaces;
using DBManageSystem.SharedKernel.Interfaces;
using Humanizer;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Nodes;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace DBManageSystem.Infrastructure.Services.DbManageServices;
public class BaseDbServerOpsService : IDatabaseOperationsService
{

  public BaseDbServerOpsService()
  {

  }

  public virtual DbContext GetDbContext(DatabaseServer server,string databaseName = null)
  {
     throw new NotImplementedException(); 
  }

  public async Task<Result<DatabaseStatusEnum>> CheckStatus(DatabaseServer server)
  {
    try
    {

      DbContext db = GetDbContext(server);
      var result = await db.Database.CanConnectAsync();
      if(result == true)
      {
        return new Result<DatabaseStatusEnum>(DatabaseStatusEnum.Online);
      }
      else
      {
        return new Result<DatabaseStatusEnum>(DatabaseStatusEnum.Offline);
      }
    }
    catch(Exception ex)
    {
      return Result<DatabaseStatusEnum>.Error(ex.Message);
    }
  }

  private object ConvertData(DbDataReader reader,string columnName)
  {
    var ordinal = reader.GetOrdinal(columnName);
    var isNull = reader.IsDBNull(ordinal);
    if (isNull)
    {
      return "";
    }
    else
    {
      var fieldType = reader.GetFieldType(ordinal);
      if(fieldType == typeof(DateTime))
      {
        var dateData = reader.GetDateTime(ordinal);
        return dateData.ToString("yyyy-MM-dd HH:mm:ss.ffff");
      }
      else
      {
        return reader[ordinal];
      }
    }
  }



  public async Task<SqlExecuteResult> ExecuteSql(DatabaseServer server, string databaseName ,string sqlText)
  {
    
    try
    {
      DbContext db = GetDbContext(server, databaseName);
      DbConnection dbConnection = db.Database.GetDbConnection();
      dbConnection.Open();

      var sqlcmd = dbConnection.CreateCommand();
      sqlcmd.CommandText = sqlText;
      var reader = await sqlcmd.ExecuteReaderAsync();
      var columns = reader.GetColumnSchema();
      var rowsAffected = reader.RecordsAffected;
      var hasData = reader.HasRows;

      if(hasData == true)
      {
        JsonArray jsonArray = new JsonArray();
        while (reader.Read())
        {
          JsonObject jsonobj = new JsonObject();
          foreach (var column in columns)
          {
            var result = ConvertData(reader, column.ColumnName);
            jsonobj.Add(column.ColumnName, JsonValue.Create(result));
          }
         jsonArray.Add(jsonobj);
        }
        
        return SqlExecuteResult.SuccessWithDatas(jsonArray.ToJsonString());
      }
      else
      {
        return SqlExecuteResult.SuccessWithoutData(rowsAffected);
      }
    }
    catch (Exception ex)
    {

      return SqlExecuteResult.ExecuteError(ex.Message);
    }
  }

  public Task<Result<List<ColumnSchema>>> GetColumnSchemas(int dbServerId, string TableName)
  {
    throw new NotImplementedException();
  }

  public Task<Result<List<DatabaseSchema>>> GetDatabaseSchemas(int dbServerId)
  {
    throw new NotImplementedException();
  }


  public Task<Result<List<TableSchema>>> GetTableSchemas(int dbServerId, string databaseName)
  {
    throw new NotImplementedException();
  }

}
