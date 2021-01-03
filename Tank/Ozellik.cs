using System;
using System.Collections.Generic;
using SFMLResim;



namespace Tank
{
    class Ozellik
    {
      
        static List<int> ozelGuc = new List<int>();
        static List<int> ozelGucEffect = new List<int>();


        float ozelGucAnimNo = 0, ozelGunEffectAnimNo = 0;
        static Ozellik()
        {
  

            for (int i = 0; i <= 13; i++)
                ozelGuc.Add(Resim.Ekle("img\\ozelGuc\\" + i + ".png"));

            for (int i = 0; i <= 15; i++)
            {
                ozelGucEffect.Add(Resim.Ekle("img\\ozelGuc\\effect\\" + i + ".png"));
            }

       

            int bgSound = SFMLSes.Ses.MusikEkle("sound\\bg.ogg");
     
            SFMLSes.Ses.MusikCal(bgSound);





        }
        private int canSes = SFMLSes.Ses.SesEkle("sound\\can.wav");
        private int mermiSes = SFMLSes.Ses.SesEkle("sound\\mermi.wav");
        private int kalkanSes = SFMLSes.Ses.SesEkle("sound\\kalkan.wav");
        private int kukre = SFMLSes.Ses.SesEkle("sound\\kukreme.wav");
        private int can = Resim.Ekle("img\\ozellik\\can.png");
        private int mermi = Resim.Ekle("img\\ozellik\\mermi.png");
        private int cerceve = Resim.Ekle("img\\ozelGuc\\cerceve.png");
        private int kutu = Resim.Ekle("img\\ozellik\\kutu.png");
        private int kalkan = Resim.Ekle("img\\ozellik\\kalkan.png");
        float canZaman = 0, mermiZaman = 0;
        Random r = new Random();
        int canx = 0, cany = 0, mermix, mermiy, sayacOrtala = 0;
        float ortala = 0;
        float ozellikBuyukluk = 100;
        float ozellikDonus = 0;
        Boolean ozellikBuyuklukSoru = true;




        public void MermiCiz(Tank tank1, Tank tank2)
        {
            ozellikDonus += 20 * Resim.FrameTime;


            if (ozellikBuyuklukSoru == true)
            {
                if (ozellikBuyukluk > 150)
                {
                    ozellikBuyuklukSoru = false;
                }
                else
                {
                    ozellikBuyuklukSoru = true;
                    ozellikBuyukluk += 20 * Resim.FrameTime;
                    sayacOrtala++;
                    if (sayacOrtala == 2)
                    {
                        ortala += 20 * Resim.FrameTime;
                        sayacOrtala = 0;
                    }
                }

            }
            if (ozellikBuyuklukSoru == false)
            {
                if (ozellikBuyukluk < 100)
                {
                    ozellikBuyuklukSoru = true;
                }
                else
                {
                    ozellikBuyuklukSoru = false;
                    ozellikBuyukluk -= 20 * Resim.FrameTime;
                    sayacOrtala++;
                    if (sayacOrtala == 2)
                    {
                        ortala -= 20 * Resim.FrameTime;
                        sayacOrtala = 0;
                    }

                }

            }
            if (mermiZaman > 8.0f && (!tank1.oyunBitti && !tank2.oyunBitti))
            {


                Resim.Ciz(mermi, mermix - ortala, mermiy - ortala, ozellikBuyukluk, ozellikBuyukluk, ozellikDonus);

                if (mermiy + 35 > tank1.y && mermiy + 35 < tank1.y + 70 && mermix + 35 > tank1.x && mermix + 35 < tank1.x + 35 * 2)
                {
                    SFMLSes.Ses.SesCal(mermiSes);
                    mermiZaman = 0;
                    if (tank1.mermiHakki <= 0) tank1.mermiHakki = 30;
                    else tank1.mermiHakki += 30;
                }
                if (mermiy + 35 > tank2.y && mermiy + 35 < tank2.y + 70 && mermix + 35 > tank2.x && mermix + 35 < tank2.x + 35 * 2)
                {
                    SFMLSes.Ses.SesCal(mermiSes);
                    mermiZaman = 0;
                    if (tank2.mermiHakki <= 0) tank2.mermiHakki = 30;
                    else tank2.mermiHakki += 30;
                }


            }
            else
            {
                do
                {
                    mermix = r.Next(50, 1180);
                    mermiy = r.Next(10, 760);
                } while (mermix + 35 * 2 > 550 && mermix < 670 + 35 * 2 && mermiy + 35 * 2 > 325 && mermiy < 445 + 35 * 2);

            }
            mermiZaman += Resim.FrameTime;
        }



