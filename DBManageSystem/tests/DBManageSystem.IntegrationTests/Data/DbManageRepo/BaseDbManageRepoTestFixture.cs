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
  private const string ConnectionString = @"Server=localhost;Database=dbsystest;Uid=root;Pwd=1995072132Mh.;charset=UTF8";

  public DbManageSysDbContext _dbContext { get; private set; }

  public BaseDbManageRepoTestFixture()
  {
    var builder = new DbContextOptionsBuilder<DbManageSysDbContext>();
    builder.UseMySql(ConnectionString, MySqlServerVersion.LatestSupportedServerVersion);
    _dbContext = new DbManageSysDbContext(builder.Options);
    _dbContext.Database.EnsureDeleted();
    _dbContext.Database.EnsureCreated();
  } 

  public void Dispose()
  {
    _dbContext.Database.EnsureDeleted();
  }
}
