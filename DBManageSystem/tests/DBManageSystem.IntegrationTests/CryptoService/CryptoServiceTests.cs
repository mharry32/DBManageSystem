using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBManageSystem.Core.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace DBManageSystem.IntegrationTests.CryptoService;
public class CryptoServiceTests:IClassFixture<CryptoServiceTestFixture>
{
  IServiceProvider _serviceProvider { get; }
  public CryptoServiceTests(CryptoServiceTestFixture fixture)
  {
    _serviceProvider = fixture._serviceProvider;
  }

  [Fact]
  public async Task TestEncryptAndDecrypt()
  {
    var cryptoService = _serviceProvider.CreateScope().ServiceProvider.GetRequiredService<IDbPasswordCryptoService>();
    var password = "123456aaaaAA@";
    var encryptResult = await cryptoService.Encrypt(password);
    Assert.True(encryptResult.IsSuccess);

    var decryptResult = await cryptoService.Decrypt(encryptResult.Value);
    Assert.True(decryptResult.IsSuccess);
    Assert.Equal(password, decryptResult.Value);
    
  }
}
