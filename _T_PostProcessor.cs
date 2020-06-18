using System;
using NUnit.Framework;
using AeroCalcCore;
using System.IO;
using System.IO.Enumeration;

namespace TestAeroCalc
{


    [TestFixture]
    public class _T_PostProcessor
    {

        PostProcessor PP;
        EnvironmentContext EC;

        [SetUp]
        public void SetUp() {
            EC = new EnvironmentContext("config" + Path.DirectorySeparatorChar + "testconfig.xml");
            PP = new PostProcessor(EC);
        }



        [Test]
        public void formatMsg_1() {
            string[] table = { "MSG1", "MSG2" };
            string message = "Test avec $0, et aussi $1";
            string output = PP._A_formatMsg(message, table);

            StringAssert.AreEqualIgnoringCase("Test avec MSG1, et aussi MSG2", output);
        }

        [Test]
        public void setLanguage_1() {
            Language l = new Language("truc", "zz", "", true);
            Assert.IsTrue(PP.changeLanguage(l) == AeroCalcCommand.ECODE_ERR_LANGFILE_PATH);
        }

        [Test]
        public void setLanguage_2() {
            // ES.xml est un fichier xml valide mais ne contgenant rien de loadable
            string fileName = EC.configDirPath + Path.DirectorySeparatorChar + "es.xml";
            Language l = new Language("truc", "zz", fileName, true);
            Assert.IsTrue(PP.changeLanguage(l) == AeroCalcCommand.ECODE_ERR_LANGFILE_CONTENT);
        }

        [Test]
        public void setLanguage_3() {
            string fileName = EC.configDirPath + Path.DirectorySeparatorChar + "fr.xml";
            Language l = new Language("truc", "zz", fileName, true);
            int feedback = PP.changeLanguage(l);
            Assert.AreEqual(AeroCalcCommand.ECODE_LANG_CHANGED_SUCCESSFULL, feedback);
            //Assert.IsTrue(PP.changeLanguage(fileName) == AeroCalcCommand.ECODE_ERR_LANG_ALREADY_SET);
        }
    }
}
