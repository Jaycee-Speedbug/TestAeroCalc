using System;
using NUnit.Framework;
using AeroCalcCore;



namespace TestAeroCalc
{


    [TestFixture]
    public class _T_PostProcessor
    {



        PostProcessor PP;



        [SetUp]
        public void SetUp()
        {
            PP = new PostProcessor(new EnvironmentContext("config/config.xml"));
        }



        [Test]
        public void formatMsg_1()
        {
            string[] table = { "MSG1", "MSG2" };
            string message = "Test avec $0, et aussi $1";
            string output = PP._A_formatMsg(message, table);

            StringAssert.AreEqualIgnoringCase("Test avec MSG1, et aussi MSG2", output);
        }



    }
}
