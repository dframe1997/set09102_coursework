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

        [TestMethod]
        public void phoneNumTest()
        {
            string expectedException = "Please ensure that your phone number is between 8 and 16 digits, and that it is a number.";
            try
            {
                Sms testSMS = new Sms("STEST4", "I'M NOT A PHONE NUMBER!", "TESTMESSAGE", dataManager, false);
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ex.Message, expectedException);
            }
        }

        [TestMethod]
        public void noPhoneTest()
        {
            string expectedException = "Please include a phone number in the sender box.";
            try
            {
                Sms testSMS = new Sms("STEST5", "", "TESTMESSAGE", dataManager, false);
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ex.Message, expectedException);
            }
        }

        [TestMethod]
        public void smsLimTest()
        {
            string expectedException = "Message too long. Please stay below 140 characters (currently 141)";
            try
            {
                //Message is 143 characters long to mimic the end charcters from the rich text box.
                string message = "yztvahwgsoqfeeaekmwabhdrhrdhrdhhzdrhabqctlfquftbhlesgqlwzigssdohqmqqvojqdmowfhkgrmmooxwkcwdvbpnqiwjtmcvfxaqbmoycvmmtdppsdlaffumbmntbtclwkuzfgmw";
                Sms testSMS = new Sms("STEST6", "20594860", message, dataManager, false);
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ex.Message, expectedException);
            }
        }

        [TestMethod]
        public void tweetLimTest()
        {
            string expectedException = "Message too long. Please stay below 140 characters (currently 141)";
            try
            {
                //Message is 143 characters long to mimic the end charcters from the rich text box.
                string message = "yztvahwgsoqfeeaekmwabhdrhrdhrdhhzdrhabqctlfquftbhlesgqlwzigssdohqmqqvojqdmowfhkgrmmooxwkcwdvbpnqiwjtmcvfxaqbmoycvmmtdppsdlaffumbmntbtclwkuzfgmw";
                Tweet testSMS = new Tweet("TTEST3", "@myAccount", message, dataManager, false);
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ex.Message, expectedException);
            }
        }

        [TestMethod]
        public void emailLimTest()
        {
            string expectedException = "Message too long. Please stay below 1028 characters (currently 1029)";
            try
            {
                //Message is 1031 characters long to mimic the end charcters from the rich text box.
                string message = "nfwbjyarbxxtybhjkpqxeyuykvwlxmxiwdlfgnduzppvlwzlqpoymvyambfmuqsakrecoenlotmekrcdatmctitrpztastwmcossrypreosmvwhjyvkadcuiffbfwnnoeqxhpwzfmundubcrznalsgiqskdfgxtmgzyolpvgyyunkmvwwhupljpsndsjofqkczwclqvdxzznfxpawpnpaksopdlmmpvqmunuiabkntikyaigovgrblpqftnnobyywkjvplktilclyaswauixwqwhixudanyucbfvlguizhsljobxkwszxgeemtndbcoxyalnvosltrlopxuyxvfrgzljjhmpwzorogwkqapkujhqqevwztvujcrdkjlmwowgzmqgxizozgjbbvobicikmhscmlldaviqqbykisrnlujiyggdqipasffcysuolhxlwkvfpjjlisylidwcpewikqtsotebzldnfucskkatixxebgofsahczztzomwtxvvrwwcxkdeyzmjmxozqdylnezuqnysnrxpgwaayvijxumholjqgdjpzjfpvmoxutdxjrxnzkqbntnkvdzmvdjbbmzcxobhhgsckadxdifxspqyajascdgwqccnyzathxvadykarnhfwvlpqomgowlnvjnyvmarnjejuvdpixkvfolxvhccfeulxqzxaryvfbgbnlnmyroazxhskmppozfsvrpyunaidsagaqxaubmkvyqnibbhtmtucwimeekalzklevqgaqsivqlhsmsunyniyvcvosgbfoxlqgpomrljwpgrcegzojmgeliayesbaxcnryronkpagcymstuwvzphbrktvhoxyslrbraampyovnugphdajmtaaqzfhbaqnfsczpoefpbzcebdoftrvowuyqilpybjqmytacvxkuqpdfwixmzkbldtprawfrhvlbbeagjawcblkayrnucfmamaudeuugtvyniqdtuvljbzamnulvxhfhuinoqqjqgpgoljmpwqomvy";
                Email testEmail = new Email("ETEST6", "test@test.com", "TESTSUBJECT", message, dataManager, false);
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ex.Message, expectedException);
            }
        }

        [TestMethod]
        public void eAddressTest()
        {
            string expectedException = "Please provide a valid email address.";
            try
            {
                dataManager.setCSVPath(@"..\..\..\CourseworkApplication\bin\textwords.csv");
                dataManager.readFromCSV();

                Email testEmail = new Email("ETEST7", "THIS IS NOT AN EMAIL ADDRESS!", "TESTSUBJECT", "TESTMESSAGE", dataManager, false);
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ex.Message, expectedException);
            }
        }

        [TestMethod]
        public void noEAddressTest()
        {
            string expectedException = "Please include an email address in the sender box and a subject with up to 20 characters in the subject box.";
            try
            {
                dataManager.setCSVPath(@"..\..\..\CourseworkApplication\bin\textwords.csv");
                dataManager.readFromCSV();

                Email testEmail = new Email("ETEST8", "", "TESTSUBJECT", "TESTMESSAGE", dataManager, false);
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ex.Message, expectedException);
            }
        }

        [TestMethod]
        public void noSubjectTest()
        {
            string expectedException = "Please include an email address in the sender box and a subject with up to 20 characters in the subject box.";
            try
            {
                dataManager.setCSVPath(@"..\..\..\CourseworkApplication\bin\textwords.csv");
                dataManager.readFromCSV();

                Email testEmail = new Email("ETEST9", "test@test.com", "", "TESTMESSAGE", dataManager, false);
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ex.Message, expectedException);
            }
        }

        [TestMethod]
        public void subjectLimTest()
        {
            string expectedException = "Your subject is too long, the limit is 20 characters.";
            try
            {
                dataManager.setCSVPath(@"..\..\..\CourseworkApplication\bin\textwords.csv");
                dataManager.readFromCSV();

                Email testEmail = new Email("ETEST10", "test@test.com", "THIS SUBJECT IS TOO LOOOOOOOOOOOOOOOOOOOOOOONG", "TESTMESSAGE", dataManager, false);
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ex.Message, expectedException);
            }
        }

        [TestMethod]
        public void AtIDTest()
        {
            string expectedException = "In the sender box, please include a twitter ID of up to 15 characters (not including @).";
            try
            {
                dataManager.setCSVPath(@"..\..\..\CourseworkApplication\bin\textwords.csv");
                dataManager.readFromCSV();
    
                Tweet testTweet = new Tweet("TTEST4", "NOAtIncluded", "TESTMESSAGE", dataManager, false);
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ex.Message, expectedException);
            }
        }

        [TestMethod]
        public void TwitIDLimTest()
        {
            string expectedException = "In the sender box, please include a twitter ID of up to 15 characters (not including @).";
            try
            {
                dataManager.setCSVPath(@"..\..\..\CourseworkApplication\bin\textwords.csv");
                dataManager.readFromCSV();

                Tweet testTweet = new Tweet("TTEST5", "@TooLooooooooooooooooooooooooooooooooooong", "TESTMESSAGE", dataManager, false);
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ex.Message, expectedException);
            }
        }

        [TestMethod]
        public void noIDTest()
        {
            string expectedException = "In the sender box, please include a twitter ID of up to 15 characters (not including @).";
            try
            {
                dataManager.setCSVPath(@"..\..\..\CourseworkApplication\bin\textwords.csv");
                dataManager.readFromCSV();

                Tweet testTweet = new Tweet("TTEST6", "", "TESTMESSAGE", dataManager, false);
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ex.Message, expectedException);
            }
        }

        [TestMethod]
        public void SIRSortCodeTest()
        {
            string expectedException = "Please include a valid sort code on the first line of the message.";
            try
            {
                dataManager.setCSVPath(@"..\..\..\CourseworkApplication\bin\textwords.csv");
                dataManager.readFromCSV();

                string SIRmessage = @"I'M NOT A SORT CODE!
Theft
My message";

                SIR testSIR = new SIR("ETEST11", "test@test.com", "TESTSUBJECT", SIRmessage, dataManager, false);
            }
            catch(Exception ex)
            {
                Assert.AreEqual(ex.Message, expectedException);
            }
        }

        [TestMethod]
        public void SIRNOITest()
        {
            string expectedException = "Please provide a valid nature of incident on the second line of the message.";
            try
            {
                dataManager.setCSVPath(@"..\..\..\CourseworkApplication\bin\textwords.csv");
                dataManager.readFromCSV();

                string SIRmessage = @"09-09-09
I'M NOT A VALID NATURE OF INCIDENT!
My message";

                SIR testSIR = new SIR("ETEST12", "test@test.com", "TESTSUBJECT", SIRmessage, dataManager, false);
            }
            catch(Exception ex)
            {
                Assert.AreEqual(ex.Message, expectedException);
            }
        }
    }
}
