using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartWell.Models
{
    public class Pipes : object
    {
        private readonly List<CGeomethry> _columns;

        public Pipes()
        {
            _columns = new List<CGeomethry>
            {
                new CGeomethry {Index = 0, Dout = 140, W = 7.8, Type = 1},
                new CGeomethry {Index = 1, Dout = 146, W = 6.5, Type = 1},
                new CGeomethry {Index = 2, Dout = 146, W = 7, Type = 1},
                new CGeomethry {Index = 3, Dout = 146, W = 7.7, Type = 1},
                new CGeomethry {Index = 4, Dout = 146, W = 8.5, Type = 1},
                new CGeomethry {Index = 5, Dout = 146, W = 9.5, Type = 1},
                new CGeomethry {Index = 6, Dout = 146, W = 10.7, Type = 1},
                new CGeomethry {Index = 7, Dout = 168, W = 7.3, Type = 1},
                new CGeomethry {Index = 8, Dout = 168, W = 8.0, Type = 1},
                new CGeomethry {Index = 9, Dout = 168, W = 8.9, Type = 1},
                new CGeomethry {Index = 10, Dout = 168, W = 10.6, Type = 1},
                new CGeomethry {Index = 11, Dout = 178, W = 8.2, Type = 1},
                new CGeomethry {Index = 12, Dout = 178, W = 9.3, Type = 1},
                new CGeomethry {Index = 13, Dout = 178, W = 10.04, Type = 1},
                new CGeomethry {Index = 14, Dout = 178, W = 10.5, Type = 1},
                new CGeomethry {Index = 15, Dout = 219, W = 8.8, Type = 1},
                new CGeomethry {Index = 16, Dout = 219, W = 10.2, Type = 1},
                new CGeomethry {Index = 17, Dout = 245, W = 11.1, Type = 1},

                new CGeomethry {Index = 18, Dout = 102, W = 6.5, Type = 2},
                new CGeomethry {Index = 19, Dout = 114, W = 6.4, Type = 2},
                new CGeomethry {Index = 20, Dout = 114, W = 7, Type = 2},
                new CGeomethry {Index = 21, Dout = 114, W = 7.4, Type = 2},

                new CGeomethry {Index = 22, Dout = 48, W = 4.0, Type = 3},
                new CGeomethry {Index = 23, Dout = 60, W = 5.0, Type = 3},
                new CGeomethry {Index = 24, Dout = 73, W = 5.5, Type = 3},
                new CGeomethry {Index = 25, Dout = 73, W = 6, Type = 3},
                new CGeomethry {Index = 26, Dout = 73, W = 7, Type = 3},
                new CGeomethry {Index = 27, Dout = 89, W = 6, Type = 3},
                new CGeomethry {Index = 28, Dout = 89, W = 6.5, Type = 3},
                new CGeomethry {Index = 29, Dout = 89, W = 7.34, Type = 3},
                new CGeomethry {Index = 30, Dout = 89, W = 8, Type = 3},
                new CGeomethry {Index = 31, Dout = 102, W = 6.5, Type = 3},
                new CGeomethry {Index = 32, Dout = 114, W = 7, Type = 3}
            };
        }

        public Dictionary<int, string> GetList(int type)
        {
            return _columns.Where(x => x.Type == type).ToDictionary(itm => itm.Index, itm =>
                $"D={itm.Dout} мм, S={itm.W} мм");
        }

        public Geomethry GetByIndex(int i)
        {
            var tmp = _columns.First(x => x.Index == i);
            return new Geomethry(tmp.Dout, tmp.W);
        }
    }
}
