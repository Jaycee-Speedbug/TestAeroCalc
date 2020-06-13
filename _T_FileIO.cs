using System;
using System.IO;
using NUnit.Framework;
using AeroCalcCore;
using System.Collections.Generic;

namespace TestAeroCalc
{

    // TODO Test à revoir, accès à une classe abstraite !!!

    [TestFixture]
    public class _T_FileIO
    {


        _A_FileIO fio;



        [SetUp]
        public void SetUp()
        {
            fio = new _A_FileIO();
        }



        [Test]
        public void setWorkDirectory_1()
        {
            // FileIO fio = new FileIO();
            Assert.IsTrue(fio.setWorkDirectory(AppDomain.CurrentDomain.BaseDirectory + "/testdir/"));
            Assert.AreEqual(FileIO.FILEOP_SUCCESSFUL, fio.IOStatus);
            /*
            */
        }

        [Test]
        public void filesInDirectory_1() {

            fio.setWorkDirectory("theunknowndir");
            List<string> ls = fio.filesInDirectory("", "*.*");
            Assert.AreEqual(0, ls.Count);

            fio.setWorkDirectory(AppDomain.CurrentDomain.BaseDirectory + "/testdir/");
            ls = fio.filesInDirectory("", "*.txt");
            Assert.AreEqual(2, ls.Count);
            /*
            */
        }

    }









}