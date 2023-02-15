using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TakmicenjeAlgoritama.Klase;

namespace TakmicenjeAlgoritama.Algoritmi
{
    public class Pretrage
    {

        //Iscrpna pretraga (linear search, brute-force search) 
        //Vremenska kompleksnost: O(n)
        public static int iscrpnaPretraga<T>(IList<T> niz, T element)
        {

            int N = niz.Count();
            for (int i = 0; i < N; i++)
            {
                if (EqualityComparer<T>.Default.Equals(element, niz[i]))
                    return i;
            }
            return -1;
        }

        public static int iscrpnaPretraga(IList<Osoba> niz, Osoba element)
        {

            int N = niz.Count();
            for (int i = 0; i < N; i++)
            {
                if (niz[i] == element)
                    return i;
            }
            return -1;
        }

        //Fibonacijeva pretraga (Fibonacci Search)
        //Vremenska kompleksnost: O(log(n))
        public static int fibonaciPretraga<T>(IList<T> niz, T x) where T : IComparable<T>
        {
            int fibBRm2 = 0;
            int fibBRm1 = 1;
            int fibM = fibBRm2 + fibBRm1;

            while (fibM < niz.Count())
            {
                fibBRm2 = fibBRm1;
                fibBRm1 = fibM;
                fibM = fibBRm2 + fibBRm1;
            }

            int offset = -1;

            while (fibM > 1)
            {
                int i = min(offset + fibBRm2, niz.Count() - 1);
                if (niz[i].CompareTo(x) < 0)
                {
                    fibM = fibBRm1;
                    fibBRm1 = fibBRm2;
                    fibBRm2 = fibM - fibBRm1;
                    offset = i;
                }

                else if (niz[i].CompareTo(x) > 0)
                {
                    fibM = fibBRm2;
                    fibBRm1 = fibBRm1 - fibBRm2;
                    fibBRm2 = fibM - fibBRm1;
                }
                else
                    return i;
            }
            if (fibBRm1 == 1 && EqualityComparer<T>.Default.Equals(niz[niz.Count() - 1], x))
                return niz.Count() - 1;

            return -1;
        }

        public static int fibonaciPretraga(IList<Osoba> niz, Osoba x)
        {
            var niz2 = vratiSortirano(niz);


            int fibBRm2 = 0;
            int fibBRm1 = 1;
            int fibM = fibBRm2 + fibBRm1;

            while (fibM < niz2.Count())
            {
                fibBRm2 = fibBRm1;
                fibBRm1 = fibM;
                fibM = fibBRm2 + fibBRm1;
            }

            int offset = -1;

            while (fibM > 1)
            {
                int i = min(offset + fibBRm2, niz2.Count() - 1);
                if (niz2[i].CompareTo(x) < 0)
                {
                    fibM = fibBRm1;
                    fibBRm1 = fibBRm2;
                    fibBRm2 = fibM - fibBRm1;
                    offset = i;
                }

                else if (niz2[i].CompareTo(x) > 0)
                {
                    fibM = fibBRm2;
                    fibBRm1 = fibBRm1 - fibBRm2;
                    fibBRm2 = fibM - fibBRm1;
                }
                else
                    return i;
            }
            if (fibBRm1 == 1 && niz2[niz2.Count() - 1] == x)
                return niz2.Count() - 1;

            return -1;
        }

        //Rekurzivna binarna pretraga (Recursive binary search)
        //Vremenska kompleksnost O(log n)
        public static int rekurzivnaBinarnaPretraga<T>(IList<T> niz, T element) where T : IComparable<T>
        {

            return pomocna(niz, 0, niz.Count() - 1, element);
        }

        public static int rekurzivnaBinarnaPretraga(IList<Osoba> niz, Osoba element)
        {
            var niz2 = vratiSortirano(niz);
            return pomocna(niz2, 0, niz2.Count() - 1, element);
        }

        //Binarna pretraga (binary search)
        //Vremenska kompleksnost: O(log(n))
        public static int binarnaPretraga<T>(IList<T> niz, T element) where T : IComparable<T>
        {

            int donja = 0;
            int gornja = niz.Count() - 1;
            int sredina;

            while (gornja - donja > 1)
            {
                sredina = (gornja + donja) / 2;
                if (niz[sredina].CompareTo(element) < 0)
                {
                    donja = sredina + 1;
                }
                else
                {
                    gornja = sredina;
                }
            }
            if (EqualityComparer<T>.Default.Equals(niz[donja], element))
                return donja;
            else if (EqualityComparer<T>.Default.Equals(niz[gornja], element))
                return gornja;
            else return -1;
        }

