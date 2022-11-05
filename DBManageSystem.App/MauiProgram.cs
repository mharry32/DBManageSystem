using DBManageSystem.Core.Constants;
using DBManageSystem.Infrastructure.Services;
using Microsoft.AspNetCore.DataProtection;
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

		builder.Services.AddScoped<DbServiceStrategy>();
		builder.Services.AddLogging();
		builder.Services.AddDataProtection().DisableAutomaticKeyGeneration()
.SetApplicationName(ApplicationConstants.APP_NAME)
		 .PersistKeysToFileSystem(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData));
        return builder.Build();
	}
}

