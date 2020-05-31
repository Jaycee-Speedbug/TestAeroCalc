using System;
using System.IO;
using NUnit.Framework;
using AeroCalcCore;



namespace TestAeroCalc
{



    [TestFixture]
    public class _T_CSVFile
    {



        [SetUp]
        public void SetUp()
        {
            CSVFile csvf = new CSVFile();

            List<string> l = new List<string>();
            l.Add("Truc");
            l.Add("Machin");
            l.Add("Chose");
            l.Add("Et autre...");
        }



        [Test]
        public void getLineIndex_1()
        {
            Assert.IsEqual(2, csvf.getLineIndex("Chose"));
        }

    }









}