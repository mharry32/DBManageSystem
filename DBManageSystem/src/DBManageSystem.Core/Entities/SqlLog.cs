using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBManageSystem.SharedKernel;
using DBManageSystem.SharedKernel.Interfaces;

namespace DBManageSystem.Core.Entities;
public class SqlLog:EntityBase,IAggregateRoot
{

  private SqlLog()
  {

  }

  public SqlLog(string connectUrl,string sqlText)
  {
    ConnectUrl = connectUrl;
    SqlText = sqlText;
    ExecuteTime = DateTime.Now;
  }
  public string ConnectUrl { get;private set; }

  public DateTime ExecuteTime { get;private set; }

  public string SqlText { get;private set; }
}
