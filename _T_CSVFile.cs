using System;
using System.IO;
using NUnit.Framework;
using AeroCalcCore;
using System.Collections.Generic;


// TODO Test à revoir complètement, CSVFile est une classe abstraite et getLineIndex est inaccessible !

namespace TestAeroCalc
{



    [TestFixture]
    public class _T_CSVFile
    {
        _A_CSVFile CSVF;

        [SetUp]
        public void SetUp()
        {
            CSVF = new _A_CSVFile();

            List<string> l = new List<string>();
            l.Add("Truc");
            l.Add("Machin");
            l.Add("Chose");
            l.Add("Et autre...");

            CSVF.setFileLines(l.ToArray());
        }



        [Test]
        public void getLineIndex_1()
        {
            //Assert.IsEqual(2, CSVF.("Chose"));
            Assert.AreEqual(2,CSVF._A_getLineIndex("Chose"));
        }

    }









}