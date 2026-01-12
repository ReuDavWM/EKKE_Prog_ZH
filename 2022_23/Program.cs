using System;
using System.Collections.Generic;

namespace ZH_2022_23
{
    internal class Program
    {
        static void Main()
        {
            Benzinkut();      // 1. feladat
            Pizzeria();       // 2. feladat
            FenyofaTermeles(); // 3. feladat
        }

        // 1. FELADAT – Benzinkút szimuláció
        static void Benzinkut()
        {
            Console.WriteLine("=== 1. feladat: Benzinkút ===");

            // Típus bekérés és ellenőrzés
            string tipus;
            do
            {
                Console.Write("Gépjármű típusa (ceg/magan): ");
                tipus = Console.ReadLine();
            } while (tipus != "ceg" && tipus != "magan");

            // Benzin típus bekérése
            int benzin;
            do
            {
                Console.Write("Benzin típusa (95/100): ");
            } while (!int.TryParse(Console.ReadLine(), out benzin) || (benzin != 95 && benzin != 100));

            // Tankolt mennyiség bekérése
            double liter;
            do
            {
                Console.Write("Hány litert tankoltál? (1 - 50): ");
            } while (!double.TryParse(Console.ReadLine(), out liter) || liter < 1 || liter > 50);

            // Árak és pontok kiszámítása
            int literAr = 0;
            int pontSzam = 0;
            if (benzin == 95)
            {
                literAr = tipus == "ceg" ? 800 : 480;
                pontSzam = 1;
            }
            else
            {
                literAr = 830;
                pontSzam = 5;
            }

            int fizetendo = (int)Math.Round(liter * literAr);
            Console.WriteLine($"Fizetendő: {fizetendo} Ft");

            // Pontgyűjtő kártya
            string valasz;
            do
            {
                Console.Write("Van pontgyűjtő kártyád? (igen/nem): ");
                valasz = Console.ReadLine();
            } while (valasz != "igen" && valasz != "nem");

            if (valasz == "igen")
            {
                int pontok = (int)(liter * pontSzam);
                Console.WriteLine($"Jóváírt pontok: {pontok}");
            }

            Console.WriteLine("Köszönjük a vásárlást!\n");
        }

        // 2. FELADAT – MixAPizza étterem
        static void Pizzeria()
        {
            Console.WriteLine("=== 2. feladat: Pizzéria ===");

            // Három kategóriába sorolt feltétek
            string[] kat1 = { "sonka", "kukorica", "gomba" };
            string[] kat2 = { "kolbász", "ananász", "jalapenho" };
            string[] kat3 = { "kagyló", "articsóka", "oliva" };

            List<string> valasztottFeltetek = new List<string>();
            int ar = 1350;

            while (valasztottFeltetek.Count < 5)
            {
                Console.Write("Add meg a feltétet (- ha nincs több): ");
                string feltet = Console.ReadLine();

                if (feltet == "-")
                {
                    break;
                }

                if (Array.Exists(kat1, f => f == feltet))
                {
                    ar += 200;
                }
                else if (Array.Exists(kat2, f => f == feltet))
                {
                    ar += 250;
                }
                else if (Array.Exists(kat3, f => f == feltet))
                {
                    ar += 300;
                }
                else
                {
                    Console.WriteLine("Ismeretlen feltét, kérlek próbáld újra!");
                    continue;
                }

                valasztottFeltetek.Add(feltet);
            }

            Console.WriteLine($"\nFeltétek száma: {valasztottFeltetek.Count}");
            Console.WriteLine($"Fizetendő ár: {ar} Ft\n");
        }

        // 3. FELADAT – Fenyőfa termelés
        static void FenyofaTermeles()
        {
            Console.WriteLine("=== 3. feladat: Fenyőfa kitermelés ===");

            int napiDarab;
            do
            {
                Console.Write("Napi kitermelt fák száma (35 - 55): ");
            } while (!int.TryParse(Console.ReadLine(), out napiDarab) || napiDarab < 35 || napiDarab > 55);

            int[] fak = new int[14 * napiDarab];
            Random r = new Random();

            for (int i = 0; i < fak.Length; i++)
            {
                fak[i] = r.Next(150, 301); // Magasság cm-ben
            }

            // 3.2 Napi átlagok kiírása
            Console.WriteLine("\nNapi átlagmagasságok (m):");
            for (int nap = 0; nap < 14; nap++)
            {
                int osszeg = 0;
                for (int i = 0; i < napiDarab; i++)
                {
                    osszeg += fak[nap * napiDarab + i];
                }

                double atlag = osszeg / (double)napiDarab / 100.0; // méter
                Console.WriteLine($"{nap + 1}. nap: {atlag:F2} m");
            }

            // 3.3 Legalacsonyabb fa és napja
            int min = fak[0], minNap = 0;
            for (int i = 1; i < fak.Length; i++)
            {
                if (fak[i] < min)
                {
                    min = fak[i];
                    minNap = i / napiDarab;
                }
            }
            Console.WriteLine($"\nLegalacsonyabb fa: {min} cm, {minNap + 1}. napon");

            // 3.4 Napi bevételek
            int[] bevetel = new int[14];
            for (int nap = 0; nap < 14; nap++)
            {
                for (int i = 0; i < napiDarab; i++)
                {
                    int hossz = fak[nap * napiDarab + i];
                    int ar = ((hossz + 9) / 10) * 500; // 10 cm-re felfelé kerekítés
                    bevetel[nap] += ar;
                }
            }

            Console.WriteLine("\nNapi bevételek:");
            for (int i = 0; i < bevetel.Length; i++)
            {
                Console.WriteLine($"{i + 1}. nap: {bevetel[i]} Ft");
            }

            // 3.5 Legnagyobb bevételű nap
            int max = bevetel[0], maxNap = 0;
            for (int i = 1; i < bevetel.Length; i++)
            {
                if (bevetel[i] > max)
                {
                    max = bevetel[i];
                    maxNap = i;
                }
            }
            Console.WriteLine($"\nA legnagyobb bevételű nap: {maxNap + 1}. nap, {max} Ft");
        }
    }
}
