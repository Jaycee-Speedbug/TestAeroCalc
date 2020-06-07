using System;
using NUnit.Framework;
using AeroCalcCore;



namespace TestAeroCalc
{


    [TestFixture]
    public class _T_PostProcessor
    {



        [SetUp]
        public void SetUp()
        {

        }



        [Test]
        public void formatMessage()
        {
            string[] table = { "MSG1", "MSG2" };
            string message = "Ceci est un message de test avec une première donnée : $0, et une seconde : $1";

            StringAssert.AreEqual("Ceci est un message de test avec une première donnée : MSG1, et une seconde : MSG2", formatMessage(message,table));
        }



    }
}
