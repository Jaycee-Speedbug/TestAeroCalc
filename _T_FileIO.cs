using System;
using System.IO;
using NUnit.Framework;
using AeroCalcCore;



namespace TestAeroCalc
{



    [TestFixture]
    public class _T_FileIO
    {



        [SetUp]
        public void SetUp()
        {

        }



        [Test]
        public void setWorkDirectory_1()
        {
            FileIO fio = new FileIO();
            Assert.IsTrue(fio.setWorkDirectory(AppDomain.CurrentDomain.BaseDirectory + "/testdir/"));
            Assert.AreEqual(FileInfo.FILEOP_SUCCESSFULL, fio.IOStatus);
        }

    }









}