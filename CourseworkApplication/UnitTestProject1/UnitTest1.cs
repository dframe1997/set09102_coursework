using System;
using CourseworkApplication;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void ValidationTest()
        {
            string message = "Test message LOL";

            string expectedResult = "Test message LOL <Laugh Out Loud>";

            Sms testSMS = new Sms("TEST", "TEST", "TEST", new DataManager());

            string actualResult = testSMS.keywordReplace(message);

            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
