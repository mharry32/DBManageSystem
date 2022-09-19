using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.HttpClientTestExtensions;
using DBManageSystem.Core.Entities;
using DBManageSystem.ManageWebAPI;
using DBManageSystem.ManageWebAPI.Endpoints.AuthEndpoints;
using DBManageSystem.Web;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xunit;

namespace DBManageSystem.FunctionalTests.ApiEndpoints.AuthEndpoints;

[Collection("ValidAuthTests")]
public class ValidAuthTests:IClassFixture<FunctionalTestWebApplicationFactory<ManageWebMarker>>
{
  private readonly HttpClient _client;


  public ValidAuthTests(FunctionalTestWebApplicationFactory<ManageWebMarker> factory)
  {
    _client = factory.CreateClient();
    
  }

  [Fact]
  public async Task PerformValidLogin()
  {
    var tokenHandler = new JwtSecurityTokenHandler();
    SecurityToken securityToken = null;
    LoginRequest loginRequest = new LoginRequest()
    {
      UserName = IdentityTestingConstants.TestLoginUserName,
      Password = IdentityTestingConstants.TestLoginPassword
    };
    var request = new StringContent(JsonConvert.SerializeObject(loginRequest), Encoding.UTF8, "application/json");
    var result = await _client.PostAndDeserializeAsync<LoginResponse>(LoginRequest.Routes, request);
    Assert.NotNull(result);
    try
    {
      tokenHandler.ValidateToken(result.Token, new TokenValidationParameters
      {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(IdentityTestingConstants.TestJwtSecret)),
        ValidateIssuer = false,
        ValidateAudience = false
      }, out securityToken);

     var jwtToken =  tokenHandler.ReadJwtToken(result.Token);
      Assert.NotNull(jwtToken);
      var username = jwtToken.Claims.FirstOrDefault(t => t.Type == "name");
      Assert.NotNull(username);
      Assert.Equal(IdentityTestingConstants.TestLoginUserName, username.Value);
    }
    catch (Exception ex)
    {
      Assert.Fail(ex.Message);
    }
  }

  [Fact]
  public async Task PerformModifyPassword()
  {

  }
}
