using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBManageSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DBManageSystem.IntegrationTests.DbOpServices;
public class TestContext:DbContext
{
  public DbSet<TestEntity> testEntities { get; set; }
  public TestContext(DbContextOptions options) : base(options)
  {

  }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    TestEntity testEntity = new TestEntity();
    testEntity.Id = 1;
    testEntity.Name = "test!";
    testEntity.DateData = DateTime.Now;
    testEntity.Nums = 3.14159M;
    testEntity.LongNum = 10000000000;

    modelBuilder.Entity<TestEntity>().HasData(testEntity);

    TestEntity otherTestEntity = new TestEntity();
    otherTestEntity.Id = 2;
    otherTestEntity.Name = "test!2";
    otherTestEntity.DateData = DateTime.Now;
    otherTestEntity.Nums = 3.14159M;
    otherTestEntity.LongNum = 10000000000;
    otherTestEntity.NullName = "not null";

    modelBuilder.Entity<TestEntity>().HasData(otherTestEntity);

  }
}
