using System;
using NUnit.Framework;
using AeroCalcCore;



namespace TestAeroCalc
{



    [TestFixture]
    public class _T_PerfSerie
    {



        [SetUp]
        public void SetUp() {

        }



        [Test]
        public void PerfSerie_1() {

            PerfSerie ps = new PerfSerie();
            ps.add(new PerfPoint(3, 10, false));
            ps.add(new PerfPoint(1, 5, true));
            ps.add(new PerfPoint(-5, 1, false));

            PerfSerie psClone = new PerfSerie(ps);

            Assert.That(3, Is.EqualTo(psClone.count));

            for (int count = 0; count < ps.count; count++) {
                Assert.That(ps.pointAt(count).factorValue, Is.EqualTo(psClone.pointAt(count).factorValue));
            }
        }


        // Remplacer Assert.IsTrue(...) par Assert.That(..., Is.True) et Assert.IsFalse(...) par Assert.That(..., Is.False)

        [Test]
        public void add_1()
        {

            PerfSerie ps = new PerfSerie();

            Assert.That(ps.add(new PerfPoint(1, 5, false)), Is.True);
            Assert.That(ps.add(new PerfPoint(1, 10, false)), Is.False);
        }


        // Test du tri des layers de performance
        [Test]
        public void add_2() {

            PerfSerie ps = new PerfSerie();
            ps.add(new PerfPoint(3, 10, false));
            ps.add(new PerfPoint(1, 5, true));
            ps.add(new PerfPoint(-5, 1, false));
            ps.add(new PerfPoint(-1, 15, false));
            ps.add(new PerfPoint(8, 6, false));

            Assert.That(-5, Is.EqualTo(ps.pointAt(0).factorValue));
            Assert.That(-1, Is.EqualTo(ps.pointAt(1).factorValue));
            Assert.That(1, Is.EqualTo(ps.pointAt(2).factorValue));
            Assert.That(3, Is.EqualTo(ps.pointAt(3).factorValue));
            Assert.That(8, Is.EqualTo(ps.pointAt(4).factorValue));
        }


        // Test du comptage des layers sélectionnés
        [Test]
        public void selectedCount_1() {

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
            pp3.selected = true;
            pp4.selected = true;

            Assert.That(2, Is.EqualTo(ps.selectedCount()));
        }


