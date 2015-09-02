using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Algorithms.WpfApp.Ioc;

namespace Algorithms.WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DrawLine();
        }

        private void DrawLine()
        {

           
        }

        private void btnStartWorker_Click(object sender, RoutedEventArgs e)
        {
            if(comboWorkers.SelectedItem == null) return;
            var selectedValue = ((ComboBoxItem)comboWorkers.SelectedItem).Content.ToString();
            var workersBuilder = new WorkersBuilder();
            var worker = workersBuilder.Resolve(selectedValue);
            worker.SetCanvas(MainCanvas);
            worker.Run();
        }
    }
}
