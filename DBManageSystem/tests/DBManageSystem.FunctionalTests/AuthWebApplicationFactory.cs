﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;

namespace DBManageSystem.FunctionalTests;
public class AuthWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
{
}
