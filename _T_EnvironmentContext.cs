using Microsoft.VisualStudio.TestTools.UnitTesting;
using AeroCalcCore;


namespace TestAeroCalc
{
    [TestClass]
    public class _T_EnvironmentContext
    {

        public readonly EnvironmentContext EC;


        [TestMethod]
        public void _T_EnvironmentContext()
        {
            EC = new EnvironmentContext("nothing.xml");
            Assert.IsFalse(EC.configFilePath, "");

        }
    }
}
