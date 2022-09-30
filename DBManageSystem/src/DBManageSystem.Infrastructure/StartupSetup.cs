using DBManageSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DBManageSystem.Infrastructure;
public static class StartupSetup
{
  public static void AddDbContext(this IServiceCollection services, string connectionString) { }
}
