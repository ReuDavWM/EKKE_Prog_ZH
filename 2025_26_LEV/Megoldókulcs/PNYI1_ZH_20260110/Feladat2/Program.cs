namespace F002
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            int beolvasott;// ebbe fogom belolvasni az adatot 
            int pontszamDb = 0; // ebbe tárolom, hogy hányat olvastam sikeresen 
            // a ciklus után tudnom kell, hogy van-e maxPont -> logikai, hiszen igen-nem kérdésre kell válaszolni, és amíg nem olvastunk addig biztos nem olvashattukn max pontot 
            bool vanMaxPont = false;
            // az átlaghoz a darabszám már meglesz (pontszamDb), már csak pontszámok összegét kell tudni -> új változót vezetek be 
            int pontszamSum = 0;
            // nyilvántartom a legkisebb pontszámot -> olyan értéket kell kezdőértéknek választani, amit biztosan rögtön felülírunk (elfogadott legnyaobb plusz egy)
            // mivel a 151-et választok és a helyes értékek a [0,150] intervallumból származnak, így biztosan az első sikeres beolvasott éték kisebb lesz 
            int minPont = 151;

            // muszáj olvasni ahhoz, hogy el tudjam dönteni, hogy ki kell lépni vagy ismételni -> hátultesztelő ciklus
            do
            {
                // beolvasom a pontszámot 
                Console.Write("Pontszám: ");
                beolvasott = int.Parse(Console.ReadLine());

                // ha a beolvasott értékem nagyobb, mint 150, akkor úgy kell tekinteni mintha 150-et írtak volna be 
                if(150 < beolvasott)
                {
                    beolvasott = 150;
                }

                // a beolvasott értéket csak akkor tekintem pontszámnak, ha 0 és 150 között van
                if(0 <= beolvasott && beolvasott <= 150)
                {
                    pontszamDb++; // egy újabb sikeres olvasás, növelem az értékét eggyel 
                    pontszamSum += beolvasott; // újabb sikeres olavasás, a beolvasott értékkel növelem az összeget

                    // ha a pontszám 150, akkor van olyan aki maxPontot ért el 
                    // else ág nem kell, mert a kérdés globális (volt-e valaha olyan olvasás, ahol 150-et olvastunk)
                    if(beolvasott == 150)
                    {
                        vanMaxPont = true;
                    }

                    // ha a beolvasott érték kisebb mint a min, akkor felülírom a min változó értékét 
                    if(beolvasott < minPont)
                    {
                        minPont = beolvasott;
                    }
                }
            }
            while (beolvasott != -1); // -1 olvasásáig maradok a ciklusban 

            // kiírom a ciklus után a gyűjtött adatokat (a ciklusban nem írhatom ki, hiszen még újabb adatokat olvasok)
            Console.WriteLine("A beolvasás során {0} alkalommal olvastunk pontszámot sikeresen!", pontszamDb);

            // az eldöntéshez kapcsolódó szöveghez if-else kell (mit írjunk ki ha igaz, és mit ha hamis) 
            if (vanMaxPont == true)
            {
                Console.WriteLine("Van olyan tanuló, aki maximális pontszámot szerzett.");
            }
            else
            {
                Console.WriteLine("Nincs olyan tanuló, aki maximális pontszámot szerzett.");
            }

            // átlag (először ki kell számolni az összeg / db alapján )
            double atlag = (double)pontszamSum / pontszamDb; // int / int eredménye int lenne, viszont dobule / int eredménye double lesz !!! 
            atlag = Math.Round(atlag, 0); // egészekre kerekítek -> 0 tizedes
            Console.WriteLine("A pontszámok átlaga: {0}", atlag);

            // min pontszám kiírása 
            Console.WriteLine("A legrosszab pontszám {0} volt", minPont);
        }
    }
}
