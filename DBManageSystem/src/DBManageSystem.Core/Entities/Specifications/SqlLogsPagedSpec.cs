using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Specification;

namespace DBManageSystem.Core.Entities.Specifications;
public class SqlLogsPagedSpec: Specification<SqlLog>
{
  public SqlLogsPagedSpec(int skip, int take)
  {
    if (take == 0)
    {
      take = int.MaxValue;
    }
    Query.OrderByDescending(t=>t.ExecuteTime).Skip(skip).Take(take);
  }
}
