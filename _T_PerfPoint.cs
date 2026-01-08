using AeroCalcCore;
using NUnit.Framework;



namespace TestAeroCalc
{



    [TestFixture]
    public class _T_PerfPoint
    {
        PerfPoint ppA;
        PerfPoint ppB;
        PerfPoint ppC;

        [SetUp]
        public void SetUp()
        {

        }



        [Test]
        public void PerfPoint_1()
        {
            ppA = new PerfPoint(3, 2, true);
            ppB = new PerfPoint(ppA);
            Assert.That(ppA.input, Is.EqualTo(3));
            Assert.That(ppA.output, Is.EqualTo(2));
            Assert.That(ppA.isBreak, Is.EqualTo(true));
        }



        [Test]
        public void PerfPoint_2()
        {
            ppA = new PerfPoint(5, -1.5, false);
            Assert.That(ppA.input, Is.EqualTo(5));
            Assert.That(ppA.output, Is.EqualTo(-1.5));
            Assert.That(ppA.isBreak, Is.EqualTo(false));
        }



        [Test]
        public void compareTo_1()
        {
            ppA = new PerfPoint(1, 5, false);
            ppB = new PerfPoint(3, 2, false);
            ppC = new PerfPoint(1, 1, false);

            Assert.That(ppA.CompareTo(ppB) < 0);
            Assert.That(ppB.CompareTo(ppA) > 0);
            Assert.That(ppA.CompareTo(ppC), Is.EqualTo(0));
        }
    }
}
