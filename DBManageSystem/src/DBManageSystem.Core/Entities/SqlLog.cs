using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBManageSystem.SharedKernel;

namespace DBManageSystem.Core.Entities;
public class SqlLog:EntityBase
{
  public string ConnectUrl { get; set; }

  public DateTime ExecuteTime { get; set; }

  public string SqlText { get; set; }
}
