using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestiranjeSoftvera_Zadaca2.Klase
{
    public class Osoba : IComparable<Osoba>
    {
        public string ime { get; set; }
        public string prezime { get; set; }
        public int visina { get; set; }
        public int tezina { get; set; }
        public uint JMBG { get; set; }
        public DateTime datum_rodenja { get; set; }

        public Osoba(string ime, string prezime, int visina, int tezina, uint JMBG, DateTime datum_rodenja)
        {
            this.ime = ime;
            this.prezime = prezime;
            this.visina = visina;
            this.tezina = tezina;
            this.datum_rodenja = datum_rodenja;
            this.JMBG = JMBG;
        }

        public static bool operator ==(Osoba o1, Osoba o2)
        {
            return o1.JMBG == o2.JMBG;
        }

        public static bool operator >(Osoba o1, Osoba o2)
        {
            return o1.JMBG > o2.JMBG;
        }

        public static bool operator >=(Osoba o1, Osoba o2)
        {
            return o1.JMBG >= o2.JMBG;
        }

        public static bool operator <(Osoba o1, Osoba o2)
        {
            return !(o1.JMBG > o2.JMBG);
        }

        public static bool operator <=(Osoba o1, Osoba o2)
        {
            return !(o1.JMBG >= o2.JMBG);
        }

        public static bool operator !=(Osoba o1, Osoba o2)
        {
            return !(o1.JMBG == o2.JMBG);
        }
        public int CompareTo(Osoba that)
        {
            return this.JMBG.CompareTo(that.JMBG);
        }

        public static string DajRandomString(int size)
        {
            Random rand = new Random();
            const string Alphabet = "abcdefghijklmnopqrstuvwyxzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            char[] chars = new char[size];
            for (int i = 0; i < size; i++)
            {
                chars[i] = Alphabet[rand.Next(Alphabet.Length)];
            }
            return new string(chars);
        }

        public static Osoba DajRandomOsobu()
        {
            Random rand = new Random();
            return new Osoba(Osoba.DajRandomString(4), Osoba.DajRandomString(6), rand.Next(1, 100), rand.Next(1, 100), (uint)rand.Next(100000000, 999999999), new DateTime(rand.Next(2000, 2050), rand.Next(1, 12), rand.Next(1, 27)));
        }
    }
}
