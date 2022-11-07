using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Diagnostics;
using System.IO;
using TestiranjeSoftvera_Zadaca2.Algoritmi;
using TestiranjeSoftvera_Zadaca2.Klase;

namespace Zadaca2
{
    internal class main
    {

        /* Ukoliko je ova konstanta stavljena na true, sve informacija koje se zapisuju na konzolu ce takoder biti
        Zapisane i u .txt file koji ce se nalaziti pored .exe file-a samog projekta. Ukoliko je stavljen na false
        Ispis ce se samo vrsiti na konzolu. */
        const bool ZAPISI_U_DATOTEKU = true;

        static void Main(string[] args)
        {
            /* Postavlja sirinu dovoljnu za ispis informacija, jer ne mora znaciti da su svima iste dimenzije */
            Console.SetWindowSize(180, 40);

            /* Vraca uredjenu sestorku (6-tuple). Glavna funkcionalnost se desava u metodi ListingKomparacije
            Prima tri parametra, pocetak i kraj tj. kojim opsegom elemenata zelimo da napunimo listu ili niz, sto se moze i promijeniti
            tako sto se elementi same liste/niza popune random vrijednostima, medutim zbog osnovnog zadatka ovog projekta
            zadrzali smo se na fiksnim vrijednostima. Treci parametar je true/false koji omogucava testiranje liste i niza respektivno 
            (false za niz, true za listu) kao sto je u zadatku i zadano. Dodavanjem novog kontejnera, ovaj parametar bi se
            mogao promijeniti u string, cime bi navodeci ime samog kontejnera testirali isti. */

            List<List<Tuple<string, TimeSpan, int, int, string, string>>> rezultati1 = ListingKomparacije(1, 1000, false);
            List<List<Tuple<string, TimeSpan, int, int, string, string>>> rezultati2 = ListingKomparacije(1, 10000, false);
            List<List<Tuple<string, TimeSpan, int, int, string, string>>> rezultati3 = ListingKomparacije(1, 1000000, false);

            List<List<Tuple<string, TimeSpan, int, int, string, string>>> rezultati4 = ListingKomparacije(1, 1000, true);
            List<List<Tuple<string, TimeSpan, int, int, string, string>>> rezultati5 = ListingKomparacije(1, 10000, true);
            List<List<Tuple<string, TimeSpan, int, int, string, string>>> rezultati6 = ListingKomparacije(1, 1000000, true);

            var ukupni_rezultati = new List<List<List<Tuple<string, TimeSpan, int, int, string, string>>>>();
            ukupni_rezultati.Add(rezultati1); ukupni_rezultati.Add(rezultati2); ukupni_rezultati.Add(rezultati3);
            ukupni_rezultati.Add(rezultati4); ukupni_rezultati.Add(rezultati5); ukupni_rezultati.Add(rezultati6);
            int i = 0;

            /* Zapisuje sve informacije i u file 'algoritmi_informacije.txt' koji se nalazi pored .exe file-a. */
            if (ZAPISI_U_DATOTEKU)
                ZapisiSveUFile(ukupni_rezultati);

            /* Beskonacna petlja za pomjeranje listinga algoritama, pritiskom na ESC program zavrsava sa radom. */
            while (true)
            {
                Console.Clear();
                IspisiListing(ukupni_rezultati[i % ukupni_rezultati.Count()]);
                Console.WriteLine("Bilo koja tipka za iduci listing, ESC za kraj...");

                var unos = Console.ReadKey().Key;
                if (unos != ConsoleKey.Escape)
                    i++;
                else return;
            }

        }

        /* Metoda zapisuje sve iteracije algoritama u file */
        static void ZapisiSveUFile(List<List<List<Tuple<string, TimeSpan, int, int, string, string>>>> ukupni_rezultati)
        {
            if (File.Exists("algoritmi_informacije.txt"))
            {
                File.Delete("algoritmi_informacije.txt");
            }

            if (ZAPISI_U_DATOTEKU)
                for (int j = 0; j < ukupni_rezultati.Count(); j++)
                    ZapisiUFile(ukupni_rezultati[j]);
        }

