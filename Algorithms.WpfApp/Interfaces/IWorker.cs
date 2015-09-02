using System.Windows.Controls;

namespace Algorithms.WpfApp.Interfaces
{
    public interface IWorker
    {
        void Run();

        void SetCanvas(Canvas canvas);
    }
}
