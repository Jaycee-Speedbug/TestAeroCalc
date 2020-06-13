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

            Assert.AreEqual(2, pl.count);
            Assert.AreEqual(3, pl.SerieAt(0).count);
            Assert.AreEqual(4, pl.SerieAt(1).count);
            Assert.AreEqual(-4, pl.SerieAt(0).pointAt(1).factorValue);
            Assert.AreEqual(-1, pl.SerieAt(1).pointAt(2).factorValue);
        }


        [Test]
        public void sortedClosestSeries_1() {

            PerfLayer pl = new PerfLayer();
            pl.add(new PerfPoint(1, 4, false), 100);
            pl.add(new PerfPoint(3, 5, false), 120);
            pl.add(new PerfPoint(8, 6, false), 200);
            pl.add(new PerfPoint(10, 10, false), 210);
            pl.add(new PerfPoint(3, 5, false), 260);

            Assert.AreEqual(5, pl.count);

            int[] table = pl._A_SortedClosestSeries(195);

            Assert.AreEqual(5, table.Length);
            Assert.AreEqual(2, table[0]);
            Assert.AreEqual(3, table[1]);
            Assert.AreEqual(4, table[2]);
            Assert.AreEqual(1, table[3]);
            Assert.AreEqual(0, table[4]);
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
            Assert.AreEqual(3, pl.selectedCount());
            Assert.IsFalse(pl.SerieAt(0).selected);
            Assert.IsFalse(pl.SerieAt(1).selected);
            Assert.IsTrue(pl.SerieAt(2).selected);
            Assert.IsTrue(pl.SerieAt(3).selected);
            Assert.IsTrue(pl.SerieAt(4).selected);
            Assert.IsFalse(pl.SerieAt(5).selected);
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
            try {
                Assert.AreEqual(1.375, pl.predict(2, 150));
            }
            catch (ModelException) {
                // No exception should be raised here
                result = false;
            }
            Assert.IsTrue(result);
        }

    }

}
