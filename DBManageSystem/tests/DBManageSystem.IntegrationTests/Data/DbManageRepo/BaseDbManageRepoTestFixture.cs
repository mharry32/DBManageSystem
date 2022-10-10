using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBManageSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DBManageSystem.IntegrationTests.Data.DbManageRepo;
public class BaseDbManageRepoTestFixture:IDisposable
{

  public DbManageSysDbContext _dbContext { get; private set; }

  public BaseDbManageRepoTestFixture()
  {
    var builder = new DbContextOptionsBuilder<DbManageSysDbContext>();
    builder.UseMySql(TestConstants.DbConnectString, MySqlServerVersion.LatestSupportedServerVersion);
    _dbContext = new DbManageSysDbContext(builder.Options);
    _dbContext.Database.EnsureDeleted();
    _dbContext.Database.EnsureCreated();
  } 

  public void Dispose()
  {
    _dbContext.Database.EnsureDeleted();
  }
}
