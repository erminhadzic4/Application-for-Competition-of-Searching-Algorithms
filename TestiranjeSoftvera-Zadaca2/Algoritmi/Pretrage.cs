using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TestiranjeSoftvera_Zadaca2.Algoritmi
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

        //Rekurzivna binarna pretraga (Recursive binary search)
        //Vremenska kompleksnost O(log n)
        public static int rekurzivnaBinarnaPretraga<T>(IList<T> niz, T element) where T : IComparable<T>
        {
            return pomocna(niz, 0, niz.Count() - 1, element);
        }

        //Interpolacijska pretraga (Interpolation search)
        //Vremenska kompleksnost: O(log2(log2 n))
        public static int interpolacijskaPretraga(IList<int> niz, int element)
        {
            int min = 0, max = niz.Count() - 1;

            while (min <= max && element >= niz[min] && element <= niz[max])
            {
                if (min == max)
                {
                    if (niz[min] == element) return min;
                    return -1;
                }

                int pos = (int)(min + (((double)(max - min) / (niz[max] - niz[min])) * (element - niz[min])));

                if (niz[pos] == element)
                    return pos;
                if (niz[pos] < element)
                    min = pos + 1;
                else
                    max = pos - 1;
            }
            return -1;
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


        //Eksponencijalna pretraga (Exponential search)
        //Vremenska kompleksnost O(log n)
        public static int eksponencijalnaPretraga<T>(IList<T> niz, T trazeniElement) where T : IComparable<T>
        {
            int n = niz.Count;
            if (EqualityComparer<T>.Default.Equals(niz[0], trazeniElement)) return 0;

            int i = 1;
            while (i < n && niz[i].CompareTo(trazeniElement) <= 0)
                i = i * 2;

            return pomocna(niz, i / 2, Math.Min(i, n - 1), trazeniElement);
        }

        static int pomocna<T>(IList<T> niz, int lijevo, int desno, T element) where T : IComparable<T>
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
    }
}
