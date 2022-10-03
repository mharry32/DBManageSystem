using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBManageSystem.Core.Enums;

namespace DBManageSystem.Core.Entities;
public class RelationalDatabaseServer:DatabaseServer
{
  
  public RelationalDatabaseTypeEnum DatabaseType { get; set; }


}
