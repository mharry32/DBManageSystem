using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBManageSystem.Core.Entities;
using DBManageSystem.Core.Entities.SchemaAggregate;
using Microsoft.EntityFrameworkCore;

namespace DBManageSystem.Infrastructure.Services.DbManageServices;
public class SQLServerOperationService:BaseDbServerOpsService
{
  public override DbContext GetDbContext(DatabaseServer server, string databaseName = null)
  {
    DbContextOptionsBuilder builder = new DbContextOptionsBuilder();
    DbConnectionStringBuilder connectStringbuilder = new DbConnectionStringBuilder()
    {
      { "Data Source", server.ConnectUrl }
    };

    if(server.UserName != null)
    {
      connectStringbuilder.Add("User Id", server.UserName);
    }

    if(server.Password != null)
    {
      connectStringbuilder.Add("Password", server.Password);
    }

    if (databaseName != null)
    {
      connectStringbuilder.Add("Initial Catalog", databaseName);
    }
    builder.UseSqlServer(connectStringbuilder.ConnectionString);
    DbContext db = new DbContext(builder.Options);
    return db;
  }

  public override DatabaseSchema ParseDatabaseSchema(string key, string value)
  {
    if (key == "database_name")
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
    if (data["TABLE_CATALOG"].ToString() == databaseName)
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
    if (row["TABLE_CATALOG"].ToString() == databaseName && row["TABLE_NAME"].ToString() == tableName)
    {
      return new ColumnSchema()
      {
        Name = row["COLUMN_NAME"]?.ToString(),
        DataType = row["DATA_TYPE"]?.ToString(),
        IsNullable = row["IS_NULLABLE"]?.ToString() == "YES"?true:false
      };
    }
    else
    {
      return null;
    }
  }
}
