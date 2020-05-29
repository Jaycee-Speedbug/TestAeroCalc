using NUnit.Framework;
using AeroCalcCore;
using System.IO;
using System;
using System.Collections.Generic;



namespace TestAeroCalc
{


    [TestFixture]
    public class _T_XMLFile
    {

        [SetUp]
        public void SetUp()
        {

        }




        [Test]
        public void _T_SaveUnits()
        {

            // Création du dictionnaire

            UnitsFile uf = new UnitsFile();
            Units ud = uf.readFile(AppDomain.CurrentDomain.BaseDirectory + Path.DirectorySeparatorChar + "UnitsDictionary.csv");
            List<Unit> lu = ud.getUnits();
            Assert.IsTrue(lu.Count > 0);
            foreach (Unit u in lu)
            {
                System.Console.WriteLine(u.ToString());
            }

            XMLFile xmlFile = new XMLFile();
            //xmlFile.setOutputFileAbsolutePath(AppDomain.CurrentDomain.BaseDirectory + "TEST-Units.xml");
            xmlFile.saveUnitDictionaryToXML(ud, AppDomain.CurrentDomain.BaseDirectory + "TEST-Units.xml");



        }

    }

}