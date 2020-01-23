using SmartWell.Models;
using SmartWell.ViewModels;
using System.Windows;
using System.Windows.Media;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Shapes;

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
            
            CbCasingPipe.Items.Clear();
            CbCasingPipe.ItemsSource = pl.GetList(1);
            CbCasingPipe.SelectedValuePath = "Key";
            CbCasingPipe.DisplayMemberPath = "Value";
            
            CbCasingLiner.Items.Clear();
            CbCasingLiner.ItemsSource = pl.GetList(2);
            CbCasingLiner.SelectedValuePath = "Key";
            CbCasingLiner.DisplayMemberPath = "Value";

            CbTubingUpperSuspension.Items.Clear();
            CbTubingUpperSuspension.ItemsSource = pl.GetList(2);
            CbTubingUpperSuspension.SelectedValuePath = "Key";
            CbTubingUpperSuspension.DisplayMemberPath = "Value";

            CbTubingLowerSuspension.Items.Clear();
            CbTubingLowerSuspension.ItemsSource = pl.GetList(3);
            CbTubingLowerSuspension.SelectedValuePath = "Key";
            CbTubingLowerSuspension.DisplayMemberPath = "Value";

            DataContext = MainWinsowDataContext;
        }

        private void SetCasingShema()
        {
            var pl = new Pipes();
            
            UpdateLayout();
            gPict.Children.Clear();

            var sWidth = gPict.ActualWidth;
            var sHeight = gPict.ActualHeight;
            var w1 = pl.GetByIndex(((KeyValuePair<int, string>)CbCasingShoe.SelectedItem).Key).GetDOut();
            var w2 = pl.GetByIndex(((KeyValuePair<int, string>)CbCasingPipe.SelectedItem).Key).GetDOut();
            var w3 = pl.GetByIndex(((KeyValuePair<int, string>)CbCasingLiner.SelectedItem).Key).GetDOut();
            var n1 = pl.GetByIndex(((KeyValuePair<int, string>)CbTubingUpperSuspension.SelectedItem).Key).GetDOut();
            var n2 = pl.GetByIndex(((KeyValuePair<int, string>)CbTubingLowerSuspension.SelectedItem).Key).GetDOut();
            var MaxDiam = (new double[] { w1, w2, w3 }).Max();
            var fHeight = MainWinsowDataContext.CasingPipeLenght + MainWinsowDataContext.CasingLinerLenght;
            var dx = sWidth / 3 / MaxDiam;
            var dy = sHeight / fHeight;

            var lenList = new List<VolumeItem>
            {
                new VolumeItem { Layer = 1, MarkLabel = MainWinsowDataContext.CasingPipeLenght },
                new VolumeItem { Layer = 1, MarkLabel = MainWinsowDataContext.CasingPipeLenght+MainWinsowDataContext.CasingLinerLenght },
                new VolumeItem { Layer = 2, MarkLabel = MainWinsowDataContext.TubingUpperSuspensionLenght },
                new VolumeItem { Layer = 2, MarkLabel = MainWinsowDataContext.TubingUpperSuspensionLenght+MainWinsowDataContext.TubingLowerSuspensionLenght }
            }.OrderBy(x=>x.MarkLabel).ToArray();

            var rectsB = new List<Rectangle>();
            var rectsF = new List<Rectangle>();
            var clrs = VolumeGradients.Volumes();
            // var top = 0;
            
            var counter = 0;
            var prevMark = 0;
            foreach (var itm in lenList)
            {
                var width = itm.MarkLabel > MainWinsowDataContext.CasingPipeLenght ? w2 : w1;
                var rectangle = new Rectangle
                {
                    Stroke = new SolidColorBrush(Colors.Gray),//clrs[counter]),
                    StrokeThickness = 2,
                    Fill = new VolumeGradient(clrs[counter]).GetValue(),
                    Width = dx * width,
                    Height = itm.MarkLabel * dy - prevMark*dy
                };
                Canvas.SetLeft(rectangle, sWidth / 2 - dx * width / 2);
                Canvas.SetTop(rectangle, prevMark * dy);
                gPict.Children.Add(rectangle);
               // top = (int)prevMark;
                counter++;
                
                prevMark = (int)itm.MarkLabel;
            };

            
            prevMark = 0;
            foreach (var itm in lenList.Where(x=>x.MarkLabel<= MainWinsowDataContext.TubingUpperSuspensionLenght + MainWinsowDataContext.TubingLowerSuspensionLenght))
            {
                var width = itm.MarkLabel > MainWinsowDataContext.TubingUpperSuspensionLenght ? n2 : n1;
                var rectangle = new Rectangle
                {
                    Stroke = new SolidColorBrush(clrs[counter]),
                    StrokeThickness = 2,
                    Fill = new VolumeGradient(clrs[counter]).GetValue(),
                    Width = dx * width,
                    Height = itm.MarkLabel * dy - prevMark * dy
                };
                Canvas.SetLeft(rectangle, sWidth / 2 - dx * width / 2);
                Canvas.SetTop(rectangle, prevMark * dy);
                gPict.Children.Add(rectangle);
                // top = (int)prevMark;
                counter++;

                prevMark = (int)itm.MarkLabel;
            };

            //var rect = new Rectangle
            //{
            //    Stroke = new SolidColorBrush(Colors.Blue),
            //    StrokeThickness = 2,
            //    Fill = new VolumeGradient(Colors.DarkBlue).GetValue(),
            //    Width = dx * w1,
            //    Height = MainWinsowDataContext.CasingShoeLenght * dy
            //};
            //Canvas.SetLeft(rect, sWidth/2 - dx * w1/2);
            //Canvas.SetTop(rect, 0);
            //gPict.Children.Add(rect);

            //var rect2 = new Rectangle
            //{
            //    Stroke = new SolidColorBrush(Colors.Green),
            //    StrokeThickness = 2,
            //    Fill = new VolumeGradient(Colors.DarkGreen).GetValue(),
            //    Width = dx * w2,
            //    Height = MainWinsowDataContext.CasingPipeLenght * dy
            //};
            //Canvas.SetLeft(rect2, sWidth / 2 - dx * w2 / 2);
            //Canvas.SetTop(rect2,0);
            //gPict.Children.Add(rect2);

            //var rect3 = new Rectangle
            //{
            //    Stroke = new SolidColorBrush(Colors.Red),
            //    StrokeThickness = 2,
            //    Fill = new VolumeGradient(Colors.DarkRed).GetValue(),
            //    Width = dx * w3,
            //    Height = MainWinsowDataContext.CasingLinerLenght * dy
            //};
            //Canvas.SetLeft(rect3, sWidth / 2 - dx * w3 / 2);
            //Canvas.SetTop(rect3, MainWinsowDataContext.CasingPipeLenght * dy);
            //gPict.Children.Add(rect3);

            //var rect4 = new Rectangle
            //{
            //    Stroke = new SolidColorBrush(Colors.Brown),
            //    StrokeThickness = 2,
            //    Fill = new VolumeGradient(Colors.Brown).GetValue(),
            //    Width = dx * n1,
            //    Height = MainWinsowDataContext.TubingUpperSuspensionLenght * dy
            //};
            //Canvas.SetLeft(rect4, sWidth / 2 - dx * n1 / 2);
            //Canvas.SetTop(rect4, 0);
            //gPict.Children.Add(rect4);

            //var rect5 = new Rectangle
            //{
            //    Stroke = new SolidColorBrush(Colors.DarkCyan),
            //    StrokeThickness = 2,
            //    Fill = new VolumeGradient(Colors.DarkCyan).GetValue(),
            //    Width = dx * n2,
            //    Height = MainWinsowDataContext.TubingLowerSuspensionLenght * dy
            //};
            //Canvas.SetLeft(rect5, sWidth / 2 - dx * n2 / 2);
            //Canvas.SetTop(rect5, MainWinsowDataContext.TubingUpperSuspensionLenght * dy);
            //gPict.Children.Add(rect5);
            //var wMax = new int[] { w1, w2, w3 }.Ma
        }

        protected override void OnRender(DrawingContext dc)
        {
            SetCasingShema();
        }

        private void gDigit_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            UpdateLayout();
            
            var sWidth = gPict.ActualWidth;
            var sHeight = gPict.ActualHeight;
            
            var fHeight = MainWinsowDataContext.CasingPipeLenght + MainWinsowDataContext.CasingLinerLenght;
            
            var dy = sHeight / fHeight;
            var t = e.GetPosition(gPict);
            gDigit.Children.Clear();
            var rLine = new Line
            {
                Stroke = Brushes.LightSteelBlue,
                X1 = sWidth - 20,
                X2 = sWidth - 75,
                Y1 = t.Y,
                Y2 = t.Y,
                StrokeThickness = 2
            };
            gDigit.Children.Add(rLine);

            var lLine = new Line
            {
                Stroke = Brushes.LightSteelBlue,
                X1 = 20,
                X2 = 75,
                Y1 = t.Y,
                Y2 = t.Y,
                StrokeThickness = 2
            };
            gDigit.Children.Add(lLine);

            TextBlock textBlock = new TextBlock
            {
                Text = (t.Y/dy).ToString("F0"),
                Foreground = new SolidColorBrush(Colors.DarkGray)
            };
            Canvas.SetLeft(textBlock, 30);
            Canvas.SetTop(textBlock, t.Y);
            gDigit.Children.Add(textBlock);
        }
    }
}
