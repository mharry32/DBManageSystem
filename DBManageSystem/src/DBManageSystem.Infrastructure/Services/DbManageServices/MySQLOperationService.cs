using System;
using System.Collections.Generic;
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
      { "Uid", server.UserName },
      { "Pwd", server.Password },
      { "charset", "UTF8" }
    };
    if(databaseName != null)
    {
      connectStringbuilder.Add("Database", databaseName);
    }
    builder.UseMySql(connectStringbuilder.ConnectionString, MySqlServerVersion.LatestSupportedServerVersion);
    DbContext db = new DbContext(builder.Options);
    return db;
  }
}
