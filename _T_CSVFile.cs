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
        _A_CSVFile? CSVF;

        [SetUp]
        public void SetUp()
        {
            CSVF = new _A_CSVFile();

            List<string> l = new List<string>();
            l.Add("Truc");
            l.Add("Machin");
            l.Add("Chose");
            l.Add("Et autre...");
            l.Add("A;B;C;D;E;F;G;H");
            l.Add("100;101;102;103;104;105;106;107;108");
            l.Add("200;201;202;203;204;205;206;207");
            l.Add("END");

            CSVF.setFileLines(l.ToArray());
        }



        [Test]
        public void GetLineIndex_1()
        {
            Assert.That(CSVF!._A_GetLineIndex("Inexistant"), Is.EqualTo(-1));
            Assert.That(CSVF!._A_GetLineIndex("Chose"), Is.EqualTo(2));
        }

        [Test]
        public void StrRightOf_1()
        {
            Assert.That(CSVF!._A_StrRightOf("D"), Is.EqualTo("E"));
            Assert.That(CSVF!._A_StrRightOf("H"), Is.Null);
        }

        [Test]
        public void GetColumnIndex_1()
        {
            Assert.That(CSVF!._A_GetColumnIndex("Inexistant"), Is.EqualTo(-1));
            Assert.That(CSVF!._A_GetColumnIndex("Chose"), Is.EqualTo(0));
            Assert.That(CSVF!._A_GetColumnIndex("D"), Is.EqualTo(3));
            Assert.That(CSVF!._A_GetColumnIndex("H"), Is.EqualTo(7));
        }

        [Test]
        public void ValueWithFieldName_1()
        {
            Assert.That(CSVF!._A_ValueWithFieldName("F", -1), Is.EqualTo("105"));
            Assert.That(CSVF!._A_ValueWithFieldName("B", 6), Is.EqualTo("201"));
            Assert.That(CSVF!._A_ValueWithFieldName("D", 15), Is.Null);
            Assert.That(CSVF!._A_ValueWithFieldName("J", 6), Is.Null);
        }

    }

}