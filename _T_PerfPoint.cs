using System;
using NUnit.Framework;
using AeroCalcCore;



namespace TestAeroCalc
{



    [TestFixture]
    public class _T_PerfPoint
    {
        PerfPoint ppA;
        PerfPoint ppB;
        PerfPoint ppC;

        [SetUp]
        public void SetUp() {

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
