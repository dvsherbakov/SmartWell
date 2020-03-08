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
        public Color Color { get; set; }
    }

    public class VolumesList : ObservableCollection<VolumeItem>
    {
       public VolumesList(): base()
        {
           // Add(new VolumeItem { Color = Colors.Red, Id = 1, PipeProps = new Geomethry(120, 3, 1234) });
        }
    }

    public class NameList : ObservableCollection<PersonName>
    {
        public NameList() : base()
        {
            Add(new PersonName("Willa", "Cather"));
            Add(new PersonName("Isak", "Dinesen"));
            Add(new PersonName("Victor", "Hugo"));
            Add(new PersonName("Jules", "Verne"));
        }
    }

    public class PersonName
    {
        private string firstName;
        private string lastName;

        public PersonName(string first, string last)
        {
            this.firstName = first;
            this.lastName = last;
        }

        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }

        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }
    }
}
