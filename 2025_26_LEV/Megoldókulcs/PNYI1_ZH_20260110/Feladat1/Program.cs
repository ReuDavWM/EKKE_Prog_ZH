namespace F001
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // bekérjük a vizsga szintjét
            Console.Write("Vizsga szintje: ");
            string szint = Console.ReadLine();
            
            // csak akkor dolgozunk tovább, ha az emelt vagy közép szót írta be a felhasználó különben hibaüzenet 
            // -> két eset van (vagy jó a bemenet vagy nem) -> if-else
            if(szint == "közép" || szint == "emelt")
            {
                // minden vizsgaszinten van írásbeli és szóbeli rész, így ezeket bekérjük 
                Console.Write("Írásbeli vizsgarész pontszám: ");
                int irasbeliP = int.Parse(Console.ReadLine());

                Console.Write("Szóbeli vizsgarész pontszám: ");
                int szobeliP = int.Parse(Console.ReadLine());

                // az összteljesítmény számolása azonos mindkét vizsgaszinten (mennyi ponotot szerzetek 150-ből) 
                double osszTelj = (double)(irasbeliP + szobeliP) / 150 * 100; // kell típuskényszerítés mert két egész osztása egészet eredményezne
                // két tizedesre kerekítek függvény segítségével (lehetne az előzőbe is ágyazni)
                osszTelj = Math.Round(osszTelj,2);

                Console.WriteLine("Az érettségi vizsgán nyújtott teljesítmény: {0}%", osszTelj);

                // külön-külön is kizsámítjuk a részvizsgák eredményét, viszont a pontszámok megoszlása más a két szinten 
                // -> máshogyan kell eljárnunk közép és emelt szint esetén -> if-else 
                double irasbeliTelj, szobeliTelj; // if-en kívül deklarálom, hogy később fel tudjam használni a számított eredményeket 
                if(szint == "közép")
                {
                    irasbeliTelj = Math.Round((double)irasbeliP / 100 * 100, 2);
                    szobeliTelj = Math.Round((double)szobeliP / 50 * 100, 2);
                }
                else // a külső if alapján tudjuk, hogy ez az eset az "emelt" lehet csak 
                {
                    irasbeliTelj = Math.Round((double)irasbeliP / 120 * 100, 2);
                    szobeliTelj = Math.Round((double)szobeliP / 30 * 100, 2);
                }

                // a kiírás közös, független a vizsga szintjétől 
                Console.WriteLine("A szóbeli vizsgarészen nyújtott teljesítmény: {0}%", szobeliTelj);
                Console.WriteLine("Az írásbeli vizsgarészen nyújtott teljesítmény: {0}%", irasbeliTelj);
            }
            else
            {
                Console.WriteLine("Hibásan adta meg a vizsga szintjét, csak közép vagy emelt adható meg!");
            }
        }
    }
}
