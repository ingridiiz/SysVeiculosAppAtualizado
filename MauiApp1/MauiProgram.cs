using Microsoft.Extensions.Logging;
using System.IO; 

namespace SysVeiculosApp
{
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

#if DEBUG
            builder.Logging.AddDebug();
#endif

           
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "veiculos.db3");
            builder.Services.AddSingleton(s => new DatabaseHelper(dbPath));

            return builder.Build();
        }
    }
}
