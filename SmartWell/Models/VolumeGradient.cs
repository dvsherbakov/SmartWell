using System.Collections.Generic;
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
            var result = new List<Color>() {
                (Color) ColorConverter.ConvertFromString("#CCCCCC"),
                (Color) ColorConverter.ConvertFromString("#BBBBBB"),
                (Color) ColorConverter.ConvertFromString("#AAAAAA"),
                (Color) ColorConverter.ConvertFromString("#999999"),
                (Color) ColorConverter.ConvertFromString("#888888"),
                (Color) ColorConverter.ConvertFromString("#777777"),
                (Color) ColorConverter.ConvertFromString("#666666"),
                (Color) ColorConverter.ConvertFromString("#555555"),
                (Color) ColorConverter.ConvertFromString("#444444"),
                (Color) ColorConverter.ConvertFromString("#333333"),
                (Color) ColorConverter.ConvertFromString("#ffcccb"),
            }.ToArray();
            return result;
        }
    }

    internal static class HatchingGradient
    {
        private static LinearGradientBrush GenerateGradient(Point start, Point end)
        {
            var stops = new GradientStopCollection
            {
                new GradientStop(Colors.Black, 0),
                new GradientStop(Colors.Black, 0.2),
                new GradientStop(Colors.White, 0.2),
                new GradientStop(Colors.White, 1)
            };

            return new LinearGradientBrush
            {
                EndPoint = end,
                StartPoint = start,
                MappingMode = BrushMappingMode.Absolute,
                SpreadMethod = GradientSpreadMethod.Repeat,
                GradientStops = stops
            };
        }

        public static LinearGradientBrush[] Volumes()
        {
            return new List<LinearGradientBrush>()
            {
                GenerateGradient(new Point(0,0), new Point(4,4)),
                GenerateGradient(new Point(4,0), new Point(0,4)),
                GenerateGradient(new Point(0,4), new Point(4,4)),
                GenerateGradient(new Point(4,0), new Point(4,4)),
            }.ToArray();
        }
    }
}
