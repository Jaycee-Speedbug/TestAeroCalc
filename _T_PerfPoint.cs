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

        /*
        */



        [Test]
        public void Compare_1() {
            ppA = new PerfPoint((2 * Math.Sqrt(2)) / 2, 6, false);
            ppB = new PerfPoint((Math.Sqrt(2) / 2) * 2, 7, true);

            Assert.That(ppA.CompareTo(ppB), Is.EqualTo(0));
        }

        [Test]
        public void Compare_2() {
            ppA = new PerfPoint(1, 5, false);
            ppB = new PerfPoint(3, 2, false);

            Assert.That(ppA.CompareTo(ppB), Is.EqualTo(-1));
        }

        [Test]
        public void Compare_3() {
            ppA = new PerfPoint(10, 5, false);
            ppB = new PerfPoint(3, 2, false);

            Assert.That(ppA.CompareTo(ppB), Is.EqualTo(1));
        }
    }
}
