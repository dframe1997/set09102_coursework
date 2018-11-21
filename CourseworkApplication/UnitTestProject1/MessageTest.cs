using System;
using CourseworkApplication;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
    [TestClass]
    public class MessageTest
    {
        DataManager dataManager = new DataManager();

        [TestMethod]
        public void SMSTest()
        {
            dataManager.setCSVPath(@"..\..\..\CourseworkApplication\bin\textwords.csv");
            dataManager.readFromCSV();

            Sms testSMS = new Sms("STEST", "34958748", "TESTMESSAGE", dataManager, false);
        }

        [TestMethod]
        public void TweetTest()
        {
            dataManager.setCSVPath(@"..\..\..\CourseworkApplication\bin\textwords.csv");
            dataManager.readFromCSV();

            Tweet testTweet = new Tweet("TTEST", "@TEST", "TESTMESSAGE", dataManager, false);
        }

        [TestMethod]
        public void EmailTest()
        {
            dataManager.setCSVPath(@"..\..\..\CourseworkApplication\bin\textwords.csv");
            dataManager.readFromCSV();

            Email testEmail = new Email("ETEST", "test@test.com", "TESTSUBJECT", "TESTMESSAGE", dataManager, false);
        }

        [TestMethod]
        public void SIRTest()
        {
            dataManager.setCSVPath(@"..\..\..\CourseworkApplication\bin\textwords.csv");
            dataManager.readFromCSV();
            string message = @"09-09-09
Theft
My message";
            SIR testSIR = new SIR("ETEST2", "test@test.com", "TESTSUBJECT", message, dataManager, false);
        }

        [TestMethod]
        public void KeywordTest()
        {
            dataManager.setCSVPath(@"..\..\..\CourseworkApplication\bin\textwords.csv");
            dataManager.readFromCSV();

            string message = "Test message LOL";

            string expectedResult = "Test message LOL <Laughing out loud>";

            Sms testSMS = new Sms("STEST2", "34958748", "TESTMESSAGE", dataManager, false);

            string actualResult = testSMS.keywordReplace(message);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void URLTest()
        {
            dataManager.setCSVPath(@"..\..\..\CourseworkApplication\bin\textwords.csv");
            dataManager.readFromCSV();

            string message = "The URL I want to quarantine is https://moodle.napier.ac.uk/course/view.php?id=28466 but not mywebsite.net";

            string expectedResult = "The URL I want to quarantine is <URL Quarantined> but not mywebsite.net";

            Email testEmail = new Email("ETEST3", "test@test.com", "TESTSUBJECT", message, dataManager, false);

            string actualResult = testEmail.removeURLS(message);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void JSONOutTest()
        {
            dataManager.setCSVPath(@"..\..\..\CourseworkApplication\bin\textwords.csv");
            dataManager.readFromCSV();

            string SIRmessage = @"09-09-09
Theft
My message";

            Sms testSMS = new Sms("STEST3", "34958748", "TESTMESSAGE", dataManager, true);
            Email testEmail = new Email("ETEST4", "test@test.com", "TESTSUBJECT", "TESTMESSAGE", dataManager, true);
            Tweet testTweet = new Tweet("TTEST2", "@TEST", "TESTMESSAGE", dataManager, true);
            SIR testSIR = new SIR("ETEST5", "test@test.com", "TESTSUBJECT", SIRmessage, dataManager, true);
        }
    }
}
