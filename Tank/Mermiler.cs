using System;
using System.Collections.Generic;
using SFMLSes;
using SFMLResim;


namespace Tank
{
    class Mermiler
    {
        int tankKalkan = Ses.SesEkle("sound\\tankkalkan.wav");
        int TankOzelAtes = Ses.SesEkle("sound\\ozelates.wav");
        int tankPatla = Ses.SesEkle("sound\\patla.wav");
        int ates = Ses.SesEkle("sound\\ates.wav");
        int patla = Ses.SesEkle("sound\\patla.wav");
        List<Mermi> mermiler1 = new List<Mermi>();
        List<Ates> tankAtes = new List<Ates>();
        //Ates tankAtes = new Ates();
        static private int mermi1 = Resim.Ekle("img\\mermiler\\mermi1\\1.png");
        static private int mermi2 = Resim.Ekle("img\\mermiler\\mermi2\\1.png");
        float zaman = 0;
        float ozelGucZaman = 0;
        public void MermiEkle(Mermi mermi, float zamanAtes, Tank tank)
        {
            if (ozelGucZaman > 0.1f)
            {
                if (tank.ozelGucDurum)
                {
                    Ses.SesCal(TankOzelAtes);
                    mermiler1.Add(mermi);
                }
                ozelGucZaman = 0;
            }
            else if (zaman > 0.2f && !tank.ozelGucDurum)
            {

                if (tank.mermiHakki > 0)
                {
                    Ses.SesCal(TankOzelAtes);
                    mermiler1.Add(mermi);
                    tank.mermiHakki--;
                }
                zaman = 0;
            }
            zaman += zamanAtes;
            ozelGucZaman += zamanAtes;
        }
        //float animNo = 0, kutuCarpmaAnimNo = 0;
        //Boolean animDeger = false, kutuCarpmaAnimDeger = false;
        public void MermileriCiz(Tank tank, Tank gercekTank, Tank tank1)
        {
            //if (animDeger > 0)
            //{
            //    //if (animNo > 15)
            //    //{
            //    //    animDeger--;
            //    //    animNo = 0;
            //    //}
            //    ////Resim.Ciz(kutuCarpma[(int)animNo], tank.x - 70, tank.y - 50, 200, 200);
            //    //Resim.Ciz(kutuCarpma[(int)animNo], tank.x - 40 , tank.y - 25 , 150, 150);
            //    //animNo += 30 * Resim.FrameTime;
            foreach (Ates item in tankAtes)
            {
                if (item.durum)
                {
                    item.Ciz(tank, tank1, gercekTank);
                }
            }
            for (int i = 0; i < tankAtes.Count; i++)
            {
                if (!tankAtes[i].durum)
                {
                    tankAtes.Remove(tankAtes[i]);
                }
            }
            //}
            //if (kutuCarpmaAnimDeger == true)
            //{
            //    if (kutuCarpmaAnimNo > 10)
            //    {
            //        kutuCarpmaAnimDeger = false;
            //        kutuCarpmaAnimNo = 0;
            //    }
            //    Resim.Ciz(kutuCarpma[(int)kutuCarpmaAnimNo], tank1.kutuX - 50, tank1.kutuY - 60, 200, 200);
            //    kutuCarpmaAnimNo += 30 * Resim.FrameTime;


            //}
            for (int i = 0; i < mermiler1.Count; i++)
            {
                if (gercekTank.ozelGucDurum) Resim.Ciz(mermi2, mermiler1[i].X, mermiler1[i].Y, 60, 100, mermiler1[i].Rotation);
                else Resim.Ciz(mermi1, mermiler1[i].X, mermiler1[i].Y, 60, 100, mermiler1[i].Rotation);

                mermiler1[i].X += (float)((Resim.FrameTime * 500) * Math.Sin(mermiler1[i].Rotation * Math.PI / 180));
                mermiler1[i].Y -= (float)((Resim.FrameTime * 500) * Math.Cos(mermiler1[i].Rotation * Math.PI / 180));

                if ((mermiler1[i].Y + 35 > tank.y && mermiler1[i].Y + 35 < tank.y + 70 && mermiler1[i].X + 35 > tank.x && mermiler1[i].X + 35 < tank.x + 35 * 2) && tank.kalkanDurum)
                {
                    // animDeger = true;
                    Ses.SesCal(tankKalkan);
                    mermiler1.Remove(mermiler1[i]);
                }
                else if ((mermiler1[i].Y + 35 > tank.y && mermiler1[i].Y + 35 < tank.y + 70 && mermiler1[i].X + 35 > tank.x && mermiler1[i].X + 35 < tank.x + 35 * 2))
                {
                    //animDeger = true;

                    Ses.SesCal(ates);
                    tankAtes.Add(new Ates(0));
                    mermiler1.Remove(mermiler1[i]);
                    if (gercekTank.ozelGucDurum) tank.Can += 5;
                    else tank.Can += 2;

                    if (tank.Can >= 100) Ses.SesCal(tankPatla);

                }
                else if (mermiler1[i].X > 1280 || mermiler1[i].X < -100 || mermiler1[i].Y > 960 || mermiler1[i].Y < -100)
                {
                    mermiler1.Remove(mermiler1[i]);
                }
                else if (mermiler1[i].X + 20 * 2 > 550 && mermiler1[i].X < 670 + 20 * 2 && mermiler1[i].Y + 20 * 2 > 325 && mermiler1[i].Y < 445 + 20 * 2 && tank.cerceveSoru == false)
                {
                    //if (x + r * 2 > 550 && x < 670 + r * 2 && y + r * 2 > 325 && y < 445 + r * 2)
                    mermiler1.Remove(mermiler1[i]);

                }
                else if (mermiler1[i].X - 10 + 20 * 2 > tank1.kutuX && mermiler1[i].X - 25 < tank1.kutuX + 20 * 2 && mermiler1[i].Y + 10 + 20 * 2 > tank1.kutuY && mermiler1[i].Y - 15 < tank1.kutuY + 20 * 2 && tank1.kutuDurum)
                {
                    if (tank1.kutuCan == 1) Ses.SesCal(patla);
                    else Ses.SesCal(ates);
                    mermiler1.Remove(mermiler1[i]);
                    tank1.kutuCan--;
                    tankAtes.Add(new Ates(1));
                }
            }
        }
        public void MermiHakkiYaz(string isim, int x, int y, Tank tank)
        {
            if (zaman > 0.1f && tank.mermiHakki <= 0)
            {
                tank.mermiHakki--;
                zaman = 0;
            }
            if (tank.mermiHakki <= 0 || tank.ozelGucDurum)
            {
                if (tank.mermiHakki % 2 == 0 || tank.ozelGucDurum)
                {
                    if (tank.ozelGucDurum) Resim.YaziYaz("SINIRSIZ MERMI", x, y - 30, 50);
                    else
                    {
                        Resim.YaziYaz("MERMINIZ BITTI", x, y - 30, 50);
                        Resim.YaziYaz(isim, x + 100, y + 20, 25);
                    }
                }
            }
            else
            {
                Resim.YaziYaz("KALAN MERMI SAYISI = " + " " + tank.mermiHakki.ToString(), x, y - 30, 30);
                Resim.YaziYaz(isim, x, y, 20);
            }
            if (tank.mermiHakki <= 0) zaman += Resim.FrameTime;
        }
    }
}
