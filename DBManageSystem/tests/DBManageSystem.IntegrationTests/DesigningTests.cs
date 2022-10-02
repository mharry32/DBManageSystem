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
    stringbuilder.Add("Uid", "root");
    stringbuilder.Add("Pwd", "1995072132Mh.");
    stringbuilder.Add("charset", "UTF8");
    builder.UseMySql(stringbuilder.ConnectionString, MySqlServerVersion.LatestSupportedServerVersion);
    DbContext db = new DbContext(builder.Options);
    var con =  db.Database.GetDbConnection();
    con.Open();
    var shemas = con.GetSchema();
    
    //var dt = con.GetSchema("Databases");
    string[] restricts = new string[4];
    restricts[1] = "dbtest";
    var dt = con.GetSchema("Tables", restricts);



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
    //var dt = con.GetSchema("Databases");
    var dt = con.GetSchema("Columns");
/*    con.Close();
    stringbuilder.Add("Initial Catalog", "MHXinZHIHU");
    db.Database.SetConnectionString(stringbuilder.ConnectionString);
    con = db.Database.GetDbConnection();
    con.Open();*/

    //dt = con.GetSchema("Tables");
    Assert.NotNull(dt);
  }
}
