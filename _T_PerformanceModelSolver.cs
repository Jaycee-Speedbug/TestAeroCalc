using System;
using NUnit.Framework;
using AeroCalcCore;



namespace TestAeroCalc
{

    [TestFixture]
    public class _T_PerformanceModelSolver
    {

        [SetUp]
        public void SetUp() {
            //ps = new PerfSerie();
            //pms = new PerformanceModelSolver(ps);
        }



        [Test]
        public void constructor()
        {
            PerfSerie ps2 = null;
            PerformanceModelSolver pms;
            try
            {
                // Prédiction sur une série vide, doit lever une exception
                pms = new PerformanceModelSolver(ps2);
            }
            catch (ModelException ex)
            {
                Assert.That(ex.nature == AeroCalc.E_VOID_SYSTEM);
            }

            ps2 = new PerfSerie();
            try
            {
                // Prédiction sur une série ne comportant qu'un seul point, doit lever une exception
                pms = new PerformanceModelSolver(ps2);
            }
            catch (ModelException ex)
            {
                Assert.That(ex.nature == AeroCalc.E_TOO_SHORT_SERIE);
            }

        }



        // Test des helpers
        /// <summary>
        /// Tests that the ordered index retrieval by distance returns the expected order of indexes for a given target
        /// value.
        /// </summary>
        /// <remarks>
        /// This test verifies that the PerformanceModelSolver._A_orderedIndexesByDistance method
        /// correctly orders the indexes of PerfSerie points based on their proximity to the specified value. It asserts
        /// that the returned array matches the expected order for a sample data set.
        /// </remarks>
        [Test]
        public void orderedIndexesByDistance_1()
        {
            
            PerfSerie ps2 = new PerfSerie();
            ps2.add(new PerfPoint(3, 0.5 * Math.Pow(3, 2) + (3) - 1, false));
            ps2.add(new PerfPoint(1, 0.5 * Math.Pow(1, 2) + (1) - 1, false));
            ps2.add(new PerfPoint(-5, 0.5 * Math.Pow(-5, 2) + (-5) - 1, false));
            ps2.add(new PerfPoint(-2, 0.5 * Math.Pow(-2, 2) + (-2) - 1, false));
            ps2.add(new PerfPoint(8, 0.5 * Math.Pow(8, 2) + (8) - 1, false));
            
            // Abscisses des points (triés) de la série : -5, -2, 1, 3, 8
            // On teste la recherche des points les plus proches de -1.0

            PerformanceModelSolver pms = new PerformanceModelSolver(ps2);

            int[] testTable = pms._A_orderedIndexesByDistance(-1.0);
            Assert.That(testTable[0] == 1); // point d'abscisse -2
            Assert.That(testTable[1] == 2); // point d'abscisse 1
            Assert.That(testTable[2] == 0); // point d'abscisse -5
            Assert.That(testTable[3] == 3); // point d'abscisse 3
            Assert.That(testTable[4] == 4); // point d'abscisse 8

        }


        /// <summary>
        /// Test de la régression polynomiale sur un système vide
        /// </summary>
        [Test]
        public void interpolate_1()
        {
            PerfSerie ps = new PerfSerie();
            // Test de la régression polynomiale
            PerformanceModelSolver pms = new PerformanceModelSolver(ps);
            bool result = false;
            try {
                pms.interpolateLagrange(5.0);
            }
            catch (ModelException e) {
                result = (e.nature == AeroCalc.E_VOID_SYSTEM ? true : false);
            }
            Assert.That(result, Is.True);
        }


        /// <summary>
        /// Test de la régression polynomiale sur un système à deux points
        /// </summary>
        [Test]
        public void interpolate_3()
        {
            PerfSerie ps = new PerfSerie();
            ps.add(new PerfPoint(1, 3, false));
            ps.add(new PerfPoint(4, 15, false));
            PerformanceModelSolver pms = new PerformanceModelSolver(ps);
            double expected = 3 + (15 - 3) / (4 - 1) * (2 - 1); // 7
            Assert.That(pms.interpolateLagrange(2), Is.EqualTo(expected));
        }



