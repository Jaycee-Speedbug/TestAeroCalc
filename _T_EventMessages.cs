using NUnit.Framework;
using AeroCalcCore;
using System;

namespace TestAeroCalc
{


    [TestFixture]
    public class _T_EventMessages
    {
        private EventMessages EM;

        [SetUp]
        public void SetUp()
        {
            EM = new EventMessages();
        }



        [Test]
        public void AddEventMsg_1()
        {
            EM.addItem(500, "Message évènement 500");
            EM.addItem(10510, "Message évènement -510");
            EM.addItem(10450, "Message évènement 450");
            EM.Add(new EventMessage(10400, "Message évènement 400"));

            // We should find that record
            string msg = EM._A_getEventMessage(-510).msgStr;
            Assert.IsNotNull(msg);

            // Should find that message
            msg = EM._A_getEventMessage(500).msgStr;
            StringAssert.AreEqualIgnoringCase("Message évènement 500", msg);

            // We should have recorded that 'Count' elements
            Assert.AreEqual(EM.Count, 4);

            // No addition of an EventMessage with the same eventCode
            int count = EM.Count;
            EM.Add(new EventMessage(500, "Il y en a déjà un message pour le code -500..."));
            Assert.IsTrue(count == EM.Count);

            // No addition of an EventMessage with the same eventCode
            count = EM.Count;
            EM.addItem(10510, "Il y en a déjà un message pour le code -510...");
            Assert.IsTrue(count == EM.Count);
        }

    }

}