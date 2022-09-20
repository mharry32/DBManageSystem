using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.HttpClientTestExtensions;
using DBManageSystem.Core.Entities;
using DBManageSystem.Core.Interfaces;
using DBManageSystem.FunctionalTests.ApiEndpoints.AuthEndpoints;
using DBManageSystem.Infrastructure.Configs;
using DBManageSystem.Infrastructure.Data;
using DBManageSystem.Infrastructure.Identity;
using DBManageSystem.ManageWebAPI.Endpoints.AuthEndpoints;
using DBManageSystem.Web;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Xunit;

namespace DBManageSystem.FunctionalTests;
public class FunctionalTestWebApplicationFactory<TStartup> :WebApplicationFactory<TStartup> where TStartup : class
{
  private readonly string testAppIdentityDbConnectString = "Server=localhost;Database=dbtest;Uid=root;Pwd=1995072132Mh.;charset=UTF8";
  protected override IHost CreateHost(IHostBuilder builder)
  {
    builder.UseEnvironment("Development"); // will not send real emails
    var host = builder.Build();
    host.Start();
    // Get service provider.
    var serviceProvider = host.Services;

    // Create a scope to obtain a reference to the database
    // context (AppDbContext).
    using (var scope = serviceProvider.CreateScope())
    {
      var scopedServices = scope.ServiceProvider;
      var db = scopedServices.GetRequiredService<AppIdentityDbContext>();

      var logger = scopedServices
          .GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();

      var userManager = scopedServices.GetRequiredService<UserManager<User>>();

      db.Database.EnsureDeleted();
      // Ensure the database is created.
      db.Database.EnsureCreated();

      try
      {
        // Can also skip creating the items
        //if (!db.ToDoItems.Any())
        //{
        // Seed the database with test data.
        User userTestLogin = new User();
        userTestLogin.UserName = IdentityTestingConstants.TestLoginUserName;
        userManager.CreateAsync(userTestLogin, IdentityTestingConstants.TestLoginPassword).Wait();

        User userTestModifyPassword = new User();
        userTestModifyPassword.Id = IdentityTestingConstants.TestModifyPassWordUserId;
        userTestModifyPassword.UserName = IdentityTestingConstants.TestModifyPassword_UserName;
        userManager.CreateAsync(userTestModifyPassword,IdentityTestingConstants.TestModifyPassword_OldPassword).Wait();

        IdentitySeedData.PopulateTestData(db);
        //}
      }
      catch (Exception ex)
      {
        logger.LogError(ex, "An error occurred seeding the " +
                            "database with test messages. Error: {exceptionMessage}", ex.Message);
      }
    }

    return host;
  }


  public async Task<string> GetAuthToken(HttpClient client)
  {
    LoginRequest loginRequest = new LoginRequest()
    {
      UserName = IdentityTestingConstants.TestLoginUserName,
      Password = IdentityTestingConstants.TestLoginPassword
    };
    var request = new StringContent(JsonConvert.SerializeObject(loginRequest), Encoding.UTF8, "application/json");
    var result = await client.PostAndDeserializeAsync<LoginResponse>(LoginRequest.Routes, request);

    return result.Token;


  }



  protected override void ConfigureWebHost(IWebHostBuilder builder)
  {
    builder
        .ConfigureServices(services =>
        {
          // Remove the app's ApplicationDbContext registration.
          var descriptor = services.SingleOrDefault(
          d => d.ServiceType ==
              typeof(DbContextOptions<AppIdentityDbContext>));

          if (descriptor != null)
          {
            services.Remove(descriptor);
          }

          // Add ApplicationDbContext using an in-memory database for testing.
          services.AddDbContext<AppIdentityDbContext>(options =>
          {
            options.UseMySql(testAppIdentityDbConnectString, MySqlServerVersion.LatestSupportedServerVersion);
          });


          var jwtSecretServiceDescriptor = services.SingleOrDefault(
            d => d.ServiceType == typeof(JwtSecret));

          if(jwtSecretServiceDescriptor != null)
          {
            services.Remove(jwtSecretServiceDescriptor);
          }

          services.AddSingleton<JwtSecret>(new JwtSecret(IdentityTestingConstants.TestJwtSecret));

        });
  }
}
