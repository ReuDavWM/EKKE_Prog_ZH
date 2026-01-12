using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2024_25
{
    internal class Program
    {
        static void Main(string[] args)
        {

            // A fájl beolvasása UTF-8 kódolással
            StreamReader sr = new StreamReader("MP1_ZH2_2024_25_vizsgak.csv", encoding: Encoding.UTF8);

            // Fejléc kihagyása
            string fejlec = sr.ReadLine();

            // Vizsga objektumokat tároló lista
            List<Vizsga> lista = new List<Vizsga>();

            // CSV beolvasása soronként, amíg van adat
            while (!sr.EndOfStream)
            {
                lista.Add(new Vizsga(sr.ReadLine()));
            }
            // .MMMM visszaadja szóban a hónap nevét, pl: január
            Console.WriteLine($"{DateTime.Today.ToString("MMMM")} elejei 70% fölötti vizsgák");

            Vizsga grafikaVizsga = null;
            Vizsga nyelvekVizsga = null;

            // Vizsgák átvizsgálása
            foreach (Vizsga v in lista)
            {
                string vizsgamod = v.VizsgaMod ? "írásbeli" : "szóbeli";

                // Kiírás, ha a vizsga jövőbeli és 70% fölötti a telítettség
                if (v.Idopont > DateTime.Now && F2_Vizsga_Telitettseg(v) > 70)
                {
                    Console.WriteLine($"{v.Idopont.ToString("MMMM")} {v.Idopont.Day}. {v.Idopont.Hour:D2}:{v.Idopont.Minute:D2} - {v.TantargyKod} - {v.TantargyNev} ({vizsgamod}) - {v.JelentkezokSzama}/{v.MaxLetszam} - {F2_Vizsga_Telitettseg(v)}%");
                }

                // Legutóbbi teljes grafika vizsga keresése
                if (v.TantargyNev == "Bevezetés a számítógépi grafikába" &&
                     v.JelentkezokSzama == v.MaxLetszam)
                {
                    if (grafikaVizsga == null || v.Idopont > grafikaVizsga.Idopont)
                    {
                        grafikaVizsga = v;
                    }
                }

                // Legutóbbi teljes MP1 vizsga keresése
                if (v.TantargyNev == "Magasszintű programozási nyelvek I" &&
                  v.JelentkezokSzama == v.MaxLetszam)
                {
                    if (nyelvekVizsga == null || v.Idopont > nyelvekVizsga.Idopont)
                    {
                        nyelvekVizsga = v;
                    }
                }
            }

            // Összehasonlítás: melyik vizsgán voltak többen
            if (grafikaVizsga.JelentkezokSzama > nyelvekVizsga.JelentkezokSzama)
            {
                Console.WriteLine("A Bevezetés a számítógépi grafikába vizsgán többen vannak.");
            }
            else if (grafikaVizsga.JelentkezokSzama < nyelvekVizsga.JelentkezokSzama)
            {
                Console.WriteLine("A Magasszintű programozási nyelvek 1 vizsgán többen vannak.");
            }
            else
            {
                Console.WriteLine("Mindkét vizsgán ugyanannyian vannak.");
            }

            // Kérdések a felhasználónak
            string[] bemenetek = new string[3];
            string[] kerdesek = new string[3] { "Tantárgy", "Vizsga típusa", "Tagozat" };

            for (int i = 0; i < kerdesek.Length; i++)
            {
                string valasz;
                do
                {
                    Console.Write($"{kerdesek[i]}: ");
                    valasz = Console.ReadLine().Trim();
                }
                while (string.IsNullOrEmpty(valasz));
                bemenetek[i] = valasz;
            }

            // Vizsgatípus eldöntése
            bool keresettTipus = bemenetek[1].ToLower() == "írásbeli";

            // Feltételek szerinti vizsgák kiírása
            foreach (Vizsga v in lista)
            {
                if (v.Idopont > DateTime.Now && F2_Vizsga_Telitettseg(v) < 99 && F5_Kinek(v, bemenetek[0], keresettTipus, bemenetek[2]))
                {
                    Console.WriteLine($"{v.Idopont.ToString("yyyy.MM.dd hh:ss")} - {v.JelentkezokSzama}/{v.MaxLetszam} ({v.Terem})");
                }
            }

            Console.WriteLine("8. feladat");

            // Egyedi tantárgynevek listázása
            List<string> tantargyak = F7_Tantargyak(lista);

            foreach (string tantargy in tantargyak)
            {
                int db = 0;
                Vizsga maxVizsga = null;

                foreach (Vizsga v in lista)
                {
                    if (v.TantargyNev == tantargy && v.MaxLetszam >= 10)
                    {
                        db++;
                        if (maxVizsga == null || v.JelentkezokSzama > maxVizsga.JelentkezokSzama)
                        {
                            maxVizsga = v;
                        }
                    }
                }

                if (maxVizsga != null)
                {
                    string jeloles = db < 3 ? " (!!!)" : "";
                    Console.WriteLine($"{tantargy}: {maxVizsga.Idopont.ToString("yyyy.MM.dd HH:mm")}, {maxVizsga.MaxLetszam} fő, {maxVizsga.Terem}{jeloles}");
                }
            }

            // Ütköző vizsgák keresése
            for (int i = 0; i < lista.Count; i++)
            {
                for (int j = 0; j < lista.Count; j++)
                {
                    if (i == j) continue;

                    if ((lista[i].Idopont == lista[j].Idopont) && (lista[i].Terem == lista[j].Terem))
                    {
                        Console.WriteLine($"{lista[i].TantargyNev} - {lista[i].Idopont}");
                    }
                }
            }

            // Vizsga telítettség százalékos visszaadása
            static int F2_Vizsga_Telitettseg(Vizsga vizsga)
            {
                double telitettseg = (double)vizsga.JelentkezokSzama / vizsga.MaxLetszam * 100;
                return (int)Math.Round(telitettseg);
            }

            // Vizsga keresés adott paraméterek alapján
            static bool F5_Kinek(Vizsga vizsga, string tantargyNev, bool vizsgaTipus, string tagozat)
            {
                return vizsga.TantargyNev == tantargyNev &&
                    vizsga.VizsgaMod == vizsgaTipus &&
                    char.ToLower(vizsga.TantargyKod[0]) == char.ToLower(tagozat[0]);
            }

            // Egyedi tantárgynevek kigyűjtése és visszafelé rendezése
            static List<string> F7_Tantargyak(List<Vizsga> lista)
            {
                List<string> tantargyak = new List<string>();
                foreach (var item in lista)
                {
                    if (!tantargyak.Contains(item.TantargyNev))
                    {
                        tantargyak.Add(item.TantargyNev);
                    }
                }
                tantargyak.Sort();
                tantargyak.Reverse();
                return tantargyak;
            }

        }
    }
    public class Vizsga
    {
        public string TantargyKod { get; set; }
        public string TantargyNev { get; set; }
        public bool VizsgaMod { get; set; }
        public DateTime Idopont { get; set; }
        public int MaxLetszam { get; set; }
        public int JelentkezokSzama { get; set; }
        public string Terem { get; set; }
        public Vizsga(string fajlSor)
        {
            string[] adatok = fajlSor.Split(';');
            TantargyKod = adatok[0];
            TantargyNev = adatok[1];
            VizsgaMod = adatok[2] == "I" ? true : false;
            Idopont = DateTime.Parse(adatok[3]).AddYears(1); // .AddYears(1) - hozzáadtunk 1-et az évhez hogy kapjunk valami eredményt is
            MaxLetszam = int.Parse(adatok[4]);
            JelentkezokSzama = int.Parse(adatok[5]);
            Terem = adatok[6];
        }
    }

}