        /// <summary>
        /// Test de la régression polynomiale sur une série viable
        /// </summary>
        [Test]
        public void interpolate_2()
        {
            // Série basée sur le polynome : 0.5 . x^2 + x - 1
            PerfSerie ps = new PerfSerie();
            ps.add(new PerfPoint(3, 0.5 * Math.Pow(3, 2) + (3) - 1, false));
            ps.add(new PerfPoint(1, 0.5 * Math.Pow(1, 2) + (1) - 1, false));
            ps.add(new PerfPoint(-5, 0.5 * Math.Pow(-5, 2) + (-5) - 1, false));
            ps.add(new PerfPoint(-1, 0.5 * Math.Pow(-1, 2) + (-1) - 1, false));
            ps.add(new PerfPoint(8, 0.5 * Math.Pow(8, 2) + (8) - 1, false));
            ps.setRange();

            // Test de la régression polynomiale
            PerformanceModelSolver pms = new PerformanceModelSolver(ps);
            double expected = 0.5 * Math.Pow(5, 2) + (5) - 1; // 16.5

            Assert.That(pms.interpolateLagrange(5), Is.EqualTo(expected));
        }

    }
}




/*
public void Test_PI_pointsOfInterest_1() {

    PerfSerie ps1 = new PerfSerie();
    ps1.add(new PerfPoint(3, 10, false));
    ps1.add(new PerfPoint(1, 5, true));
    ps1.add(new PerfPoint(-5, 1, false));
    ps1.add(new PerfPoint(-1, 15, false));
    ps1.add(new PerfPoint(8, 6, false));
    pms = new PerformanceModelSolver(ps1);
    ps1.selectAll();

    // Extraction des trois points les plus proche avec pp1 en dehors du range des points sélectionnés
    int[] tab = pms._A_selectedPointsTable(-6, 3);
    Assert.AreEqual(0, tab[0]);
    Assert.AreEqual(1, tab[1]);
    Assert.AreEqual(2, tab[2]);
}


        [Test]
        public void interpolate_1() {

            PerfSerie ps = new PerfSerie();
            ps.selectAll();
            ps.setRange();
            bool result = false;

            try {
                //pms.interpolateLagrange(5.0);
            }
            catch (ModelException e) {
                result = (e.nature == AeroCalc.E_VOID_SYSTEM ? true : false);
            }
            Assert.That(result, Is.True);
        }




/*
[Test]



[TestMethod]
public void Test_PI_pointsOfInterest_2() {

    PerfSerie ps1 = new PerfSerie();
    ps1.add(new PerfPoint(3, 10, false));
    ps1.add(new PerfPoint(1, 5, true));
    ps1.add(new PerfPoint(-5, 1, false));
    ps1.add(new PerfPoint(-1, 15, false));
    ps1.add(new PerfPoint(8, 6, false));
    PolynomialInterpolation pms = new PolynomialInterpolation(ps1);
    ps1.selectAll();

    // Extraction des 3 layers les plus proches de 0.5 (situé dans le coeur du range)
    int[] tab = pms.test_pointsOfInterest(0.5, 3);
    Assert.AreEqual(2, tab[0]);
    Assert.AreEqual(1, tab[1]);
    Assert.AreEqual(3, tab[2]);

}



[TestMethod]
public void Test_PI_pointsOfInterest_3() {

    PerfSerie ps1 = new PerfSerie();
    ps1.add(new PerfPoint(-12, 10, false));
    ps1.add(new PerfPoint(-8, 5, true));
    ps1.add(new PerfPoint(0, 1, false));
    ps1.add(new PerfPoint(-1, -1, false));
    ps1.add(new PerfPoint(3, 0, false));
    ps1.add(new PerfPoint(7, 3, false));
    ps1.add(new PerfPoint(15, 9, false));
    ps1.add(new PerfPoint(32, 41, false));
    PolynomialInterpolation pms = new PolynomialInterpolation(ps1);
    ps1.selectAll();

    // Extraction des 4 layers les plus proches de 7.1 (situé dans le coeur du range)
    int[] tab = pms.test_pointsOfInterest(7.1, 4);
    Assert.AreEqual(5, tab[0]);
    Assert.AreEqual(4, tab[1]);
    Assert.AreEqual(3, tab[2]);
    Assert.AreEqual(6, tab[3]);

}



[TestMethod]
public void Test_PI_pointsOfInterest_4() {

    PerfSerie ps1 = new PerfSerie();
    ps1.add(new PerfPoint(-12, 10, false));
    ps1.add(new PerfPoint(-8, 5, true));
    PolynomialInterpolation pms = new PolynomialInterpolation(ps1);
    ps1.selectAll();

    // Sélection de 4 layers, mais dans un range de 2 layers
    int[] tab = pms.test_pointsOfInterest(-15, 4);
    Assert.AreEqual(2, tab.Length);
    Assert.AreEqual(0, tab[0]);
    Assert.AreEqual(1, tab[1]);

    // Extraction d'un seul point, en dehors du range formé par deux layers 
    tab = pms.test_pointsOfInterest(-15, 1);
    Assert.AreEqual(1, tab.Length);
    Assert.AreEqual(0, tab[0]);
}
*/
