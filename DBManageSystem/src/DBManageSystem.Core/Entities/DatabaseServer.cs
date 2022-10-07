using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBManageSystem.Core.Enums;
using DBManageSystem.SharedKernel;
using DBManageSystem.SharedKernel.Interfaces;

namespace DBManageSystem.Core.Entities;
public class DatabaseServer:EntityBase,IAggregateRoot
{
  public DatabaseServer()
  {

  }
  public string Name { get; set; }
  public string ConnectUrl { get; set; }

  public string? UserName { get; set; }

  public string? Password { get; set; }

  public DateTime? LastUpdatedTime { get;private set; }

  public DatabaseStatusEnum Status { get;private set; }

  public DatabaseTypeEnum DatabaseType { get; set; }

  public bool IsMonitored { get; set; }

  public void UpdateStatus(DatabaseStatusEnum status)
  {
    this.Status = status;
    this.LastUpdatedTime = DateTime.Now;
  }

}
