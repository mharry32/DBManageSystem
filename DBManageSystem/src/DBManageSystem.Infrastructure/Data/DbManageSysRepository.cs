using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Specification.EntityFrameworkCore;
using DBManageSystem.SharedKernel.Interfaces;

namespace DBManageSystem.Infrastructure.Data;
public class DbManageSysRepository<T>: RepositoryBase<T>, IReadRepository<T>, IRepository<T> where T : class, IAggregateRoot
{
  public DbManageSysRepository(DbManageSysDbContext dbContext) : base(dbContext)
  {

  }
}
