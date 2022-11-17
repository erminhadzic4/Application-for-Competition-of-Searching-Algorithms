using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using TestiranjeSoftvera_Zadaca2.Algoritmi;
using TestiranjeSoftvera_Zadaca2.Klase;
using Zadaca2;

namespace Testovi
{
    //Klasa MockOsoba za citanje podataka u DataDriven Testu
    //Nije zadano u zadatku eksplicitno
    public class MockOsoba : Osoba 
    {
        public MockOsoba(string ime = default, string prezime = default, int visina = default, int tezina = default, uint JMBG = default, DateTime datum_rodenja = default) : 
                    base(ime, prezime, visina, tezina, JMBG, datum_rodenja) {}
        public static MockOsoba DajRandomMockOsobu()
        {
            Random rand = new Random();
            return new MockOsoba(Osoba.DajRandomString(4), Osoba.DajRandomString(6), rand.Next(1, 100), rand.Next(1, 100), (uint)rand.Next(100000000, 999999999), new DateTime(rand.Next(2000, 2050), rand.Next(1, 12), rand.Next(1, 27)));
        }
    }

    //interface za Fake zamjenski objekat, zadatak 2.
    public interface FakeInterface
    {
        List<MockOsoba> UcitajIzBaze(int zaUcitati);
        int BrojUcitanih();
        int Kapacitet();

    }

    //Implementacija interface-a, zadatak 2.
    public class FakeBazaPodataka : FakeInterface
    {
        int kapacitet { get; }
        int ucitano { get; set; }
        public FakeBazaPodataka(int kapacitet)
        {
            this.kapacitet = kapacitet;
            this.ucitano = 0;
        }

        public int BrojUcitanih()
        {
            return ucitano;
        }

        public int Kapacitet()
        {
            return kapacitet;
        }



        //Simuliramo ucitavanje osoba iz baze u Listu MockOsobe
        public List<MockOsoba> UcitajIzBaze(int zaUcitati)
        {
            Random rand = new Random();
            var osobe = new List<MockOsoba>();
            if (zaUcitati > kapacitet) throw new InvalidOperationException("Ne moze se ucitati vise od kapaciteta!");
            for (int i = 0; i < zaUcitati; i++)
            {
                osobe.Add(new MockOsoba(Osoba.DajRandomString(4), Osoba.DajRandomString(6), rand.Next(1, 100), rand.Next(1, 100), (uint)rand.Next(100000000, 999999999), new DateTime(rand.Next(2000, 2050), rand.Next(1, 12), rand.Next(1, 27))));
                ucitano++;
            }
            return osobe;
        }
    }

    [TestClass]
    public class Testovi_ZamjenskiObjekat
    {
        [TestMethod]
        public void TestZamjenkogObjekta_TestBrojUcitanih()
        {
            var db = new FakeBazaPodataka(100);
            Assert.AreEqual(db.BrojUcitanih(), 0);
        }

        [TestMethod]
        public void TestZamjenkogObjekta_TestKapacitet()
        {
            var db = new FakeBazaPodataka(100);
            Assert.AreEqual(db.Kapacitet(), 100);
        }

