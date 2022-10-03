using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Result;
using DBManageSystem.Core.Entities;
using DBManageSystem.Core.Enums;
using DBManageSystem.SharedKernel.Interfaces;

namespace DBManageSystem.Core.Interfaces;
public interface IDatabaseServerService
{
  Task<Result<DatabaseStatusEnum>> GetStatus(DatabaseServer server);
}
