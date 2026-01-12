namespace _2025_26
{
    internal class Program
    {
        static List<string> F2_7_Kategoriak(List<Keszlet> lista)
        {
            List<string> kategoriak = new List<string>();
            foreach (var item in lista)
            {
                if (!kategoriak.Contains(item.Kategoria))
                {
                    kategoriak.Add(item.Kategoria);
                }
            }
            kategoriak.Sort();
            return kategoriak;
        }
        static double F2_5_Atlag(Keszlet keszlet)
        {
            if (keszlet.ElemekSzama != 0)
            {
                return Math.Round((double)keszlet.Ar / keszlet.ElemekSzama, 1);
            }
            else
            {
                return 0;
            }
        }
        static void LegoRaktar()
        {
            StreamReader sr = new StreamReader("lego.txt");
            string fejlec = sr.ReadLine();
            List<Keszlet> lista = new List<Keszlet>();
            int hanyDobozLego = 0;
            while (!sr.EndOfStream)
            {
                lista.Add(new Keszlet(sr.ReadLine()));
                hanyDobozLego += lista[lista.Count() - 1].Mennyiseg;
            }
            sr.Close();
            Console.WriteLine("2.2 feladat");
            Console.WriteLine($"Az áruházban {lista.Count} féle készlet található.");
            Console.WriteLine($"Jelenleg összesen {hanyDobozLego} doboz Lego van raktáron.\r\n");
            Keszlet elso = lista[0];
            foreach (Keszlet k in lista)
            {
                if (k.Ar < elso.Ar)
                {
                    elso = k;
                }
            }
            Console.WriteLine("2.3 feladat");
            Console.WriteLine($"Legolcsóbb készlet: {elso.SorozatSzam} - {elso.Kategoria}: {elso.Ar} Ft");
            elso = lista[0];
            foreach (Keszlet k in lista)
            {
                if (k.Ar > elso.Ar)
                {
                    elso = k;
                }
            }
            Console.WriteLine($"Legdrágább készlet: {elso.SorozatSzam} - {elso.Kategoria}: {elso.Ar} Ft");
            int osszeg = 0;
            foreach (Keszlet k in lista)
            {
                if (k.Mennyiseg < 5)
                {
                    int hianyzoDarab = 5 - k.Mennyiseg;
                    osszeg += hianyzoDarab * k.Ar;
                }
            }
            Console.WriteLine($"A raktár feltöltése {osszeg} Ft-ba kerülne.");
            Console.WriteLine("2.6 feladat");
            int sorozatSzam = 0;
            string bemenet = "";
            do
            {
                Console.Write("Készlet sorozatszáma: ");
                bemenet = Console.ReadLine();
            } while (!int.TryParse(bemenet, out sorozatSzam));
            bool talalt = false;
            foreach (Keszlet k in lista)
            {
                if (k.SorozatSzam == sorozatSzam)
                {
                    talalt = true;
                    Console.WriteLine($"A kérdéses {k.Kategoria} típusú készletben az egyes elemek átlagos ára {F2_5_Atlag(k)} Ft");
                    break;
                }

            }
            if (!talalt)
            {
                Console.WriteLine("Nincsen ilyen készlet!");
            }

            var kategoriak = F2_7_Kategoriak(lista);
            foreach (string kategoria in kategoriak)
            {
                Keszlet max = null;
                int osszesKeszletAr = 0;
                int osszesKeszletElemszam = 0;
                foreach (Keszlet k in lista)
                {
                    if (k.Kategoria == kategoria && k.Mennyiseg > 0)
                    {
                        if (max == null || k.ElemekSzama > max.ElemekSzama)
                        {
                            max = k;
                        }
                    }
                }
                if (max != null)
                {
                    Console.WriteLine($"{kategoria} - sorszám: {max.SorozatSzam}, {max.Ar} Ft ({max.ElemekSzama} db lego elem)");
                }
            }
        }
        static void AI_Alkotasok()
        {
            int muveszekSzama = 0;
            string bemenet = "";
            do
            {
                Console.Write("Kérem add meg az AI művészek mennyiségét (20-50): ");
                bemenet = Console.ReadLine();

            } while (!int.TryParse(bemenet, out muveszekSzama) || muveszekSzama > 50 || muveszekSzama < 20);

            double[] alkotasok = new double[muveszekSzama * 30];
            double osszeg = 0;
            double osszegAr = 0;
            int _75pontDB = 0;
            int kiemelkedoMuveszek = 0;
            bool vanTokeletesMuvesz = false;

            Random rnd = new Random();

            for (int i = 0; i < muveszekSzama; i++)
            {
                double _1muveszPontOsszeg = 0;
                bool mindenjo = true;

                for (int j = 0; j < 30; j++)
                {
                    double pont = Math.Round(rnd.NextDouble() * 50 + 50, 1);
                    alkotasok[i * 30 + j] = pont;

                    _1muveszPontOsszeg += pont;

                    if (pont > 75.0)
                    {
                        osszeg += pont;
                        _75pontDB++;
                    }

                    if (pont > 95.0)
                    {
                        double ertek = (450000 + Math.Pow(pont, 4)) / 1000;
                        osszegAr += ertek;
                    }

                    if (pont <= 70.0)
                    {
                        mindenjo = false;
                    }
                }

                if ((_1muveszPontOsszeg / 30) >= 85)
                {
                    kiemelkedoMuveszek++;
                }

                if (mindenjo)
                {
                    vanTokeletesMuvesz = true;
                }
            }

            double atlag = Math.Round(osszeg / _75pontDB, 1);
            Console.WriteLine($"Az átlag: {atlag}");
            Console.WriteLine($"95.0 képek össze értéke: {Math.Round(osszegAr, 1)} Ft");
            Console.WriteLine($"Van tökéletes művész: {vanTokeletesMuvesz}");
            Console.WriteLine($"Kiemelkedő művészek: {kiemelkedoMuveszek}");
        }
        static void Main(string[] args)
        {
           AI_Alkotasok();
        }
    }
}
