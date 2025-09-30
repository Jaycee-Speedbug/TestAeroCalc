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



        // Remplacer Assert.AreEqual(...) par Assert.That(..., Is.EqualTo(...))
        [Test]
        public void setWorkDirectory_1()
        {
            Assert.That(fio.setWorkDirectory(AppDomain.CurrentDomain.BaseDirectory + "/testdir/"), Is.True);
            Assert.That(fio.IOStatus, Is.EqualTo(FileIO.FILEOP_SUCCESSFUL));
        }

        [Test]
        public void filesInDirectory_1()
        {

            fio.setWorkDirectory("theunknowndir");
            List<string> ls = fio.filesInDirectory("", "*.*");
            Assert.That(ls.Count, Is.EqualTo(0));

            fio.setWorkDirectory(AppDomain.CurrentDomain.BaseDirectory + "/testdir/");
            ls = fio.filesInDirectory("", "*.txt");
            Assert.That(ls.Count, Is.EqualTo(2));
            /*
            */
        }

    }









}