        public void CanCiz(Tank tank1, Tank tank2)
        {

            if (canZaman > 5.0f && (!tank1.oyunBitti && !tank2.oyunBitti))
            {



                Resim.Ciz(can, canx - ortala, cany - ortala, ozellikBuyukluk, ozellikBuyukluk, ozellikDonus);

                if (cany + 35 > tank1.y && cany + 35 < tank1.y + 70 && canx + 35 > tank1.x && canx + 35 < tank1.x + 35 * 2)
                {
                    SFMLSes.Ses.SesCal(canSes);
                    tank1.Can -= 20;
                    canZaman = 0;
                }
                if (cany + 35 > tank2.y && cany + 35 < tank2.y + 70 && canx + 35 > tank2.x && canx + 35 < tank2.x + 35 * 2)
                {
                    SFMLSes.Ses.SesCal(canSes);
                    tank2.Can -= 20;
                    canZaman = 0;
                }


            }
            else
            {
                do
                {
                    canx = r.Next(50, 1180);
                    cany = r.Next(10, 760);
                } while (canx + 35 * 2 > 550 && canx < 670 + 35 * 2 && cany + 35 * 2 > 325 && cany < 445 + 35 * 2);


            }
            canZaman += Resim.FrameTime;

        }

        Boolean kalkanDurum = false;
        public void KutuCiz(Tank tank1, Tank tank2)
        {

            if ((tank1.ozelGucDurum || tank2.ozelGucDurum) && tank1.kutuAcik)
            {
                Resim.Ciz(kutu, tank1.kutuX, tank1.kutuY, 100, 100);
                tank1.kutuDurum = true;
                tank2.kutuDurum = true;
                if (tank1.kutuCan == 0)
                {
                    tank1.kutuDurum = false;
                    tank2.kutuDurum = false;
                    tank1.kutuAcik = false;
                    tank2.kutuAcik = false;
                    kalkanDurum = false;
                }


            }
            else if (!tank1.kutuAcik && !tank2.kutuAcik && !kalkanDurum)
            {
                Resim.Ciz(kalkan, tank1.kutuX - 25, tank1.kutuY - 25, 150, 150);

                if (tank1.kutuY + 35 > tank1.y && tank1.kutuY + 35 < tank1.y + 70 && tank1.kutuX + 35 > tank1.x && tank1.kutuX + 35 < tank1.x + 35 * 2)
                {
                    SFMLSes.Ses.SesCal(kalkanSes);
                    kalkanDurum = true;
                    tank1.kalkanDurum = true;

                }
                if (tank1.kutuY + 35 > tank2.y && tank1.kutuY + 35 < tank2.y + 70 && tank1.kutuX + 35 > tank2.x && tank1.kutuX + 35 < tank2.x + 35 * 2)
                {
                
                    SFMLSes.Ses.SesCal(kalkanSes);
                    kalkanDurum = true;
                    tank2.kalkanDurum = true;

                }
            }
            //else if (!tank1.ozelGucDurum && !tank2.ozelGucDurum && tank1.kutuCan == 0)
            //{
            //    tank1.kutuX = tank2.kutuX = r.Next(50, 1180);
            //    tank1.kutuY = tank2.kutuY = r.Next(10, 760);

            //}


        }








