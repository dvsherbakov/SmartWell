using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace SmartWell.Models
{
    class VolumeGradient
    {
        private readonly LinearGradientBrush fiveColorLGB;

        public VolumeGradient(Color color)
        {
            fiveColorLGB = new LinearGradientBrush
            {
                StartPoint = new Point(0, 0),
                EndPoint = new Point(0.5, 0),
                SpreadMethod = GradientSpreadMethod.Reflect
            };
            GradientStop blueGS = new GradientStop
            {
                Color = color,
                Offset = 0.0
            };
            fiveColorLGB.GradientStops.Add(blueGS);
            GradientStop orangeGS = new GradientStop
            {
                Color = Colors.LightGray,
                Offset = 0.8
            };
            fiveColorLGB.GradientStops.Add(orangeGS);
        }

        public LinearGradientBrush GetValue()
        {
            return fiveColorLGB;
        }
    }

    static class VolumeGradients
    {
        public static List<VolumeGradient> Volumes()
        {
            return new List<VolumeGradient>() { 
                new VolumeGradient(Colors.BlueViolet),
                new VolumeGradient(Colors.Brown),
                new VolumeGradient(Colors.DarkKhaki),
                new VolumeGradient(Colors.DarkOliveGreen)
            };
        }
    }
}
