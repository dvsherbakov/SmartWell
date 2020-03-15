using SmartWell.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public double ConductorWidth { get; set; }

        // ТехКолонна
        public double CasingShoeLengthStart { get; set; }
        public double CasingShoeLengthEnd { get; set; }
        public bool CasingShoeIsCement { get; set; }
        public int CasingShoeIndex { get; set; }
        public double CasingShoeWidth { get; set; }

        // Колонна
        public double CasingPipeLengthStart { get; set; }
        public double CasingPipeLengthEnd { get; set; }
        public double CasingPipeHeightStart { get; set; }
        public double CasingPipeHeightEnd { get; set; }
        public int CasingPipeIndex { get; set; }
        public double CasingPipeWidth { get; set; }
        public Geomethry CasingPipeGeometry { get; set; }

        //Хвостовик колонны
        public double CasingLinerLengthStart { get; set; }
        public double CasingLinerLengthEnd { get; set; }
        public double CasingLinerHeightStart { get; set; }
        public double CasingLinerHeightEnd { get; set; }
        public int CasingLinerIndex { get; set; }
        public double CasingLinerWidth { get; set; }
        public Geomethry CasingLinerGeometry {get; set;}

        //НКТ вехняя подвеска
        public double TubingUpperSuspensionLengthStart { get; set; }
        public double TubingUpperSuspensionLengthEnd { get; set; }
        public double TubingUpperSuspensionHeightStart { get; set; }
        public double TubingUpperSuspensionHeightEnd { get; set; }
        public int TubingUpperSuspensionIndex { get; set; }
        public double TubingUpperSuspensionWidth { get; set; }
        public Geomethry TubingUpperSuspensionGeometry { get; set; }

        //НКТ вехняя подвеска
        public double TubingLowerSuspensionLengthStart { get; set; }
        public double TubingLowerSuspensionLengthEnd { get; set; }
        public double TubingLowerSuspensionHeightStart { get; set; }
        public double TubingLowerSuspensionHeightEnd { get; set; }
        public int TubingLowerSuspensionIndex { get; set; }
        public double TubingLowerSuspensionWidth { get; set; }
        public Geomethry TubingLowerSuspensionGeometry { get; set; }

        public readonly SplineInterpolator Scaller;

        public CanvasProps props { get; set; }

        readonly Pipes _pipes;

        public VolumesList Volumes { get; set; }
        //public NameList Names;

        public MainWindowViewModel()
        {
            props = new CanvasProps();
            _pipes = new Pipes();

            Volumes = new VolumesList();
            Volumes.Clear();
            //Names = new NameList();

            //Кондуктор
            ConductorLengthStart = 0;
            ConductorLengthEnd = 452;
            ConductorIsCement = true;
            CasingShoeIndex = 0;
            //Тех колонна
            CasingShoeLengthStart = 0;
            CasingShoeLengthEnd = 1649;
            CasingShoeIsCement = true;
            CasingShoeIndex = 16;
            //ConductorSelected =  new KeyValuePair<int, string>( pipes.GetByIndex(40).;
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

            var known = new Dictionary<double, double>
            {
                {0.0, 0.0},
                {CasingPipeLengthEnd, CasingPipeHeightEnd},
                {CasingLinerLengthEnd, CasingLinerHeightEnd},
                {TubingUpperSuspensionLengthEnd, TubingUpperSuspensionHeightEnd},
                {TubingLowerSuspensionLengthEnd, TubingLowerSuspensionHeightEnd}
            };
            Scaller = new SplineInterpolator(known.OrderBy(x => x.Key)
                .ToDictionary(pair => pair.Key, pair => pair.Value));
            
        }

        public LengthItem[] GetLengthList()
        {
            return new List<LengthItem>
            {
                new LengthItem { Layer = 1, MarkLabel = CasingPipeLengthEnd  },
                new LengthItem { Layer = 1, MarkLabel = CasingLinerLengthEnd },
                new LengthItem { Layer = 2, MarkLabel = TubingUpperSuspensionLengthEnd },
                new LengthItem { Layer = 2, MarkLabel = TubingLowerSuspensionLengthEnd }
            }.OrderBy(x => x.MarkLabel).ToArray();
        }

        public void ShowLengthMarker(Canvas canvas)
        {

            canvas.Children.Clear();
            AddMarkLevel(canvas, CasingPipeLengthEnd, CasingPipeHeightEnd);
            AddMarkLevel(canvas, CasingLinerLengthEnd, CasingLinerHeightEnd);
            //AddMarkLevel(canvas, TubingUpperSuspensionLengthEnd, TubingUpperSuspensionHeightEnd);
            AddMarkLevel(canvas, TubingUpperSuspensionLengthEnd, TubingLowerSuspensionHeightEnd);
        }

        private void AddMarkLevel(Panel canvas, double length, double height)
        {
            var rLine = new Line
            {
                Stroke = Brushes.LightSteelBlue,
                X1 = props.width - 20,
                X2 = props.width - 75,
                Y1 = length * props.dy,
                Y2 = length * props.dy,
                StrokeThickness = 2
            };
            canvas.Children.Add(rLine);

            var lLine = new Line
            {
                Stroke = Brushes.LightSteelBlue,
                X1 = 20,
                X2 = 75,
                Y1 = length * props.dy,
                Y2 = length * props.dy,
                StrokeThickness = 2
            };
            canvas.Children.Add(lLine);

            var textBlock = new TextBlock
            {
                Text = (length).ToString("F0"),
                Foreground = new SolidColorBrush(Colors.Brown)
            };
            Canvas.SetLeft(textBlock, 30);
            Canvas.SetTop(textBlock, length * props.dy);
            canvas.Children.Add(textBlock);

            var heightBlock = new TextBlock
            {
                Text = (height).ToString("F0"),
                Foreground = new SolidColorBrush(Colors.Brown)
            };
            Canvas.SetLeft(heightBlock, props.width- 65);
            Canvas.SetTop(heightBlock, length * props.dy);
            canvas.Children.Add(heightBlock);
        }

        public void FreeRect(Panel canvas, double top, double width, double height, int colorNum)
        {
            var color = VolumeGradients.Volumes()[colorNum];
            var rectangle = new Rectangle
            {
                Stroke = new SolidColorBrush(color),
                StrokeThickness = 2,
                Fill = new VolumeGradient(color).GetValue(),
                Width =  props.dx * width,
                Height = height* props.dy
            };
            Canvas.SetLeft(rectangle, props.width/ 2 - props.dx * width / 2);
            Canvas.SetTop(rectangle, top* props.dy);
            canvas.Children.Add(rectangle);
        }

        private void HatchingRect(Panel canvas, double top, double width, double height, int colorNum)
        {
            var rectangle = new Rectangle
            {
                Stroke = new SolidColorBrush(Colors.Black),
                StrokeThickness = 2,
                Fill = HatchingGradient.Volumes()[colorNum],
                Width = props.dx * width,
                Height = height * props.dy
            };
            Canvas.SetLeft(rectangle, props.width / 2 - props.dx * width / 2);
            Canvas.SetTop(rectangle, top * props.dy);
            canvas.Children.Add(rectangle);
        }

        private void GenerateConductor(Panel canvas)
        {
            if (ConductorIsCement)
                HatchingRect(canvas, 0, ConductorWidth, ConductorLengthEnd, 0);
            else
                FreeRect(canvas, 0, ConductorWidth, ConductorLengthEnd, 8);
        }

        private void GenerateTechnical(Panel canvas)
        {
            if (CasingShoeIsCement)
                HatchingRect(canvas, 0, CasingShoeWidth, CasingShoeLengthEnd, 1);
            else 
                FreeRect(canvas, 0, CasingShoeWidth, CasingShoeLengthEnd, 7);
        }

        private void GenerateCasing(Panel canvas)
        {
            var heights = GetLengthList();
            for (var i = 0; i < heights.Length; i++)
            {
                var top = i == 0 ? 0 : heights[i - 1].MarkLabel;
                var width = top < CasingPipeLengthEnd ? CasingPipeWidth : CasingLinerWidth;
                var geometry = top < CasingPipeLengthEnd ? CasingPipeGeometry : CasingLinerGeometry;
                var geom = new Geomethry(geometry.GetDOut(), geometry.ReturnW(), heights[i].MarkLabel - top);
                Volumes.Add(new VolumeItem { Id = Volumes.Count + 1, PipeProps = geom, Top = top, Width = width, Height = heights[i].MarkLabel - top });
                FreeRect(canvas, top, width, heights[i].MarkLabel-top, i);
            }
        }

        private void GenerateTubing(Panel canvas)
        {
            var heights = GetLengthList().Where(x => x.MarkLabel <= TubingLowerSuspensionLengthEnd).ToArray();
            for (var i = 0; i < heights.Length; i++)
            {
                var top = i == 0 ? 0 : heights[i - 1].MarkLabel;
                var width = top < TubingUpperSuspensionLengthEnd
                    ? TubingUpperSuspensionWidth
                    : TubingLowerSuspensionWidth;
                var geometry = top < TubingUpperSuspensionLengthEnd ? TubingUpperSuspensionGeometry : TubingLowerSuspensionGeometry;
                var geom = new Geomethry(geometry.GetDOut(), geometry.ReturnW(), heights[i].MarkLabel - top);
                Volumes.Add(new VolumeItem { Id = Volumes.Count + 1, PipeProps = geom, Top = top, Width = width, Height = heights[i].MarkLabel - top });
                FreeRect(canvas, top, width, heights[i].MarkLabel - top, 4+i);
            }
        }

        public void GenerateSchema(Panel canvas)
        {
            Volumes.Clear();
            GenerateConductor(canvas);
            GenerateTechnical(canvas);
            GenerateCasing(canvas);
            GenerateTubing(canvas);
        }

        public double MaxDiam()
        {
            return (new[] { ConductorWidth, CasingShoeWidth, CasingPipeWidth, CasingLinerWidth }).Max();
        }

        public double MaxLength()
        {
            return (new[] { CasingShoeLengthEnd, CasingPipeLengthEnd, CasingLinerLengthEnd }).Max();
        }
    }
}
