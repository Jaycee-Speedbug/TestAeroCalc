using System;
using NUnit.Framework;
using AeroCalcCore;



namespace TestAeroCalc
{

    [TestFixture]
    public class _T_PolInter
    {
        PerfSerie ps;
        PolInter pi;

        [SetUp]
        public void SetUp() {
            ps = new PerfSerie();
            pi = new PolInter(ps);
        }



        // Prédiction sur une série vide, doit lever une exception
        [Test]
        public void interpolate_1() {

            ps.selectAll();
            ps.setRange();
            bool result = false;

            try {
                pi.interpolate(5.0);
            }
            catch (ModelException e) {
                result = (e.nature == AeroCalc.E_VOID_SYSTEM ? true : false);
            }
            Assert.IsTrue(result);
        }



        [Test]
        public void interpolate_2() {

            ps = new PerfSerie();
            // Polynome               0.5 . x^2 + x -1
            ps.add(new PerfPoint(3, 0.5 * Math.Pow(3, 2) + (3) - 1, false));
            ps.add(new PerfPoint(1, 0.5 * Math.Pow(1, 2) + (1) - 1, false));
            ps.add(new PerfPoint(-5, 0.5 * Math.Pow(-5, 2) + (-5) - 1, false));
            ps.add(new PerfPoint(-1, 0.5 * Math.Pow(-1, 2) + (-1) - 1, false));
            ps.add(new PerfPoint(8, 0.5 * Math.Pow(8, 2) + (8) - 1, false));
            ps.setRange();
            pi = new PolInter(ps);

            // Lève une exception quand aucun point n'est sélectionné

            bool result = false;
            try {
                ps.selectNone();
                pi.interpolate(0.0);
            }
            catch (ModelException e) {
                result = (e.nature == AeroCalc.E_VOID_SYSTEM ? true : false);
            }
            Assert.IsTrue(result);

            // Test de la prédiction de niveau 2 ( polynôme ^2 )

            result = true;
            double calculation = 0.0;
            double expected = 0.5 * Math.Pow(5, 2) + (5) - 1; // 16.5
            try {
                ps.selectAll();
                calculation = pi.interpolate(5);
            }
            catch (ModelException e) {
                // Dans ce cas, aucune exception ne doit être levée
                result = false;
            }
            Assert.AreEqual(expected, calculation);
            Assert.IsTrue(result);
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
    pi = new PolInter(ps1);
    ps1.selectAll();

    // Extraction des trois points les plus proche avec pp1 en dehors du range des points sélectionnés
    int[] tab = pi._A_selectedPointsTable(-6, 3);
    Assert.AreEqual(0, tab[0]);
    Assert.AreEqual(1, tab[1]);
    Assert.AreEqual(2, tab[2]);
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
    PolynomialInterpolation pi = new PolynomialInterpolation(ps1);
    ps1.selectAll();

    // Extraction des 3 layers les plus proches de 0.5 (situé dans le coeur du range)
    int[] tab = pi.test_pointsOfInterest(0.5, 3);
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
    PolynomialInterpolation pi = new PolynomialInterpolation(ps1);
    ps1.selectAll();

    // Extraction des 4 layers les plus proches de 7.1 (situé dans le coeur du range)
    int[] tab = pi.test_pointsOfInterest(7.1, 4);
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
    PolynomialInterpolation pi = new PolynomialInterpolation(ps1);
    ps1.selectAll();

    // Sélection de 4 layers, mais dans un range de 2 layers
    int[] tab = pi.test_pointsOfInterest(-15, 4);
    Assert.AreEqual(2, tab.Length);
    Assert.AreEqual(0, tab[0]);
    Assert.AreEqual(1, tab[1]);

    // Extraction d'un seul point, en dehors du range formé par deux layers 
    tab = pi.test_pointsOfInterest(-15, 1);
    Assert.AreEqual(1, tab.Length);
    Assert.AreEqual(0, tab[0]);
}
*/
