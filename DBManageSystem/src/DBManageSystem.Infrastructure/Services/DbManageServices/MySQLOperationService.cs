using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Result;
using DBManageSystem.Core.Entities;
using DBManageSystem.Core.Entities.SchemaAggregate;
using DBManageSystem.Core.Enums;
using DBManageSystem.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DBManageSystem.Infrastructure.Services.DbManageServices;
public class MySQLOperationService : BaseDbServerOpsService
{
  public override DbContext GetDbContext(DatabaseServer server, string databaseName = null)
  {
    DbContextOptionsBuilder builder = new DbContextOptionsBuilder();
    DbConnectionStringBuilder connectStringbuilder = new DbConnectionStringBuilder()
    {
      { "Server", server.ConnectUrl },
      { "charset", "UTF8" }
    };
    if (server.UserName != null)
    {
      connectStringbuilder.Add("Uid", server.UserName);
    }

    if (server.Password != null)
    {
      connectStringbuilder.Add("Pwd", server.Password);
    }
    if (databaseName != null)
    {
      connectStringbuilder.Add("Database", databaseName);
    }
    builder.UseMySql(connectStringbuilder.ConnectionString, MySqlServerVersion.LatestSupportedServerVersion);
    DbContext db = new DbContext(builder.Options);
    return db;
  }

  public override DatabaseSchema ParseDatabaseSchema(string key, string value)
  {
    if (key == "SCHEMA_NAME")
    {
      return new DatabaseSchema() { Name = value };
    }
    else
    {
      return null;
    }
  }

  public override TableSchema ParseTableSchema(DataRow data, string databaseName)
  {
    var lowerdbName = databaseName.ToLower();
    var upperdbName = databaseName.ToUpper();
    if (data["TABLE_SCHEMA"].ToString() == lowerdbName|| data["TABLE_SCHEMA"].ToString() == upperdbName)
    {
      return new TableSchema()
      {
        Name = data["TABLE_NAME"]?.ToString()
      };
    }
    else
    {
      return null;
    }
  }

  public override ColumnSchema ParseColumnSchema(DataRow row, string tableName, string databaseName)
  {
    var lowerdbName = databaseName.ToLower();
    var upperdbName = databaseName.ToUpper();

    var lowerTableName = tableName.ToLower();
    var upperTableName = tableName.ToUpper();
    if (row["TABLE_SCHEMA"].ToString() == lowerdbName && row["TABLE_NAME"].ToString() == lowerTableName)
    {
      return new ColumnSchema()
      {
        Name = row["COLUMN_NAME"]?.ToString(),
        DataType = row["DATA_TYPE"]?.ToString(),
        IsNullable = row["IS_NULLABLE"]?.ToString() == "YES" ? true : false
      };
    }

    if (row["TABLE_SCHEMA"].ToString() == upperdbName && row["TABLE_NAME"].ToString() == upperTableName)
    {
      return new ColumnSchema()
      {
        Name = row["COLUMN_NAME"]?.ToString(),
        DataType = row["DATA_TYPE"]?.ToString(),
        IsNullable = row["IS_NULLABLE"]?.ToString() == "YES" ? true : false
      };
    }

    return null;
  }
}