        /* Metoda ispisuje sve iteracije na konzolu */
        static void IspisiListing(List<List<Tuple<string, TimeSpan, int, int, string, string>>> rezultati)
        {

            /* Prolazi kroz sve rezultate i sortira ih po vremenu izvrsenja - rastući poredak */
            for (int i = 0; i < rezultati.Count(); i++)
            {
                rezultati[i].Sort((x, y) => x.Item2.CompareTo(y.Item2));
            }

            Console.WriteLine("{0,-30} {1,20} {2, 20} {3,20} {4,20} {5,20}\n", "Naziv algoritma", "Vrijeme izvrsavanja", "Na indeksu", "Trazeni element", "Kontejner", "Na opsegu");
            foreach (var rezultat in rezultati)
            {
                foreach (var vrijednost in rezultat)
                {
                    Console.WriteLine("{0,-30} {1,20} {2, 20} {3,20} {4,20} {5,20}", vrijednost.Item1, vrijednost.Item2, vrijednost.Item3, vrijednost.Item4, vrijednost.Item5, vrijednost.Item6);
                }
                Console.WriteLine(new String('-', 135));
            }
            Console.WriteLine(new String('\n', 3));
        }

        /* Glavna metoda, u kojoj se najveci dio posla obavlja */
        static List<List<Tuple<string, TimeSpan, int, int, string, string>>> ListingKomparacije(int pocetak, int kraj, bool testListeIliNiza)
        {
            Random random = new Random();
            List<List<Tuple<string, TimeSpan, int, int, string, string>>> iteracija = new List<List<Tuple<string, TimeSpan, int, int, string, string>>>();

            if (testListeIliNiza)
            {
                List<Tuple<string, TimeSpan, int, int, string, string>> rezultatiLista1 = ZapocniKomparaciju(pocetak, kraj, pocetak, testListeIliNiza); //pretraga pocetka
                List<Tuple<string, TimeSpan, int, int, string, string>> rezultatiLista2 = ZapocniKomparaciju(pocetak, kraj, (pocetak + kraj) / 2, testListeIliNiza); //pretraga sredine
                List<Tuple<string, TimeSpan, int, int, string, string>> rezultatiLista3 = ZapocniKomparaciju(pocetak, kraj, kraj, testListeIliNiza); //pretraga kraja
                List<Tuple<string, TimeSpan, int, int, string, string>> rezultatiLista4 = ZapocniKomparaciju(pocetak, kraj, Math.Abs(pocetak) + Math.Abs(kraj), testListeIliNiza); //pretraga nepostojeceg elementa
                iteracija.Add(rezultatiLista1);
                iteracija.Add(rezultatiLista2);
                iteracija.Add(rezultatiLista3);
                iteracija.Add(rezultatiLista4);
            }

            else
            {
                List<Tuple<string, TimeSpan, int, int, string, string>> rezultatiLista1 = ZapocniKomparaciju(pocetak, kraj, pocetak, testListeIliNiza);
                List<Tuple<string, TimeSpan, int, int, string, string>> rezultatiLista2 = ZapocniKomparaciju(pocetak, kraj, (pocetak + kraj) / 2, testListeIliNiza);
                List<Tuple<string, TimeSpan, int, int, string, string>> rezultatiLista3 = ZapocniKomparaciju(pocetak, kraj, kraj, testListeIliNiza);
                List<Tuple<string, TimeSpan, int, int, string, string>> rezultatiLista4 = ZapocniKomparaciju(pocetak, kraj, Math.Abs(pocetak) + Math.Abs(kraj), testListeIliNiza);
                iteracija.Add(rezultatiLista1);
                iteracija.Add(rezultatiLista2);
                iteracija.Add(rezultatiLista3);
                iteracija.Add(rezultatiLista4);
            }
            return iteracija;
        }

        /* Pomocna metoda za popunjavanje niza elementima */
        static int[] DajNiz(int pocetak, int kraj)
        {
            int[] niz = new int[Math.Abs(kraj - pocetak + 1)];
            for (int i = 0; i < niz.Count(); i++)
            {
                niz[i] = pocetak;
                pocetak++;
            }
            return niz;
        }

        /* Pomocna metoda za popunjavanje liste elementima */
        static List<int> DajListu(int pocetak, int kraj)
        {
            List<int> lista = new List<int>(Math.Abs(kraj - pocetak));
            for (int i = pocetak; i <= kraj; i++)
                lista.Add(i);
            return lista;
        }

