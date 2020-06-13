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

        [SetUp]
        public void SetUp() {

        }



        [Test]
        public void areColocated_1() {
            ppA = new PerfPoint(1, 5, false);
            ppB = new PerfPoint(3, 2, false);

            Assert.IsFalse(PerfPoint.areColocated(ppA, ppB));
        }

        [Test]
        public void areColocated_2() {
            ppA = new PerfPoint((2 * Math.Sqrt(2)) / 2, 6, false);
            ppB = new PerfPoint((Math.Sqrt(2) / 2) * 2, 7, true);

            Assert.IsTrue(PerfPoint.areColocated(ppA, ppB));
        }

        [Test]
        public void Compare_1() {
            ppA = new PerfPoint((2 * Math.Sqrt(2)) / 2, 6, false);
            ppB = new PerfPoint((Math.Sqrt(2) / 2) * 2, 7, true);

            Assert.AreEqual(0, ppA.Compare(ppA, ppB));
        }

        [Test]
        public void Compare_2() {
            ppA = new PerfPoint(1, 5, false);
            ppB = new PerfPoint(3, 2, false);

            Assert.AreEqual(-1, ppA.Compare(ppA, ppB));
        }

        [Test]
        public void Compare_3() {
            ppA = new PerfPoint(10, 5, false);
            ppB = new PerfPoint(3, 2, false);

            Assert.AreEqual(1, ppA.Compare(ppA, ppB));
        }
    }
}
