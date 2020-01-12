using SmartWell.Models;
using SmartWell.ViewModels;
using System.Windows;
using System.Windows.Media;
using System.Linq;
using System.Collections.Generic;

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