        //za objekte
        public static int binarnaPretraga(IList<Osoba> niz, Osoba element)
        {
            var niz2 = vratiSortirano(niz);

            int min = 0;
            int max = niz2.Count() - 1;
            while (min <= max)
            {
                int mid = (min + max) / 2;
                if (element == niz2[mid])
                {
                    return mid;
                }
                else if (element < niz2[mid])
                {
                    max = mid - 1;
                }
                else
                {
                    min = mid + 1;
                }
            }
            return -1;
        }

        public static int bibliotecnaPretraga<T>(IList<T> niz, T element) where T : IComparable<T>
        {
            return niz.IndexOf(element);
        }

        public static int bibliotecnaPretraga(IList<Osoba> niz, Osoba element)
        {
            return niz.IndexOf(element);
        }

        //test metoda
        public static IList<Osoba> vratiSortirano(IList<Osoba> niz)
        {
            return niz.OrderBy(x => x.JMBG).ToList();
        }

        //Skok pretraga (Jump search)
        //Vremenska kompleksnost O(√n)
        public static int skokPretraga<T>(IList<T> niz, T trazeniElement) where T : IComparable<T>
        {
            ArrayList.Adapter((IList)niz).Sort();

            int n = niz.Count;

            int korak = (int)Math.Sqrt(n);

            int prev = 0;
            while (niz[Math.Min(korak, n) - 1].CompareTo(trazeniElement) < 0)
            {
                prev = korak;
                korak += (int)Math.Sqrt(n);
                if (prev >= n)
                    return -1;
            }

            while (niz[prev].CompareTo(trazeniElement) < 0)
            {
                prev++;

                if (prev == Math.Min(korak, n))
                    return -1;
            }

            if (EqualityComparer<T>.Default.Equals(niz[prev], trazeniElement))
                return prev;

            return -1;
        }

        // Za objekte osoba
        public static int skokPretraga(IList<Osoba> niz, Osoba trazeniElement)
        {
            var niz2 = vratiSortirano(niz);

            int n = niz2.Count;

            int korak = (int)Math.Sqrt(n);

            int prev = 0;
            while (niz2[Math.Min(korak, n) - 1].CompareTo(trazeniElement) < 0)
            {
                prev = korak;
                korak += (int)Math.Sqrt(n);
                if (prev >= n)
                    return -1;
            }

            while (niz2[prev].CompareTo(trazeniElement) < 0)
            {
                prev++;

                if (prev == Math.Min(korak, n))
                    return -1;
            }

            if (niz2[prev] == trazeniElement)
                return prev;

            return -1;
        }

        public static int bogoPretraga<T>(IList<T> niz, T trazeniElement) where T : IComparable<T>
        {
            Random rand = new Random();
            List<int> lista = new List<int>(niz.Count());
            int count = 0;
            while (true)
            {
                int i = rand.Next(0, niz.Count());
                if (lista.Contains(i)) continue;
                lista.Add(i);
                count++;
                if ((EqualityComparer<T>.Default.Equals(niz[i], trazeniElement)))
                {
                    return i;
                }
            }
        }

        public static int bogoPretraga(IList<Osoba> niz, Osoba trazeniElement)
        {
            Random rand = new Random();
            List<int> lista = new List<int>(niz.Count());
            int count = 0;
            while (true)
            {
                int i = rand.Next(0, niz.Count());
                if (lista.Contains(i)) continue;
                lista.Add(i);
                count++;
                if (niz[i] == trazeniElement)
                {
                    return i;
                }
            }
        }

        //Eksponencijalna pretraga (Exponential search)
        //Vremenska kompleksnost O(Log n)
        public static int eksponencijalnaPretraga<T>(IList<T> niz, T trazeniElement) where T : IComparable<T>
        {
            int n = niz.Count;
            if (EqualityComparer<T>.Default.Equals(niz[0], trazeniElement)) return 0;

            int i = 1;
            while (i < n && niz[i].CompareTo(trazeniElement) <= 0)
                i = i * 2;

            return pomocna(niz, i / 2, Math.Min(i, n - 1), trazeniElement);
        }

