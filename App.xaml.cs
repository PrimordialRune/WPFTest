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
            host = Host.CreateDefaultBuilder().ConfigureServices((context, services) => { ConfigureServices(context.Configuration, services); }).Build();
            ServiceProvider = host.Services;
        }
        private void ConfigureServices(IConfiguration configuration, IServiceCollection services)
        {
            services.Configure<Models.DBSettings>(configuration.GetSection(nameof(Models.DBSettings)));
            services.AddScoped<Services.IServiceDB, Services.ServiceGameDB>();
            services.AddSingleton<ViewModels.MainWindowViewModel>();
            services.AddTransient<Views.MainWindowView>();
        }
        protected override async void OnStartup(StartupEventArgs e)
        {
            await host.StartAsync();
            base.OnStartup(e);

            var mainWindow = ServiceProvider.GetRequiredService<Views.MainWindowView>();
            mainWindow.Show();

            /*this.MainWindow = new Views.MainWindowView();
            this.MainWindow.DataContext = new ViewModels.MainWindowViewModel();
            this.MainWindow.Show();*/
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
