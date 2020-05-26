using NUnit.Framework;
using CLTool;
using AeroCalcCore;


namespace TestAeroCalc
{
    [TestFixture]
    public class _T_EnvironmentContext
    {
        [SetUp]
        public void Setup()
        {
            EnvironementContext EC = new EnvironementContext("nothing.xml");
        }

        [Test]
        public void Test1()
        {
            
            Assert.Pass();
        }
    }
}