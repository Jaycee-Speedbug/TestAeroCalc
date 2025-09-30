using System;
using System.IO;
using NUnit.Framework;
using AeroCalcCore;

namespace TestAeroCalc
{


    [TestFixture]
    public class _T_PerfLayer
    {


        [SetUp]
        public void SetUp() {

        }



        [Test]
        public void PerfLayer_1() {

            PerfLayer pl = new PerfLayer();
            pl.add(new PerfPoint(-8, 1, false), 100);
            pl.add(new PerfPoint(-4, 1, false), 100);
            pl.add(new PerfPoint(0, 1, false), 100);
            pl.add(new PerfPoint(-10, 2, false), 200);
            pl.add(new PerfPoint(-3, 2, false), 200);
            pl.add(new PerfPoint(-1, 2, false), 200);
            pl.add(new PerfPoint(4, 2, false), 200);

            Assert.That(pl.count, Is.EqualTo(2));
            Assert.That(pl.SerieAt(0).count, Is.EqualTo(3));
            Assert.That(pl.SerieAt(1).count, Is.EqualTo(4));
            Assert.That(pl.SerieAt(0).pointAt(1).factorValue, Is.EqualTo(-4));
            Assert.That(pl.SerieAt(1).pointAt(2).factorValue, Is.EqualTo(-1));
            Assert.That(pl.count, Is.EqualTo(5));
        }


        [Test]
        public void sortedClosestSeries_1() {

            PerfLayer pl = new PerfLayer();
            pl.add(new PerfPoint(1, 4, false), 100);
            pl.add(new PerfPoint(3, 5, false), 120);
            pl.add(new PerfPoint(8, 6, false), 200);
            pl.add(new PerfPoint(10, 10, false), 210);
            pl.add(new PerfPoint(3, 5, false), 260);

            Assert.That(pl.count, Is.EqualTo(5));

            int[] table = pl._A_SortedClosestSeries(195);

            Assert.That(table.Length, Is.EqualTo(5));
            Assert.That(table[0], Is.EqualTo(2));
            Assert.That(table[1], Is.EqualTo(3));
            Assert.That(table[2], Is.EqualTo(4));
            Assert.That(table[3], Is.EqualTo(1));
            Assert.That(table[4], Is.EqualTo(0));
        }


        [Test]
        public void selectSubLayer_1() {

            PerfLayer pl = new PerfLayer();

            pl.add(new PerfPoint(1, 4, false), 100);
            pl.add(new PerfPoint(3, 5, false), 120);
            pl.add(new PerfPoint(8, 6, false), 200);
            pl.add(new PerfPoint(10, 10, false), 210);
            pl.add(new PerfPoint(3, 5, false), 260);
            pl.add(new PerfPoint(8, 12, false), 270);

            pl._A_SelectSubLayer(195, 3);

            Assert.That(pl.count, Is.EqualTo(6));
            Assert.That(pl.selectedCount(), Is.EqualTo(3));

            Assert.That(pl.SerieAt(0).selected, Is.False);
            Assert.That(pl.SerieAt(1).selected, Is.False);
            Assert.That(pl.SerieAt(2).selected, Is.True);
            Assert.That(pl.SerieAt(3).selected, Is.True);
            Assert.That(pl.SerieAt(4).selected, Is.True);
            Assert.That(pl.SerieAt(5).selected, Is.False);
        }


        [Test]
        public void predict_1() {

            bool result = false;
            PerfSerie ps1 = new PerfSerie(100);
            PerfSerie ps2 = new PerfSerie(200);
            PerfSerie ps3 = new PerfSerie(300);
            PerfLayer pl = new PerfLayer();

            ps1.add(new PerfPoint(-8, 1, false));
            ps1.add(new PerfPoint(-4, 1, false));
            ps1.add(new PerfPoint(-1, 1, false));
            ps1.add(new PerfPoint(0, 1, false));
            ps1.add(new PerfPoint(6, 1, false));

            ps2.add(new PerfPoint(-10, 2, false));
            ps2.add(new PerfPoint(-3, 2, false));
            ps2.add(new PerfPoint(-1, 2, false));
            ps2.add(new PerfPoint(7, 2, false));
            ps2.add(new PerfPoint(9, 2, false));

            ps3.add(new PerfPoint(-6, 4, false));
            ps3.add(new PerfPoint(-2.5, 4, false));
            ps3.add(new PerfPoint(-0.5, 4, false));
            ps3.add(new PerfPoint(0.5, 4, false));
            ps3.add(new PerfPoint(4, 4, false));

            pl.add(ps1);
            pl.add(ps2);
            pl.add(ps3);

            result = true;
            try
            {
                Assert.That(pl.predict(2, 150), Is.EqualTo(1.375));
            }
            catch (ModelException)
            {
                // No exception should be raised here
                result = false;
            }
            // Remplacez Assert.IsTrue(result); par Assert.That(result, Is.True);
            Assert.That(result, Is.True);
        }

    }

}
