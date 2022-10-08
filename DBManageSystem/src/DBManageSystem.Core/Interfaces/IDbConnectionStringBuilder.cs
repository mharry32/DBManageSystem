using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBManageSystem.Core.Entities;

namespace DBManageSystem.Core.Interfaces;
public interface IDbConnectionStringBuilder
{
  string Build(DatabaseServer server, string databaseName = null);
}
