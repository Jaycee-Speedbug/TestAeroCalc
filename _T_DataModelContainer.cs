using System;
using System.IO;
using System.Collections.Generic;
using NUnit.Framework;
using AeroCalcCore;

namespace TestAeroCalc
{

    [TestFixture]
    public class _T_DataModelContainer
    {
        private DataModelContainer dataModels = null!;

        [SetUp]
        public void SetUp() {

        }

        [Test]
        public void expendFilter_1() {

            dataModels = new DataModelContainer();

            string[] filter1 = { "AAA", "BBB", "CCC", "DDD" };
            string[] returnTable = dataModels._A_expendFilter(filter1, 4);
            Assert.That(returnTable.Length, Is.EqualTo(filter1.Length));

            for (int count = 0; count < 4; count++) {
                // TODO à revoir complètement
                // Assert.That(returnTable, Is.Null);
            }

            string[] filter2 = { "AAA", "*", "DDD" };
            returnTable = dataModels._A_expendFilter(filter2, 4);
            Assert.That(returnTable[0], Is.EqualTo("AAA"));
            Assert.That(returnTable[1], Is.EqualTo("*"));
            Assert.That(returnTable[2], Is.EqualTo("*"));
            Assert.That(returnTable[3], Is.EqualTo("DDD"));

            string[] filter3 = { "AAA", "*" };
            returnTable = dataModels._A_expendFilter(filter3, 4);
            //Assert.AreEqual("AAA", returnTable[0]);
            // Remplacez tous les appels à Assert.AreEqual(...) par Assert.That(..., Is.EqualTo(...))
            Assert.That(returnTable[0], Is.EqualTo("AAA"));
            Assert.That(returnTable[1], Is.EqualTo("*"));
            Assert.That(returnTable[2], Is.EqualTo("*"));
            Assert.That(returnTable[3], Is.EqualTo("*"));

            string[] filter4 = { "*", "DDD" };
            returnTable = dataModels._A_expendFilter(filter4, 4);
            Assert.That(returnTable[0], Is.EqualTo("*"));
            Assert.That(returnTable[1], Is.EqualTo("*"));
            Assert.That(returnTable[2], Is.EqualTo("*"));
            Assert.That(returnTable[3], Is.EqualTo("DDD"));

        }

        [Test]
        public void test_compute() {

            dataModels = new DataModelContainer();

            string modelName = "DA.FALCON.7X.7X.UV1.DRY";
            List<CommandFactor> factors = new List<CommandFactor>();
            factors.Add(new CommandFactor("SF=2"));
            factors.Add(new CommandFactor("ALT=1600"));
            factors.Add(new CommandFactor("TEMP=30"));
            factors.Add(new CommandFactor("GW=62000"));
            factors.Add(new CommandFactor("A/I=0"));

            //System.Diagnostics.Trace.Listeners.Add(new System.Diagnostics.DefaultTraceListener());


            dataModels.loadDataModels("", "DA.*");
            // TODO Tester si on peut bien injecter un nouveau directory path grace à loadDataModels()


            //System.Diagnostics.Debug.WriteLine("Data Models Directory : " + dataModels.getDataModelsDirectory()[0]);
            //System.Diagnostics.Debug.WriteLine("Press a key");

            // Assert.IsTrue(dataModels.dataModelIndex(modelName) == 0);
            // Assert.IsFalse(double.IsNaN(dataModels.compute(modelName, factors)));

        }
    }
}
