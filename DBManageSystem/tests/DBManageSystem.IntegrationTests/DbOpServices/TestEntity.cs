using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBManageSystem.IntegrationTests.DbOpServices;
public class TestEntity
{
  public int Id { get; set; }

  public string Name { get; set; }

  public string? NullName { get; set; }

  public DateTime DateData { get; set; }

  public decimal Nums { get; set; }

  public DateTime? NullDate { get; set; }

  public long LongNum { get; set; }

}
