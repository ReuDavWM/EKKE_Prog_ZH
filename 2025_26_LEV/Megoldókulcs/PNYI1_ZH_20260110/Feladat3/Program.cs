namespace F003
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 12 tanulónak kell 3-3 eredményt eltárolni, 12 * 3 méretű vektorra van szükség 
            int[] pontszamok = new int[12 * 3];
            // a feladatmegoldás során véletlenszámot is kell generálni, így szükségem lesz egy Random változóra 
            Random rnd = new Random();

            // feltöltés: minden tanulónak 3 pontszáma van -> ha 0-tól hármassával ugrok a tömbben akkor tanulót váltok, a tanuló első eredményét fogom látni
            for(int i = 0; i < 12; i++) // i értékei: 0, 1, 2, 3, 4 ... 
            {
                // az eltolás mértéke 3, mivel minden tanulóhoz 3 érték tartozik 
                // i * 3 + 0-n található a tanuló első pontszáma (Dokumentumkészítés vagy Táblázatkezelés, max 35 p)
                // i * 3+ 1-n a második pontszáma (Adatbázis-kezelés, max 35 p)
                // i * 3+ 2-n a harmadik pontszáma (Algoritmizálás és programozás, max 50 p)

                // ennem megfelelően generáljuk a számokat 
                pontszamok[i * 3] = rnd.Next(0, 36); // felülíról az intervallum nyitott, ezért van 36 (35 az utolsó amit generálhat)
                pontszamok[i * 3 + 1] = rnd.Next(0, 36);
                pontszamok[i * 3 + 2] = rnd.Next(0, 51);
            }

            // keressük annak a tanulónak a sorszámát, aki max pontot ért el a 3. témakörben (50 pontot)
            // Letárolom a tanuló sorszámát, de nem írhatunk bele 0-t, mivel az egy valid tömbindex
            // így az algoritmus végén nem tudnám, hogy 0 azért mert az volt a kezdőérték, vagy a 
            // 0. tanuló (a tömb szempontjából) elérte a max pontot. Ha -1-et választok akkor nem 
            // lesz ilyen probléma, mivel az algoritmus futása során -1 nem kerülhet a változóba 
            // (nem létezik ilyen tömbindex)
            int algoMaxSsz = -1; 
            for (int i = 0; i < 12; i = i + 3) // léptetem a tanulók  
            {
                // i-hez képest mindig +2 az utolsó pontszám, az adott tanulóhoz az Algoritmizálás
                if (pontszamok[i + 2] == 50)
                {
                    algoMaxSsz = i; // megjegyzem a tanuló sorszámát 
                }
            }

            if(algoMaxSsz == -1)
            {
                Console.WriteLine("Nincs ilyen tanuló!");
            }
            else
            {
                // ugyan programozásban a 0., 1. stb tanulóról beszélünk de megjelenítésnél +1 eltolássa "természetes" sorszámot írok ki
                Console.WriteLine("A(z) {0}. tanuló maximális pontot szerzett a témakörben!", algoMaxSsz + 1);
            }


            // keressük annak a tanulónak a sorszámát, aki a legjobb teljesítményt nyújtotta 
            // feltételezem, hogy az első tanuló nyújtott a legjobb teljesítményt 
            int maxPont = pontszamok[0] + pontszamok[1] + pontszamok[2]; // a pontszám 3 részterület összege 
            int maxSsz = 0; // amit a 0. tanuló ért el 

            for (int i = 1; i < 12; i++) // i mehet 1-től, hiszen a 0. tanuló eredménye már számolva van 
            {
                // kiszámolom az i-edik tanulóra a 3 részterület összegét 
                int pont = pontszamok[i * 3] + pontszamok[i * 3 + 1] + pontszamok[i * 3 + 2];

                // ha ez több mint a maxpont akkor módosítom mind a maxpontot mind a sorszámot
                if(maxPont < pont)
                {
                    maxPont = pont;
                    maxSsz = i;
                }
            }

            Console.WriteLine("A legjobb teljesítményt a(z) {0}. tanuló nyújtotta", maxSsz + 1);


            // területenként ki kell számolnunk az átlagot ehhez kell a darabaszám (azt tudjuk, hogy 
            // 12 tanuló van), és kell a területen szerzett pontszám ha 3-mal ugrálok a tömbben, akkor 
            // a különböző tanulók ugyanaz témaköri eredményeit látjuk 
            // összegzéshez előkészítem a változókat 
            int t1ossz = 0;
            int t2ossz = 0;
            int t3ossz = 0;

            for (int i = 0; i < pontszamok.Length; i += 3)
            {
                t1ossz += pontszamok[i];
                t2ossz += pontszamok[i + 1];
                t3ossz += pontszamok[i + 2];
            }

            // átlagszámítás (igazából lehetett volna eleve olyan változókat deklarálni ami felhasználható közvetlenül) 
            // viszont két terület 35-35 pont a harmadik 50 így a pontok alapján teljesítményt számolok
            double t1Atlag = (double)t1ossz / 12 / 35;
            double t2Atlag = (double)t2ossz / 12 / 35;
            double t3Atlag = (double)t3ossz / 12 / 50;

            // döntés 
            if(t1Atlag <= t2Atlag && t1Atlag <= t3Atlag)
            {
                Console.WriteLine("Az 1. témakör (Dokumentumkészítés vagy Táblázatkezelés) átlagpontszáma a legrosszabb!");
            }
            else if(t2Atlag <= t1Atlag &&  t2Atlag <= t3Atlag)
            {
                Console.WriteLine("Az 2. témakör (Adatbázis-kezelés) átlagpontszáma a legrosszabb!");
            }
            else // ebben a feltételben a <= bal oldlalára a t3-ak kerülnénkek 
            {
                Console.WriteLine("Az 3. témakör (Algoritmizálás és programozás) átlagpontszáma a legrosszabb!");
            }


        }
    }
}
