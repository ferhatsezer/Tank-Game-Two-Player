
using System.Collections.Generic;
using SFMLResim;


namespace Tank
{
    class Ates
    {
        public bool durum { get; set; }

        static List<int> buyukCarpma = new List<int>();
        static List<int> kucukCarpma = new List<int>();
        float animNo = 0, kutuCarpmaAnimNo = 0;
        int deger;
        static Ates()
        {
            for (int i = 0; i <= 15; i++)
            {
                buyukCarpma.Add(Resim.Ekle("img\\carpma\\" + i + ".png"));
                kucukCarpma.Add(Resim.Ekle("img\\carpmakucuk\\" + i + ".png"));
            }




        }
        public Ates(int gelenDeger)
        {
            durum = true;
            deger = gelenDeger;

        }

        public void Ciz(Tank tank, Tank tank1, Tank gercekTank)
        {
            if (deger == 0)
            {
                if (animNo > 15)
                {
                    animNo = 0;
                    durum = false;
                }
                if (tank.Can >= 100)
                {
                    Resim.Ciz(buyukCarpma[(int)animNo], tank.x - 165, tank.y - 140, 400, 400);
                    animNo += 20 * Resim.FrameTime;
                }
                //else if(tank.Can >= 100) Resim.Ciz(buyukCarpma[(int)animNo], tank.x - 165, tank.y - 140, 400, 400);
                else if (gercekTank.ozelGucDurum) Resim.Ciz(buyukCarpma[(int)animNo], tank.x - 70, tank.y - 50, 200, 200);
                else Resim.Ciz(kucukCarpma[(int)animNo], tank.x - 40, tank.y - 25, 150, 150);
                if (tank.Can < 100) animNo += 40 * Resim.FrameTime;


            }
            if (deger == 1)
            {
                if (kutuCarpmaAnimNo > 15)
                {
                    kutuCarpmaAnimNo = 0;
                    durum = false;
                }
                if (gercekTank.ozelGucDurum) Resim.Ciz(buyukCarpma[(int)kutuCarpmaAnimNo], tank1.kutuX - 50, tank1.kutuY - 60, 200, 200);
                else Resim.Ciz(kucukCarpma[(int)kutuCarpmaAnimNo], tank.kutuX - 20, tank.kutuY - 25, 150, 150);
                kutuCarpmaAnimNo += 30 * Resim.FrameTime;
            }



        }

    }
}

