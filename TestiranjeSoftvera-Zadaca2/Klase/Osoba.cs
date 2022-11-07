using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestiranjeSoftvera_Zadaca2.Klase
{
    internal class Osoba
    {
        private string ime { get; set; }
        private string prezime { get; set; }
        private int visina { get; set; }
        private int tezina { get; set; }
        private uint JMBG { get; }
        private DateTime datum_rodenja { get; set; }

        public Osoba(string ime, string prezime, int visina, int tezina, uint JMBG, DateTime datum_rodenja)
        {
            this.ime = ime;
            this.prezime = prezime;
            this.visina = visina;
            this.tezina = tezina;
            this.datum_rodenja = datum_rodenja;
            this.JMBG = JMBG;
        }
    }
}
