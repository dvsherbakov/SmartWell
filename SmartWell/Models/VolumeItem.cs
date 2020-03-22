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
        public double Top { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public bool IsChecked { get; set; }
    }

    public class VolumesList : ObservableCollection<VolumeItem>
    {
       
    }

   
}