        public void OzelGucCiz(Tank tank1, Tank tank2)
        {
            ozelGunEffectAnimNo += 15 * Resim.FrameTime;
            ozelGunEffectAnimNo = ozelGunEffectAnimNo > 15 ? 0 : ozelGunEffectAnimNo;

            if (!tank1.ozelGucDurum && !tank2.ozelGucDurum)
            {
                ozelGucAnimNo = ozelGucAnimNo > 13 ? 0 : ozelGucAnimNo;
                Resim.Ciz(ozelGuc[(int)ozelGucAnimNo], 580, 362, 125, 150);
                ozelGucAnimNo += 15 * Resim.FrameTime;
            }
            if (tank1.ozelGucDurum)
            {
                Resim.Ciz(ozelGucEffect[(int)ozelGunEffectAnimNo], tank1.x - 100, tank1.y - 70, 250, 250);
            }
            if (tank2.ozelGucDurum)
            {
                Resim.Ciz(ozelGucEffect[(int)ozelGunEffectAnimNo], tank2.x - 45, tank2.y - 25, 150, 150);
            }




            if (362 + 35 > tank1.y && 362 + 35 < tank1.y + 70 && 580 + 35 > tank1.x && 580 + 35 < tank1.x + 35 * 2 && !tank2.ozelGucDurum)
            {
                SFMLSes.Ses.SesCal(kukre);
                tank1.ozelGucDurum = true;
                tank1.x = 400;
                tank1.y = 400;
                tank2.x = 800;
                tank2.y = 400;
                tank1.rotation = 0;
                tank2.rotation = 0;
                tank1.kutuAcik = true;
                tank1.kutuCan = 3;
                do
                {
                    tank1.kutuX = tank2.kutuX = r.Next(50, 1180);
                    tank1.kutuY = tank2.kutuY = r.Next(10, 760);
                } while (tank1.kutuX + 35 * 2 > 550 && tank1.kutuX < 670 + 35 * 2 && tank1.kutuY + 35 * 2 > 325 && tank1.kutuY < 445 + 35 * 2);



            }
            else if (362 + 35 > tank2.y && 362 + 35 < tank2.y + 70 && 580 + 35 > tank2.x && 580 + 35 < tank2.x + 35 * 2 && !tank1.ozelGucDurum)
            {
                SFMLSes.Ses.SesCal(kukre);
                tank2.ozelGucDurum = true;
                tank1.x = 400;
                tank1.y = 400;
                tank2.x = 800;
                tank2.y = 400;
                tank1.rotation = 0;
                tank2.rotation = 0;
                tank1.kutuAcik = true;
                tank2.kutuAcik = true;
                tank1.kutuCan = 3;
                do
                {
                    tank1.kutuX = tank2.kutuX = r.Next(50, 1180);
                    tank1.kutuY = tank2.kutuY = r.Next(10, 760);
                } while (tank1.kutuX + 35 * 2 > 550 && tank1.kutuX < 670 + 35 * 2 && tank1.kutuY + 35 * 2 > 325 && tank1.kutuY < 445 + 35 * 2);

            }

        }
        float ozelGucZaman = 0, kontrolZaman = 0;
        public void OzelGucKontrol(Tank tank1, Tank tank2)
        {
            if (kontrolZaman > 10.0f && (tank1.ozelGucDurum || tank2.ozelGucDurum))
            {
                tank1.ozelGucDurum = false;
                tank2.ozelGucDurum = false;
                kalkanDurum = true;
                tank1.kalkanDurum = false;
                tank2.kalkanDurum = false;

                tank1.kutuDurum = false;
                tank1.kutuAcik = false;

                tank2.kutuDurum = false;
                tank2.kutuAcik = false;

            }
            else if (tank1.ozelGucDurum || tank2.ozelGucDurum) kontrolZaman += Resim.FrameTime;
            else kontrolZaman = 0;

        }

        public void OzelGucCerceve(Tank tank1, Tank tank2)
        {
            if ((tank1.ozelGucDurum || tank2.ozelGucDurum))
            {
                ozelGucZaman = 0;
                tank1.cerceveSoru = false;
                tank2.cerceveSoru = false;



            }
            if (ozelGucZaman < 20.0f && (!tank1.oyunBitti && !tank2.oyunBitti))
            {
                Resim.Ciz(cerceve, 540, 336, 200, 200);
                Resim.YaziYaz((20 - (int)ozelGucZaman).ToString(), 600, 10, 50);


            }

            else
            {
                tank1.cerceveSoru = true;
                tank2.cerceveSoru = true;

            }

            ozelGucZaman += Resim.FrameTime;
        }
        public void TekrarOyna(Tank tank1,Tank tank2)
        {
            if (Resim.KeyPressed(SFML.Window.Keyboard.Key.Return) && (tank1.oyunBitti || tank2.oyunBitti) )
            {
                tank1.oyunBitti = false;
                tank2.oyunBitti = false;
                tank1.Can = 0;
                tank2.Can = 0;
                tank1.mermiHakki = 30;
                tank2.mermiHakki = 30;
                tank1.x = 400;
                tank1.y = 400;
                tank2.x = 800;
                tank2.y = 400;
                tank1.rotation = 0;
                tank2.rotation = 0;
                ozelGucZaman = 0;
                kontrolZaman = 0;
                kalkanDurum = false;
                ozellikBuyuklukSoru = false;
                tank1.kutuAcik = true;
                tank2.kutuAcik = true;
                tank1.cerceveSoru = false;
                tank2.cerceveSoru = false;
                tank1.ozelGucDurum = false;
                tank2.ozelGucDurum = false;
                tank1.kutuDurum = false;
                tank2.kutuDurum = false;
                tank1.kalkanDurum = false;
                tank2.kalkanDurum = false;
                canZaman = 0;
                mermiZaman = 0;
            }
        }




    }
}
