using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.SmartEnum;

namespace DBManageSystem.Core.Enums;
public class DatabaseStatusEnum : SmartEnum<DatabaseStatusEnum>
{
  public static readonly DatabaseStatusEnum UnKnown = new DatabaseStatusEnum(nameof(UnKnown), 0);
  public static readonly DatabaseStatusEnum Online = new DatabaseStatusEnum(nameof(Online), 1);
  public static readonly DatabaseStatusEnum Offline = new DatabaseStatusEnum(nameof(Offline), 2);



  private DatabaseStatusEnum(string name, int value) : base(name, value)
  {
  }
}
