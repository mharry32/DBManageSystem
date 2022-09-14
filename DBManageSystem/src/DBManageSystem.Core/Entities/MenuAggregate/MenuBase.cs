using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBManageSystem.SharedKernel;
using DBManageSystem.SharedKernel.Interfaces;

namespace DBManageSystem.Core.Entities.MenuAggregate;
public class MenuBase : EntityBase,IAggregateRoot
{
  public string Name { get; set; }

  public int Order { get; set; }

}
