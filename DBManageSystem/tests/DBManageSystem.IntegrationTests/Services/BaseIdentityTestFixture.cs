using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Autofac.Core;
using DBManageSystem.Core.Entities;
using DBManageSystem.Infrastructure.Configs;
using DBManageSystem.Infrastructure.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;

namespace DBManageSystem.IntegrationTests.Services;
public class BaseIdentityTestFixture : IDisposable
{
  private const string ConnectionString = @"Server=localhost;Database=dbsystest;Uid=root;Pwd=1995072132Mh.;charset=UTF8";
  public IServiceProvider _serviceProvider { get; private set; }
  public UserManager<User> userManager { get; private set; }
  public RoleManager<Role> roleManager { get; private set; }

  public SignInManager<User> signInManager { get; private set; }

  public AppIdentityDbContext identityContext { get; private set; }

  public DefaultPassword defaultPassword { get; private set; }

  public JwtSecret jwtSecret { get; private set; }

  public string LoginNameForContext = "User";
  public BaseIdentityTestFixture()
  {
    var serviceProvider = new ServiceCollection();
    serviceProvider.AddLogging(t => t.AddConsole());
    serviceProvider.AddDbContext<AppIdentityDbContext>(options => options.UseMySql(ConnectionString, MySqlServerVersion.LatestSupportedServerVersion));
    serviceProvider.AddIdentity<User, Role>().AddEntityFrameworkStores<AppIdentityDbContext>();

    defaultPassword = new DefaultPassword(UserConstants.DefaultPassword);
    jwtSecret = new JwtSecret(UserConstants.TestJwtSecret);
    var scope = serviceProvider.BuildServiceProvider().CreateScope();
    _serviceProvider = scope.ServiceProvider;
    userManager = _serviceProvider.GetRequiredService<UserManager<User>>();
    roleManager = _serviceProvider.GetRequiredService<RoleManager<Role>>();
    signInManager = _serviceProvider.GetRequiredService<SignInManager<User>>();
    identityContext = _serviceProvider.GetRequiredService<AppIdentityDbContext>();
    
    identityContext.Database.EnsureCreated();


  }

  public void Dispose()
  {
    identityContext.Database.EnsureDeleted();
  }
}
