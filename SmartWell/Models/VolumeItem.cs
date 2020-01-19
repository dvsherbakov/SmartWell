using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartWell.Models
{
    class VolumeItem
    {
        public int Id { get; }
        private readonly Geomethry Value;

        public VolumeItem(int id, Geomethry geom)
        {
            Id = id;
            Value = geom;
        }

        public double GetLen()
        {
            return Value.GetLen();
        }
    }
}
