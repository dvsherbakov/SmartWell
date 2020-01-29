using SmartWell.Models;
using SmartWell.ViewModels;
using System.Windows;
using System.Windows.Media;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SmartWell
{
    public partial class MainWindow : Window
    {
        private MainWindowViewModel MainWindowDataContext { get; set; }


        public MainWindow()
        {
            InitializeComponent();
            InitControls();
        }

        private void InitControls()
        {
            MainWindowDataContext = new MainWindowViewModel();

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

            CbConductor.Items.Clear();
            CbConductor.ItemsSource = pl.GetList(4);
            CbConductor.SelectedValuePath = "Key";
            CbConductor.DisplayMemberPath = "Value";

            DataContext = MainWindowDataContext;
        }

        private void SetCasingScheme()
        {
            UpdateLayout();
            UpdateScheme();
        }


        private void UpdateScheme()
        {
            var sWidth = gPict.ActualWidth;
            var sHeight = gPict.ActualHeight;

            gPict.Children.Clear();


            MainWindowDataContext.ShowLengthMarker(gData);

            var pl = new Pipes();

            var w1 = pl.GetByIndex(((KeyValuePair<int, string>)CbCasingShoe.SelectedItem).Key).GetDOut();
            var w2 = pl.GetByIndex(((KeyValuePair<int, string>)CbCasingPipe.SelectedItem).Key).GetDOut();
            var w3 = pl.GetByIndex(((KeyValuePair<int, string>)CbCasingLiner.SelectedItem).Key).GetDOut();
            var n1 = pl.GetByIndex(((KeyValuePair<int, string>)CbTubingUpperSuspension.SelectedItem).Key).GetDOut();
            var n2 = pl.GetByIndex(((KeyValuePair<int, string>)CbTubingLowerSuspension.SelectedItem).Key).GetDOut();
            var maxDiam = (new[] { w1, w2, w3 }).Max();
            var fHeight = MainWindowDataContext.CasingPipeLength + MainWindowDataContext.CasingLinerLength;
            var dx = sWidth / 3 / maxDiam;
            var dy = sHeight / fHeight;

            var lenList = MainWindowDataContext.GetLengthList();

            var colors = VolumeGradients.Volumes();
            
            var counter = 0;
            var prevMark = 0;
            foreach (var itm in lenList)
            {
                var width = itm.MarkLabel > MainWindowDataContext.CasingPipeLength ? w2 : w1;
                var rectangle = new Rectangle
                {
                    Stroke = new SolidColorBrush(Colors.Gray),
                    StrokeThickness = 2,
                    Fill = new VolumeGradient(colors[counter]).GetValue(),
                    Width = dx * width,
                    Height = itm.MarkLabel * dy - prevMark * dy
                };
                Canvas.SetLeft(rectangle, sWidth / 2 - dx * width / 2);
                Canvas.SetTop(rectangle, prevMark * dy);
                gPict.Children.Add(rectangle);
              
                counter++;
                prevMark = (int)itm.MarkLabel;
            }

            prevMark = 0;
            foreach (var itm in lenList.Where(x => x.MarkLabel <= MainWindowDataContext.TubingUpperSuspensionLength + MainWindowDataContext.TubingLowerSuspensionLength))
            {
                var width = itm.MarkLabel > MainWindowDataContext.TubingUpperSuspensionLength ? n2 : n1;
                var rectangle = new Rectangle
                {
                    Stroke = new SolidColorBrush(colors[counter]),
                    StrokeThickness = 2,
                    Fill = new VolumeGradient(colors[counter]).GetValue(),
                    Width = dx * width,
                    Height = itm.MarkLabel * dy - prevMark * dy
                };
                Canvas.SetLeft(rectangle, sWidth / 2 - dx * width / 2);
                Canvas.SetTop(rectangle, prevMark * dy);
                gPict.Children.Add(rectangle);
               
                counter++;
                prevMark = (int)itm.MarkLabel;
            }
        }
        protected override void OnRender(DrawingContext dc)
        {
            SetCasingScheme();
        }

        private void GDigit_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            UpdateLayout();
            
            var sWidth = gPict.ActualWidth;
            var sHeight = gPict.ActualHeight;
            
            var fHeight = MainWindowDataContext.CasingPipeLength + MainWindowDataContext.CasingLinerLength;
            
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

            var textBlock = new TextBlock
            {
                Text = (t.Y/dy).ToString("F0"),
                Foreground = new SolidColorBrush(Colors.DarkGray)
            };
            Canvas.SetLeft(textBlock, 30);
            Canvas.SetTop(textBlock, t.Y);
            gDigit.Children.Add(textBlock);

            if (!(t.Y > 0)) return;
            var heightBlock = new TextBlock
            {
                Text = (MainWindowDataContext.Scaller.GetValue(t.Y / dy)).ToString("F0"),
                Foreground = new SolidColorBrush(Colors.DarkGray)
            };
            Canvas.SetLeft(heightBlock, sWidth - 65);
            Canvas.SetTop(heightBlock, t.Y);
            gDigit.Children.Add(heightBlock);
        }

        private void SaveToImage()
        {
            var rtb = new RenderTargetBitmap((int)gPict.RenderSize.Width,
                (int)gPict.RenderSize.Height, 96d, 96d, System.Windows.Media.PixelFormats.Default);
            rtb.Render(gPict);

            var crop = new CroppedBitmap(rtb, new Int32Rect(50, 50, 250, 250));

            BitmapEncoder pngEncoder = new PngBitmapEncoder();
            pngEncoder.Frames.Add(BitmapFrame.Create(crop));

            using (var fs = System.IO.File.OpenWrite("logo.png"))
            {
                pngEncoder.Save(fs);
            }
        }
    }
}
