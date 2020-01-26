using SmartWell.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

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
        //НКТ вехняя подвеска
        public double TubingUpperSuspensionLenght { get; set; }
        public double TubingUpperSuspensionHeight { get; set; }
        public int TubingUpperSuspensionIndex { get; set; }
        public KeyValuePair<int, string> TubingUpperSuspensionSelected { get; set; }
        //НКТ вехняя подвеска
        public double TubingLowerSuspensionLenght { get; set; }
        public double TubingLowerSuspensionHeight { get; set; }
        public int TubingLowerSuspensionIndex { get; set; }
        public KeyValuePair<int, string> TubingLowerSuspensionSelected { get; set; }
        
        public SplineInterpolator Scaller;

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

            TubingUpperSuspensionLenght = 1280.0;
            TubingUpperSuspensionHeight = 1186.0;
            //TubingUpperSuspensionIndex = 6;

            TubingLowerSuspensionLenght = 680.0;
            TubingLowerSuspensionHeight = 586.0;
            //TubingLowerSuspensionIndex = 0;

            var known = new Dictionary<double, double> {
                { 0.0, 0.0 },
                { CasingPipeLenght,  CasingPipeHeight },
                { CasingPipeLenght+CasingLinerLenght, CasingPipeHeight+CasingLinerHeight },
                { TubingUpperSuspensionLenght, TubingUpperSuspensionHeight },
                { TubingUpperSuspensionLenght+TubingLowerSuspensionLenght, TubingUpperSuspensionHeight+TubingLowerSuspensionHeight }
            };
            Scaller = new SplineInterpolator(known.OrderBy(x=>x.Key).ToDictionary(pair => pair.Key, pair => pair.Value));
        }

        public LengthItem[] GetLengthList()
        {
            return new List<LengthItem>
            {
                new LengthItem { Layer = 1, MarkLabel = CasingPipeLenght },
                new LengthItem { Layer = 1, MarkLabel = CasingPipeLenght+CasingLinerLenght },
                new LengthItem { Layer = 2, MarkLabel = TubingUpperSuspensionLenght },
                new LengthItem { Layer = 2, MarkLabel = TubingUpperSuspensionLenght+TubingLowerSuspensionLenght }
            }.OrderBy(x => x.MarkLabel).ToArray();
        }

        public void ShowLenghtMarker(Canvas canvas)
        {
            var sWidth = canvas.ActualWidth;
            var sHeight = canvas.ActualHeight;

            canvas.Children.Clear();


        }

    }
}
