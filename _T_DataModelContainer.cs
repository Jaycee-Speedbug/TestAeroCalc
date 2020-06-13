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
        DataModelContainer dataModels;

        [SetUp]
        public void SetUp() {

        }

        [Test]
        public void expendFilter_1() {

            dataModels = new DataModelContainer();

            string[] filter1 = { "AAA", "BBB", "CCC", "DDD" };
            string[] returnTable = dataModels._A_expendFilter(filter1, 4);
            for (int count = 0; count < 4; count++) {
                //TODO A revoir complètement
                StringAssert.AreEqualIgnoringCase(filter1[count], returnTable[count]);
            }

            string[] filter2 = { "AAA", "*", "DDD" };
            returnTable = dataModels._A_expendFilter(filter2, 4);
            Assert.AreEqual("AAA", returnTable[0]);
            Assert.AreEqual("*", returnTable[1]);
            Assert.AreEqual("*", returnTable[2]);
            Assert.AreEqual("DDD", returnTable[3]);

            string[] filter3 = { "AAA", "*" };
            returnTable = dataModels._A_expendFilter(filter3, 4);
            Assert.AreEqual("AAA", returnTable[0]);
            Assert.AreEqual("*", returnTable[1]);
            Assert.AreEqual("*", returnTable[2]);
            Assert.AreEqual("*", returnTable[3]);

            string[] filter4 = { "*", "DDD" };
            returnTable = dataModels._A_expendFilter(filter4, 4);
            Assert.AreEqual("*", returnTable[0]);
            Assert.AreEqual("*", returnTable[1]);
            Assert.AreEqual("*", returnTable[2]);
            Assert.AreEqual("DDD", returnTable[3]);
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
