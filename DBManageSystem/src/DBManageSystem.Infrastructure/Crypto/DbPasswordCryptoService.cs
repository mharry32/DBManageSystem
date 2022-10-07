using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Result;
using DBManageSystem.Core.Interfaces;
using DBManageSystem.Infrastructure.Data.Migrations.DbManageSysDbContext;
using Microsoft.AspNetCore.DataProtection;

namespace DBManageSystem.Infrastructure.Crypto;
public class DbPasswordCryptoService : IDbPasswordCryptoService
{
  IDataProtector _protector;
  public DbPasswordCryptoService(IDataProtectionProvider provider)
  {
    _protector = provider.CreateProtector("DbPasswordCrypto");
  }
  public Task<Result<string>> Decrypt(string encryptedPassword)
  {
    return Task.Run(() =>
    {
      try
      {
        string unProtectedPayload = _protector.Unprotect(encryptedPassword);
        return new Result<string>(unProtectedPayload);
      }
      catch (Exception ex)
      {
        return Result<string>.Error(ex.Message);
      }
    });

  }

  public Task<Result<string>> Encrypt(string dbPassword)
  {
    return Task.Run(() =>
    {
      try
      {
        string protectedPayload = _protector.Protect(dbPassword);
        return new Result<string>(protectedPayload);
      }
      catch (Exception ex)
      {
        return Result<string>.Error(ex.Message);
      }
    });
  }
}
