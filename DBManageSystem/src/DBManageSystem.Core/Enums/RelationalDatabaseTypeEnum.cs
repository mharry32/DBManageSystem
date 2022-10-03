using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.SmartEnum;

namespace DBManageSystem.Core.Enums;
public class RelationalDatabaseTypeEnum : SmartEnum<RelationalDatabaseTypeEnum>
{
  public static readonly RelationalDatabaseTypeEnum SQLServer = new RelationalDatabaseTypeEnum(nameof(SQLServer), 0);
  public static readonly RelationalDatabaseTypeEnum MySQL = new RelationalDatabaseTypeEnum(nameof(MySQL), 1);

  private RelationalDatabaseTypeEnum(string name, int value) : base(name, value)
  {
  }
}
