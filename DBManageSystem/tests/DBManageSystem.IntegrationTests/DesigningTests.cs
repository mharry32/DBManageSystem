using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Xunit;
using System.Text.Json;

namespace DBManageSystem.IntegrationTests;
public class DesigningTests
{
  private const string ConnectionString = @"Server=localhost;Uid=root;Pwd=1995072132Mh.;charset=UTF8";

  [Fact]
  public async Task GetConnect()
  {
    DbContextOptionsBuilder builder = new DbContextOptionsBuilder();
    DbConnectionStringBuilder stringbuilder = new DbConnectionStringBuilder();
    stringbuilder.Add("Server", "localhost");
    stringbuilder.Add("Database", "dbtest");
    stringbuilder.Add("Uid", "root");
    stringbuilder.Add("Pwd", "1995072132Mh.");
    stringbuilder.Add("charset", "UTF8");
    builder.UseMySql(stringbuilder.ConnectionString, MySqlServerVersion.LatestSupportedServerVersion);
    DbContext db = new DbContext(builder.Options);
    var con =  db.Database.GetDbConnection();
    con.Open();
    //var dt = con.GetSchema("Databases");
    string[] restricts = new string[4];
    restricts[1] = "dbtest";
    restricts[2] = "mainmenus";
    //var dt = con.GetSchema("Tables", restricts);
    var dt = con.GetSchema("Columns", restricts);


    var cmd = con.CreateCommand();
    cmd.CommandText = "select * from newtable";
    var reader =  cmd.ExecuteReader();
    var datacls = reader.GetColumnSchema();

    while (reader.Read())
    {
      string result = "";
      foreach(var cl in datacls)
      {
        result = result + cl.ColumnName + ":" + reader[cl.ColumnName] + " ";
      }
      Debug.WriteLine(result);
    }


    //还需测试update delete的效果
    Assert.NotNull(dt);
  }


  [Fact]
  public void GetSqlserver()
  {
    DbContextOptionsBuilder builder = new DbContextOptionsBuilder();
    DbConnectionStringBuilder stringbuilder = new DbConnectionStringBuilder();
    stringbuilder.Add("Data Source", "14.29.249.230");
    stringbuilder.Add("User Id", "sa");
    stringbuilder.Add("Password", "!tpr0gram");
    //stringbuilder.Add("Initial Catalog", "abptest");
    Debug.WriteLine(stringbuilder.ConnectionString);
    builder.UseSqlServer(stringbuilder.ConnectionString);
    DbContext db = new DbContext(builder.Options);
    var con = db.Database.GetDbConnection();
    con.Open();
    var shemas = con.GetSchema();
    var restricts = new string[4];
    restricts[1] = "";
    //var dt = con.GetSchema("Databases");
    var dt = con.GetSchema("Columns",new string[] {});
/*    con.Close();
    stringbuilder.Add("Initial Catalog", "MHXinZHIHU");
    db.Database.SetConnectionString(stringbuilder.ConnectionString);
    con = db.Database.GetDbConnection();
    con.Open();*/

    //dt = con.GetSchema("Tables");
    Assert.NotNull(dt);
  }
}
