using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBManageSystem.Infrastructure.Configs;
public class DefaultPassword : BaseConfig
{
  public const string Name = "DefaultPassword";
  public DefaultPassword(string value) : base(value)
  {
  }
}
