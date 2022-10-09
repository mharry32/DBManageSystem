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
using System.Data;
using System.Net.WebSockets;

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

  public virtual DatabaseSchema ParseDatabaseSchema(string key,string value)
  {
    throw new NotImplementedException();
  }

  public async Task<Result<DatabaseStatusEnum>> CheckStatus(DatabaseServer server)
  {
    try
    {

      DbContext db = GetDbContext(server);
      await db.Database.OpenConnectionAsync();
      await db.Database.CloseConnectionAsync();
      return new Result<DatabaseStatusEnum>(DatabaseStatusEnum.Online);

    }
    catch(Exception ex)
    {
      return new Result<DatabaseStatusEnum>(DatabaseStatusEnum.Offline);
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

        JsonArray jsonHeader = new JsonArray();
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
        
        foreach(var column in columns)
        {
          jsonHeader.Add(JsonValue.Create<string>(column.ColumnName));
        }
        return SqlExecuteResult.SuccessWithDatas(jsonArray.ToJsonString(), jsonHeader.ToJsonString());
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

  public async Task<Result<List<ColumnSchema>>> GetColumnSchemas(DatabaseServer server, string TableName, string databaseName)
  {
    try
    {
      DbContext db = GetDbContext(server, databaseName);
      DbConnection dbConnection = db.Database.GetDbConnection();
      dbConnection.Open();
      var columnsDataTable = await dbConnection.GetSchemaAsync("Columns");
      var schemas = new List<ColumnSchema>();
      foreach (DataRow row in columnsDataTable.Rows)
      {
        var column = ParseColumnSchema(row, TableName ,databaseName);
        if (column != null)
        {
          schemas.Add(column);
        }
      }

      return Result.Success<List<ColumnSchema>>(schemas);
    }
    catch (Exception ex)
    {
      return Result.Error(ex.Message);
    }
  }

  public virtual ColumnSchema ParseColumnSchema(DataRow row, string tableName, string databaseName)
  {
    throw new NotImplementedException();
  }

  public async Task<Result<List<DatabaseSchema>>> GetDatabaseSchemas(DatabaseServer server)
  {
    try
    {
      DbContext db = GetDbContext(server);
      DbConnection dbConnection = db.Database.GetDbConnection();
      dbConnection.Open();
      var databaseTable = await dbConnection.GetSchemaAsync("Databases");

      var schemas = new List<DatabaseSchema>();
      foreach(DataRow row in databaseTable.Rows) 
      {
        foreach(DataColumn column in databaseTable.Columns)
        {
          var schema = ParseDatabaseSchema(column.ColumnName, row[column]?.ToString());
          if(schema != null)
          {
            schemas.Add(schema);
          }
        }
      }

      return Result.Success<List<DatabaseSchema>>(schemas);
    }
    catch (Exception ex)
    {
      return Result.Error(ex.Message);
    }
  }

  public virtual TableSchema ParseTableSchema(DataRow data,string databaseName)
  {
    throw new NotImplementedException();
  }

  public async Task<Result<List<TableSchema>>> GetTableSchemas(DatabaseServer server, string databaseName)
  {
    try
    {
      DbContext db = GetDbContext(server,databaseName);
      DbConnection dbConnection = db.Database.GetDbConnection();
      dbConnection.Open();
      var tablesDataTable = await dbConnection.GetSchemaAsync("Tables");
      var schemas = new List<TableSchema>();
      foreach (DataRow row in tablesDataTable.Rows)
      {
        var table = ParseTableSchema(row, databaseName);
        if(table != null)
        {
          schemas.Add(table);
        }
      }

      return Result.Success<List<TableSchema>>(schemas);
    }
    catch (Exception ex)
    {
      return Result.Error(ex.Message);
    }
  }

}
