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
        public void KeywordTest()
        {
            dataManager.setCSVPath(@"..\..\..\CourseworkApplication\bin\textwords.csv");
            dataManager.readFromCSV();

            string message = "Test message LOL";

            string expectedResult = "Test message LOL <Laughing out loud>";

            Sms testSMS = new Sms("TEST", "TEST", "TEST", dataManager, false);

            string actualResult = testSMS.keywordReplace(message);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void KeywordTest2()
        {
            dataManager.setCSVPath(@"..\..\..\CourseworkApplication\bin\textwords.csv");
            dataManager.readFromCSV();

            string message = "Test message LOL";

            string expectedResult = "Test message LOL <Laughing out loud>";

            Sms testSMS = new Sms("TEST", "TEST", "TEST", dataManager, false);

            string actualResult = testSMS.keywordReplace(message);

            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
