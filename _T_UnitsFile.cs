using NUnit.Framework;
using AeroCalcCore;
using System.IO;
using System;
using System.Collections.Generic;



namespace TestAeroCalc
{


    /// Test de fonctions de différents objets UnitsXMLFile et UnitsCSVFile
    [TestFixture]
    public class _T_UnitsFile
    {



        [SetUp]
        public void SetUp()
        {

        }



        [Test]
        public void saveUnitDictionaryToXML_1()
        {
            // Création du dictionnaire

            UnitsCSVFile uf = new UnitsCSVFile();
            Units ud = uf.getUnitsFromCSV(AppDomain.CurrentDomain.BaseDirectory + Path.DirectorySeparatorChar + "UnitsDictionary.csv");
            List<Unit> lu = ud.getUnits();
            Assert.IsTrue(lu.Count > 0);
            foreach (Unit u in lu)
            {
                System.Console.WriteLine(u.ToString());
            }

            UnitsXMLFile xmlFile = new UnitsXMLFile("");
            xmlFile.saveUnitDictionaryToXML(ud, AppDomain.CurrentDomain.BaseDirectory + "TEST-Units.xml");
        }

    }

}