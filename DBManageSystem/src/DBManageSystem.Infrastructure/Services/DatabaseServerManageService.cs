using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Ardalis.Result;
using DBManageSystem.Core.Entities;
using DBManageSystem.Core.Enums;
using DBManageSystem.Core.Interfaces;
using DBManageSystem.SharedKernel.Interfaces;

namespace DBManageSystem.Infrastructure.Services;
public class DatabaseServerManageService : IDatabaseServerService
{

  private readonly IDbPasswordCryptoService _dbPasswordCryptoService;
  private readonly IRepository<DatabaseServer> _dbServerRepository;
  private readonly IAppLogger<UserService> _logger;
  private readonly IRepository<SqlLog> _sqlLogRepository;

  public DatabaseServerManageService(IDbPasswordCryptoService dbPasswordCryptoService,
    IRepository<DatabaseServer> dbServerRepository, 
    IRepository<SqlLog> sqlLogRepository,
    IAppLogger<UserService> logger)
  {
    _dbPasswordCryptoService = dbPasswordCryptoService;
    _dbServerRepository = dbServerRepository;
    _logger = logger;
    _sqlLogRepository = sqlLogRepository;
  }
  public async Task<Result> CreateDatabaseServer(DatabaseServer databaseServer)
  {
    try
    {
      Guard.Against.NullOrEmpty(databaseServer.Name);
      Guard.Against.NullOrEmpty(databaseServer.ConnectUrl);
      Guard.Against.Null(databaseServer.DatabaseType);
      if (string.IsNullOrEmpty(databaseServer.Password)!=true)
      {
        var passwordEncryptResult = await _dbPasswordCryptoService.Encrypt(databaseServer.Password);
        if(passwordEncryptResult.IsSuccess == false)
        {
          _logger.LogInformation(passwordEncryptResult.Errors.FirstOrDefault());
          return Result.Error("Database Password Can not be encrypted.");
        }
        else
        {
          databaseServer.Password = passwordEncryptResult.Value;
        }
      }

      await _dbServerRepository.AddAsync(databaseServer);

      return Result.Success();
    }
    catch (Exception ex)
    {
      _logger.LogInformation(ex.ToString());
      return Result.Error(ex.Message);
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
      _logger.LogInformation(ex.ToString());
      return Result.Error(ex.Message);
    }
  }

  public async Task<Result<DatabaseServer>> GetDatabaseServerWithPasswordDecrptedById(int dbServerId)
  {
    try
    {
      var dbServerInDb = await _dbServerRepository.GetByIdAsync(dbServerId);
      Guard.Against.Null(dbServerInDb);
      if (string.IsNullOrEmpty(dbServerInDb.Password) != true)
      {
        var passwordDecryptResult = await _dbPasswordCryptoService.Decrypt(dbServerInDb.Password);
        if (passwordDecryptResult.IsSuccess == false)
        {
          _logger.LogInformation(passwordDecryptResult.Errors.FirstOrDefault());
          return Result.Error("Database Password Can not be encrypted.");
        }
        else
        {
          dbServerInDb.Password = passwordDecryptResult.Value;
        }
      }
      return Result.Success(dbServerInDb);
    }
    catch (ArgumentNullException argex)
    {
      return Result.Error($"Error:Can not find Database Server.dbServerId:{dbServerId}");
    }
    catch (Exception ex)
    {
      _logger.LogInformation(ex.ToString());
      return Result.Error(ex.Message);
    }
  }

  public Result<IReadOnlyCollection<DatabaseTypeEnum>> GetDatabaseTypes()
  {
    var dbTypes = DatabaseTypeEnum.List;
    return Result.Success(dbTypes);
  }

  public async Task<Result<List<DatabaseServer>>> GetServerList()
  {
    try
    {
      var databaseServerList = await _dbServerRepository.ListAsync();
      return Result.Success(databaseServerList);
    }
    catch (Exception ex)
    {
      _logger.LogInformation(ex.ToString());
      return Result.Error(ex.Message);
    }
  }

  public async Task LogExecutedSql(DatabaseServer server, string sql)
  {
    try
    {
      SqlLog sqlLog = new SqlLog(server.ConnectUrl,sql);
      await _sqlLogRepository.AddAsync(sqlLog);
     
    }
    catch (Exception ex)
    {
      _logger.LogInformation(ex.ToString());
    }
  }

  public async Task<Result> UpdateDatabaseServer(DatabaseServer databaseServer)
  {
    try
    {
      Guard.Against.Null(databaseServer.ConnectUrl);
      Guard.Against.Null(databaseServer.DatabaseType);
      Guard.Against.Null(databaseServer.Name);
      var dbServerInDb = await _dbServerRepository.GetByIdAsync(databaseServer.Id);
      Guard.Against.Null(dbServerInDb);
      if (string.IsNullOrEmpty(databaseServer.Password) != true)
      {
        var passwordEncryptResult = await _dbPasswordCryptoService.Encrypt(databaseServer.Password);
        if (passwordEncryptResult.IsSuccess == false)
        {
          _logger.LogInformation(passwordEncryptResult.Errors.FirstOrDefault());
          return Result.Error("Database Password Can not be encrypted.");
        }
        else
        {
          dbServerInDb.Password = passwordEncryptResult.Value;
        }
      }
      dbServerInDb.ConnectUrl = databaseServer.ConnectUrl;
      dbServerInDb.DatabaseType = databaseServer.DatabaseType;
      dbServerInDb.IsMonitored = databaseServer.IsMonitored;
      dbServerInDb.Name = databaseServer.Name;
      dbServerInDb.UserName = databaseServer.UserName;
      await _dbServerRepository.UpdateAsync(dbServerInDb);
      return Result.Success();
    }
    catch (ArgumentNullException argex)
    {
      return Result.Error($"Error:Can not find Database Server to update.dbServerId:{databaseServer.Id}");
    }
    catch (Exception ex)
    {
      _logger.LogInformation(ex.ToString());
      return Result.Error(ex.Message);
    }
  }
}
