using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using Graphviz4Net.Graphs;

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

            //var graph = new Graph<Person>();
            //var a = new Person(graph) { Name = "Jonh", Avatar = "./Avatars/avatar1.jpg" };
            //var b = new Person(graph) { Name = "Michael", Avatar = "./Avatars/avatar2.gif" };
            //graph.AddVertex(a);
            //graph.AddVertex(b);
            //mainGraph.Graph = graph;
            //graph.AddEdge(new Edge<Person>(a, b, new DiamondArrow()) { Label = "Boss" });
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

    public class Person : INotifyPropertyChanged
    {
        private readonly Graph<Person> graph;

        public Person(Graph<Person> graph)
        {
            this.graph = graph;
            this.Avatar = "./Avatars/avatarAnon.gif";
        }

        private string name;
        public string Name
        {
            get { return this.name; }
            set
            {
                this.name = value;
                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged(this, new PropertyChangedEventArgs("Name"));
                }
            }
        }

        public string Avatar { get; set; }

        public string Email
        {
            get
            {
                return this.Name.ToLower().Replace(' ', '.') + "@gmail.com";
            }
        }

        public ICommand RemoveCommand
        {
            get { return new RemoveCommandImpl(this); }
        }

        private class RemoveCommandImpl : ICommand
        {
            private Person person;

            public RemoveCommandImpl(Person person)
            {
                this.person = person;
            }

            public void Execute(object parameter)
            {
                this.person.graph.RemoveVertexWithEdges(this.person);
            }

            public bool CanExecute(object parameter)
            {
                return true;
            }

            public event EventHandler CanExecuteChanged;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
