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
using Microsoft.EntityFrameworkCore;

namespace DBManageSystem.Infrastructure.Services.DbManageServices;
public class SQLServerManageService : IDatabaseServerService
{
  private readonly IDbPasswordCryptoService _dbPasswordCryptoService;
  private readonly IRepository<DatabaseServer> _dbServerRepository;
  public SQLServerManageService(IDbPasswordCryptoService dbPasswordCryptoService,IRepository<DatabaseServer> dbServerRepository)
  {
    _dbPasswordCryptoService = dbPasswordCryptoService;
    _dbServerRepository = dbServerRepository;
  }

  public async Task<Result<DatabaseStatusEnum>> CheckStatus(DatabaseServer server)
  {
    try
    {
      var password = _dbPasswordCryptoService.Decrypt(server.Password);
      DbContextOptionsBuilder builder = new DbContextOptionsBuilder();
      DbConnectionStringBuilder connectStringBuilder = new DbConnectionStringBuilder
      {
        { "Data Source", server.ConnectUrl },
        { "User Id", server.UserName },
        { "Password", password }
      };
      builder.UseSqlServer(connectStringBuilder.ConnectionString);
      DbContext db = new DbContext(builder.Options);
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

  public async Task<Result> CreateDatabaseServer(DatabaseServer databaseServer)
  {
    try
    {
      Guard.Against.Null(databaseServer.Name);
      Guard.Against.Null(databaseServer.ConnectUrl);
      Guard.Against.Null(databaseServer.DatabaseType);

      await _dbServerRepository.AddAsync(databaseServer);

      return Result.Success();
    }
    catch(Exception ex)
    {
      return Result.Error(ex.ToString());
    }
  }

  public async Task<Result> DeleteDatabaseServer(int dbServerId)
  {
    try
    {
      var dbServerInDb = await _dbServerRepository.GetByIdAsync(dbServerId);
      Guard.Against.Null(dbServerInDb);
      await _dbServerRepository.DeleteAsync(dbServerInDb);
      return Result.Success();
    }
    catch(ArgumentNullException argex)
    {
      return Result.Error($"Error:Can not find Database Server to delete.dbServerId:{dbServerId}");
    }
    catch(Exception ex)
    {
      return Result.Error(ex.ToString());
    }
  }

  public Task<SqlExecuteResult> ExecuteSql(DatabaseServer server, string databaseName ,string sqlText)
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
