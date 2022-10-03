using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBManageSystem.Core.Entities.SchemaAggregate;
public class ColumnSchema
{
  public string Name { get; set; }

  public string DataType { get; set; }

  public bool IsNullable { get; set; }
}
