using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace SmartWell.Models
{
    internal class VolumeGradient
    {
        private readonly LinearGradientBrush _fiveColorLgb;

        public VolumeGradient(Color color)
        {
            _fiveColorLgb = new LinearGradientBrush
            {
                StartPoint = new Point(0, 0),
                EndPoint = new Point(0.5, 0),
                SpreadMethod = GradientSpreadMethod.Reflect
            };
            var blueGs = new GradientStop
            {
                Color = Colors.LightGray,
                Offset = 0.2
            };
            _fiveColorLgb.GradientStops.Add(blueGs);
            var orangeGs = new GradientStop
            {
                Color = color,
                Offset = 0.8
            };
            _fiveColorLgb.GradientStops.Add(orangeGs);
        }

        public LinearGradientBrush GetValue()
        {
            return _fiveColorLgb;
        }
    }

    internal static class VolumeGradients
    {
        public static Color[] Volumes()
        {
            return new List<Color>() { 
                Colors.BlueViolet,
                Colors.Brown,
                Colors.DarkKhaki,
                Colors.DarkOliveGreen,
                Colors.Chocolate,
                Colors.DarkGreen,
                Colors.DarkTurquoise,
                Colors.DarkSalmon
            }.ToArray();
        }
    }

    internal static class HatchingGradient
    {
        public static LinearGradientBrush LinesBrush()
        {
            var stops = new GradientStopCollection
            {
                new GradientStop(Colors.Black, 0),
                new GradientStop(Colors.Black, 0.1),
                new GradientStop(Colors.White, 0.1),
                new GradientStop(Colors.White, 1)
            };

            var res = new LinearGradientBrush
            {
                EndPoint = new Point(0, 0),
                StartPoint = new Point(8,8),
                MappingMode = BrushMappingMode.Absolute,
                SpreadMethod = GradientSpreadMethod.Repeat,
                GradientStops = stops
            };

            return res;
        }
    }
}
