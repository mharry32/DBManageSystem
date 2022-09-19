using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBManageSystem.Infrastructure.Configs;
public class BaseConfig
{

  public string Value { get;private set; }

  public BaseConfig(string value)
  {
    Value = value;
  }
}
