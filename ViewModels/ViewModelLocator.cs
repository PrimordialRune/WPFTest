using Microsoft.Extensions.DependencyInjection;

namespace Games.ViewModels
{
    public class ViewModelLocator
    {
        public ViewModels.MainWindowViewModel MainViewModel => App.ServiceProvider.GetRequiredService<ViewModels.MainWindowViewModel>();

        public ViewModels.AddGameWindowViewModel AddGameViewModel => App.ServiceProvider.GetRequiredService<ViewModels.AddGameWindowViewModel>();
    }
}