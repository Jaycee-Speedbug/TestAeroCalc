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
        public void loadConfigFile()
        {
            EnvCtxt.loadConfigFile("");
            Assert.AreEqual((double)EnvCtxt.status, (double)FileIO.FILEOP_INVALID_PATH);

            EnvCtxt.loadConfigFile("config/config.xml");
            Assert.AreEqual(EnvCtxt.status, FileIO.FILEOP_SUCCESSFUL);
            
        }

    }

}