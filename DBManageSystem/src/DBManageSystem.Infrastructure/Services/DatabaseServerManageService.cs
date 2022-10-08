using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Ardalis.Result;
using DBManageSystem.Core.Entities;
using DBManageSystem.Core.Interfaces;
using DBManageSystem.SharedKernel.Interfaces;

namespace DBManageSystem.Infrastructure.Services;
public class DatabaseServerManageService : IDatabaseServerService
{

  private readonly IDbPasswordCryptoService _dbPasswordCryptoService;
  private readonly IRepository<DatabaseServer> _dbServerRepository;
  public DatabaseServerManageService(IDbPasswordCryptoService dbPasswordCryptoService, IRepository<DatabaseServer> dbServerRepository)
  {
    _dbPasswordCryptoService = dbPasswordCryptoService;
    _dbServerRepository = dbServerRepository;
  }
  public async Task<Result> CreateDatabaseServer(DatabaseServer databaseServer)
  {
    try
    {
      Guard.Against.Null(databaseServer.Name);
      Guard.Against.Null(databaseServer.ConnectUrl);
      Guard.Against.Null(databaseServer.DatabaseType);
      //TODO:创建时应该判断密码是否为空，不为空则加密
      await _dbServerRepository.AddAsync(databaseServer);

      return Result.Success();
    }
    catch (Exception ex)
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
    catch (ArgumentNullException argex)
    {
      return Result.Error($"Error:Can not find Database Server to delete.dbServerId:{dbServerId}");
    }
    catch (Exception ex)
    {
      return Result.Error(ex.ToString());
    }
  }

  public Task<Result<DatabaseServer>> GetDatabaseServerWithPasswordDecrptedById(int dbServerId)
  {
    throw new NotImplementedException();
  }

  public Task<Result<List<DatabaseServer>>> GetServerList()
  {
    throw new NotImplementedException();
  }

  public Task<Result> UpdateDatabaseServer(DatabaseServer databaseServer)
  {
    throw new NotImplementedException();
  }
}
