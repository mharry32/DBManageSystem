using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBManageSystem.IntegrationTests.DbOpServices;
public class TestEntityDTO
{
  public int Id { get; set; }

  public string Name { get; set; }

  public string? NullName { get; set; }

  public string DateData { get; set; }

  public decimal Nums { get; set; }

  public string NullDate { get; set; }

  public long LongNum { get; set; }
}
