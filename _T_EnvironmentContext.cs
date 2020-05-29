using NUnit.Framework;
using AeroCalcCore;



namespace TestAeroCalc
{
    [TestFixture]
    public class _T_EnvironmentContext
    {

        private EnvironmentContext EnvCtxt;



        [SetUp]
        public void SetUp()
        {
            EnvCtxt = new EnvironmentContext();


        }



        [Test]
        public void Test1()
        {
            Assert.Pass("It's OK!");
        }



        [Test]
        public void loadConfigFile_1()
        {
            EnvCtxt.loadConfigFile("");
            //Assert.AreEqual((double)EnvCtxt.status, (double)FileIO.FILEOP_INVALID_PATH);
            Assert.IsTrue(EnvCtxt.status != FileIO.FILEOP_SUCCESSFUL);
        }

        [Test]
        public void loadConfigFile_2()
        {
            EnvCtxt.loadConfigFile("/etc/bin/bash.txt");
            Assert.AreEqual((double)EnvCtxt.status, (double)FileIO.FILEOP_INVALID_PATH);
        }

        [Test]
        public void loadConfigFile_3()
        {
            EnvCtxt.loadConfigFile("config/config.xml");
            Assert.AreEqual(EnvCtxt.status, FileIO.FILEOP_SUCCESSFUL);
        }
    }

}