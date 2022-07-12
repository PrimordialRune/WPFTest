using System.Threading.Tasks;

namespace Games.Navigation
{
    public interface IActivable
    {
        Task ActivateAsync(object parameter);
    }
}
