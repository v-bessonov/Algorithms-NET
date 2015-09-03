using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Algorithms.Core.Helpers;
using Algorithms.Core.StdLib;
using Algorithms.WpfApp.Attributes;
using Algorithms.WpfApp.Interfaces;

namespace Algorithms.WpfApp.Workers.Helpers
{
    [WpfCommand("Points2D", "class is an immutable data type to encapsulate a two-dimensional point with real-value coordinates.")]
    public class Points2DWorker : IWorker
    {
        public Canvas Canvas { get; set; }

        public void Run()
        {
            //var myLine = new Line
            //{
            //    Stroke = Brushes.LightSteelBlue,
            //    X1 = 1,
            //    X2 = 50,
            //    Y1 = 1,
            //    Y2 = 50,
            //    HorizontalAlignment = HorizontalAlignment.Left,
            //    VerticalAlignment = VerticalAlignment.Center,
            //    StrokeThickness = 2
            //};
            //Canvas.Children.Add(myLine);
            for (var i = 0; i < 100; i++)
            {
                var x = StdRandom.Uniform(800);
                var y = StdRandom.Uniform(800);

                var ellipse = new Ellipse
                {
                    Height = 10,
                    Width = 10,
                    StrokeThickness = 2,
                    Stroke = Brushes.Blue,
                    Fill = Brushes.Yellow,
                    Margin = new Thickness
                    {
                        Left = x, Top = y, Bottom = 0, Right = 0
                    }

                };

                //Rectangle rec = new Rectangle();
                //Canvas.SetTop(rec, y);
                //Canvas.SetLeft(rec, x);
                //rec.Width = 1;
                //rec.Height = 1;
                //rec.Fill = new SolidColorBrush(Colors.Red);
                //Canvas.Children.Add(rec);
                //((Polyline)Canvas.Children[0]).Points.Add(new Point(x,y));

                Canvas.Children.Add(ellipse);



                //Canvas.Children.Add(new Line
                //{
                //    Stroke = Brushes.Red,
                //    X1 = x,
                //    X2 = x,
                //    Y1 = y,
                //    Y2 = y,

                //    StrokeThickness = 1
                //});
            }
        }

        public void SetCanvas(Canvas canvas)
        {
            Canvas = canvas;
        }
    }
}
