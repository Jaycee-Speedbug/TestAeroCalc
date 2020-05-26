using NUnit.Framework;
using NUnit;
using CLTool;
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
            EnvCtxt = new EnvironmentContext("nothing.xml");
        }

        [Test]
        public void Test1()
        {
            
            Assert.Pass();
            
        }
    }
}