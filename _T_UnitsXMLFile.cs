using System;
using System.IO;
using NUnit.Framework;
using AeroCalcCore;



namespace TestAeroCalc
{


    [TestFixture]
    public class _T_UnitsXMLFile
    {
        [SetUp]
        public void SetUp()
        {
        }

        [Test]
        public void getUnitsFromXML_1()
        {
            // Création du dictionnaire

            UnitsXMLFile uf = new UnitsXMLFile(AppDomain.CurrentDomain.BaseDirectory +
                                               Path.DirectorySeparatorChar +
                                               "Test_UnitsDictionary.xml");
            Units ud = uf.getUnitsFromXML(AppDomain.CurrentDomain.BaseDirectory +
                                          Path.DirectorySeparatorChar +
                                          "Test_UnitsDictionary.xml");
            Assert.That(ud, Is.Not.Null);

            /*
            List<Unit> lu = ud.getUnits();
            Assert.IsTrue(lu.Count > 1);
            
            foreach (Unit u in lu)
            {
                System.Console.WriteLine(u.ToString());
            }

            // Enregistrement du dictionnaire
            UnitsXMLFile xmlFile = new UnitsXMLFile("");
            xmlFile.saveUnitDictionaryToXML(ud, AppDomain.CurrentDomain.BaseDirectory + "TEST-Units.xml");
            */
        }
    }

}