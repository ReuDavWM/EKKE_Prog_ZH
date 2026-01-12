namespace F004
{
    // a saját típust a Program-on kívül (azza egy szinten hozzuk létre), a saját 
    // típust struct kulcsszóval hozzuk létre, majd megadjuk a nevét, ezt követően 
    // a kapcsos zárójelpárok között felsoroljuk a struktúra mezőinek típusát és nevét (deklaráljuk azokat)
    struct Eredmeny
    {
        // a struct-on belül minden elé public-ot írunk! 
        public int irasbeli;
        public int szobeli;
        public int ev;
        public bool emelt;
        public string tazon;

        // így minden eredmény típusú változóban 5 értéket tárolunk el, ha egy 
        // konkrét értéket el szeretnénk érni, akár írás akár olvasás szempontjából
        // akkor a változó neve után . kell és elérjük a benne lévő mezőket 
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            // A fájlt a projekhez adjuk. Solution Explorer-ben a projekt nevében jobb 
            // gomb -> Add -> Existion Item -> Betallózzuk a CSV-t (figyeljünk arra, hogy 
            // a fájltípus át kell állítani All Files-ra, különben nem látjuk a CSV fájlt) 
            // utána a fájlon jobb gomb -> Properties -> és a Copty to Output Drirectory tulajdonságot
            // Copy Always-re állítjuk

            // meghívom a függvényt és az eredményét eltárolom 
            List<Eredmeny> tanulok = Beolvas("eredmenyek.csv"); // mivel a projekthez adtam a file-t, így a vs az exe mellé másolja (nem kell elérési út)
            // kiíratom a lista felhasználávals a 2025-ös év eredményei 
            Kiir(tanulok, 2025);
            

        }

        static void Kiir(List<Eredmeny> eredmenyek, int ev) // megakpom a listát, amit kell írni és az évet, hogy tudjak szűrni, a visszatérési típus void, mert nem akarok értéket visszaadni 
        {
            for (int i = 0; i < eredmenyek.Count; i++) // végigmegyek az eredményeken 
            {
                if (eredmenyek[i].ev == ev) // csak azokkal foglalkozom, ahol az év megfelelő 
                {
                    // eldöntöm hogy MEGFELELT vagy NEM FELELT MEG jelenjen meg 
                    // felteszem, hogy MEGFELELT, de ha az átment false, akkor módosítom 
                    string minosites = "MEGFELELT";
                    if (Atment(eredmenyek[i]) == false)
                    {
                        minosites = "NEM FELELT MEG";
                    }

                    Console.WriteLine("{0} - {1}", eredmenyek[i].tazon, minosites);
                }
            }
        }

        static bool Atment(Eredmeny eredmeny) // paraméter az eredmeny, mivel abból számolunk és mivel egy eldöntendő kérdésre kell választ adni, ezért logikaival térek vissza
        {
            // a korábbi feladatok alapján összesen 150 pont szerezhető az érettségin, ha nem érjük el
            // a 25%-ot, akkor buktunk, különbne átmentünk 
            double teljesitmeny = (double)(eredmeny.irasbeli + eredmeny.szobeli) / 150 * 100;

            if(teljesitmeny < 25)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        static List<Eredmeny> Beolvas(string fajlnev) // List<Eredmeny>-t adunk vissza a hívás végén, és a hívás során át kell adni a fájl nevét szövegként 
        {
            // üres lista, ebbe fogjuk rakni a beolvasott sorokból készített Eredmeny értékeket
            List<Eredmeny> lista = new List<Eredmeny>();

            // a fájl beolvasásához egy Streamet nyitunk
            StreamReader olvaso = new StreamReader(fajlnev);
            olvaso.ReadLine(); // az első sort nem dolgozom fel (oszlopnevek vannak benne)
            
            while(olvaso.EndOfStream != true) // addig olvasok amíg el nem fogynak a sorok
            {
                string sor = olvaso.ReadLine(); // új sort olvasok a fájlból 
                string[] mezok = sor.Split(';'); // az elválasztó karakter mentén felvágom, így hozzáférek az egyes adatokhoz külön-külön 

                Eredmeny e; // a mezők elemeit feldolgozva feltöltöm az e változót 
                e.tazon = mezok[0]; // a legelső mezőben van az azonosító, egyszerű string
                e.ev = int.Parse(mezok[1]); // a második mező az év, de ez nem szöveg, így konverátlni kell

                // a szint problémás, mivel a fájlban egy emelt/közép szöveg van, de nekünk boolean kell 
                // -> alapértelmezetten false-ra állítom és ha a beolvasott szöveg értéke emelt akkor true-ra állítom a mezőt
                e.emelt = false;
                if (mezok[2] == "emelt")
                {
                    e.emelt = true;
                }

                e.irasbeli = int.Parse(mezok[3]);
                e.szobeli = int.Parse(mezok[4]);

                //e-t a listához adom 
                lista.Add(e);
            }

            olvaso.Close();

            return lista; // visszaadom a hívás helyére a listát, így fel tudjuk dolgozni az eredményt a hívó fél 
        }
    }
}