        // Test de la fonction renvoyant l'index d'un point de performance
        [Test]
        public void getIndexOf_1() {

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


        // Test du classement des indexes de layers par rapport à une abscisse de référence
        [Test]
        public void sortedClosestPoints_1() {
            PerfSerie ps = new PerfSerie();
            int[] table;

            Assert.That(ps._A_sortedClosestPoints(0), Is.Null);

            ps.add(new PerfPoint(-12, 10, false));
            table = ps._A_sortedClosestPoints(-13);
            Assert.That(0, Is.EqualTo(table[0]));

            ps.add(new PerfPoint(-9, 5, true));
            table = ps._A_sortedClosestPoints(-10);
            Assert.That(1, Is.EqualTo(table[0]));
            Assert.That(0, Is.EqualTo(table[1]));

            ps.add(new PerfPoint(-8, 5, true));
            ps.add(new PerfPoint(-5, -1, false));
            ps.add(new PerfPoint(-1, -1, false));
            ps.add(new PerfPoint(0, 1, false));
            ps.add(new PerfPoint(1, 1, false));
            ps.add(new PerfPoint(3, 0, false));
            ps.add(new PerfPoint(7, 3, false));
            ps.add(new PerfPoint(11, 0, false));
            ps.add(new PerfPoint(15, 9, false));
            ps.add(new PerfPoint(32, 41, false));

            table = ps._A_sortedClosestPoints(-13);
            Assert.That(0, Is.EqualTo(table[0]));
            Assert.That(11, Is.EqualTo(table[11]));

            table = ps._A_sortedClosestPoints(-7.5);
            Assert.That(2, Is.EqualTo(table[0]));
        }


        // Remplacer tous les Assert.IsTrue(...) et Assert.IsFalse(...) dans le fichier par Assert.That(..., Is.True) ou Assert.That(..., Is.False) respectivement.

        // Exemple pour selectPoints_1 :
        [Test]
        public void selectPoints_1()
        {

            PerfSerie ps = new PerfSerie();
            Assert.That(ps._A_selectPoints(0, 5), Is.False);

            ps.add(new PerfPoint(-12, 10, false));
            Assert.That(ps._A_selectPoints(0, 5), Is.True);
            Assert.That(ps.pointAt(0).selected, Is.True);

            ps.add(new PerfPoint(-9, 5, false));
            Assert.That(ps._A_selectPoints(0, 3), Is.True);
            Assert.That(ps.pointAt(0).selected, Is.True);
            Assert.That(ps.pointAt(1).selected, Is.True);

            ps.add(new PerfPoint(-8, 5, false));
            Assert.That(ps._A_selectPoints(-10, 2), Is.True);
            Assert.That(ps.pointAt(0).selected, Is.True);
            Assert.That(ps.pointAt(1).selected, Is.True);
            Assert.That(ps.pointAt(2).selected, Is.False);

            ps.add(new PerfPoint(-5, -1, false));
            ps.add(new PerfPoint(-1, -1, false));
            Assert.That(ps._A_selectPoints(-5.5, 4), Is.True);
            Assert.That(ps.pointAt(3).selected, Is.True);
            Assert.That(ps.pointAt(2).selected, Is.True);
            Assert.That(ps.pointAt(1).selected, Is.True);
            Assert.That(ps.pointAt(4).selected, Is.True);
            Assert.That(ps.pointAt(0).selected, Is.False);

            ps.add(new PerfPoint(0, 1, false));
            ps.add(new PerfPoint(1, 1, false));
            ps.add(new PerfPoint(3, 0, false));
            ps.add(new PerfPoint(7, 3, false));
            Assert.That(ps._A_selectPoints(-1.5, 3), Is.True);
            Assert.That(ps.pointAt(0).selected, Is.False);
            Assert.That(ps.pointAt(1).selected, Is.False);
            Assert.That(ps.pointAt(2).selected, Is.False);
            Assert.That(ps.pointAt(3).selected, Is.False);
            Assert.That(ps.pointAt(4).selected, Is.True);
            Assert.That(ps.pointAt(5).selected, Is.True);
            Assert.That(ps.pointAt(6).selected, Is.True);
            Assert.That(ps.pointAt(7).selected, Is.False);
            Assert.That(ps.pointAt(8).selected, Is.False);

        }


        // Test de sélection des layers d'intérêts, avec isBreak
        [Test]
        public void selectPoints_2() {

            PerfSerie ps = new PerfSerie();
            ps.add(new PerfPoint(-12, 10, false));
            ps.add(new PerfPoint(-9, 5, false));
            ps.add(new PerfPoint(-8, 5, false));
            ps.add(new PerfPoint(-5, -1, false));
            ps.add(new PerfPoint(-1, -1, true));
            ps.add(new PerfPoint(0, 1, false));
            ps.add(new PerfPoint(1, 1, true));
            ps.add(new PerfPoint(3, 0, true));
            ps.add(new PerfPoint(7, 3, false));
            Assert.That(ps._A_selectPoints(-1.5, 3), Is.Null);

            Assert.That(ps.pointAt(0).selected, Is.Null);
            Assert.That(ps.pointAt(1).selected, Is.Null);
            Assert.That(ps.pointAt(2).selected, Is.Null);
            Assert.That(ps.pointAt(3).selected, Is.Null);
            Assert.That(ps.pointAt(4).selected, Is.Null);
            Assert.That(ps.pointAt(5).selected, Is.Null);
            Assert.That(ps.pointAt(6).selected, Is.Null);
            Assert.That(ps.pointAt(7).selected, Is.Null);
            Assert.That(ps.pointAt(8).selected, Is.Null);

            Assert.That(ps._A_selectPoints(2, 3), Is.Null);

            Assert.That(ps.pointAt(0).selected, Is.Null);
            Assert.That(ps.pointAt(1).selected, Is.Null);
            Assert.That(ps.pointAt(2).selected, Is.Null);
            Assert.That(ps.pointAt(3).selected, Is.Null);
            Assert.That(ps.pointAt(4).selected, Is.Null);
            Assert.That(ps.pointAt(5).selected, Is.Null);
            Assert.That(ps.pointAt(6).selected, Is.Null);
            Assert.That(ps.pointAt(7).selected, Is.Null);
            Assert.That(ps.pointAt(8).selected, Is.Null);

        }


        // Test du service de prédiction
        [Test]
        public void predict_1() {

            PerfSerie ps = new PerfSerie();
            bool result = false;

            // Lève une exception quand aucun point n'est inséré dans la série
            try {
                ps.predict(0);
            }
            catch (ModelException e) {
                result = true;
            }
            Assert.That(result, Is.Null);

            // Insertion de layers (pp1 ; pp2 = (1/8)pp1² + pp1 + 3)
            ps.add(new PerfPoint(-8, 3, false));
            ps.add(new PerfPoint(-4, 1, false));

            // Lève une exception quand on tente une interpolation en dehors du domaine de calcul
            result = false;
            try {
                ps.predict(10);
            }
            catch (ModelException e) {
                result = true;
            }
            Assert.That(result, Is.Null);

            // Interpolation linéaire quand il n'pp2 a que deux layers
            // Remplacer Assert.AreEqual(...) par Assert.That(..., Is.EqualTo(...))
            Assert.That(2.5, Is.EqualTo(ps.predict(-7)));

            ps.add(new PerfPoint(1, (1 / 8) * Math.Pow(1, 2) + (1) + 3, false));
            ps.add(new PerfPoint(3, (1 / 8) * Math.Pow(3, 2) + (3) + 3, false));
            ps.add(new PerfPoint(8, (1 / 8) * Math.Pow(8, 2) + (8) + 3, false));
            ps.setRange();

            // Test de la prédiction de niveau 2 (polynôme ² avec 3 layers)
            // Remplacer Assert.IsTrue(...) par Assert.That(..., Is.True)
            Assert.That(isWithinPrecision((double)ps.predict(5),
                                         (1 / 8) * Math.Pow(5, 2) + (5) + 3,
                                         0.0000001), Is.True);
        }



        bool isWithinPrecision(double x1, double x2, double precision) {

            if (x1 >= x2 - precision || x1 <= x2 + precision) {
                return true;
            }
            else {
                return false;
            }

        }
    }
}
