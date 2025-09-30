using AeroCalcCore;
using NUnit.Framework;

namespace TestAeroCalc
{


    [TestFixture]
    public class _T_PostProcessor
    {

        PostProcessor? PP;
        EnvironmentContext? EC;

        [SetUp]
        public void SetUp()
        {
            EC = new EnvironmentContext("config" + Path.DirectorySeparatorChar + "testconfig.xml");
            PP = new PostProcessor(EC);
        }




        // Remplacez la ligne suivante dans le test formatMsg_1 :
        // Assert.AreEqual("Test avec MSG1, et aussi MSG2", output, ignoreCase: true);


        // Exemple de correction dans le test :
        [Test]
        public void formatMsg_1()
        {
            string[] table = { "MSG1", "MSG2" };
            string message = "Test avec $0, et aussi $1";
            string output = PP._A_formatMsg(message, table);

            Assert.That(output, Is.EqualTo("Test avec MSG1, et aussi MSG2").IgnoreCase);
        }

        [Test]
        public void setLanguage_1()
        {
            Language l = new Language("truc", "zz", "", true);
            Assert.That(PP.changeLanguage(l), Is.EqualTo(AeroCalcCommand.ECODE_ERR_LANGFILE_PATH));
        }

        [Test]
        public void setLanguage_2()
        {
            // ES.xml est un fichier xml valide mais ne contenant rien de loadable
            string fileName = EC.configDirPath + Path.DirectorySeparatorChar + "testes.xml";
            Language l = new Language("truc", "zz", fileName, true);
            Assert.That(PP.changeLanguage(l), Is.EqualTo(AeroCalcCommand.ECODE_ERR_LANGFILE_CONTENT_MISSING));
        }

        [Test]
        public void setLanguage_3()
        {
            string fileName = EC.configDirPath + Path.DirectorySeparatorChar + "testfr.xml";
            Language l = new Language("français", "FR", fileName, true);
            int feedback = PP.changeLanguage(l);
            Assert.That(feedback, Is.EqualTo(AeroCalcCommand.ECODE_LANG_CHANGED_SUCCESSFULL));
        }

        [Test]
        public void setLanguage_4()
        {
            string fileName = EC.configDirPath + Path.DirectorySeparatorChar + "testez.xml";
            Language l = new Language("truc", "ZZ", fileName, true);
            int feedback = PP.changeLanguage(l);
            Assert.That(feedback, Is.EqualTo(AeroCalcCommand.ECODE_ERR_LANGFILE_ID));
        }
    }
}
