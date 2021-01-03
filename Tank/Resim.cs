using System;
using System.Collections.Generic;
using System.Diagnostics;

using SFML.Window;
using SFML.Graphics;
using SFML.System;

namespace SFMLResim
{

    public static class Resim
    {
        private static RenderWindow window;
        private static List<Sprite> sprites;
        private static Font fnt;
        private static Text text;
        private static Clock clk;

        public static float FrameTime {get; private set; }
        public static Vector2i FarePos { get; private set; }
        public static Vector2i FareDownPos { get; private set; }
        public static Vector2i FareUpPos { get; private set; }
        public static bool FareDown { get; private set; }
        public static bool FareUp { get; private set; }
       
        private static void Kapat(object sender, EventArgs e)
        {
            RenderWindow window = (RenderWindow)sender;
            window.Close();
        }

        static void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.Button == Mouse.Button.Left)
            {
                FareDown = true;
                FareDownPos = new Vector2i(e.X, e.Y);
            }
        }

        static void OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (e.Button == Mouse.Button.Left)
            {
                FareUp = true;
                FareUpPos = new Vector2i(e.X, e.Y);
            }
        }

        public static void Yeni(uint en, uint boy, string s, bool tamEkran=false)
        {
            if (tamEkran)
                window = new RenderWindow(new VideoMode(en, boy), s, Styles.Fullscreen);
            else
                window = new RenderWindow(new VideoMode(en, boy), s, Styles.Close);

            clk = new Clock();
            FrameTime = 0;

            window.Closed += new EventHandler(Kapat);
            window.MouseButtonPressed += new EventHandler<MouseButtonEventArgs>(OnMouseDown);
            window.MouseButtonReleased += new EventHandler<MouseButtonEventArgs>(OnMouseUp);

            fnt = new Font("font.ttf");
            text = new Text("", fnt, 10);
            text.Style = Text.Styles.Bold;

            sprites = new List<Sprite>();

            FarePos = new Vector2i();
            FareDown = false;
            FareUp = false;
        }

        public static void Temizle()
        {
            sprites.Clear();
        }

        public static int Ekle(string s)
        {
            Texture texture = new Texture(s);
            texture.Smooth = true;

            Sprite sprite = new Sprite(texture);
            sprites.Add(sprite);

            return sprites.Count - 1;
        }

        public static void Ciz(int n, float x, float y, float en, float boy)
        {
            if (n < sprites.Count)
            {
                FloatRect r = sprites[n].GetLocalBounds();
                float tx = r.Width;
                float ty = r.Height;

                sprites[n].Origin = new Vector2f(0, 0);
                sprites[n].Scale = new Vector2f(en / tx, boy / ty);
                sprites[n].Position = new Vector2f(x, y);
                sprites[n].Rotation = 0;
                sprites[n].Color = new Color(255, 255, 255, 255);

                window.Draw(sprites[n]);
            }
        }

        public static void Ciz(int n, float x, float y, float en, float boy, float a)
        {
            if (n < sprites.Count)
            {
                FloatRect r = sprites[n].GetLocalBounds();
                float tx = r.Width;
                float ty = r.Height;

                sprites[n].Origin = new Vector2f(tx / 2, ty / 2);
                sprites[n].Scale = new Vector2f(en / tx, boy / ty);
                sprites[n].Position = new Vector2f(x + en / 2, y + boy / 2);
                sprites[n].Rotation = a;
                sprites[n].Color = new Color(255, 255, 255, 255);

                window.Draw(sprites[n]);
            }
        }

        public static void Ciz(int n, float x, float y, float en, float boy, float a, int alpha, int r = 255, int g = 255, int b = 255)
        {
            if (n < sprites.Count)
            {
                FloatRect rect = sprites[n].GetLocalBounds();
                float tx = rect.Width;
                float ty = rect.Height;

                sprites[n].Origin = new Vector2f(tx / 2, ty / 2);
                sprites[n].Scale = new Vector2f(en / tx, boy / ty);
                sprites[n].Position = new Vector2f(x + en / 2, y + boy / 2);
                sprites[n].Rotation = a;
                sprites[n].Color = new Color((byte)r, (byte)g, (byte)b, (byte)alpha);
                window.Draw(sprites[n]);
            }
        }

        public static void YaziYaz(string s, int x, int y, uint h)
        {
            text.DisplayedString = s;
            text.CharacterSize = h;

            text.Color = new Color(0, 0, 0, 255);
            text.Position = new Vector2f(x+h/15, y+h/15);
            window.Draw(text);

            text.Color = new Color(255, 255, 255, 255);
            text.Position = new Vector2f(x, y);
            window.Draw(text);
        }

        public static void YaziYaz(string s, int x, int y, uint h, int r, int g, int b, int a=255)
        {
            text.Position = new Vector2f(x, y);
            text.Color = new Color((byte)r, (byte)g, (byte)b, (byte)a);
            text.DisplayedString = s;
            text.CharacterSize = h;

            window.Draw(text);
        }

        public static void DogruCiz(int n, float x1, float y1, float x2, float y2, float k)
        {
            if (n < sprites.Count)
            {
                FloatRect r = sprites[n].GetLocalBounds();
                float tx = r.Width;
                float ty = r.Height;

                float a = (float)Math.Atan2(y2 - y1, x2 - x1);
                a = (180 * a) / (float)Math.PI;

                float b = (float)Math.Sqrt((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2));
                sprites[n].Origin = new Vector2f(0, ty / 2);
                sprites[n].Scale = new Vector2f(b / tx, k / ty);
                sprites[n].Position = new Vector2f(x1, y1);
                sprites[n].Rotation = a;
                
                window.Draw(sprites[n]);
            }
        }

        public static bool Acik()
        {
            return window.IsOpen;
        }

        public static void Baslat()
        {
            window.DispatchEvents();
            if (Keyboard.IsKeyPressed(Keyboard.Key.Escape))
                window.Close();

            FarePos = Mouse.GetPosition(window);

            FrameTime = clk.Restart().AsSeconds();
        }

        public static bool KeyPressed(Keyboard.Key k)
        {
            return Keyboard.IsKeyPressed(k);
        }

        public static void Bitir()
        {
            FareDown = false;
            FareUp = false;
            window.Display();
        }

        public static bool FareSol()
        {
            return Mouse.IsButtonPressed(Mouse.Button.Left);
        }

        public static bool FareSag()
        {
            return Mouse.IsButtonPressed(Mouse.Button.Right);
        }

        public static void SetFPSLimit(uint n)
        {
            window.SetFramerateLimit(n);
        }

    }

}
