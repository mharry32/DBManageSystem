using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Result;

namespace DBManageSystem.Core.Interfaces;
public interface IDbPasswordCryptoService
{
  Task<Result<string>> Encrypt(string dbPassword);
  Task<Result<string>> Decrypt(string encryptedPassword);
}
