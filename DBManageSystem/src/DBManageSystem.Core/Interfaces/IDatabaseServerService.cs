using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Result;
using DBManageSystem.Core.Entities;
using DBManageSystem.Core.Entities.SchemaAggregate;
using DBManageSystem.Core.Enums;
using DBManageSystem.SharedKernel.Interfaces;

namespace DBManageSystem.Core.Interfaces;
public interface IDatabaseServerService
{


  Task<Result<List<DatabaseServer>>> GetServerList();

  Task<Result> CreateDatabaseServer(DatabaseServer databaseServer);

  Task<Result> UpdateDatabaseServer(DatabaseServer databaseServer);

  Task<Result> DeleteDatabaseServer(int dbServerId);

  Task<Result<DatabaseServer>> GetDatabaseServerWithPasswordDecrptedById(int dbServerId);

 Result<IReadOnlyCollection<DatabaseTypeEnum>> GetDatabaseTypes();

  Task LogExecutedSql(DatabaseServer server, string sql);

}
