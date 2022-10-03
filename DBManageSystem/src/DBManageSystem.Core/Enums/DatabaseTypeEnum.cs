using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.SmartEnum;

namespace DBManageSystem.Core.Enums;
public class DatabaseTypeEnum : SmartEnum<DatabaseTypeEnum>
{
  public static readonly DatabaseTypeEnum SQLServer = new DatabaseTypeEnum(nameof(SQLServer), 0);
  public static readonly DatabaseTypeEnum MySQL = new DatabaseTypeEnum(nameof(MySQL), 1);

  private DatabaseTypeEnum(string name, int value) : base(name, value)
  {
  }
}
