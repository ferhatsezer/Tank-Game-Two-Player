

namespace Tank
{
    class Mermi
    {


        public float R { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public float Rotation { get; set; }
        public Mermi(Tank tank)
        {
            this.R = tank.r;
            this.X = tank.x;
            this.Y = tank.y;
            this.Rotation = tank.rotation;
        }

        //public void MermiCiz(Tank tank)
        //{
        //    this.r = tank.r;
        //    this.x = tank.x;
        //    this.y = tank.y;
        //    this.rotation = tank.rotation;
        //    Resim.Ciz(mermi1,x,y,60,100,rotation);
        //}

    }
}
