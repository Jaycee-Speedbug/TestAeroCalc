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
        public void _T_LoadCSV_SaveXML()
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

            //XMLFile xmlFile = new XMLFile();
            UnitsXMLFile xmlFile = new UnitsXMLFile("");
            //xmlFile.setOutputFileAbsolutePath(AppDomain.CurrentDomain.BaseDirectory + "TEST-Units.xml");
            xmlFile.saveUnitDictionaryToXML(ud, AppDomain.CurrentDomain.BaseDirectory + "TEST-Units.xml");



        }

    }

}