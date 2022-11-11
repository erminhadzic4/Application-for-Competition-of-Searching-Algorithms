using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TestiranjeSoftvera_Zadaca2.Klase;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestKlasaOsoba_IspravneDodjeleAtributaUKons()
        {
            var osoba = new Osoba("Pero", "Peric", 170, 72, 12345, new DateTime(2001, 03, 06));

            Assert.AreEqual(osoba.ime, "Pero");
            Assert.AreEqual(osoba.prezime, "Peric");
            Assert.AreEqual(osoba.visina, 170);
            Assert.AreEqual(osoba.tezina, 72);
            Assert.AreEqual(osoba.JMBG, (uint)12345);
            Assert.AreEqual(osoba.datum_rodenja, new DateTime(2001, 03, 06));
        }

        [TestMethod]
        public void TestKlasaOsoba_IspravniPreklopljeniOperatori()
        {
            var osoba1 = new Osoba("Pero", "Peric", 170, 72, 12345, new DateTime(2001, 03, 06));
            var osoba2 = new Osoba("Meho", "Mehic", 179, 101, 123456, new DateTime(1999, 03, 03));

            Assert.AreEqual(osoba1 > osoba2, false);
            Assert.AreEqual(osoba1 >= osoba2, false);
            Assert.AreEqual(osoba1 == osoba2, false);
            Assert.AreEqual(osoba1 < osoba2, true);
            Assert.AreEqual(osoba1 <= osoba2, true);
        }
    }
}
