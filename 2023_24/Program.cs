using System;

namespace _2023_24
{
    internal class Program
    {
        static void Main(string[] args)
        {
            EgriStrand();
            Bonbonok();
            KinaiKampusz();
        }

        // ========================================================
        // 1. FELADAT – Egri strand belépőár számítása
        // ========================================================
        static void EgriStrand()
        {
            string tipus = "";
            // Jegytípus bekérése, csak "egyéni" vagy "családi" elfogadható
            while (tipus != "egyéni" && tipus != "családi")
            {
                Console.Write("Jegytípus (egyéni / családi): ");
                tipus = Console.ReadLine();
            }

            int ar = 0;

            if (tipus == "egyéni")
            {
                string vanKedvezmeny = "";
                // Kedvezmény meglétének bekérése
                while (vanKedvezmeny != "igen" && vanKedvezmeny != "nem")
                {
                    Console.Write("Van kedvezmény? (igen / nem): ");
                    vanKedvezmeny = Console.ReadLine();
                }

                if (vanKedvezmeny == "igen")
                {
                    string tipusKedvezmeny = "";
                    // Kedvezmény típusa
                    while (tipusKedvezmeny != "diák" && tipusKedvezmeny != "nyugdíjas" && tipusKedvezmeny != "városkártya")
                    {
                        Console.Write("Kedvezmény típusa (diák / nyugdíjas / városkártya): ");
                        tipusKedvezmeny = Console.ReadLine();
                    }

                    if (tipusKedvezmeny == "városkártya") ar = 1500;
                    else ar = 1000;
                }
                else
                {
                    int ora = 0;
                    // Óra bekérése, 9 és 19 között
                    while (ora < 9 || ora > 19)
                    {
                        Console.Write("Hány óra van most? (9-19): ");
                        int.TryParse(Console.ReadLine(), out ora);
                    }

                    if (ora > 16)
                        ar = 1800;
                    else
                        ar = 3800;
                }
            }
            else if (tipus == "családi")
            {
                int gyerekek = 0;
                // Gyermekek számának bekérése 1–68 között
                while (gyerekek < 1 || gyerekek > 68)
                {
                    Console.Write("Hány gyermek jön? (1-68): ");
                    int.TryParse(Console.ReadLine(), out gyerekek);
                }

                if (gyerekek == 1) ar = 9700;
                else if (gyerekek == 2) ar = 12100;
                else ar = 12100 + (gyerekek - 2) * 2400;
            }

            Console.WriteLine($"Fizetendő ár: {ar} Ft\n");
        }

        // ========================================================
        // 2. FELADAT – Bonbonos feladat
        // ========================================================
        static void Bonbonok()
        {
            // Bonbon típusok – csoportosítva ár és tömeg alapján
            string[] alap = { "ét", "tej", "fehér" };            // 150 Ft, 10g
            string[] kozep = { "mogyorós", "diós" };             // 200 Ft, 20g
            string[] draga = { "szilvás", "pisztáciás", "nugátos", "ananászos" }; // 300 Ft, 30g

            string input = "";
            int gramm = 0;
            int ar = 0;

            // Bonbon bekérése ismétlődve
            while (input != "végeztem")
            {
                Console.Write("Add meg a bonbon ízét (végeztem a kilépéshez): ");
                input = Console.ReadLine();

                bool talalat = false;

                // Alap bonbon keresés
                foreach (string s in alap)
                {
                    if (s == input)
                    {
                        gramm += 10;
                        ar += 150;
                        talalat = true;
                        break;
                    }
                }

                // Közepes árkategória
                if (!talalat)
                {
                    foreach (string s in kozep)
                    {
                        if (s == input)
                        {
                            gramm += 20;
                            ar += 200;
                            talalat = true;
                            break;
                        }
                    }
                }

                // Drágább bonbon
                if (!talalat && input != "végeztem")
                {
                    foreach (string s in draga)
                    {
                        if (s == input)
                        {
                            gramm += 30;
                            ar += 300;
                            talalat = true;
                            break;
                        }
                    }
                }

                // Hibakezelés
                if (!talalat && input != "végeztem")
                {
                    Console.WriteLine("HIBA: Ilyen bonbon nem létezik!");
                }
            }

            // Dobozok kiszámítása – 120g / doboz
            int dobozok = (int)Math.Ceiling(gramm / 120.0);
            int teljesAr = ar + (dobozok * 100);

            Console.WriteLine($"\nFelhasznált dobozok: {dobozok} db");
            Console.WriteLine($"Bonbon tömeg összesen: {gramm} g");
            Console.WriteLine($"Teljes ár: {teljesAr} Ft\n");
        }

        // ========================================================
        // 3. FELADAT – Kínai kampusz statisztika
        // ========================================================
        static void KinaiKampusz()
        {
            int emeletek = 0;

            // Emeletszám bekérése – 10 és 25 között
            while (emeletek < 10 || emeletek > 25)
            {
                Console.Write("Add meg az emeletek számát (10-25): ");
                int.TryParse(Console.ReadLine(), out emeletek);
            }

            int[] termek = new int[emeletek * 12]; // Minden emelet 12 terem
            Random rand = new Random();
            int min = 40, max = 15;
            bool vanTeljesenTele = false;
            double osszes = 0;

            for (int i = 0; i < emeletek; i++)
            {
                bool emeletTele = true;
                Console.Write($"{i + 1}. emelet: ");
                for (int j = 0; j < 12; j++)
                {
                    int letszam = rand.Next(15, 41); // 15-40 közötti érték
                    termek[i * 12 + j] = letszam;
                    osszes += letszam;

                    if (letszam < 40) emeletTele = false;
                    if (letszam < min) min = letszam;
                    if (letszam > max) max = letszam;

                    Console.Write(letszam + " ");
                }

                Console.WriteLine();
                if (emeletTele)
                {
                    Console.WriteLine(">> Ez az emelet teljesen tele van!");
                    vanTeljesenTele = true;
                }
            }

            double kapacitas = emeletek * 12 * 40;
            double kihasznaltsag = osszes / kapacitas * 100;

            Console.WriteLine($"\nKihasználtság: {Math.Round(kihasznaltsag, 2)}%");
            Console.WriteLine($"Különbség a legnagyobb és legkisebb teremlétszám között: {max - min}");

            // Van-e olyan terem, ahol pontosan 15 fő van?
            bool vanMinimumnal = false;
            for (int i = 0; i < termek.Length; i++)
            {
                if (termek[i] == 15)
                {
                    vanMinimumnal = true;
                    break;
                }
            }

            if (vanMinimumnal)
            {
                Console.WriteLine(">> Van terem, amely pontosan 15 fővel működik.");
            }
        }
    }
}
