using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBManageSystem.Core.Enums;
using DBManageSystem.Core.Interfaces;

namespace DBManageSystem.Infrastructure.Services;
public class DbServiceStrategy : IDbServiceStrategy
{
  public IDatabaseServerService Decide(DatabaseTypeEnum databaseType)
  {
    throw new NotImplementedException();
  }
}
