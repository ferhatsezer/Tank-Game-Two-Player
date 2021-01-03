using System;
using System.Collections.Generic;
using SFMLResim;


namespace Tank
{
    class Tank
    {
        
        public Boolean ozelGucDurum { get; set; }
        private float displayTank;
        public float r { get; set; }
        public float x { get; set; }
        public float y { get; set; }
        public float rotation { get; set; }
        private int Health;
        public int Can
        {
            get { return Health; }
            set
            {
                if (value > 100) Health = 100;
                else if (value < 0) Health = 0;

                else Health = value;
            }
        }
        private int Mermi;
        public int mermiHakki
        {
            get { return Mermi; }
            set
            {
                if (value > 100) Mermi = 100;


                else Mermi = value;
            }
        }
        public Boolean cerceveSoru { get; set; }
        public Boolean kutuDurum { get; set; }
        public int kutuX { get; set; }
        public int kutuY { get; set; }
        public int kutuCan { get; set; }
        public Boolean kalkanDurum { get; set; }
        public Boolean kutuAcik { get; set; }
        public Boolean oyunBitti { get; set; }
        static List<int> tanklar = new List<int>();
        static List<int> ariza = new List<int>();

        int kalanCanDis = Resim.Ekle("img\\ozellik\\kalancan\\kalancanDis.png");
        int mavi = Resim.Ekle("img\\ozellik\\kalancan\\mavi.png");
        int renkli = Resim.Ekle("img\\ozellik\\kalancan\\renkli.png");
        int kalkan = Resim.Ekle("img\\ozellik\\kalkan.png");
        float arizaAnimNo;
        static Tank()
        {
            for (int i = 1; i <= 2; i++)
                tanklar.Add(Resim.Ekle("img\\tanklar\\tank1\\" + i + ".png"));


            for (int i = 14; i >= 0; i--)
                ariza.Add(Resim.Ekle("img\\ariza\\" + i + ".png"));
        }

        public Tank(float r, float x, float y)
        {
            this.r = r;
            this.x = x;
            this.y = y;
            Can = 0;
            displayTank = 0;
            mermiHakki = 30;
            cerceveSoru = false;
            ozelGucDurum = false;
            kutuDurum = false;
            kutuCan = 0;
            kalkanDurum = false;
            kutuAcik = true;
            oyunBitti = false;
            arizaAnimNo = 0;
        }

        public void KalkanCiz()
        {
            if (kalkanDurum) Resim.Ciz(kalkan, x - 45, y - 25, 150, 150);


        }

        public void Ciz(int canx, int cany, int tankNo)
        {
            //Resim.YaziYaz(Can.ToString(), 100, 100, 50);
            if (Can > 50 && !oyunBitti)
            {
                if (arizaAnimNo > 14)
                {
                    arizaAnimNo = 0;
                }
                 Resim.Ciz(ariza[(int)arizaAnimNo], x - 70, y - 50, 200, 200);
                arizaAnimNo += 25 * Resim.FrameTime;
            }

            Resim.Ciz(renkli, canx + 8, cany + 38, 393, 35);
            Resim.Ciz(mavi, canx + 8, cany + 38, Can * 4, 35);
            Resim.Ciz(kalanCanDis, canx, cany, 500, 108);

            if (ozelGucDurum && !oyunBitti) Resim.Ciz(tanklar[(int)displayTank], x, y, 60, 100, rotation, 255, 105, 139, 34);
            else if (tankNo == 1 && !oyunBitti) Resim.Ciz(tanklar[(int)displayTank], x, y, 60, 100, rotation, 255, 255, 48, 48);
            else if (tankNo == 2 && !oyunBitti) Resim.Ciz(tanklar[(int)displayTank], x, y, 60, 100, rotation, 255, 125, 38, 205);
            //Resim.YaziYaz("CAN = " + Can, canx, cany - 30, 50);
            string tank = tankNo == 1 ? "MAVI TANK" : "KIRMIZI TANK";
            if (Can == 100)
            {
                oyunBitti = true;
                Resim.YaziYaz(tank + " KAZANDI", 250, 250, 100);
                Resim.YaziYaz("Tekrar Oynamak Icin ENTER", 380, 500, 50);

               
            }


        }
        public void Display(float t)
        {
            displayTank += 20 * t;
            if ((int)displayTank > tanklar.Count - 1)
                displayTank = 0;
        }


        public void Right()
        {
            if (rotation < 360) rotation += (Resim.FrameTime * 360);
            else rotation = 0;
        }
        public void Left()
        {
            if (rotation > 0) rotation -= (Resim.FrameTime * 360);
            else rotation = 360;
        }
        public void Up(Tank tnk)
        {
            x = x > 20 ? x : 20;
            x = x < 1200 ? x : 1200;
            y = y > 0 ? y : 0;
            y = y < 770 ? y : 770;




            if (x + r * 2 > kutuX && x < kutuX + 35 + r * 2 && y + r * 2 > kutuY - 30 && y < kutuY + 30 + r * 2 && kutuAcik && kutuDurum)
            {
                if (x > kutuX) x++;
                if (x < kutuX) x--;
                if (y > kutuY) y++;
                if (y < kutuY) y--;
            }
            else if (x + r * 2 > 550 && x < 670 + r * 2 && y + r * 2 > 325 && y < 445 + r * 2 && cerceveSoru == false)
            {
                if (x > 550) x++;
                if (x < 670) x--;
                if (y > 325) y++;
                if (y < 445) y--;
            }

            else if (x + r * 2 > tnk.x && x < tnk.x + r * 2 && y + r * 2 > tnk.y && y < tnk.y + r * 2)
            {
                if (x > tnk.x) x++;
                if (x < tnk.x) x--;
                if (y > tnk.y) y++;
                if (y < tnk.y) y--;
            }
            else
            {
                x += (float)((Resim.FrameTime * 300) * Math.Sin(rotation * Math.PI / 180));
                y -= (float)((Resim.FrameTime * 300) * Math.Cos(rotation * Math.PI / 180));
            }
        }

        public void Down(Tank tnk)
        {

            x = x > 20 ? x : 20;
            x = x < 1200 ? x : 1200;
            y = y > 10 ? y : 10;
            y = y < 770 ? y : 770;


            if (x + r * 2 > kutuX && x < kutuX + 35 + r * 2 && y + r * 2 > kutuY - 30 && y < kutuY + 30 + r * 2 && kutuAcik)
            {
                if (x > kutuX) x++;
                if (x < kutuX) x--;
                if (y > kutuY) y++;
                if (y < kutuY) y--;
            }
            else if (x + r * 2 > 550 && x < 670 + r * 2 && y + r * 2 > 325 && y < 445 + r * 2 && cerceveSoru == false)
            {
                if (x > 550) x++;
                if (x < 670) x--;
                if (y > 325) y++;
                if (y < 445) y--;
            }

            else if (x + r * 2 > tnk.x && x < tnk.x + r * 2 && y + r * 2 > tnk.y && y < tnk.y + r * 2)
            {
                if (x > tnk.x) x++;
                if (x < tnk.x) x--;
                if (y > tnk.y) y++;
                if (y < tnk.y) y--;
            }
            else
            {
                x -= (float)((Resim.FrameTime * 300) * Math.Sin(rotation * Math.PI / 180));
                y += (float)((Resim.FrameTime * 300) * Math.Cos(rotation * Math.PI / 180));
            }
        }



    }
}
