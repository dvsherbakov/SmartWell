using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using SmartWell.ViewModels;
using Color = System.Drawing.Color;

namespace SmartWell.Models
{
    internal class PictModel
    {
        private readonly int _x;
        private readonly int _y;
        private double _dX, _dY;
        private readonly Metafile _mf;
        private readonly HatchBrush[] _gradientsHatch;
        private readonly Color[] _colors;

        private bool ConductorIsCement;
        private double ConductorWidth;
        private double ConductorLengthEnd;

        public PictModel()
        {
            _x = 625*3;
            _y = 960*3;
           
            _mf = MakeMetaFile(_x, _y, "logo.emf");
            _gradientsHatch = new List<HatchBrush> { 
                new HatchBrush(HatchStyle.DarkDownwardDiagonal, Color.Black, Color.White),
                new HatchBrush(HatchStyle.DarkUpwardDiagonal, Color.Black, Color.White),
                new HatchBrush(HatchStyle.DarkHorizontal, Color.Black, Color.White),
                new HatchBrush(HatchStyle.BackwardDiagonal, Color.Black, Color.White)
            }.ToArray();

            _colors = new List<Color>
            {
                Color.FromArgb(150,150,150),
                Color.FromArgb(50,50,50),
                Color.FromArgb(175,175,175),
                Color.FromArgb(75,75,75),
                Color.FromArgb(100,100,100),
                Color.FromArgb(35,35,35),
                Color.FromArgb(120,120,120)
            }.ToArray();
        }

        private static Metafile MakeMetaFile(float width, float height,
            string filename)
        {
            using (var bm = new Bitmap(16, 16))
            {
                using (var gr = Graphics.FromImage(bm))
                {
                    var bounds =
                        new RectangleF(0, 0, width, height);

                    Metafile mf;
                    if (!string.IsNullOrEmpty(filename))
                        mf = new Metafile(filename, gr.GetHdc(),
                            bounds, MetafileFrameUnit.Pixel);
                    else
                        mf = new Metafile(gr.GetHdc(), bounds,
                            MetafileFrameUnit.Pixel);

                    gr.ReleaseHdc();
                    return mf;
                }
            }
        }

        private void DrawOnMetaFile()
        {
            using (var gr = Graphics.FromImage(_mf))
            {
                gr.SmoothingMode = SmoothingMode.AntiAlias;
                gr.InterpolationMode = InterpolationMode.HighQualityBicubic;
                gr.PixelOffsetMode = PixelOffsetMode.HighQuality;

                if (ConductorIsCement)
                    HatchingRect(gr, 0, ConductorWidth, ConductorLengthEnd, 0);
                else
                    FreeRect(gr, 0, ConductorWidth, ConductorLengthEnd, 8);

                //gr.FillRectangle(_gradients[0], new Rectangle(50, 50, _x - 50*2, _y - 50*2));
                //gr.FillRectangle(_gradients[1], new Rectangle(100, 100, _x - 100 * 2, _y - 100 * 2));
                //gr.FillRectangle(_gradients[2], new Rectangle(150, 150, _x - 150 * 2, _y - 150 * 2));
                //gr.FillRectangle(_gradients[3], new Rectangle(200, 200, _x - 200 * 2, _y - 200 * 2));

                //using (Brush brush = new SolidBrush(
                //    Color.FromArgb(128, 128, 128, 255)))
                //{
                //    gr.FillEllipse(brush, 25, 5, 50, 90);
                //}
                //Point[] points =
                //{
                //    new Point(50, 5),
                //    new Point(94, 50),
                //    new Point(50, 94),
                //    new Point(5, 50),
                //};
                //gr.DrawPolygon(Pens.Blue, points);

                var drawFont = new Font(FontFamily.GenericSansSerif,16.0F, FontStyle.Regular);
                var drawBrush = new SolidBrush(Color.Black);
                const float x = 150.0F;
                var y = 50.0F;
                var drawFormat = new StringFormat();
                gr.DrawString("1660", drawFont, drawBrush, x, y, drawFormat);

            }
        }

        private void FreeRect(Graphics g, double top, double width, double height, int colorNum)
        {
            g.FillRectangle(GenerateBrush(colorNum), new Rectangle((int)(_x / 2 - _dX * width / 2), (int)top, (int)(width*_dX), (int)(height*_dY)));
        }

        private void HatchingRect(Graphics g, double top, double width, double height, int colorNum)
        {
            g.FillRectangle(_gradientsHatch[colorNum], new Rectangle((int)(_x / 2 - _dX * width / 2), (int)top, (int)(width * _dX), (int)(height * _dY)));
        }

        public void GeneratePict(MainWindowViewModel vm)
        {
            _dX = _x / 3 / vm.MaxDiam();
            _dY = _y / vm.MaxLength();
            ConductorIsCement = vm.ConductorIsCement;
            ConductorWidth = vm.ConductorWidth;
            ConductorLengthEnd = vm.ConductorLengthEnd;
        }

        private SolidBrush GenerateBrush(int c)
        {
            return new SolidBrush(_colors[c]);
        }

        public void SavePict()
        {
            DrawOnMetaFile();
            
        }

    }
}
