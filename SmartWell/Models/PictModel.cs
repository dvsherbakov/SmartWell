using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Controls;
using SmartWell.ViewModels;
using Color = System.Drawing.Color;

namespace SmartWell.Models
{
    internal class PictModel
    {
        private readonly int _x;
        private readonly int _y;
        private double _dX, _dY;
        private Metafile _mf;
        private readonly HatchBrush[] _gradientsHatch;
        private readonly Color[] _colors;

        private bool _conductorIsCement;
        private double _conductorWidth;
        private double _conductorLengthEnd;
        private bool _casingShoeIsCement;
        private double _casingShoeWidth;
        private double _casingShoeLengthEnd;
        private LengthItem[] _lItem;
        private double _casingPipeLengthEnd;
        private double _casingPipeWidth;
        private double _casingLinerWidth;
        private readonly Font _drawFont;
        private readonly SolidBrush _drawBrush;
        private readonly SolidBrush _drawTextBrush;
        private readonly StringFormat _drawFormat;
        private double _tubingUpperSuspensionLengthEnd;
        private double _tubingUpperSuspensionWidth;
        private double _tubingLowerSuspensionWidth;
        private double _tubingLowerSuspensionLengthEnd;

        public PictModel()
        {
            _x = 625*3;
            _y = 960*3;

            _drawFont = new Font(FontFamily.GenericSansSerif, 12.0F, FontStyle.Regular);
            _drawBrush = new SolidBrush(Color.Black);
            _drawTextBrush = new SolidBrush(Color.Yellow);
            _drawFormat = new StringFormat();

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
                Color.FromArgb(125,125,125),
                Color.FromArgb(75,75,75),
                Color.FromArgb(100,100,100),
                Color.FromArgb(35,35,35),
                Color.FromArgb(120,120,120),
                Color.FromArgb(30,30,30),
                Color.FromArgb(95,95,95),
            }.ToArray();
        }

        private static Metafile MakeMetaFile(float width, float height,
            string filename)
        {
            if (File.Exists(filename)) return null;
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
            var tmpDate = DateTime.Now.ToShortTimeString().Replace(':','_');
            _mf = MakeMetaFile(_x, _y, $"logo-{tmpDate}.emf");
            using (var gr = Graphics.FromImage(_mf))
            {
                gr.SmoothingMode = SmoothingMode.AntiAlias;
                gr.InterpolationMode = InterpolationMode.HighQualityBicubic;
                gr.PixelOffsetMode = PixelOffsetMode.HighQuality;

                if (_conductorIsCement)
                    HatchingRect(gr, 0, _conductorWidth, _conductorLengthEnd, 0);
                else
                    FreeRect(gr, 0, _conductorWidth, _conductorLengthEnd, 8);
                SetLengthMarker(gr, _conductorLengthEnd, _conductorWidth, "Кондуктор");

                if (_casingShoeIsCement)
                    HatchingRect(gr, 0, _casingShoeWidth, _casingShoeLengthEnd, 1);
                else
                    FreeRect(gr, 0, _casingShoeWidth, _casingShoeLengthEnd, 7);
                SetLengthMarker(gr, _casingShoeLengthEnd, _casingShoeWidth, "Тех.Колонна");

                for (var i = 0; i < _lItem.Length; i++)
                {
                    var top = i == 0 ? 0 : _lItem[i - 1].MarkLabel;
                    var width = top < _casingPipeLengthEnd ? _casingPipeWidth : _casingLinerWidth;
                    FreeRect(gr, top, width, _lItem[i].MarkLabel - top, i);
                    SetVolumeText(gr, width, _lItem[i].MarkLabel, i+1);
                    SetLengthMarker(gr, _lItem[i].MarkLabel, width, width.ToString(CultureInfo.CurrentCulture));
                }

                var heights = _lItem.ToList().Where(x => x.MarkLabel <= _tubingLowerSuspensionLengthEnd).ToArray();
                for (var i = 0; i < heights.Length; i++)
                {
                    var top = i == 0 ? 0 : heights[i - 1].MarkLabel;
                    var width = top < _tubingUpperSuspensionLengthEnd ? _tubingUpperSuspensionWidth : _tubingLowerSuspensionWidth;
                    SetVolumeText(gr, width, _lItem[i].MarkLabel, i + 5);
                    FreeRect(gr, top, width, heights[i].MarkLabel - top, 4 + i);
                }

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

            }
        }

        private void SetLengthMarker(Graphics g, double len, double width, string additionText)
        {
            g.DrawString((len).ToString(CultureInfo.InvariantCulture), _drawFont, _drawBrush, _x-150, (float)(len * _dY), _drawFormat);
            g.DrawString(additionText, _drawFont, _drawBrush, _x - 150, (float)(len * _dY-20), _drawFormat);
            var pen = new Pen(_colors[0]);
            g.DrawLine(pen, new PointF((float)(_x / 2 + _dX * width / 2+15), (float)(len * _dY)), new PointF(_x - 165, (float)(len * _dY)));

        }

        private void FreeRect(Graphics g, double top, double width, double height, int colorNum)
        {
            g.FillRectangle(GenerateBrush(colorNum), new Rectangle((int)(_x / 2 - _dX * width / 2), (int)(top*_dY), (int)(width*_dX), (int)(height*_dY)));
            //g.DrawString((top+height).ToString(CultureInfo.InvariantCulture), _drawFont, _drawBrush, 150, (float)((top + height) * _dY), _drawFormat);
        }

        private void HatchingRect(Graphics g, double top, double width, double height, int colorNum)
        {
            g.FillRectangle(_gradientsHatch[colorNum], new Rectangle((int)(_x / 2 - _dX * width / 2), (int)(top*_dY), (int)(width * _dX), (int)(height * _dY)));
           // g.DrawString((top + height).ToString(CultureInfo.InvariantCulture), _drawFont, _drawBrush, 150, (float)((top + height)*_dY), _drawFormat);
        }

        private void SetVolumeText(Graphics g, double x, double y, int volumeNum)
        {
            g.DrawString($"V{volumeNum}", _drawFont, _drawTextBrush, (float)(_x / 2 + _dX * x / 2 - 25), (float)(y * _dY - 20), _drawFormat);
        }

        public void GeneratePict(MainWindowViewModel vm)
        {
            _dX = _x / 3 / vm.MaxDiam();
            _dY = _y *0.9/ vm.MaxLength();
            _conductorIsCement = vm.ConductorIsCement;
            _conductorWidth = vm.ConductorWidth;
            _conductorLengthEnd = vm.ConductorLengthEnd;
            _casingShoeIsCement = vm.CasingShoeIsCement;
            _casingShoeWidth = vm.CasingShoeWidth;
            _casingShoeLengthEnd = vm.CasingShoeLengthEnd;
            _lItem = vm.GetLengthList();
            _casingPipeLengthEnd = vm.CasingPipeLengthEnd;
            _casingPipeWidth = vm.CasingPipeWidth;
            _casingLinerWidth = vm.CasingLinerWidth;
            _tubingUpperSuspensionLengthEnd = vm.TubingUpperSuspensionLengthEnd;
            _tubingUpperSuspensionWidth = vm.TubingUpperSuspensionWidth;
            _tubingLowerSuspensionWidth = vm.TubingLowerSuspensionWidth;
            _tubingLowerSuspensionLengthEnd = vm.TubingLowerSuspensionLengthEnd;
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
