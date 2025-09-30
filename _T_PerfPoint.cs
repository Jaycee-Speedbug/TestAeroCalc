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



        // Remplacez Assert.IsFalse(...) par Assert.That(..., Is.False)
        // Remplacez Assert.IsTrue(...) par Assert.That(..., Is.True)

        [Test]
        public void areColocated_1()
        {
            ppA = new PerfPoint(1, 5, false);
            ppB = new PerfPoint(3, 2, false);

            Assert.That(PerfPoint.areColocated(ppA, ppB), Is.False);
        }

        [Test]
        public void areColocated_2()
        {
            ppA = new PerfPoint((2 * Math.Sqrt(2)) / 2, 6, false);
            ppB = new PerfPoint((Math.Sqrt(2) / 2) * 2, 7, true);

            Assert.That(PerfPoint.areColocated(ppA, ppB), Is.True);
        }

        [Test]
        public void Compare_1() {
            ppA = new PerfPoint((2 * Math.Sqrt(2)) / 2, 6, false);
            ppB = new PerfPoint((Math.Sqrt(2) / 2) * 2, 7, true);

            Assert.That(ppA.Compare(ppA, ppB), Is.EqualTo(0));
        }

        [Test]
        public void Compare_2() {
            ppA = new PerfPoint(1, 5, false);
            ppB = new PerfPoint(3, 2, false);

            Assert.That(ppA.Compare(ppA, ppB), Is.EqualTo(-1));
        }

        [Test]
        public void Compare_3() {
            ppA = new PerfPoint(10, 5, false);
            ppB = new PerfPoint(3, 2, false);

            Assert.That(ppA.Compare(ppA, ppB), Is.EqualTo(1));
        }
    }
}