        //objekat
        public static int eksponencijalnaPretraga(IList<Osoba> niz, Osoba trazeniElement)
        {
            var niz2 = vratiSortirano(niz);
            int n = niz.Count;
            if (niz[0] == trazeniElement) return 0;

            int i = 1;
            while (i < n && niz2[i] <= trazeniElement)
                i = i * 2;

            return pomocna(niz2, i / 2, Math.Min(i, n - 1), trazeniElement);
        }

        public static int pomocna<T>(IList<T> niz, int lijevo, int desno, T element) where T : IComparable<T>
        {
            if (desno >= lijevo)
            {
                int mid = lijevo + (desno - lijevo) / 2;

                if (EqualityComparer<T>.Default.Equals(niz[mid], element))
                    return mid;

                if (niz[mid].CompareTo(element) > 0)
                    return pomocna(niz, lijevo, mid - 1, element);

                return pomocna(niz, mid + 1, desno, element);
            }

            return -1;
        }

        //pomocna funkcija
        public static int min(int x, int y)
        {
            return (x <= y) ? x : y;
        }

        //Ternarna pretraga O(log3n)
        public static int ternarnaPretraga<T>(IList<T> niz, T trazeniElement) where T : IComparable<T>
        {
            //var niz2 = vratiSortirano(niz);
            return pomocnaZaTernarnu<T>(niz, trazeniElement, 0, niz.Count());
        }

        public static int pomocnaZaTernarnu<T>(IList<T> niz, T trazeniElement, int pocetak, int kraj) where T : IComparable<T>
        {
            if (niz.Count() >= 0)
            {
                // Nalazimo sredine
                int sredina1 = pocetak + (kraj - pocetak) / 3;
                int sredina2 = kraj - (kraj - pocetak) / 3;

                // Provjera da li se nalazi na sredinama
                if (EqualityComparer<T>.Default.Equals(niz[sredina1], trazeniElement)) return sredina1;
                if (EqualityComparer<T>.Default.Equals(niz[sredina2], trazeniElement)) return sredina2;
                // Nije na sredini pa provjerava u kojem je i sve tako dok ne nade
                if (niz[sredina1].CompareTo(trazeniElement) > 0)
                {
                    // Trazeni element je izmedu pocetka i prve sredine
                    return pomocnaZaTernarnu(niz, trazeniElement, pocetak, sredina1 - 1);
                }
                else if (niz[sredina2].CompareTo(trazeniElement) < 0)
                {

                    // Trazeni element je izmedu druge sredine i kraja
                    return pomocnaZaTernarnu(niz, trazeniElement, sredina2 + 1, kraj);
                }
                else
                {
                    // Trazeni element je izmedu dvije sredine
                    return pomocnaZaTernarnu(niz, trazeniElement, sredina1 + 1, sredina2 - 1);
                }
            }
            // Nema elementa u nizu
            return -1;
        }

        //Ternarna pretraga O(log3n)
        public static int ternarnaPretraga(IList<Osoba> niz, Osoba trazeniElement)
        {
            var niz2 = vratiSortirano(niz);
            return pomocnaZaTernarnu(niz2, trazeniElement, 0, niz2.Count());
        }

        public static int pomocnaZaTernarnu(IList<Osoba> niz, Osoba trazeniElement, int pocetak, int kraj)
        {
            if (niz.Count() >= 0)
            {
                // Nalazimo sredine
                int sredina1 = pocetak + (kraj - pocetak) / 3;
                int sredina2 = kraj - (kraj - pocetak) / 3;

                // Provjera da li se nalazi na sredinama
                if (niz[sredina1] == trazeniElement) return sredina1;
                if (niz[sredina2] == trazeniElement) return sredina2;

                // Nije na sredini pa provjerava u kojem je i sve tako dok ne nade
                if (trazeniElement < niz[sredina1])
                {
                    // Trazeni element je izmedu pocetka i prve sredine
                    return pomocnaZaTernarnu(niz, trazeniElement, pocetak, sredina1 - 1);
                }
                else if (trazeniElement > niz[sredina2])
                {

                    // Trazeni element je izmedu druge sredine i kraja
                    return pomocnaZaTernarnu(niz, trazeniElement, sredina2 + 1, kraj);
                }
                else
                {
                    // Trazeni element je izmedu dvije sredine
                    return pomocnaZaTernarnu(niz, trazeniElement, sredina1 + 1, sredina2 - 1);
                }
            }
            // Nema elementa u nizu
            return -1;
        }
    }
}