        [TestMethod]
        public void TestZamjenkogObjekta_TestCitanjaIzBaze()
        {
            var db = new FakeBazaPodataka(100);
            db.UcitajIzBaze(50);
            Assert.AreEqual(db.BrojUcitanih(), 50);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestZamjenkogObjekta_TestCitanjaIzBaze_Izuzetak()
        {
            var db = new FakeBazaPodataka(100);
            db.UcitajIzBaze(500);
            Assert.AreEqual(db.BrojUcitanih(), 50);
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
        public void TestKlaseOsoba_TestPreklopljenihOperatora_Manje()
        {
            MockOsoba osoba = new MockOsoba(Convert.ToString(TestContext.DataRow["Ime"]), Convert.ToString(TestContext.DataRow["Prezime"]),
                                    Convert.ToInt32(TestContext.DataRow["Visina"]), Convert.ToInt32(TestContext.DataRow["Visina"]),
                                    Convert.ToUInt32(TestContext.DataRow["JMBG"]), Convert.ToDateTime(TestContext.DataRow["DatumRodenja"]));
            Assert.AreEqual(osoba < mock, true);

        }

        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML",
        "|DataDirectory|\\osobe.xml", "osoba", DataAccessMethod.Sequential),
        DeploymentItem("osobe.xml"), TestMethod]
        public void TestKlaseOsoba_TestPreklopljenihOperatora_ManjeJednako()
        {
            MockOsoba osoba = new MockOsoba(Convert.ToString(TestContext.DataRow["Ime"]), Convert.ToString(TestContext.DataRow["Prezime"]),
                                    Convert.ToInt32(TestContext.DataRow["Visina"]), Convert.ToInt32(TestContext.DataRow["Visina"]),
                                    Convert.ToUInt32(TestContext.DataRow["JMBG"]), Convert.ToDateTime(TestContext.DataRow["DatumRodenja"]));
            Assert.AreEqual(osoba <= mock, true);
        }

        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML",
        "|DataDirectory|\\osobe.xml", "osoba", DataAccessMethod.Sequential),
        DeploymentItem("osobe.xml"), TestMethod]
        public void TestKlaseOsoba_TestPreklopljenihOperatora_Vece()
        {
            MockOsoba osoba = new MockOsoba(Convert.ToString(TestContext.DataRow["Ime"]), Convert.ToString(TestContext.DataRow["Prezime"]),
                                    Convert.ToInt32(TestContext.DataRow["Visina"]), Convert.ToInt32(TestContext.DataRow["Visina"]),
                                    Convert.ToUInt32(TestContext.DataRow["JMBG"]), Convert.ToDateTime(TestContext.DataRow["DatumRodenja"]));
            Assert.AreEqual(osoba > mock, false);
        }

        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML",
        "|DataDirectory|\\osobe.xml", "osoba", DataAccessMethod.Sequential),
        DeploymentItem("osobe.xml"), TestMethod]
        public void TestKlaseOsoba_TestPreklopljenihOperatora_VeceJednako()
        {
            MockOsoba osoba = new MockOsoba(Convert.ToString(TestContext.DataRow["Ime"]), Convert.ToString(TestContext.DataRow["Prezime"]),
                                    Convert.ToInt32(TestContext.DataRow["Visina"]), Convert.ToInt32(TestContext.DataRow["Visina"]),
                                    Convert.ToUInt32(TestContext.DataRow["JMBG"]), Convert.ToDateTime(TestContext.DataRow["DatumRodenja"]));
            Assert.AreEqual(osoba >= mock, false);
        }

        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML",
        "|DataDirectory|\\osobe.xml", "osoba", DataAccessMethod.Sequential),
        DeploymentItem("osobe.xml"), TestMethod]
        public void TestKlaseOsoba_TestPreklopljenihOperatora_JednakoJednako()
        {
            MockOsoba osoba = new MockOsoba(Convert.ToString(TestContext.DataRow["Ime"]), Convert.ToString(TestContext.DataRow["Prezime"]),
                                    Convert.ToInt32(TestContext.DataRow["Visina"]), Convert.ToInt32(TestContext.DataRow["Visina"]),
                                    Convert.ToUInt32(TestContext.DataRow["JMBG"]), Convert.ToDateTime(TestContext.DataRow["DatumRodenja"]));
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
        public List<Osoba> mockOsobaList;

        [TestInitialize] public void Postavi() //Za pozivanje prije svakog testa
        {
            mockOsobaList = new List<Osoba>();  
        }

        [TestCleanup] public void OcistiSve() //Za pozivanje nakon svakog testa
        {
            mockOsobaList.Clear();
        }

        [TestMethod]
        public void TestKlasePretrage_Osoba_TestIscrpnePretrage()
        {
            for (int i = 0; i < 10; i++)
                mockOsobaList.Add(Osoba.DajRandomOsobu());

            int indeks = Pretrage.iscrpnaPretraga(mockOsobaList, mockOsobaList[4]);

            Assert.AreEqual(mockOsobaList[4].JMBG, mockOsobaList[indeks].JMBG);
        }

        [TestMethod]
        public void TestKlasePretrage_Osoba_TestFibonaciPretrage()
        {
            for (int i = 0; i < 10; i++)
                mockOsobaList.Add(Osoba.DajRandomOsobu());

            int indeks = Pretrage.fibonaciPretraga(mockOsobaList, mockOsobaList[4]);

            Assert.AreEqual(mockOsobaList[4].JMBG, mockOsobaList[indeks].JMBG);
        }

        [TestMethod]
        public void TestKlasePretrage_Osoba_TestBinarnePretrage()
        {
            for (int i = 0; i < 10; i++)
                mockOsobaList.Add(Osoba.DajRandomOsobu());

            int indeks = Pretrage.binarnaPretraga(mockOsobaList, mockOsobaList[4]);

            Assert.AreEqual(mockOsobaList[4].JMBG, mockOsobaList[indeks].JMBG);
        }

        [TestMethod]
        public void TestKlasePretrage_Osoba_TestSkokPretrage()
        {
            for (int i = 0; i < 10; i++)
                mockOsobaList.Add(Osoba.DajRandomOsobu());

            int indeks = Pretrage.skokPretraga(mockOsobaList, mockOsobaList[4]);

            Assert.AreEqual(mockOsobaList[4].JMBG, mockOsobaList[indeks].JMBG);
        }

        [TestMethod]
        public void TestKlasePretrage_Osoba_TestEksponancijalnePretrage()
        {
            for (int i = 0; i < 10; i++)
                mockOsobaList.Add(Osoba.DajRandomOsobu());

            int indeks = Pretrage.eksponencijalnaPretraga(mockOsobaList, mockOsobaList[4]);

            Assert.AreEqual(mockOsobaList[4].JMBG, mockOsobaList[indeks].JMBG);
        }

        [TestMethod]
        public void TestKlasePretrage_Osoba_TestRekurzivneBinarnePretrage()
        {
            for (int i = 0; i < 10; i++)
                mockOsobaList.Add(Osoba.DajRandomOsobu());

            int indeks = Pretrage.rekurzivnaBinarnaPretraga(mockOsobaList, mockOsobaList[4]);

            Assert.AreEqual(mockOsobaList[4].JMBG, mockOsobaList[indeks].JMBG);
        }

        [TestMethod]
        public void TestKlasePretrage_Osoba_TestTernarnePretrage()
        {   
            for (int i = 0; i < 10; i++)
                mockOsobaList.Add(Osoba.DajRandomOsobu());

            int indeks = Pretrage.ternarnaPretraga(mockOsobaList, mockOsobaList[4]);

            Assert.AreEqual(mockOsobaList[4].JMBG, mockOsobaList[indeks].JMBG);
        }

        [TestMethod]
        public void TestKlasePretrage_Osoba_TestBogoPretrage()
        {
            for (int i = 0; i < 10; i++)
                mockOsobaList.Add(Osoba.DajRandomOsobu());

            int indeks = Pretrage.bogoPretraga(mockOsobaList, mockOsobaList[4]);

            Assert.AreEqual(mockOsobaList[4].JMBG, mockOsobaList[indeks].JMBG);
        }

        [TestMethod]
        public void TestKlasePretrage_Osoba_TestBibliotecnePretrage()
        {
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
    }

    [TestClass]
    public class Testovi_KlasaPretrage_PomocneFunkcije
    {
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
    }

    [TestClass]
    public class Testovi_KlasaMain
    {
        [TestMethod]
        public void TestKlaseMain_ListingKomparacijeInt()
        {
            var odgovor = Zadaca2.main.ListingKomparacije(1, 100, true);
            Assert.IsNotNull(odgovor);
        }

        [TestMethod]
        public void TestKlaseMain_IzvrsiPretrage()
        {
            List<Tuple<string, TimeSpan, int, int, string, string>> lista = new List<Tuple<string, TimeSpan, int, int, string, string>>(0);
            lista.Add(Zadaca2.main.IzvrsiPretragu(Pretrage.iscrpnaPretraga<int>, 1, 1000000, 500000, "Iscrpna pretraga", true));
            lista.Add(Zadaca2.main.IzvrsiPretragu(Pretrage.fibonaciPretraga<int>, 1, 1000000, 500000, "Fibonacci pretraga", true));
            lista.Add(Zadaca2.main.IzvrsiPretragu(Pretrage.binarnaPretraga<int>, 1, 1000000, 500000, "Binarna pretraga", true));
            lista.Add(Zadaca2.main.IzvrsiPretragu(Pretrage.skokPretraga<int>, 1, 1000000, 500000, "Skok pretraga", true));
            lista.Add(Zadaca2.main.IzvrsiPretragu(Pretrage.eksponencijalnaPretraga<int>, 1, 1000000, 500000, "Eksponencijalna pretraga", true));
            lista.Add(Zadaca2.main.IzvrsiPretragu(Pretrage.rekurzivnaBinarnaPretraga<int>, 1, 1000000, 500000, "Rekurzivna binarna pretraga", true));
            Assert.IsNotNull(lista);
        }

        [TestMethod]
        public void TestKlaseMain_ListingKomparacijeOsobe()
        {
            var lista = Zadaca2.main.ListingKomparacijeOsoba(10000);
            Assert.IsNotNull(lista);
        }

        [TestMethod]
        public void TestKlaseMain_DajListuOsoba()
        {
            var osobe = Zadaca2.main.DajListuOsoba(100);
            Assert.IsInstanceOfType(osobe, typeof(List<Osoba>));
        }

        [TestMethod]
        public void TestKlaseMain_DajNiz()
        {
            var niz = Zadaca2.main.DajNiz(1, 100);
            Assert.IsInstanceOfType(niz, typeof(int[]));
        }

        [TestMethod]
        public void TestKlaseMain_DajListu()
        {
            var list = Zadaca2.main.DajListu(1, 100);
            Assert.IsInstanceOfType(list, typeof(List<int>));
        }

        [TestMethod]
        public void TestKlaseMain_ZapocniKomparaciju()
        {
            var odgovor = Zadaca2.main.ZapocniKomparaciju(1, 100000, 50000, true);
            Zadaca2.main.ZapocniKomparaciju(1, 100000, 50000, false);
            Assert.IsInstanceOfType(odgovor, typeof(List<Tuple<string, TimeSpan, int, int, string, string>>));
        }

        [TestMethod]
        public void TestKlaseMain_IzvrsiPretragiOsoba()
        {
            var osobe = Zadaca2.main.DajListuOsoba(100);
            var odgovor = Zadaca2.main.IzvrsiPretraguOsoba(Pretrage.iscrpnaPretraga<Osoba>, osobe, 100, osobe[40], "Iscrpna pretraga");
            Assert.IsInstanceOfType(odgovor, typeof(Tuple<string, TimeSpan, int, string, string, string>));
        }

        [TestMethod]
        public void TestKlaseMain_ZapocniKomparacijuOsoba()
        {
            var osobe = Zadaca2.main.DajListuOsoba(100);
            var odgovor = Zadaca2.main.ZapocniKomparacijuOsoba(osobe, 100, osobe[40]);
            Assert.IsInstanceOfType(odgovor, typeof(List<Tuple<string, TimeSpan, int, string, string, string>>));
        }
    }
}
