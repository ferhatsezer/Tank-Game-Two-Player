using SFMLResim;
using SFMLSes;


namespace Tank
{
    static class Program
    {
        static void Main()
        {
            
            Resim.Yeni(1280, 960, "Ferhat SEZER", false);
            int bg = Resim.Ekle("img\\bigbg.png");
           
          
            Resim.SetFPSLimit(60);

            Tank tank1 = new Tank(35, 400, 400);
            Tank tank2 = new Tank(35, 800, 400);
            Mermiler tank1Mermi = new Mermiler();
            Mermiler tank2Mermi = new Mermiler();
            Ozellik ozellik = new Ozellik();
            
            while (Resim.Acik())
            {
                Resim.Baslat();
                Resim.Ciz(bg, 0, 0, 1280, 960);

    
                


                if (Resim.KeyPressed(SFML.Window.Keyboard.Key.D)) tank1.Right();
                if (Resim.KeyPressed(SFML.Window.Keyboard.Key.A)) tank1.Left();
                if (Resim.KeyPressed(SFML.Window.Keyboard.Key.W))
                {
                    if (!tank1.oyunBitti && !tank2.oyunBitti)
                    {
                        tank1.Display(Resim.FrameTime);
                        tank1.Up(tank2);
                    }
                    
                }

                if (Resim.KeyPressed(SFML.Window.Keyboard.Key.S))
                {
                    if (!tank1.oyunBitti && !tank2.oyunBitti)
                    {
                        tank1.Display(Resim.FrameTime);
                        tank1.Down(tank2);
                    }
                   
                }

                if (Resim.KeyPressed(SFML.Window.Keyboard.Key.Right)) tank2.Right();
                if (Resim.KeyPressed(SFML.Window.Keyboard.Key.Left)) tank2.Left();
                if (Resim.KeyPressed(SFML.Window.Keyboard.Key.Up))
                {
                    if (!tank2.oyunBitti && !tank1.oyunBitti)
                    {
                        tank2.Display(Resim.FrameTime);
                        tank2.Up(tank1);
                    }

                }
                if (Resim.KeyPressed(SFML.Window.Keyboard.Key.Down))
                {
                    if (!tank2.oyunBitti && !tank1.oyunBitti)
                    {
                        tank2.Display(Resim.FrameTime);
                        tank2.Down(tank1);
                    }
                   
                }

                if (Resim.KeyPressed(SFML.Window.Keyboard.Key.RControl))
                {
                    if (!tank2.oyunBitti && !tank1.oyunBitti)
                    {
                        tank2Mermi.MermiEkle(new Mermi(tank2), Resim.FrameTime, tank2);
                    }
                   


                }
                if (Resim.KeyPressed(SFML.Window.Keyboard.Key.F))
                {
                    if (!tank2.oyunBitti && !tank1.oyunBitti)
                    {
                        tank1Mermi.MermiEkle(new Mermi(tank1), Resim.FrameTime, tank1);
                    }
                   

                }
                tank1Mermi.MermiHakkiYaz("Kirmizi TANK", 20, 40, tank1);
                tank2Mermi.MermiHakkiYaz("Mavi TANK", 960, 40, tank2);





                tank1.Ciz(20, 850, 1);
                tank2.Ciz(750, 850, 2);
                ozellik.CanCiz(tank1, tank2);
                ozellik.MermiCiz(tank1, tank2);
                ozellik.OzelGucCerceve(tank1, tank2);
                ozellik.OzelGucCiz(tank1, tank2);
                ozellik.KutuCiz(tank1, tank2);
                ozellik.OzelGucKontrol(tank1, tank2);
      
                tank1Mermi.MermileriCiz(tank2, tank1, tank1);
                tank2Mermi.MermileriCiz(tank1, tank2, tank1);
                tank1.KalkanCiz();
                tank2.KalkanCiz();
                ozellik.TekrarOyna(tank1, tank2);
      
                Resim.Bitir();
            }
        }
    }
}
