using Microsoft.Extensions.DependencyInjection;

namespace Games.ViewModels
{
    public class ViewModelLocator
    {
        public MainWindowViewModel MainWindowViewModel => App.ServiceProvider.GetRequiredService<MainWindowViewModel>();
        public AddGameWindowViewModel AddGameWindowViewModel => App.ServiceProvider.GetRequiredService<AddGameWindowViewModel>();
    }
}