using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using TestiranjeSoftvera_Zadaca2.Algoritmi;
using TestiranjeSoftvera_Zadaca2.Klase;
using Zadaca2;

namespace Testovi
{
    public class MockOsoba : Osoba
    {
        static int kolicina = 0;
        public MockOsoba(string ime = default, string prezime = default, int visina = default, int tezina = default, uint JMBG = default, DateTime datum_rodenja = default) : 
                    base(ime, prezime, visina, tezina, JMBG, datum_rodenja)
        {
            kolicina++;
        }

        public static MockOsoba DajRandomOsobu()
        {
            Random rand = new Random();
            return new MockOsoba(Osoba.DajRandomString(4), Osoba.DajRandomString(6), rand.Next(1, 100), rand.Next(1, 100), (uint)rand.Next(100000000, 999999999), new DateTime(rand.Next(2000, 2050), rand.Next(1, 12), rand.Next(1, 27)));
        }
        public List<MockOsoba> UcitajOsobeIzBaze(int velicina) 
        {
            List<MockOsoba> lista = new List<MockOsoba>();
            //Simulirano citanje osoba po redovima iz baze podataka
            //Konstruktor povecava kolicinu 'procitanih osoba'
            for (int i = 0; i < velicina; i++)
                lista.Add(new MockOsoba());
            return lista;
        }
    }
    [TestClass]
    public class Testovi_KlasaOsoba
    {
        private TestContext testContextInstance;
        private MockOsoba mock { get; set; }
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        [TestInitialize]
        public void Init()
        {
            mock = new MockOsoba("test", "test", 1, 1, 9999999, new DateTime(2012, 1, 1));
        }

        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML",
        "|DataDirectory|\\osobe.xml", "osoba", DataAccessMethod.Sequential),
        DeploymentItem("osobe.xml"), TestMethod]
        public void TestKlaseOsoba_TestPreklopljenihOperatora()
        {
            MockOsoba osoba = new MockOsoba(Convert.ToString(TestContext.DataRow["Ime"]), Convert.ToString(TestContext.DataRow["Prezime"]),
                                    Convert.ToInt32(TestContext.DataRow["Visina"]), Convert.ToInt32(TestContext.DataRow["Visina"]),
                                    Convert.ToUInt32(TestContext.DataRow["JMBG"]), Convert.ToDateTime(TestContext.DataRow["DatumRodenja"]));
            Assert.AreEqual(osoba < mock, true);
            Assert.AreEqual(osoba <= mock, true);
            Assert.AreEqual(osoba > mock, false);
            Assert.AreEqual(osoba >= mock, false);
            Assert.AreEqual(osoba == mock, false);
        }

        [TestCleanup]
        public void Clean()
        {
            mock = null;
        }

    }

    [TestClass]
    public class Testovi_KlasaPretrage_Osoba
    {
        [TestMethod]
        public void TestKlasePretrage_Osoba_TestIscrpnePretrage()
        {
            var mockOsobaList = new List<Osoba>();
            for (int i = 0; i < 10; i++)
                mockOsobaList.Add(Osoba.DajRandomOsobu());

            int indeks = Pretrage.iscrpnaPretraga(mockOsobaList, mockOsobaList[4]);

            Assert.AreEqual(mockOsobaList[4].JMBG, mockOsobaList[indeks].JMBG);
        }

        [TestMethod]
        public void TestKlasePretrage_Osoba_TestFibonaciPretrage()
        {
            var mockOsobaList = new List<Osoba>();
            for (int i = 0; i < 10; i++)
                mockOsobaList.Add(Osoba.DajRandomOsobu());

            int indeks = Pretrage.fibonaciPretraga(mockOsobaList, mockOsobaList[4]);

            Assert.AreEqual(mockOsobaList[4].JMBG, mockOsobaList[indeks].JMBG);
        }

        [TestMethod]
        public void TestKlasePretrage_Osoba_TestBinarnePretrage()
        {
            var mockOsobaList = new List<Osoba>();
            for (int i = 0; i < 10; i++)
                mockOsobaList.Add(Osoba.DajRandomOsobu());

            int indeks = Pretrage.binarnaPretraga(mockOsobaList, mockOsobaList[4]);

            Assert.AreEqual(mockOsobaList[4].JMBG, mockOsobaList[indeks].JMBG);
        }

        [TestMethod]
        public void TestKlasePretrage_Osoba_TestSkokPretrage()
        {
            var mockOsobaList = new List<Osoba>();
            for (int i = 0; i < 10; i++)
                mockOsobaList.Add(Osoba.DajRandomOsobu());

            int indeks = Pretrage.skokPretraga(mockOsobaList, mockOsobaList[4]);

            Assert.AreEqual(mockOsobaList[4].JMBG, mockOsobaList[indeks].JMBG);
        }

        [TestMethod]
        public void TestKlasePretrage_Osoba_TestEksponancijalnePretrage()
        {
            var mockOsobaList = new List<Osoba>();
            for (int i = 0; i < 10; i++)
                mockOsobaList.Add(Osoba.DajRandomOsobu());

            int indeks = Pretrage.eksponencijalnaPretraga(mockOsobaList, mockOsobaList[4]);

            Assert.AreEqual(mockOsobaList[4].JMBG, mockOsobaList[indeks].JMBG);
        }

        [TestMethod]
        public void TestKlasePretrage_Osoba_TestRekurzivneBinarnePretrage()
        {
            var mockOsobaList = new List<Osoba>();
            for (int i = 0; i < 10; i++)
                mockOsobaList.Add(Osoba.DajRandomOsobu());

            int indeks = Pretrage.rekurzivnaBinarnaPretraga(mockOsobaList, mockOsobaList[4]);

            Assert.AreEqual(mockOsobaList[4].JMBG, mockOsobaList[indeks].JMBG);
        }

        [TestMethod]
        public void TestKlasePretrage_Osoba_TestTernarnePretrage()
        {
            var mockOsobaList = new List<Osoba>();
            for (int i = 0; i < 10; i++)
                mockOsobaList.Add(Osoba.DajRandomOsobu());

            int indeks = Pretrage.ternarnaPretraga(mockOsobaList, mockOsobaList[4]);

            Assert.AreEqual(mockOsobaList[4].JMBG, mockOsobaList[indeks].JMBG);
        }

        [TestMethod]
        public void TestKlasePretrage_Osoba_TestBogoPretrage()
        {
            var mockOsobaList = new List<Osoba>();
            for (int i = 0; i < 10; i++)
                mockOsobaList.Add(Osoba.DajRandomOsobu());

            int indeks = Pretrage.bogoPretraga(mockOsobaList, mockOsobaList[4]);

            Assert.AreEqual(mockOsobaList[4].JMBG, mockOsobaList[indeks].JMBG);
        }

        [TestMethod]
        public void TestKlasePretrage_Osoba_TestBibliotecnePretrage()
        {
            var mockOsobaList = new List<Osoba>();
            for (int i = 0; i < 10; i++)
                mockOsobaList.Add(Osoba.DajRandomOsobu());

            int indeks = Pretrage.bibliotecnaPretraga(mockOsobaList, mockOsobaList[4]);

            Assert.AreEqual(mockOsobaList[4].JMBG, mockOsobaList[indeks].JMBG);
        }
    }

    [TestClass]
    public class Testovi_KlasaPretrage_Int
    {
        [TestMethod]
        public void TestKlasePretrage_Int_TestIscrpnePretrage()
        {
            Random rand = new Random();
            var ListaIntova = new int[10];
            for (int i = 0; i < 10; i++)
                ListaIntova[i] = (rand.Next(1, 10));

            int indeks = Pretrage.iscrpnaPretraga(ListaIntova, ListaIntova[4]);

            Assert.AreEqual(ListaIntova[4], ListaIntova[indeks]);
        }

        [TestMethod]
        public void TestKlasePretrage_Int_TestFibonaciPretrage()
        {
            Random rand = new Random();
            var ListaIntova = new int[10];
            for (int i = 0; i < 10; i++)
                ListaIntova[i] = (rand.Next(1, 10));

            Array.Sort(ListaIntova);

            int indeks = Pretrage.fibonaciPretraga(ListaIntova, ListaIntova[4]);

            Assert.AreEqual(ListaIntova[4], ListaIntova[indeks]);
        }

        [TestMethod]
        public void TestKlasePretrage_Int_TestBinarnePretrage()
        {
            Random rand = new Random();
            var ListaIntova = new int[10];
            for (int i = 0; i < 10; i++)
                ListaIntova[i] = (rand.Next(1, 10));

            Array.Sort(ListaIntova);

            int indeks = Pretrage.binarnaPretraga(ListaIntova, ListaIntova[4]);

            Assert.AreEqual(ListaIntova[4], ListaIntova[indeks]);
        }

        [TestMethod]
        public void TestKlasePretrage_Int_TestSkokPretrage()
        {
            Random rand = new Random();
            var ListaIntova = new int[10];
            for (int i = 0; i < 10; i++)
                ListaIntova[i] = (rand.Next(1, 10));

            Array.Sort(ListaIntova);

            int indeks = Pretrage.skokPretraga(ListaIntova, ListaIntova[4]);

            Assert.AreEqual(ListaIntova[4], ListaIntova[indeks]);
        }

        [TestMethod]
        public void TestKlasePretrage_Int_TestEksponencijalnePretrage()
        {
            Random rand = new Random();
            var ListaIntova = new int[10];
            for (int i = 0; i < 10; i++)
                ListaIntova[i] = (rand.Next(1, 10));

            Array.Sort(ListaIntova);

            int indeks = Pretrage.eksponencijalnaPretraga(ListaIntova, ListaIntova[4]);

            Assert.AreEqual(ListaIntova[4], ListaIntova[indeks]);
        }

        [TestMethod]
        public void TestKlasePretrage_Int_TestRekurzivneBinarnePretrage()
        {
            Random rand = new Random();
            var ListaIntova = new List<int>();
            for (int i = 0; i < 10; i++)
                ListaIntova.Add(rand.Next(1, 10));

            int indeks = Pretrage.rekurzivnaBinarnaPretraga(ListaIntova, ListaIntova[4]);

            Assert.AreEqual(ListaIntova[4], ListaIntova[indeks]);
        }


        [TestMethod]
        public void TestKlasePretrage_Int_TestTernarnePretrage()
        {
            Random rand = new Random();
            var ListaIntova = new int[10];
            for (int i = 0; i < 10; i++)
                ListaIntova[i] = (rand.Next(1, 10));

            Array.Sort(ListaIntova);

            int indeks = Pretrage.ternarnaPretraga(ListaIntova, ListaIntova[4]);

            Assert.AreEqual(ListaIntova[4], ListaIntova[indeks]);
        }


        [TestMethod]
        public void TestKlasePretrage_Int_TestBogoPretrage()
        {
            Random rand = new Random();
            var ListaIntova = new int[10];
            for (int i = 0; i < 10; i++)
                ListaIntova[i] = (rand.Next(1, 10));

            Array.Sort(ListaIntova);

            int indeks = Pretrage.bogoPretraga(ListaIntova, ListaIntova[4]);

            Assert.AreEqual(ListaIntova[4], ListaIntova[indeks]);
        }

        [TestMethod]
        public void TEST()
        {
            Zadaca2.main.ListingKomparacije(1, 100, true);
            List<Tuple<string, TimeSpan, int, int, string, string>> lista = new List<Tuple<string, TimeSpan, int, int, string, string>>(0);

            lista.Add(Zadaca2.main.IzvrsiPretragu(Pretrage.iscrpnaPretraga<int>, 1, 1000, 500, "Iscrpna pretraga", true));
            lista.Add(Zadaca2.main.IzvrsiPretragu(Pretrage.fibonaciPretraga<int>, 1, 1000, 500, "Fibonacci pretraga", true));
            lista.Add(Zadaca2.main.IzvrsiPretragu(Pretrage.binarnaPretraga<int>, 1, 1000, 500, "Binarna pretraga", true));
            lista.Add(Zadaca2.main.IzvrsiPretragu(Pretrage.skokPretraga<int>, 1, 1000, 500, "Skok pretraga", true));
            lista.Add(Zadaca2.main.IzvrsiPretragu(Pretrage.eksponencijalnaPretraga<int>, 1, 1000, 500, "Eksponencijalna pretraga", true));
            lista.Add(Zadaca2.main.IzvrsiPretragu(Pretrage.rekurzivnaBinarnaPretraga<int>, 1, 1000, 500, "Rekurzivna binarna pretraga", true));
            Assert.AreEqual(1, 1);
        }

        [TestMethod]
        public void TestKlasePretrage_Osoba_TestPomocneZaTernarnu()
        {
            var mockOsobaList = new List<Osoba>();
            for (int i = 0; i < 10; i++)
                mockOsobaList.Add(Osoba.DajRandomOsobu());

            int indeks = Pretrage.pomocnaZaTernarnu(mockOsobaList, mockOsobaList[4], 0, 10);

            Assert.AreNotEqual(indeks, -1);
        }

        [TestMethod]
        public void TestKlasePretrage_Test_VratiSortirano()
        {
            var mockOsobaList = new List<Osoba>();
            for (int i = 0; i < 10; i++)
                mockOsobaList.Add(Osoba.DajRandomOsobu());

            var novi = Pretrage.vratiSortirano(mockOsobaList);


            Assert.IsTrue(novi.SequenceEqual(mockOsobaList));
        }

        [TestMethod]
        public void TestKlasePretrage_Int_TestPomocneZaTernarnu()
        {
            Random rand = new Random();
            var ListaIntova = new int[10];
            for (int i = 0; i < 10; i++)
                ListaIntova[i] = (rand.Next(1, 10));

            Array.Sort(ListaIntova);

            int indeks = Pretrage.pomocnaZaTernarnu(ListaIntova, ListaIntova[4], 0, 10);

            Assert.AreNotEqual(indeks, -1);
        }

        [TestMethod]
        public void TestKlasePretrage_Int_TestBibliotecnePretrage()
        {
            Random rand = new Random();
            var ListaIntova = new int[10];
            for (int i = 0; i < 10; i++)
                ListaIntova[i] = (rand.Next(1, 10));

            Array.Sort(ListaIntova);

            int indeks = Pretrage.bibliotecnaPretraga(ListaIntova, ListaIntova[4]);

            Assert.AreEqual(ListaIntova[4], ListaIntova[indeks]);
        }

        [TestMethod]
        public void TestKlasePretrage_Pomocna()
        {
            Random rand = new Random();
            var ListaIntova = new int[10];
            for (int i = 0; i < 10; i++)
                ListaIntova[i] = (rand.Next(1, 10));

            Array.Sort(ListaIntova);

            int indeks = Pretrage.pomocna(ListaIntova, 0, 10, ListaIntova[4]);

            Assert.AreNotEqual(indeks, -1);
        }

        [TestMethod]
        public void TestKlasePretrage_Min()
        {
            Assert.IsTrue(Pretrage.min(2, 5) == 2);
        }
    }
}
