using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBManageSystem.Core.Entities;
public class SqlExecuteResult
{
  public bool IsSuccess { get;private set; }

  public bool HasData { get; private set; }

  public string Error { get; private set; }

  public string JsonData { get; private set; }

  public int RowsEffected { get; private set; }

  protected SqlExecuteResult() { }

  public static SqlExecuteResult SuccessWithDatas(string jsonData)
  {
    SqlExecuteResult result = new SqlExecuteResult();
    result.IsSuccess = true;
    result.HasData = true;
    result.JsonData = jsonData;
    return result;
  }

  public static SqlExecuteResult SuccessWithoutData(int rowsEffected)
  {
    SqlExecuteResult result = new SqlExecuteResult();
    result.IsSuccess = true;
    result.HasData = false;
    result.RowsEffected = rowsEffected;
    return result;
  }

  public static SqlExecuteResult ExecuteError(string errorMessage)
  {
    SqlExecuteResult result = new SqlExecuteResult();
    result.IsSuccess = false;
    result.Error = errorMessage;
    return result;
  }


}
