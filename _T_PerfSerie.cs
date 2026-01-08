using AeroCalcCore;
using NUnit.Framework;



namespace TestAeroCalc
{



    [TestFixture]
    public class _T_PerfSerie
    {


        [SetUp]
        public void SetUp()
        {

        }


        private PerfSerie CreateSerie((double input, double output, bool isBreak)[] points)
        {
            var serie = new PerfSerie();
            foreach (var p in points)
            {
                var pp = new PerfPoint(p.input, p.output, p.isBreak);
                Assert.That(serie.add(pp), Is.True);
            }
            return serie;
        }



        [Test]
        public void subDomain_1()
        {
            var serie = CreateSerie(new[]
            {
                (0.0,   0.0, false),
                (10.0, 10.0, false),
                (20.0, 20.0, true),   // break
                (30.0, 30.0, false),
                (40.0, 40.0, false),
                (50.0, 50.0, true),   // break
                (60.0, 60.0, false),
            });

            int[] sub;

            sub = serie._A_subDomain(1.5);
            Assert.That(sub.Length, Is.EqualTo(3));
            Assert.That(sub, Is.EqualTo(new[] { 0, 1, 2 }));

            sub = serie._A_subDomain(31.0);
            Assert.That(sub.Length, Is.EqualTo(4));
            Assert.That(sub, Is.EqualTo(new[] { 2, 3, 4, 5 }));

            sub = serie._A_subDomain(51.1);
            Assert.That(sub.Length, Is.EqualTo(2));
            Assert.That(sub, Is.EqualTo(new[] { 5, 6 }));


        }




        [Test]
        public void PerfSerie_1()
        {

            PerfSerie ps = new PerfSerie();
            ps.add(new PerfPoint(3, 10, false));
            ps.add(new PerfPoint(1, 5, true));
            ps.add(new PerfPoint(-5, 1, false));

            PerfSerie psClone = new PerfSerie(ps);

            Assert.That(3, Is.EqualTo(psClone.count));

            for (int count = 0; count < ps.count; count++)
            {
                Assert.That(ps.pointAt(count).input, Is.EqualTo(psClone.pointAt(count).input));
            }
        }



        [Test]
        public void add_1()
        {

            PerfSerie ps = new PerfSerie();

            Assert.That(ps.add(new PerfPoint(1, 5, false)), Is.True);
            Assert.That(ps.add(new PerfPoint(1, 10, false)), Is.False);
        }


        // Test du tri des layers de performance
        [Test]
        public void add_2()
        {

            PerfSerie ps = new PerfSerie();
            ps.add(new PerfPoint(3, 10, false));
            ps.add(new PerfPoint(1, 5, true));
            ps.add(new PerfPoint(-5, 1, false));
            ps.add(new PerfPoint(-1, 15, false));
            ps.add(new PerfPoint(8, 6, false));

            Assert.That(-5, Is.EqualTo(ps.pointAt(0).input));
            Assert.That(-1, Is.EqualTo(ps.pointAt(1).input));
            Assert.That(1, Is.EqualTo(ps.pointAt(2).input));
            Assert.That(3, Is.EqualTo(ps.pointAt(3).input));
            Assert.That(8, Is.EqualTo(ps.pointAt(4).input));
        }


        // Test du comptage des PerfPoints sélectionnés
        [Test]
        public void selectedCount_1()
        {

            PerfSerie ps = new PerfSerie();
            PerfPoint pp1 = new PerfPoint(1, 5, true);
            PerfPoint pp2 = new PerfPoint(-5, 1, true);
            PerfPoint pp3 = new PerfPoint(-1, 15, false);
            PerfPoint pp4 = new PerfPoint(8, 6, false);
            PerfPoint pp5 = new PerfPoint(3, 10, false);
            ps.add(pp1);
            ps.add(pp2);
            ps.add(pp3);
            ps.add(pp4);
            ps.add(pp5);
            //pp3.selected = true;
            //pp4.selected = true;

            //Assert.That(2, Is.EqualTo(ps.selectedCount()));
        }


        // Test de la fonction renvoyant l'index d'un point de performance
        [Test]
        public void getIndexOf_1()
        {

            PerfSerie ps = new PerfSerie();
            PerfPoint pp1 = new PerfPoint(1, 5, true);
            PerfPoint pp2 = new PerfPoint(-5, 1, true);
            PerfPoint pp3 = new PerfPoint(-1, 15, false);
            PerfPoint pp4 = new PerfPoint(8, 6, false);
            ps.add(pp1);
            ps.add(pp2);
            ps.add(pp3);
            ps.add(pp4);

            Assert.That(0, Is.EqualTo(ps.getIndexOf(pp2)));
            Assert.That(1, Is.EqualTo(ps.getIndexOf(pp3)));
            Assert.That(2, Is.EqualTo(ps.getIndexOf(pp1)));
            Assert.That(3, Is.EqualTo(ps.getIndexOf(pp4)));
        }


        /*
        // Test de la fonction renvoyant l'index du point de plus grande proximité
        [Test]
        public void Test_closestPointIndex2_1() {

            for (int count = 0; count < 10000; count++) {
                PerfSerie ps1 = new PerfSerie();
                ps1.add(new PerfPoint(-12, 10, false));
                ps1.add(new PerfPoint(-9, 5, true));
                ps1.add(new PerfPoint(-8, 5, true));
                ps1.add(new PerfPoint(-5, -1, false));
                ps1.add(new PerfPoint(-1, -1, false));
                ps1.add(new PerfPoint(0, 1, false));
                ps1.add(new PerfPoint(1, 1, false));
                ps1.add(new PerfPoint(3, 0, false));
                ps1.add(new PerfPoint(7, 3, false));
                ps1.add(new PerfPoint(11, 0, false));
                ps1.add(new PerfPoint(15, 9, false));
                ps1.add(new PerfPoint(32, 41, false));

                Assert.AreEqual(0, ps1.closestPointIndex(-13));
                Assert.AreEqual(2, ps1.closestPointIndex(-7.5));
                Assert.AreEqual(5, ps1.closestPointIndex(0.4999));
                Assert.AreEqual(6, ps1.closestPointIndex(1));
                Assert.AreEqual(8, ps1.closestPointIndex(7.1));
                Assert.AreEqual(10, ps1.closestPointIndex(16));
                Assert.AreEqual(11, ps1.closestPointIndex(33));
            }

        }
        */


