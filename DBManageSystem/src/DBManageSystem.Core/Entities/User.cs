using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBManageSystem.SharedKernel;
using Microsoft.AspNetCore.Identity;

namespace DBManageSystem.Core.Entities;
public class User: IdentityUser<int>
{
    public string LastLoginIP { get; set; }
}
