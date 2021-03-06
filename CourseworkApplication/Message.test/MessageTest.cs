using Microsoft.VisualStudio.TestTools.UnitTesting;
using CourseworkApplication;

namespace Message.test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void ValidationTest()
        {
            string message = "Test message LOL";

            string expectedResult = "Test message LOL <Laugh Out Loud>";

            string actualResult = CourseworkApplication.Message.keywordReplace(message);

            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
