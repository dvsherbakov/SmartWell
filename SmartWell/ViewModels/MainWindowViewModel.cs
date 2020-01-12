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
        // Колонна
        public double CasingPipeLenght { get; set; }
        public double CasingPipeHeight { get; set; }
        //Хвостовик колонны
        public double CasingLinerLenght { get; set; }
        public double CasingLinerHeight { get; set; }

        public MainWindowViewModel()
        {
            CasingShoeLenght = 350.0;
            CasingShoeHeight = 320.0;
            CasingPipeLenght = 1850.0;
            CasingPipeHeight = 1530.0;
            CasingLinerLenght = 1150.0;
            CasingLinerHeight = 880.0;
        }
    }
}
