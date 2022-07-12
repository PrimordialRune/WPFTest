using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Games
{
    public partial class App : Application
    {
        private readonly IHost host;

        public static IServiceProvider ServiceProvider { get; private set; }

        public App()
        {
            host = Host.CreateDefaultBuilder().ConfigureServices((context, services) => { ConfigureServices(context.Configuration, services);}).Build();

            ServiceProvider = host.Services;
        }

        private void ConfigureServices(IConfiguration configuration, IServiceCollection services)
        {
            services.Configure<Models.DBSettings>(configuration.GetSection(nameof(Models.DBSettings)));
            services.AddScoped<Services.IServiceDB, Services.ServiceGameDB>();

            // Add NavigationService for the application.
            services.AddScoped<Navigation.NavigationService>(serviceProvider =>
            {
                var navigationService = new Navigation.NavigationService(serviceProvider);
                navigationService.Configure(Navigation.Windows.MainWindow, typeof(Views.MainWindowView));
                navigationService.Configure(Navigation.Windows.AddGameWindow, typeof(Views.AddGameWindowView));

                return navigationService;
            });

            // Register all ViewModels.
            services.AddSingleton<ViewModels.MainWindowViewModel>();
            services.AddSingleton<ViewModels.AddGameWindowViewModel>();

            // Register all the Windows of the applications.
            services.AddTransient<Views.MainWindowView>();
            services.AddTransient<Views.AddGameWindowView>();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await host.StartAsync();

            var navigationService = ServiceProvider.GetRequiredService<Navigation.NavigationService>();
            await navigationService.ShowAsync(Navigation.Windows.MainWindow);

            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            using (host)
            {
                await host.StopAsync(TimeSpan.FromSeconds(5));
            }

            base.OnExit(e);
        }

    }
}
