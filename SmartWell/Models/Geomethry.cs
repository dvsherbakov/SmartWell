namespace SmartWell.Models
{
    public class Geomethry : object
    {
        //Объект для параметров трубы
        // private int Index { get; set; }
        private double Dout { get; } //Внешний диаметр
        private double W { get; } //Толщина стенки? 
        private double Din { get; } //Внутренний диаметр
        private double Len { get; set; }
        private double Height { get; set; }

        public Geomethry(double pd, double pw)
        {
            //Index = pi;
            Dout = pd;
            W = pw;
            Din = pd - 2 * pw;
        }

        public Geomethry(double pd, double pw, double len)
        {
            Dout = pd;
            W = pw;
            Din = pd - 2 * pw;
            Len = len;
        }

        public Geomethry(CGeomethry g)
        {
            //Index = g.Index;
            Dout = g.Dout;
            W = g.W;
            Din = Dout - 2 * W;
        }

        public string ReturnStr()
        {
            return $"D={Dout} мм, S={W} мм";
        }

        public double ReturnW()
        {
            return W;
        }

        public double RInVolume(double len)
        {
            return 0.25 * System.Math.PI * System.Math.Pow(Din / 1000, 2) * len;
        }

        public double RGetSelfInVolume()
        {
            return 0.25 * System.Math.PI * System.Math.Pow(Din / 1000, 2) * Len;
        }

        public double RMetVolume(double len)
        {
            var res = 0.25 * System.Math.PI * (System.Math.Pow(Dout / 1000, 2) - System.Math.Pow(Din / 1000, 2)) * len;
            return res;
        }

        public void SetLen(double len)
        {
            Len = len;
        }

        public double GetLen()
        {
            return Len;
        }

        public void SetHeight(double height)
        {
            Height = height;
        }

        public double GetHeight()
        {
            return Height;
        }

        public double GetDOut()
        {
            return Dout;
        }
    }
}
