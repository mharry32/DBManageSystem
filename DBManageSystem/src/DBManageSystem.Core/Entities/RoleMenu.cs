using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBManageSystem.Core.Entities.MenuAggregate;

namespace DBManageSystem.Core.Entities;
public class RoleMenu
{
  private RoleMenu() { }

  public RoleMenu(int roleId, int subMenuId)
  {
    RoleId = roleId;
    SubMenuId = subMenuId;
  }

  public int RoleId { get;private set; }


  public int SubMenuId { get;private set; }
}
