using SmartWell.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace SmartWell.ViewModels
{
    public class MainWindowViewModel
    {
        // Кондуктор
        public double ConductorLengthStart { get; set; }
        public double ConductorLengthEnd { get; set; }
        public double ConductorIndex { get; set; }
        public bool ConductorIsCement { get; set; }

        public KeyValuePair<int, string> ConductorSelected { get; set; }

        // ТехКолонна
        public double CasingShoeLengthStart { get; set; }
        public double CasingShoeLengthEnd { get; set; }
        public double CasingShoeHeightStart { get; set; }
        public double CasingShoeHeightEnd { get; set; }
        public bool CasingShoeIsCement { get; set; }
        public int CasingShoeIndex { get; set; }
        public KeyValuePair<int, string> CasingShoeSelected { get; set; }
        // Колонна
        public double CasingPipeLengthStart { get; set; }
        public double CasingPipeLengthEnd { get; set; }
        public double CasingPipeHeightStart { get; set; }
        public double CasingPipeHeightEnd { get; set; }
        public int CasingPipeIndex { get; set; }
        public KeyValuePair<int, string> CasingPipeSelected { get; set; }
        //Хвостовик колонны
        public double CasingLinerLengthStart { get; set; }
        public double CasingLinerLengthEnd { get; set; }
        public double CasingLinerHeightStart { get; set; }
        public double CasingLinerHeightEnd { get; set; }
        public int CasingLinerIndex { get; set; }
        public KeyValuePair<int, string> CasingLinerSelected { get; set; }
        //НКТ вехняя подвеска
        public double TubingUpperSuspensionLengthStart { get; set; }
        public double TubingUpperSuspensionLengthEnd { get; set; }
        public double TubingUpperSuspensionHeightStart { get; set; }
        public double TubingUpperSuspensionHeightEnd { get; set; }
        public int TubingUpperSuspensionIndex { get; set; }
        public KeyValuePair<int, string> TubingUpperSuspensionSelected { get; set; }
        //НКТ вехняя подвеска
        public double TubingLowerSuspensionLengthStart { get; set; }
        public double TubingLowerSuspensionLengthEnd { get; set; }
        public double TubingLowerSuspensionHeightStart { get; set; }
        public double TubingLowerSuspensionHeightEnd { get; set; }
        public int TubingLowerSuspensionIndex { get; set; }
        public KeyValuePair<int, string> TubingLowerSuspensionSelected { get; set; }
        
        public readonly SplineInterpolator Scaller;

        public CanvasProps props { get; set; }

        public MainWindowViewModel()
        {
            props = new CanvasProps();
            //Кондуктор
            ConductorLengthStart = 0;
            ConductorLengthEnd = 452;
            ConductorIsCement = true;
            CasingShoeIndex = 0;
           //ЭК
            CasingPipeLengthStart = 0;
            CasingPipeLengthEnd = 3845;
            CasingPipeHeightStart = 0;
            CasingPipeHeightEnd = 3646;
            CasingPipeIndex = 10;
            //Хвостовик
            CasingLinerLengthStart = 3568;
            CasingLinerLengthEnd = 4836;
            CasingLinerHeightStart = 3723;
            CasingLinerHeightEnd = 3808;
            CasingLinerIndex = 3;

            TubingUpperSuspensionLengthStart = 0;
            TubingUpperSuspensionLengthEnd = 3569;
            TubingUpperSuspensionHeightStart = 0;
            TubingUpperSuspensionHeightEnd = 3348.2;
            //TubingUpperSuspensionIndex = 6;

            TubingLowerSuspensionLengthStart = 3569;
            TubingLowerSuspensionLengthEnd = 3969;
            TubingLowerSuspensionHeightStart = 3348;
            TubingLowerSuspensionHeightEnd = 3768;

            //TubingLowerSuspensionIndex = 0;

            var known = new Dictionary<double, double> {
                { 0.0, 0.0 },
                { CasingPipeLengthEnd,  CasingPipeHeightEnd },
                { CasingPipeLengthEnd+CasingLinerLengthEnd, CasingPipeHeightEnd+CasingLinerHeightEnd },
                { TubingUpperSuspensionLengthEnd, TubingUpperSuspensionHeightEnd },
                { TubingUpperSuspensionLengthEnd+TubingLowerSuspensionLengthEnd, TubingUpperSuspensionHeightEnd+TubingLowerSuspensionHeightEnd }
            };
            Scaller = new SplineInterpolator(known.OrderBy(x=>x.Key).ToDictionary(pair => pair.Key, pair => pair.Value));
        }

        public LengthItem[] GetLengthList()
        {
            return new List<LengthItem>
            {
                new LengthItem { Layer = 1, MarkLabel = CasingPipeLengthEnd },
                new LengthItem { Layer = 1, MarkLabel = CasingPipeLengthEnd },
                new LengthItem { Layer = 2, MarkLabel = TubingUpperSuspensionLengthEnd },
                new LengthItem { Layer = 2, MarkLabel = TubingLowerSuspensionLengthEnd }
            }.OrderBy(x => x.MarkLabel).ToArray();
        }

        public void ShowLengthMarker(Canvas canvas)
        {
           

            canvas.Children.Clear();

            AddMarkLevel(canvas, CasingPipeLengthEnd, CasingPipeHeightEnd);
            AddMarkLevel(canvas, CasingLinerLengthEnd, CasingLinerLengthEnd);
            AddMarkLevel(canvas, TubingUpperSuspensionLengthEnd, TubingUpperSuspensionHeightEnd);
            AddMarkLevel(canvas, TubingUpperSuspensionLengthEnd, TubingLowerSuspensionHeightEnd);
            

        }

        private void AddMarkLevel(Panel canvas, double length, double height)
        {
            var sWidth = canvas.ActualWidth;
            var sHeight = canvas.ActualHeight;
            var fHeight = CasingPipeLengthEnd + CasingLinerLengthEnd;
            var dy = sHeight / fHeight;

            var rLine = new Line
            {
                Stroke = Brushes.LightSteelBlue,
                X1 = sWidth - 20,
                X2 = sWidth - 75,
                Y1 = length * dy,
                Y2 = length * dy,
                StrokeThickness = 2
            };
            canvas.Children.Add(rLine);

            var lLine = new Line
            {
                Stroke = Brushes.LightSteelBlue,
                X1 = 20,
                X2 = 75,
                Y1 = length * dy,
                Y2 = length * dy,
                StrokeThickness = 2
            };
            canvas.Children.Add(lLine);

            var textBlock = new TextBlock
            {
                Text = (length).ToString("F0"),
                Foreground = new SolidColorBrush(Colors.Brown)
            };
            Canvas.SetLeft(textBlock, 30);
            Canvas.SetTop(textBlock, length * dy);
            canvas.Children.Add(textBlock);

            var heightBlock = new TextBlock
            {
                Text = (height).ToString("F0"),
                Foreground = new SolidColorBrush(Colors.Brown)
            };
            Canvas.SetLeft(heightBlock, sWidth - 65);
            Canvas.SetTop(heightBlock, length * dy);
            canvas.Children.Add(heightBlock);
        }

    }
}
