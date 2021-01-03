using System;
using System.Collections.Generic;

using SFML.Audio;

namespace SFMLSes
{
    public static class Ses
    {
        private static List<Sound> sesler = new List<Sound>();
        private static List<Music> musikler = new List<Music>();

        public static int SesEkle(string s)
        {
            SoundBuffer sbuffer = new SoundBuffer(s);
            Sound snd = new Sound(sbuffer);

            sesler.Add(snd);

            return sesler.Count - 1;
        }

        public static void SesCal(int n)
        {
            if (n < sesler.Count)
                sesler[n].Play();
        }

        public static int MusikEkle(string s)
        {
            Music m = new Music(s);
            m.Loop = true;

            musikler.Add(m);

            return musikler.Count - 1;
        }

        public static void MusikCal(int n)
        {
            if (n < musikler.Count)
                musikler[n].Play();
        }

        public static void MusikDurdur(int n)
        {
            if (n < musikler.Count)
                musikler[n].Pause();
        }

        public static void MusikBitir(int n)
        {
            if (n < musikler.Count)
                musikler[n].Stop();
        }

        public static void MusikSeviye(int n, int seviye)
        {
            if (seviye < 0)
                seviye = 0;
            if (seviye > 100)
                seviye = 100;

            if (n < musikler.Count)
                musikler[n].Volume = seviye;
        }



    }
}
