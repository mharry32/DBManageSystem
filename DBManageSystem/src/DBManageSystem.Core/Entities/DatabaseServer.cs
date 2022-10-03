using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBManageSystem.Core.Enums;
using DBManageSystem.SharedKernel;


namespace DBManageSystem.Core.Entities;
public class DatabaseServer:EntityBase
{
  public string ConnectUrl { get; set; }

  public string? UserName { get; set; }

  public string? Password { get; set; }

  public DatabaseStatusEnum Status { get; set; }
}
