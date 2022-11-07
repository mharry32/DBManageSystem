using DBManageSystem.Core.Constants;
using DBManageSystem.Core.Interfaces;
using DBManageSystem.Infrastructure.Crypto;
using DBManageSystem.Infrastructure.Data;
using DBManageSystem.Infrastructure.Logging;
using DBManageSystem.Infrastructure.Services;
using DBManageSystem.SharedKernel;
using DBManageSystem.SharedKernel.Interfaces;
using MediatR;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DBManageSystem.App;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		builder.Services.AddDataProtection().DisableAutomaticKeyGeneration()
.SetApplicationName(ApplicationConstants.APP_NAME)
		 .PersistKeysToFileSystem(new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)));
		builder.Services.AddDbContext<DbManageSysDbContext>(op =>
		{
            var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "dbmanage.db");
            op.UseSqlite($"Data Source={dbPath}");
		});
		builder.Services.AddTransient(typeof(IRepository<>), typeof(DbManageSysRepository<>));
		builder.Services.AddTransient(typeof(IReadRepository<>), typeof(DbManageSysRepository<>));
        builder.Services.AddSingleton(typeof(IAppLogger<>), typeof(MAUIAppLogger<>));
		builder.Services.AddTransient(typeof(IDbServiceStrategy),typeof(DbServiceStrategy));
		builder.Services.AddTransient(typeof(IDbPasswordCryptoService),typeof(DbPasswordCryptoService));
		builder.Services.AddTransient(typeof(IDatabaseServerService),typeof(DatabaseServerManageService));

        builder.Services.AddSingleton<ViewModels.AddConnectionViewModel>();
        builder.Services.AddSingleton<Views.AddConnectionPage>();
        builder.Services.AddSingleton<ViewModels.ConnectionManageViewModel>();
        builder.Services.AddSingleton<Views.ConnectionMangePage>();

        var app = builder.Build();
        using (var scope = app.Services.CreateScope())
        {
            var scopedProvider = scope.ServiceProvider;
            try
            {
                var keyManager = scopedProvider.GetService<IKeyManager>();
                var appdir = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData));
                var files = appdir.GetFiles("key-*.xml");
                if (files.Length == 0)
                {
                    keyManager.CreateNewKey(
                activationDate: DateTimeOffset.Now,
                expirationDate: DateTimeOffset.Now.AddYears(999));
                }

                var _dbManageSysDbContext = scopedProvider.GetRequiredService<DbManageSysDbContext>();
                _dbManageSysDbContext.Database.Migrate();


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            
        }
        return app;
    }
}