        /* Metoda vrsi komparaciju algoritama pozivajuci IzvrsiPretragu koja pored ostalog prima funkciju kao parametar */
        static List<Tuple<string, TimeSpan, int, int, string, string>> ZapocniKomparaciju(int pocetak, int kraj, int element, bool testListeIliNiza)
        {
            List<Tuple<string, TimeSpan, int, int, string, string>> lista = new List<Tuple<string, TimeSpan, int, int, string, string>>(0);

            lista.Add(IzvrsiPretragu(Pretrage.iscrpnaPretraga<int>, pocetak, kraj, element, "Iscrpna pretraga", testListeIliNiza));
            lista.Add(IzvrsiPretragu(Pretrage.fibonaciPretraga<int>, pocetak, kraj, element, "Fibonacci pretraga", testListeIliNiza));
            lista.Add(IzvrsiPretragu(Pretrage.binarnaPretraga<int>, pocetak, kraj, element, "Binarna pretraga", testListeIliNiza));
            lista.Add(IzvrsiPretragu(Pretrage.interpolacijskaPretraga, pocetak, kraj, element, "Interpolacijska pretraga", testListeIliNiza));
            lista.Add(IzvrsiPretragu(Pretrage.skokPretraga<int>, pocetak, kraj, element, "Skok pretraga", testListeIliNiza));
            lista.Add(IzvrsiPretragu(Pretrage.eksponencijalnaPretraga<int>, pocetak, kraj, element, "Eksponencijalna pretraga", testListeIliNiza));
            lista.Add(IzvrsiPretragu(Pretrage.rekurzivnaBinarnaPretraga<int>, pocetak, kraj, element, "Rekurzivna binarna pretraga", testListeIliNiza));

            return lista;
        }

        /* Jedino za naglasiti je to da funkcija prima kao parametar funkciju koja ima dva parametra, listu intova, int i vraca int. */
        static Tuple<string, TimeSpan, int, int, string, string> IzvrsiPretragu(Func<IList<int>, int, int> fja, int pocetak, int kraj, int element, string ime, bool testListeIliNiza)
        {
            List<int> lista = DajListu(pocetak, kraj);
            int[] niz = DajNiz(pocetak, kraj);
            int vrijednost;
            string koristeni_kontejner = testListeIliNiza ? "Lista" : "Niz";

            /* Koristena klasa Stopwatch iz standardne System.Diagnostics biblioteke. Potrebna je za metriku izvrsavanja algoritama.
            metode Start() i Stop() pokrecu i zaustavljaju stopericu, dok za Elapsed mozemo vidjeti proteklo vrijeme od starta do kraja*/
            Stopwatch sw = new Stopwatch();
            sw.Start();
            if (testListeIliNiza)
                vrijednost = fja(lista, element);
            else
                vrijednost = fja(niz, element);
            sw.Stop();

            return new Tuple<string, TimeSpan, int, int, string, string>(ime, sw.Elapsed, vrijednost, element, koristeni_kontejner, "[" + pocetak + ", " + kraj + "]");
        }

        /* Metoda koja zapisuje jednu iteraciju (jedan opseg vrijednosti) algoritama u file. Zbog cisceg koda, koristi se u metodi koja zapisuje sve iteracije u file. 
        Koristi se StreamWriter kao klasa pomocu koje smo ostvarili pisanje u file. Pored nje se mogla koristiti i File klasa. */
        static void ZapisiUFile(List<List<Tuple<string, TimeSpan, int, int, string, string>>> rezultati)
        {
            using (StreamWriter sw = File.AppendText("algoritmi_informacije.txt"))
            {

                for (int i = 0; i < rezultati.Count(); i++)
                {
                    rezultati[i].Sort((x, y) => x.Item2.CompareTo(y.Item2));
                }

                sw.WriteLine("{0,-30} {1,20} {2, 20} {3,20} {4,20} {5,20}\n", "Naziv algoritma", "Vrijeme izvrsavanja", "Na indeksu", "Trazeni element", "Kontejner", "Na opsegu");
                foreach (var rezultat in rezultati)
                {
                    foreach (var vrijednost in rezultat)
                    {
                        sw.WriteLine("{0,-30} {1,20} {2, 20} {3,20} {4,20} {5,20}", vrijednost.Item1, vrijednost.Item2, vrijednost.Item3, vrijednost.Item4, vrijednost.Item5, vrijednost.Item6);
                    }
                    sw.WriteLine(new String('-', 135));
                }
                sw.WriteLine(new String('\n', 3));
                sw.Flush();
            }
        }
    }
}