        /*
        // Test de la fonction renvoyant l'index du point de plus grande proximité
        [Test]
        public void Test_closestPointIndex1_1() {

            for (int count = 0; count < 10000; count++) {
                PerfSerie ps1 = new PerfSerie();
                ps1.add(new PerfPoint(-12, 10, false));
                ps1.add(new PerfPoint(-9, 5, true));
                ps1.add(new PerfPoint(-8, 5, true));
                ps1.add(new PerfPoint(-5, -1, false));
                ps1.add(new PerfPoint(-1, -1, false));
                ps1.add(new PerfPoint(0, 1, false));
                ps1.add(new PerfPoint(1, 1, false));
                ps1.add(new PerfPoint(3, 0, false));
                ps1.add(new PerfPoint(7, 3, false));
                ps1.add(new PerfPoint(11, 0, false));
                ps1.add(new PerfPoint(15, 9, false));
                ps1.add(new PerfPoint(32, 41, false));

                Assert.AreEqual(0, ps1.closestPointIndex(-13));
                Assert.AreEqual(2, ps1.closestPointIndex(-7.5));
                Assert.AreEqual(5, ps1.closestPointIndex(0.4999));
                Assert.AreEqual(6, ps1.closestPointIndex(1));
                Assert.AreEqual(8, ps1.closestPointIndex(7.1));
                Assert.AreEqual(10, ps1.closestPointIndex(16));
                Assert.AreEqual(11, ps1.closestPointIndex(33));
            }

        }
        */


        // Test du classement des indexes des PerfPoint d'une PerfSerie par rapport à une abscisse de référence
        [Test]
        public void sortIndexesByDistance_1()
        {

            PerfSerie ps = new PerfSerie();
            int[] table;

            ps.add(new PerfPoint(-12, 10, false));
            table = ps._A_sortIndexesByDistance(ps._A_subDomain(1), 1);
            Assert.That(table, Is.Null);

            ps.add(new PerfPoint(-9, 5, true));
            table = ps._A_sortIndexesByDistance(ps._A_subDomain(1), 1);
            Assert.That(table[0], Is.EqualTo(1));
            Assert.That(table[1], Is.EqualTo(0));

            ps.add(new PerfPoint(-8, 5, false));
            ps.add(new PerfPoint(-5, -1, false));
            ps.add(new PerfPoint(-1, -1, true));
            ps.add(new PerfPoint(0, 1, false));
            ps.add(new PerfPoint(1, 1, false));
            ps.add(new PerfPoint(3, 0, false));
            ps.add(new PerfPoint(7, 3, false));
            ps.add(new PerfPoint(11, 0, true));
            ps.add(new PerfPoint(15, 9, false));
            ps.add(new PerfPoint(19, 13, false));
            ps.add(new PerfPoint(32, 41, false));
            Assert.That(ps.count, Is.EqualTo(13));


            table = ps._A_sortIndexesByDistance(ps._A_subDomain(3.5), 3.5);
            Assert.That(table.Length, Is.EqualTo(6));
            Assert.That(table[0], Is.EqualTo(7));

            table = ps._A_sortIndexesByDistance(ps._A_subDomain(-5), -5);
            Assert.That(table.Length, Is.EqualTo(4));
            Assert.That(table[0], Is.EqualTo(3));

        }



        // Test du service de prédiction
        [Test]
        public void predict_1()
        {

            PerfSerie ps = new PerfSerie();
            bool result = false;

            // Lève une exception quand aucun point n'est inséré dans la série
            try
            {
                ps.predict(0);
            }
            catch (ModelException e)
            {
                result = true;
            }
            Assert.That(result, Is.True);

            // Insertion de points de performance
            ps.add(new PerfPoint(-8, 3, false));
            ps.add(new PerfPoint(-4, 1, false));

            // Lève une exception quand on tente une interpolation en dehors du domaine de calcul
            result = false;
            try
            {
                ps.predict(10);
            }
            catch (ModelException e)
            {
                result = true;
            }
            Assert.That(result, Is.True);

            // Interpolation linéaire quand il n'pp2 a que deux points de performance
            Assert.That(2.5, Is.EqualTo(ps.predict(-7)));

            ps.add(new PerfPoint(1, (1 / 8) * Math.Pow(1, 2) + (1) + 3, false));
            ps.add(new PerfPoint(3, (1 / 8) * Math.Pow(3, 2) + (3) + 3, false));
            ps.add(new PerfPoint(8, (1 / 8) * Math.Pow(8, 2) + (8) + 3, false));
            ps.setRange();

            // Test de la prédiction de niveau 2 (polynôme ² avec 3 points de performance)
            Assert.That(isWithinPrecision((double)ps.predict(5),
                                         (1 / 8) * Math.Pow(5, 2) + (5) + 3,
                                         0.0000001), Is.True);
        }



        bool isWithinPrecision(double x1, double x2, double precision)
        {

            if (x1 >= x2 - precision || x1 <= x2 + precision)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}
