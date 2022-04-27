using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace kosar2004
{
    class Program
    {
        static void Main(string[] args)
        {
            //2. feladat
            List<Merkozes> merkozesek = new List<Merkozes>();
            using (StreamReader fajl = new StreamReader("eredmenyek.csv", Encoding.UTF8))
            {
                fajl.ReadLine();
                while (!fajl.EndOfStream)
                {
                    merkozesek.Add(new Merkozes(fajl.ReadLine()));
                }
            }

            //3. feladat
            Console.WriteLine($"3. feladat: Real Madrid: Hazai: {merkozesek.Count(x=>x.HazaiCsapat == "Real Madrid")}, Idegen: {merkozesek.Count(x => x.IdegenCsapat == "Real Madrid")}");

            //4. feladat
            Console.WriteLine($"4. feladat: Volt döntetlen? {(merkozesek.Count(x=>x.HazaiPontok == x.IdegenPontok) > 0 ? "igen" : "nem")}");

            //5. feladat
            Console.WriteLine($"5. feladat: barcelonai csapat neve: {merkozesek.Where(x=>x.Helyszin == "Barcelona" || x.HazaiCsapat.Contains("Barcelona")).First().HazaiCsapat}");

            //6. feladat
            Console.WriteLine("6. feladat");
            merkozesek.Where(x => x.Idopont == DateTime.Parse("2004-11-21")).ToList().ForEach(y => Console.WriteLine($"\t{y.HazaiCsapat}-{y.IdegenCsapat} ({y.HazaiPontok}-{y.IdegenPontok})"));

            //7. feladat
            Console.WriteLine("7. feladat");
            merkozesek.GroupBy(x => x.Helyszin).Select(y => new
            {
                Helyszin = y.Key,
                Merkozesszam = y.Count()
            }).Where(z => z.Merkozesszam >= 20).ToList().ForEach(z => Console.WriteLine($"\t{z.Helyszin}: {z.Merkozesszam}"));
            Console.ReadKey();
        }
        class Merkozes
        {
            string hazaiCsapat, idegenCsapat;
            int hazaiPontok, idegenPontok;
            string helyszin;
            DateTime idopont;

            public string HazaiCsapat { get => hazaiCsapat; set => hazaiCsapat = value; }
            public string IdegenCsapat { get => idegenCsapat; set => idegenCsapat = value; }
            public int HazaiPontok { get => hazaiPontok; set => hazaiPontok = value; }
            public int IdegenPontok { get => idegenPontok; set => idegenPontok = value; }
            public string Helyszin { get => helyszin; set => helyszin = value; }
            public DateTime Idopont { get => idopont; set => idopont = value; }

            public Merkozes(string adatsor)
            {
                string[] adatok = adatsor.Split(';');
                HazaiCsapat = adatok[0].Trim();
                IdegenCsapat = adatok[1].Trim();
                HazaiPontok = int.Parse(adatok[2].Trim());
                IdegenPontok = int.Parse(adatok[3].Trim());
                Helyszin = adatok[4].Trim();
                Idopont = DateTime.Parse(adatok[5].Trim());
            }
        }
    }
}
