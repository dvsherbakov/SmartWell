using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using SvgNet;
using SvgNet.SvgElements;
using SvgNet.SvgTypes;
using Color = System.Drawing.Color;
using Pen = System.Drawing.Pen;

namespace SmartWell.Models
{
    internal class PictModel
    {
        private readonly int X;
        private readonly int Y;
        private readonly SvgSvgElement _root;

        public PictModel()
        {
            X = 1240;
            Y = 1754;
            _root = new SvgSvgElement("210mm", "297mm", $"0,0, {X}, {Y}");
        }

        public void GeneratePict()
        {
            _root.AddChildren(
                new SvgRectElement(5, 5, X-5, Y-5) {Style = new SvgStyle(new Pen(Color.Bisque,3))},
               
                new SvgAElement("https://github.com/managed-commons/SvgNet").AddChildren(
                    new SvgTextElement("50", 50, 50){Style = "fill:midnightblue;stroke:navy;stroke-width:0.5px;font-size:12px;font-family:Arial"},
                    new SvgTextElement("100", 50, 100){Style = "font-style:normal;font-weight:normal;font-size:3.52777767px;line-height:1.25;font-family:sans-serif;letter-spacing:0px;word-spacing:0px;fill:#000000;fill-opacity:1;stroke:none;stroke-width:0.26458332"},
                    new SvgTextElement("150", 50, 150){Style = "fill:midnightblue;font-weight:100;stroke-width:1.5px;font-size:18px;font-family:Arial" }
                    )
            );
        }

        public void SavePict()
        {
            File.WriteAllText("logo.svg", _root.WriteSVGString(false));
        }

        public SvgSvgElement ReturnCanvas()
        {
            return _root;
        }
    }
}
