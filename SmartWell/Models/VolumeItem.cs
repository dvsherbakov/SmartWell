using System.Collections.ObjectModel;
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
        public double Top;
        public double Width;
        public double Height;
    }

    public class VolumesList : ObservableCollection<VolumeItem>
    {
       
    }

   
}
