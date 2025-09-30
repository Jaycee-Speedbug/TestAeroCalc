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
        public void loadConfigFile_1()
        {
            EnvCtxt.loadConfigFile("");
            Assert.That(EnvCtxt.status != FileIO.FILEOP_SUCCESSFUL);
        }

        [Test]
        public void loadConfigFile_2()
        {
            EnvCtxt.loadConfigFile("/etc/bin/bash.txt");
            Assert.That(EnvCtxt.status, Is.EqualTo(FileIO.FILEOP_INVALID_PATH));
        }

        [Test]
        public void loadConfigFile_3()
        {
            EnvCtxt.loadConfigFile("config/testconfig.xml");
            Assert.That(EnvCtxt.status, Is.EqualTo(FileIO.FILEOP_SUCCESSFUL));
        }
    }
}