using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBManageSystem.Core.Enums;
using DBManageSystem.Core.Interfaces;
using DBManageSystem.Infrastructure.Services.DbManageServices;

namespace DBManageSystem.Infrastructure.Services;
public class DbServiceStrategy : IDbServiceStrategy
{
  public IDatabaseOperationsService Decide(DatabaseTypeEnum databaseType)
  {
    if(databaseType==DatabaseTypeEnum.MySQL)
    {
      return new MySQLOperationService();
    }
    else if(databaseType == DatabaseTypeEnum.SQLServer)
    {
      return new SQLServerOperationService();
    }
    else
    {
      throw new NotSupportedException();
    }
  }
}
