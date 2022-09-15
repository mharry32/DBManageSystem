using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBManageSystem.Core.Entities;
using DBManageSystem.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DBManageSystem.IntegrationTests.Services.UserServiceTests;
public class BaseIdentityTestFixture:IDisposable
{
  private const string ConnectionString = @"Server=localhost;Database=dbsystest;Uid=root;Pwd=1995072132Mh.;charset=UTF8";
  public IServiceProvider _serviceProvider { get; private set; }
  public UserManager<User> userManager { get; private set; }
  public RoleManager<Role> roleManager { get; private set; }

  public SignInManager<User> signInManager { get; private set; }

  public AppIdentityDbContext identityContext { get; private set; }
  public BaseIdentityTestFixture()
  {
    var serviceProvider = new ServiceCollection();
    serviceProvider.AddLogging(t => t.AddConsole());
    serviceProvider.AddDbContext<AppIdentityDbContext>(options=>options.UseMySql(ConnectionString, MySqlServerVersion.LatestSupportedServerVersion));
    serviceProvider.AddIdentity<User, Role>().AddEntityFrameworkStores<AppIdentityDbContext>();


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
