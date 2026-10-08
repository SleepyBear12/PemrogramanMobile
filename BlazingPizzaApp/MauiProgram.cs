using BlazingPizzaApp.Data;
using BlazingPizzaApp.Services;
using Microsoft.Extensions.Logging;
using System;

namespace BlazingPizzaApp
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
                });

            builder.Services.AddMauiBlazorWebView();

            builder.Services.AddSingleton<OrderState>();

            // Configure API base address depending on platform so the app can reach the backend
            string apiBase = GetApiBaseAddress();

            builder.Services.AddHttpClient<IPizzaSpecials, PizzaSpecialsServices>(client =>
            {
                client.BaseAddress = new Uri(apiBase);
            });

            builder.Services.AddHttpClient<IOrderService, OrderService>(client =>
            {
                client.BaseAddress = new Uri(apiBase);
            });

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }

        static string GetApiBaseAddress()
        {
            // When running on the Android emulator, use the special 10.0.2.2 host to reach localhost on the development machine.
            if (OperatingSystem.IsAndroid())
            {
                // Use the API's HTTP (non-HTTPS) port for emulator to avoid certificate/trust issues
                return "http://10.0.2.2:5123/";
            }

            // iOS simulator and macOS can usually reach the API via localhost with HTTPS.
            // For Windows developer workflow, prefer the HTTP port to avoid needing to trust the dev HTTPS cert.
            if (OperatingSystem.IsIOS() || OperatingSystem.IsMacCatalyst() || OperatingSystem.IsMacOS())
            {
                return "https://localhost:7219/";
            }

            if (OperatingSystem.IsWindows())
            {
                return "http://localhost:5123/";
            }

            // Fallback to localhost
            return "https://localhost:7219/";
        }
    }
}
