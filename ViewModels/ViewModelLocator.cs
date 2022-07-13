using Microsoft.Extensions.DependencyInjection;

namespace Games.ViewModels
{
    public class ViewModelLocator
    {
        public ViewModels.MainWindowViewModel MainWindowViewModel => App.ServiceProvider.GetRequiredService<ViewModels.MainWindowViewModel>();

        public ViewModels.AddGameWindowViewModel AddGameWindowViewModel => App.ServiceProvider.GetRequiredService<ViewModels.AddGameWindowViewModel>();
    }
}