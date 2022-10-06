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
    throw new NotImplementedException();
  }

  public async Task<Result<string>> Encrypt(string dbPassword)
  {
    string protectedPayload = _protector.Protect(dbPassword);
  }
}
