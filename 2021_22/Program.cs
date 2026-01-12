using System;

namespace _2021_22
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // A három feladat futtatása egymás után
            NettoBerszamitas(); // 1. feladat
            ValutaFizetes();    // 2. feladat
            SkandinavUgras();   // 3. feladat
        }

        // 1. FELADAT – Nettó bér kiszámítása, gyerekek számával csökkentve az adót
        static void NettoBerszamitas()
        {
            const int MINIMALBER = 2100; // Minimum bruttó bér, ami alá nem lehet menni

            int brutto;
            // Bruttó bér bekérése és validálása
            while (true)
            {
                Console.Write("Add meg a bruttó bért (guba): ");
                if (int.TryParse(Console.ReadLine(), out brutto))
                {
                    if (brutto >= MINIMALBER)
                    {
                        break; // jó érték, kilépünk a ciklusból
                    }
                    else
                    {
                        Console.WriteLine("HIBA: A bruttó bér nem lehet kevesebb a minimálbérnél!");
                    }
                }
                else
                {
                    Console.WriteLine("HIBA: Nem számot adtál meg!");
                }
            }

            // Adókulcs meghatározása bruttó alapján
            double adoSzazalek;
            if (brutto < 4200)
            {
                adoSzazalek = 0.10;
            }
            else if (brutto < 8000)
            {
                adoSzazalek = 0.20;
            }
            else if (brutto < 15000)
            {
                adoSzazalek = 0.30;
            }
            else
            {
                adoSzazalek = 0.40;
            }

            // Gyermekek számának bekérése, szigorú validációval
            int gyerek;
            while (true)
            {
                Console.Write("Hány gyermeket nevelsz? ");
                if (int.TryParse(Console.ReadLine(), out gyerek))
                {
                    if (gyerek >= 0 && gyerek <= 69) // 69 mint maximális érték viccesen magas
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("HIBA: Hibás gyermek szám (0-69 között legyen)!");
                    }
                }
                else
                {
                    Console.WriteLine("HIBA: Nem számot adtál meg!");
                }
            }

            // Adócsökkentés a gyermekek számának függvényében
            double csokkentes;
            if (gyerek == 0) csokkentes = 1.0;
            else if (gyerek == 1) csokkentes = 0.90;
            else if (gyerek == 2) csokkentes = 0.80;
            else if (gyerek == 3) csokkentes = 0.70;
            else csokkentes = 0.60; // 4+ gyerekre

            // Nettó bér kiszámítása és 10-re kerekítése
            double netto = brutto - (brutto * adoSzazalek * csokkentes);
            int nettoKerek = (int)(Math.Round(netto / 10.0)) * 10;

            Console.WriteLine($"A nettó bér: {nettoKerek} guba\n");
        }

        // 2. FELADAT – Egy összeget pénzérmékkel kell befizetni, nem lehet túlfizetés
        static void ValutaFizetes()
        {
            int[] ermek = { 10, 20, 50, 100, 200 }; // Elfogadott érmék
            int osszeg;

            // Összeg bekérése, amit fizetni kell – max 2000, és 10-zel osztható legyen
            while (true)
            {
                Console.Write("Add meg a kifizetendő összeget (max 2000, 10-zel osztható): ");
                if (int.TryParse(Console.ReadLine(), out osszeg))
                {
                    if (osszeg > 0 && osszeg <= 2000 && osszeg % 10 == 0)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("HIBA: Összeg érvénytelen!");
                    }
                }
                else
                {
                    Console.WriteLine("HIBA: Nem számot adtál meg!");
                }
            }

            int maradek = osszeg;

            // Amíg van tartozás, be kell dobni érméket
            while (maradek > 0)
            {
                Console.Write($"Fizess be egy érmét ({string.Join(", ", ermek)}): ");
                int erme;
                if (!int.TryParse(Console.ReadLine(), out erme))
                {
                    Console.WriteLine("HIBA: Nem számot adtál meg!");
                    continue;
                }

                // Ellenőrzés: benne van-e az elfogadott érmék között
                bool ervenyes = false;
                foreach (int e in ermek)
                {
                    if (e == erme)
                    {
                        ervenyes = true;
                        break;
                    }
                }

                if (!ervenyes)
                {
                    Console.WriteLine("Érvénytelen pénzérme!");
                    continue;
                }

                if (erme > maradek)
                {
                    Console.WriteLine("Túlfizetés nem lehetséges!");
                    continue;
                }

                maradek -= erme;
                Console.WriteLine($"Még hátralévő összeg: {maradek} guba");
            }

            Console.WriteLine("Az összeg kifizetésre került!\n");
        }

        // 3. FELADAT – Skandináv ugrás, 5 pont minden versenyzőnek
        static void SkandinavUgras()
        {
            int versenyzok;

            // Legalább 3 versenyző kell
            while (true)
            {
                Console.Write("Hány versenyző indult? ");
                if (int.TryParse(Console.ReadLine(), out versenyzok))
                {
                    if (versenyzok >= 3)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("HIBA: Legalább 3 versenyző szükséges.");
                    }
                }
                else
                {
                    Console.WriteLine("HIBA: Nem számot adtál meg!");
                }
            }

            // Minden versenyző 5 értékelést kap [0.00–5.00] között
            double[] pontok = new double[versenyzok * 5];
            Random rand = new Random();

            for (int i = 0; i < pontok.Length; i++)
            {
                pontok[i] = Math.Round(rand.NextDouble() * 5, 2); // véletlen pontszám
            }

            double[] atlagok = new double[versenyzok]; // versenyzők átlaga

            Console.WriteLine("\nVersenyzők átlagpontszámai:");
            for (int i = 0; i < versenyzok; i++)
            {
                double sum = 0;
                for (int j = 0; j < 5; j++)
                {
                    sum += pontok[i * 5 + j]; // az i. versenyző 5 pontját adjuk össze
                }

                atlagok[i] = sum / 5;
                Console.WriteLine($"{i + 1}. versenyző átlaga: {atlagok[i]:F2}");
            }

            // Legmagasabb pont keresése
            double maxPont = pontok[0];
            int maxVersenyzo = 0;
            for (int i = 1; i < pontok.Length; i++)
            {
                if (pontok[i] > maxPont)
                {
                    maxPont = pontok[i];
                    maxVersenyzo = i / 5;
                }
            }

            Console.WriteLine($"\nLegmagasabb értékelés: {maxPont:F2}, Versenyző: #{maxVersenyzo + 1}");

            // Béna versenyzők: ha minden pontja < 2.00
            Console.WriteLine("\nBéna versenyzők:");
            for (int i = 0; i < versenyzok; i++)
            {
                bool bena = true;
                for (int j = 0; j < 5; j++)
                {
                    if (pontok[i * 5 + j] >= 2.0)
                    {
                        bena = false;
                        break;
                    }
                }
                Console.WriteLine($"{i + 1}. versenyző: {(bena ? "Béna" : "Nem béna")}");
            }

            // Győztes keresése: legmagasabb átlaggal rendelkező
            double maxAtlag = atlagok[0];
            int gyoztes = 0;
            for (int i = 1; i < atlagok.Length; i++)
            {
                if (atlagok[i] > maxAtlag)
                {
                    maxAtlag = atlagok[i];
                    gyoztes = i;
                }
            }

            Console.WriteLine($"\nA győztes a(z) {gyoztes + 1}. versenyző, {maxAtlag:F2} pontos átlaggal.");
        }
    }
}
