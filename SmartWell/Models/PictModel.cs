using SmartWell.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Svg;

namespace SmartWell.Models
{
    class PictModel
    {
        private readonly int X;
        private readonly int Y;
        private Canvas canvas;

        MainWindowViewModel ViewModel { get; set; }

        public PictModel(Canvas cn)
        {
            
            X = 1240; Y = 1754;
            Canvas canvas = new Canvas();
            canvas.Width = X;
            canvas.Height = Y;
            

            canvas.Background = Brushes.LightSteelBlue;

            var svgImage = new Svg.SvgImage
            {
                Width = X,
                Height = Y
            };

            SvgRectangle rect = new SvgRectangle() { Width = 10, Height = 20, X = 50, Y = 44 };
            svgImage.Children.Add(rect);
            

            // Add a "Hello World!" text element to the Canvas
            TextBlock txt1 = new TextBlock();
            txt1.FontSize = 14;
            txt1.Text = "Hello World!";
            Canvas.SetTop(txt1, 100);
            Canvas.SetLeft(txt1, 10);
            canvas.Children.Add(txt1);

            // Add a second text element to show how absolute positioning works in a Canvas
            TextBlock txt2 = new TextBlock();
            txt2.FontSize = 22;
            txt2.Text = "Isn't absolute positioning handy?";
            Canvas.SetTop(txt2, 200);
            Canvas.SetLeft(txt2, 75);
            canvas.Children.Add(txt2);

            var rtb = new RenderTargetBitmap((int)canvas.ActualWidth, (int)canvas.ActualHeight, 100d, 100d, PixelFormats.Default);
            rtb.Render(canvas);

            var crop = new CroppedBitmap(rtb, new Int32Rect(0, 0, (int)canvas.ActualWidth, (int)canvas.ActualHeight));

            BitmapEncoder pngEncoder = new PngBitmapEncoder();
            pngEncoder.Frames.Add(BitmapFrame.Create(crop));

            using (var fs = System.IO.File.OpenWrite("logo.png"))
            {
                pngEncoder.Save(fs);
            }
        }

        public Canvas ReturnCanvas()
        {
            return canvas;
        }
    }
}
