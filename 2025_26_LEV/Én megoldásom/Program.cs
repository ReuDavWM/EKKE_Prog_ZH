/* ELŐRE IS ELNÉZÉST A SOK KOMMENTÉRT! ÍGY TUDOK A VIZSGADRUKKAL MEGKÜDENI */
using static System.Console; // nincs kedvem mindig írni hogy Console.valami
using System.IO; // stream reader miatt kell
using System.Collections.Generic;
using System.Text;
using static System.Convert; // Convert.valamivé-hez sincs
namespace OPQOGE
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Feladat_1();
            Feladat_2();
            Feladat_3();
            Feladat_4();
        }

        static List<Eredmeny> Beolvas(string fajlNev = "eredmenyek.csv") // ha elfelejt adni fájl nevet
        {
            StreamReader sr = new StreamReader(fajlNev, encoding: Encoding.UTF8);
            List<Eredmeny> lista = new List<Eredmeny>();
            string elsoFejlecSor = sr.ReadLine(); // nem fut le jól a fejléc / első sor miatt :(
            while (!sr.EndOfStream)
            {
                // oda adjuk a fájl sorokat az új Eredemeny-nek, majd szétszedi
                string fajlSor = sr.ReadLine();
                lista.Add(new Eredmeny(fajlSor));
            }
            sr.Close(); // fel van töltve a lista, visszadjuk :^)

            return lista;
        }
        static bool Atment(Eredmeny eredmeny)
        {
            bool atment = false;
            // ezt a formulát számológéppel ellenőriztem, kis double típus kényszerítés kellett
            double szazalek = Math.Round(((double)(eredmeny.szobeli + eredmeny.irasbeli) / 150) * 100);
            if (szazalek > 25) // nem kell else, ha 25% alatt van akkor marad false
            {
                atment = true;
            }
            return atment;
        }
        static void Kiir(List<Eredmeny> lista, int ev)
        {
            foreach (Eredmeny eredmeny in lista)
            {
                if (Atment(eredmeny)) // ha átment
                {
                    WriteLine($"{eredmeny.tazon} - MEGFELELT!");
                }
                if (!Atment(eredmeny)) // ha megbukott, jó kis negáció!
                {
                    WriteLine($"{eredmeny.tazon} - NEM FELELT MEG!");
                }
            }
        }

        static void Feladat_1()
        {
            // 1. feladat
            /*
             TLDR; a feladat
            digi érettségi, 150 pont max = szóbeli + írásbeli
             */
            //bekérjük a szintet
            string szint = String.Empty; // "" , csak menőbb B)
            while (!szint.Equals("emelt", StringComparison.OrdinalIgnoreCase) &&
                    !szint.Equals("közép", StringComparison.OrdinalIgnoreCase)
                ) // eMeLt és kÖZÉP is működni fog, nem vagyunk mi szigorúak annyira
            {
                Write("Kérlek add meg az érettségi szintet (közép/emelt): ");
                szint = ReadLine();
                // kellett plusz redundáns ellenőrzés, most így jött össze
                if (!szint.Equals("emelt", StringComparison.OrdinalIgnoreCase) &&
                    !szint.Equals("közép", StringComparison.OrdinalIgnoreCase))
                {
                    WriteLine("HIBA! Rossz input: csak 'közép' és 'emelt' szavakat fogad el a rendszer!");
                }
            }
            // dinamikus bekérdezés, végigiterál a kérdéseken, ha jó, akkor hozzáadja a
            // pontok tömbhöz
            int[] pontok = new int[2];
            string[] kerdesek = new string[2] { "Kérlek add meg az írásbeli pont eredményét: ",
                "Kérlek add meg az szóbeli pont eredményét: " };
            int maxIrasbeli = 100;
            int maxSzobeli = 50;
            if (szint.Equals("emelt", StringComparison.OrdinalIgnoreCase))
            // ha emelt, felülírjuk a ponthatárokat
            {
                maxIrasbeli = 120;
                maxSzobeli = 30;
            }
        // használtam goto-t, szégyellem nagyon de kellett idő szűkében, de kimaxolva a validáció
        nemJo:
            for (int i = 0; i < kerdesek.Length; i++)
            {
                string valasz;
                do
                {
                    Write(kerdesek[i]);
                    valasz = ReadLine();

                } while (!int.TryParse(valasz, out pontok[i]));
                // 0. index az írásbeli, 1. index a szóbeli

                pontok[i] = int.Parse(valasz);
            }
            if (pontok[0] > maxIrasbeli)
            {
                WriteLine($"HIBA! Az írásbeli pontszám nem lehet nagyobb mint {maxIrasbeli} pont!");
                goto nemJo;
            }
            if (pontok[1] > maxSzobeli)
            {
                WriteLine($"HIBA! A szóbeli pontszám nem lehet nagyobb mint {maxSzobeli} pont!");

                goto nemJo;
            }
            WriteLine($"A tanuló {szint} szinten elért írásbeli eredménye {pontok[0]} pont, szóbelin pedig {pontok[1]} pont volt."
                 + $"\nÖsszesen {pontok[0] + pontok[1]} pont.");
        }

        static void Feladat_2()
        {
            // 2. feladat
            string bemenet = String.Empty; // input csak magyarul, kedzek kifogyni a kreatív változó nevekből
            int pontSzam = 0;
            int tanulokSzama = 0;
            bool van150esTanulo = false;
            int eredmenyekOsszeg = 0;
            List<int> tanuloPontok = new List<int>(); // utolsó feladatra nem úsztam meg.. de inkább mint egy tömb, nem tudni mennyi tanuló lesz
            WriteLine("Add meg a tanuló érettségi pontszámát (1 - 150), írj -1 ha ki akarsz lépni");
            do // feltételezük, hogy legalább 1 tanulót beolvasunk...
            {
                Write("Kérlek add meg a tanuló érettségi pontszámát: (1-150)");
                bemenet = ReadLine();
                if (int.TryParse(bemenet, out pontSzam) && pontSzam > 0)
                { // összegzés tétel O_O
                    eredmenyekOsszeg += pontSzam;
                    tanuloPontok.Add(pontSzam);
                    tanulokSzama++; // nem javítom át tanuloPontok.Count-ra utólag
                }
                if (pontSzam > 150)
                {
                    pontSzam = 150; // max pont ha nagyobb számot írunk be
                    van150esTanulo = true;
                }
            } while (!int.TryParse(bemenet, out pontSzam) || pontSzam >= 0 || pontSzam != -1);

            WriteLine($"Sikeresen megadta {tanulokSzama} tanuló eredményeit.");
            if (van150esTanulo) // ha van 150es pontos 
            {
                WriteLine("Van olyan tanuló, aki maximális pontszámot szerzett.");
            }
            if (!van150esTanulo) // ha nincsen 150es pontos 
            {
                WriteLine("Nincs olyan tanuló, aki maximális pontszámot szerzett.");
            }
            // átlag esetén nem szerencsés nullával osztani, szóval kell egy biztonsági öv
            // a legrosszabb pontszám is akkor fut le ha legalább 1 diák van, 0-val nehéz lenne
            int legrosszabb = tanuloPontok[0];
            if (tanulokSzama > 0)
            {
                double atlag = Math.Round((double)(eredmenyekOsszeg / tanulokSzama));
                WriteLine("A tanuló(k) átlag eredménye: {0}", atlag);
                // {0}, ... kiíratás Dr. Király Roland tanárúr tiszteletére <3
                foreach (int pont in tanuloPontok)
                {
                    if (pont < legrosszabb)
                    {
                        legrosszabb = pont;
                    }
                }
                WriteLine($"A legrosszabb tanuló pontszáma: {legrosszabb} pont.");
            }
        }
        static void Feladat_3()
        {
            // 3. Feladat
            // TLDR emelt írásbeli 120 pont, doku vagy táblázat kezelés 35pont
            // adatbázis 35 pont
            // algoritmizálás 50pont
            // tömb, 12 gyerek 3 jegyenként, 36 elemű
            int[] eredmenyek = new int[12 * 3]; // lehetne 36 is beégetve
            Random rnd = new Random();
            // tömb feltölve
            int algo50PontIndex = 0;
            bool van = false;
            string[] tanTargyak = new string[3] { "Dokumentumkészítés vagy Táblázatkezelés", "Adatbázis-kezelés", "Algortimizálás és programozás" };
            int[] osszegek = new int[3]; // itt beleégettem, mert 36 pontszám lesz, 12 összege külön-külön, tehát 3
            for (int i = 0; i < 12; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (j == 0 || j == 1) // j 0 = doku, j 1 = adatbázis, j 2 = algo
                    {
                        eredmenyek[i * 3 + j] = rnd.Next(0, 36);
                        Console.WriteLine(eredmenyek[i * 3 + j]);
                    }
                    if (j == 2) // else if és else-t nem szeretem :(
                    {
                        eredmenyek[i * 3 + j] = rnd.Next(0, 51);
                        eredmenyek[11 * 3 + 2] = 50;

                        // algo az a j 2
                        if (eredmenyek[i * 3 + j] == 50)
                        {
                            Console.WriteLine(eredmenyek[i * 3 + j]);
                            algo50PontIndex = i;
                            van = true;
                        }

                    }
                    osszegek[j] += eredmenyek[i * 3 + j];

                    //switch (j) egy ilyen megoldást is csináltam :)
                    //{
                    //    case 0:
                    //        eredmenyek[i * 3 + j] = rnd.Next(0, 36);
                    //        break;
                    //    case 1:
                    //        eredmenyek[i * 3 + j] = rnd.Next(0, 36);
                    //        break;
                    //    case 2:
                    //        eredmenyek[i * 3 + j] = rnd.Next(0, 51);
                    //        break;
                    //}
                }
            }
            Console.WriteLine(eredmenyek.Length);
            // az indexet megformáztam a biztonság kedvéért, hozzáadtam 1-et, mert kaptam olyat hogy az utolsó az, akkor 11-et írna ki a 12. helyett
            if (van)
            {
                WriteLine($"Van olyen tanuló aki Algoritmizálás és programozásból 50 max pontot ért el, a sorszáma pedig {algo50PontIndex + 1}.");
            }
            if (!van)
            {
                WriteLine("Nincs ilyen tanuló.");
            }
            double[] atlagok = new double[3]; //ugyanaz mint az osszegek, csak elosztva mint 12vel
            for (int i = 0; i < osszegek.Length; i++)
            {
                atlagok[i] = osszegek[i] / 12; //12 diákkal osztunk
            }
            double legrosszabbAtlagu = atlagok[0]; // minimum kiválasztás sem maradhat el
            string legrosszabbTanTargy = tanTargyak[0]; // mármint a legrosszabb átlagú tantárgy, egyik sem rossz :)
            for (int i = 0; i < atlagok.Length; i++) // megoldhattam volna 1 for ciklussal, mindkettőt itt 3ig megy, 16 elmúlt ez a legvége
            {
                if (atlagok[i] < legrosszabbAtlagu)
                {
                    legrosszabbAtlagu = atlagok[i];
                    legrosszabbTanTargy = tanTargyak[i];
                }
            }
            WriteLine($"A legrosszabb átlagú tantárgy a {legrosszabbTanTargy}, {Math.Round(legrosszabbAtlagu)}/120 pontra sikerült átlagban.");
        }

        static void Feladat_4() // Teljes 4. feladat tokkal-vonóval kérem tisztelttel
        {
            List<Eredmeny> lista = Beolvas();
            Kiir(lista, 2025);
        }
    }

    public class Eredmeny // minden is public, nem kell ide this.Ez , this.Az
    {
        public string tazon { get; set; }
        public int ev { get; set; }
        public string szint { get; set; }
        public int irasbeli { get; set; }
        public int szobeli { get; set; }
        public Eredmeny(string fajlSor)
        {
            string[] adatok = fajlSor.Split(';'); // pontosvessző mert csv fájl
            tazon = adatok[0];
            ev = int.Parse(adatok[1]);
            szint = adatok[2];
            irasbeli = int.Parse(adatok[3]);
            szobeli = int.Parse(adatok[4]);
        }
    }
}
/* KÖSZÖNÖM SZÉPEN, EZEK JÓ FELADATOK VOLTAK! :D */
