using SmartWell.Models;
using SmartWell.ViewModels;
using System.Windows;
using System.Windows.Media;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Controls;

namespace SmartWell
{
    public partial class MainWindow : Window
    {
        MainWindowViewModel MainWinsowDataContext { get; set; }


        public MainWindow()
        {
            InitializeComponent();
            InitControls();
        }

        private void InitControls()
        {
            MainWinsowDataContext = new MainWindowViewModel();

            var pl = new Pipes();

            CbCasingShoe.Items.Clear();
            CbCasingShoe.ItemsSource = pl.GetList(1);
            CbCasingShoe.SelectedValuePath = "Key";
            CbCasingShoe.DisplayMemberPath = "Value";
            //MainWinsowDataContext.CasingShoeSelected = CbCasingShoe.ItemsSource[17];
            CbCasingPipe.Items.Clear();
            CbCasingPipe.ItemsSource = pl.GetList(1);
            CbCasingPipe.SelectedValuePath = "Key";
            CbCasingPipe.DisplayMemberPath = "Value";
            
            CbCasingLiner.Items.Clear();
            CbCasingLiner.ItemsSource = pl.GetList(2);
            CbCasingLiner.SelectedValuePath = "Key";
            CbCasingLiner.DisplayMemberPath = "Value";
            
            DataContext = MainWinsowDataContext;
        }

        private void SetCasingShema()
        {
            var pl = new Pipes();
            //gPict.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
            //gPict.Arrange(new Rect(0, 0, gPict.DesiredSize.Width, gPict.DesiredSize.Height));
            UpdateLayout();

            var sWidth = gPict.ActualWidth;
            var sHeight = gPict.ActualHeight;
            var w1 = pl.GetByIndex(((KeyValuePair<int, string>)CbCasingShoe.SelectedItem).Key).GetDOut();
            var w2 = pl.GetByIndex(((KeyValuePair<int, string>)CbCasingPipe.SelectedItem).Key).GetDOut();
            var w3 = pl.GetByIndex(((KeyValuePair<int, string>)CbCasingLiner.SelectedItem).Key).GetDOut();
            var MaxDiam = (new double[] { w1, w2, w3 }).Max();
            var fHeight = MainWinsowDataContext.CasingShoeLenght + MainWinsowDataContext.CasingPipeLenght + MainWinsowDataContext.CasingLinerLenght;
            var dx = sWidth / 3 / MaxDiam;
            var dy = sHeight / fHeight;

            var rect = new System.Windows.Shapes.Rectangle
            {
                Stroke = new SolidColorBrush(Colors.Blue),
                StrokeThickness = 2,
                Fill = new SolidColorBrush(Colors.DarkBlue),
                Width = dx * w1,
                Height = MainWinsowDataContext.CasingShoeLenght * dy
            };
            Canvas.SetLeft(rect, sWidth/2 - dx * w1/2);
            Canvas.SetTop(rect, 0);
            gPict.Children.Add(rect);

            var rect2 = new System.Windows.Shapes.Rectangle
            {
                Stroke = new SolidColorBrush(Colors.Green),
                StrokeThickness = 2,
                Fill = new SolidColorBrush(Colors.DarkGreen),
                Width = dx * w2,
                Height = MainWinsowDataContext.CasingPipeLenght * dy
            };
            Canvas.SetLeft(rect2, sWidth / 2 - dx * w2 / 2);
            Canvas.SetTop(rect2, MainWinsowDataContext.CasingShoeLenght * dy);
            gPict.Children.Add(rect2);

            var rect3 = new System.Windows.Shapes.Rectangle
            {
                Stroke = new SolidColorBrush(Colors.Red),
                StrokeThickness = 2,
                Fill = new SolidColorBrush(Colors.DarkRed),
                Width = dx * w3,
                Height = MainWinsowDataContext.CasingLinerLenght * dy
            };
            Canvas.SetLeft(rect3, sWidth / 2 - dx * w3 / 2);
            Canvas.SetTop(rect3, MainWinsowDataContext.CasingShoeLenght * dy + MainWinsowDataContext.CasingPipeLenght * dy);
            gPict.Children.Add(rect3);
            //var wMax = new int[] { w1, w2, w3 }.Ma
        }

        protected override void OnRender(DrawingContext dc)
        {
            //dc.DrawLine(new Pen(Brushes.Blue, 2.0),
            //    new Point(0.0, 0.0),
            //    new Point(ActualWidth, ActualHeight));
            //dc.DrawLine(new Pen(Brushes.Green, 2.0),
            //    new Point(ActualWidth, 0.0),
            //    new Point(0.0, ActualHeight));

            SetCasingShema();
        }
    }
}
