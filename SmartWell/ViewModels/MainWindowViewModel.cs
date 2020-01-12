using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartWell.ViewModels
{
    public class MainWindowViewModel
    {
        // Башкмак колонны
        public double CasingShoeLenght { get; set; }
        public double CasingShoeHeight { get; set; }
        public int CasingShoeIndex { get; set; }
        public KeyValuePair<int, string> CasingShoeSelected { get; set; }
        // Колонна
        public double CasingPipeLenght { get; set; }
        public double CasingPipeHeight { get; set; }
        public int CasingPipeIndex { get; set; }
        public KeyValuePair<int, string> CasingPipeSelected { get; set; }
        //Хвостовик колонны
        public double CasingLinerLenght { get; set; }
        public double CasingLinerHeight { get; set; }
        public int CasingLinerIndex { get; set; }
        public KeyValuePair<int, string> CasingLinerSelected { get; set; }

        public MainWindowViewModel()
        {
            CasingShoeLenght = 350.0;
            CasingShoeHeight = 320.0;
            CasingShoeIndex = 17;
            
            CasingPipeLenght = 1850.0;
            CasingPipeHeight = 1530.0;
            CasingPipeIndex = 10;
            CasingLinerLenght = 1150.0;
            CasingLinerHeight = 880.0;
            CasingLinerIndex = 3;
        }
    }
}
