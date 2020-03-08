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
        private PictModel ExPict;

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

            ExPict = new PictModel();
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
            MainWindowDataContext.props.width = sWidth;
            MainWindowDataContext.props.heigth = sHeight;

            gPict.Children.Clear();

            var pl = new Pipes();
            if (CbTubingLowerSuspension.SelectedItem == null||CbTubingUpperSuspension.SelectedItem == null||CbCasingShoe.SelectedItem == null || CbCasingPipe.SelectedItem==null || CbCasingLiner.SelectedItem==null) return;
            var w1 = CbCasingShoe.SelectedItem !=null ? pl.GetByIndex(((KeyValuePair<int, string>)CbCasingShoe.SelectedItem).Key).GetDOut(): 0;
            MainWindowDataContext.CasingShoeWidth = w1;
            var w2 = CbCasingPipe.SelectedItem !=null ? pl.GetByIndex(((KeyValuePair<int, string>)CbCasingPipe.SelectedItem).Key).GetDOut():0;
            MainWindowDataContext.CasingPipeWidth = w2;
            MainWindowDataContext.CasingPipeGeometry = pl.GetByIndex(((KeyValuePair<int, string>)CbCasingPipe.SelectedItem).Key);
            var w3 = CbCasingLiner.SelectedItem!=null ? pl.GetByIndex(((KeyValuePair<int, string>)CbCasingLiner.SelectedItem).Key).GetDOut():0;
            MainWindowDataContext.CasingLinerWidth = w3;
            MainWindowDataContext.CasingLinerGeometry = pl.GetByIndex(((KeyValuePair<int, string>)CbCasingLiner.SelectedItem).Key);
            var w4 = CbConductor.SelectedItem!=null? pl.GetByIndex(((KeyValuePair<int, string>)CbConductor.SelectedItem).Key).GetDOut():0;
            MainWindowDataContext.ConductorWidth = w4;
           
            MainWindowDataContext.TubingUpperSuspensionGeometry = pl.GetByIndex(((KeyValuePair<int, string>)CbTubingUpperSuspension.SelectedItem).Key);
            var n1 = CbTubingUpperSuspension.SelectedItem!=null? pl.GetByIndex(((KeyValuePair<int, string>)CbTubingUpperSuspension.SelectedItem).Key).GetDOut():0;
            MainWindowDataContext.TubingUpperSuspensionWidth = n1;
           
            MainWindowDataContext.TubingLowerSuspensionGeometry = pl.GetByIndex(((KeyValuePair<int, string>)CbTubingLowerSuspension.SelectedItem).Key);
            var n2 = CbTubingLowerSuspension.SelectedItem!=null? pl.GetByIndex(((KeyValuePair<int, string>)CbTubingLowerSuspension.SelectedItem).Key).GetDOut():0;
            MainWindowDataContext.TubingLowerSuspensionWidth = n2;
            var maxDiam = MainWindowDataContext.MaxDiam();
            var fHeight = MainWindowDataContext.MaxLength();

            var dx = sWidth / 3 / maxDiam;
            var dy = sHeight / fHeight;
            MainWindowDataContext.props.dx = dx;
            MainWindowDataContext.props.dy = dy;

            MainWindowDataContext.GenerateSchema(gPict);
            MainWindowDataContext.ShowLengthMarker(gData);
            
            ExPict.GeneratePict(MainWindowDataContext);

            if (MainWindowDataContext.Volumes.Count > 0)
            {
                lvVolumeList.Items.Clear();
                foreach(var item in MainWindowDataContext.Volumes)
                {
                    lvVolumeList.Items.Add(new { Id = item.Id, Lenght = item.PipeProps.GetLen(), VolumeT = item.PipeProps.RGetSelfInVolume().ToString() }); ;
                }
            }
            
           //gDigit = new PictModel(MainWindowDataContext).ReturnCanvas();
        }

        protected override void OnRender(DrawingContext dc)
        {
            SetCasingScheme();
        }

        private void GDigit_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            //UpdateLayout();
            
            //var sWidth = gPict.ActualWidth;
            //var sHeight = gPict.ActualHeight;
            
            //var fHeight = MainWindowDataContext.CasingPipeLengthEnd + MainWindowDataContext.CasingLinerLengthEnd;
            
            //var dy = sHeight / fHeight;
            //var t = e.GetPosition(gPict);
            //gDigit.Children.Clear();
            //var rLine = new Line
            //{
            //    Stroke = Brushes.LightSteelBlue,
            //    X1 = sWidth - 20,
            //    X2 = sWidth - 75,
            //    Y1 = t.Y,
            //    Y2 = t.Y,
            //    StrokeThickness = 2
            //};
            //gDigit.Children.Add(rLine);

            //var lLine = new Line
            //{
            //    Stroke = Brushes.LightSteelBlue,
            //    X1 = 20,
            //    X2 = 75,
            //    Y1 = t.Y,
            //    Y2 = t.Y,
            //    StrokeThickness = 2
            //};
            //gDigit.Children.Add(lLine);

            //var textBlock = new TextBlock
            //{
            //    Text = (t.Y/dy).ToString("F0"),
            //    Foreground = new SolidColorBrush(Colors.DarkGray)
            //};
            //Canvas.SetLeft(textBlock, 30);
            //Canvas.SetTop(textBlock, t.Y);
            //gDigit.Children.Add(textBlock);

            //if (!(t.Y > 0)) return;
            //var heightBlock = new TextBlock
            //{
            //    Text = (MainWindowDataContext.Scaller.GetValue(t.Y / dy)).ToString("F0"),
            //    Foreground = new SolidColorBrush(Colors.DarkGray)
            //};
            //Canvas.SetLeft(heightBlock, sWidth - 65);
            //Canvas.SetTop(heightBlock, t.Y);
            //gDigit.Children.Add(heightBlock);
        }

        
        private void FeaturesChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateScheme();
        }

        private void TextChanged(object sender, TextChangedEventArgs e)
        {
             //UpdateScheme();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

            ExPict.SavePict();
        }


    }
}
