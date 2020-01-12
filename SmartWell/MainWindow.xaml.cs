using SmartWell.Models;
using SmartWell.ViewModels;
using System.Windows;

namespace SmartWell
{
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();
            InitControls();
        }

        private void InitControls()
        {
            var MainWinsowDataContext = new MainWindowViewModel();

            var pl = new Pipes();
            CbCasingShoe.Items.Clear();
            CbCasingShoe.ItemsSource = pl.GetList(1);
            CbCasingShoe.SelectedValuePath = "Key";
            CbCasingShoe.DisplayMemberPath = "Value";
            CbCasingShoe.SelectedIndex = 17;

            CbCasingPipe.Items.Clear();
            CbCasingPipe.ItemsSource = pl.GetList(1);
            CbCasingPipe.SelectedValuePath = "Key";
            CbCasingPipe.DisplayMemberPath = "Value";
            CbCasingPipe.SelectedIndex = 10;

            CbCasingLiner.Items.Clear();
            CbCasingLiner.ItemsSource = pl.GetList(3);
            CbCasingLiner.SelectedValuePath = "Key";
            CbCasingLiner.DisplayMemberPath = "Value";
            CbCasingLiner.SelectedIndex = 10;
            DataContext = MainWinsowDataContext;
        }
    }
}
