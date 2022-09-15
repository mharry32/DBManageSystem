using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBManageSystem.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DBManageSystem.Infrastructure.Identity;
public class AppIdentityDbContext: IdentityDbContext<User,Role,int>
{
  public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options) : base(options)
  {

  }


}
