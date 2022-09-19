using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBManageSystem.Infrastructure.Configs;
public class JwtSecret : BaseConfig
{
  public const string Name = "JwtKey";

  public JwtSecret(string value) : base(value)
  {
  }
}
