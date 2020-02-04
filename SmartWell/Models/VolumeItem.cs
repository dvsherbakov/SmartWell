using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace SmartWell.Models
{
    public class LengthItem
    {
        public int Layer { get; set; }
        public double MarkLabel { get; set; }
    }

    public class VolumeItem
    {
        public int Id { get; set; }
        public Geomethry PipeProps { get; set; }
        public Color Color { get; set; }
    }
}
