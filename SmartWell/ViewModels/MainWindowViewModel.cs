﻿using SmartWell.Models;
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
        // Кондуктор
        public double ConductorLength { get; set; }
        public double ConductorIndex { get; set; }
        public KeyValuePair<int, string> ConductorSelected { get; set; }

        // Башкмак колонны
        public double CasingShoeLength { get; set; }
        public double CasingShoeHeight { get; set; }
        public int CasingShoeIndex { get; set; }
        public KeyValuePair<int, string> CasingShoeSelected { get; set; }
        // Колонна
        public double CasingPipeLength { get; set; }
        public double CasingPipeHeight { get; set; }
        public int CasingPipeIndex { get; set; }
        public KeyValuePair<int, string> CasingPipeSelected { get; set; }
        //Хвостовик колонны
        public double CasingLinerLength { get; set; }
        public double CasingLinerHeight { get; set; }
        public int CasingLinerIndex { get; set; }
        public KeyValuePair<int, string> CasingLinerSelected { get; set; }
        //НКТ вехняя подвеска
        public double TubingUpperSuspensionLength { get; set; }
        public double TubingUpperSuspensionHeight { get; set; }
        public int TubingUpperSuspensionIndex { get; set; }
        public KeyValuePair<int, string> TubingUpperSuspensionSelected { get; set; }
        //НКТ вехняя подвеска
        public double TubingLowerSuspensionLength { get; set; }
        public double TubingLowerSuspensionHeight { get; set; }
        public int TubingLowerSuspensionIndex { get; set; }
        public KeyValuePair<int, string> TubingLowerSuspensionSelected { get; set; }
        
        public SplineInterpolator Scaller;

        public MainWindowViewModel()
        {
            CasingShoeLength = 350.0;
            CasingShoeHeight = 320.0;
            CasingShoeIndex = 17;

            CasingPipeLength = 1850.0;
            CasingPipeHeight = 1530.0;
            CasingPipeIndex = 10;

            CasingLinerLength = 1150.0;
            CasingLinerHeight = 880.0;
            CasingLinerIndex = 3;

            TubingUpperSuspensionLength = 1280.0;
            TubingUpperSuspensionHeight = 1186.0;
            //TubingUpperSuspensionIndex = 6;

            TubingLowerSuspensionLength = 680.0;
            TubingLowerSuspensionHeight = 586.0;
            //TubingLowerSuspensionIndex = 0;

            var known = new Dictionary<double, double> {
                { 0.0, 0.0 },
                { CasingPipeLength,  CasingPipeHeight },
                { CasingPipeLength+CasingLinerLength, CasingPipeHeight+CasingLinerHeight },
                { TubingUpperSuspensionLength, TubingUpperSuspensionHeight },
                { TubingUpperSuspensionLength+TubingLowerSuspensionLength, TubingUpperSuspensionHeight+TubingLowerSuspensionHeight }
            };
            Scaller = new SplineInterpolator(known.OrderBy(x=>x.Key).ToDictionary(pair => pair.Key, pair => pair.Value));
        }

        public LengthItem[] GetLengthList()
        {
            return new List<LengthItem>
            {
                new LengthItem { Layer = 1, MarkLabel = CasingPipeLength },
                new LengthItem { Layer = 1, MarkLabel = CasingPipeLength+CasingLinerLength },
                new LengthItem { Layer = 2, MarkLabel = TubingUpperSuspensionLength },
                new LengthItem { Layer = 2, MarkLabel = TubingUpperSuspensionLength+TubingLowerSuspensionLength }
            }.OrderBy(x => x.MarkLabel).ToArray();
        }

        public static void ShowLengthMarker(Canvas canvas)
        {
            var sWidth = canvas.ActualWidth;
            var sHeight = canvas.ActualHeight;

            canvas.Children.Clear();


        }

    }
}
