using NUnit.Framework;
using AeroCalcCore;


namespace TestAeroCalc
{


    [TestFixture]
    public class _T_EventMessages
    {

        private EventMessages EM;
        private string _T_MSG_500 = "Message évènement 500";
        private string _T_MSG_510 = "Message évènement 510";
        private string _T_MSG_450 = "Message évènement 450";
        private string _T_MSG_400 = "Message évènement 400";



        [SetUp]
        public void SetUp()
        {
            EM = new EventMessages();
        }



        [Test]
        public void AddEventMsg()
        {

            EM.addItem(-500, _T_MSG_500);
            EM.addItem(-510, _T_MSG_510);
            EM.addItem(-450, _T_MSG_450);
            EM.Add(new EventMessage(-400, _T_MSG_400));

            Warn.If(EM.Count != 4);

            // No addition of an EventMessage with the same eventCode
            int count = EM.Count;
            EM.Add(new EventMessage(-500, "Il y en a déjà un message pour le code -500..."));
            Assert.IsTrue(count == EM.Count);

            // We should find that record
            Assert.IsTrue(_T_MSG_510.Equals(EM.getEventMessage(-510).eMessage));



        }

    }

}