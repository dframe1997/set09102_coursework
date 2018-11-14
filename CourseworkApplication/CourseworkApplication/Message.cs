using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CourseworkApplication
{
    abstract class Message
    {
        protected string messageHeader;
        protected string messageBody;
        protected string sender;
        protected string senderError = "SENDER ERROR";
        protected List<Keyword> keywordList = new List<Keyword>();
        protected DataManager dataManager;

        public abstract string messageHeaderAccess { get; set; }
        public abstract string messageBodyAccess { get; set; }
        public abstract string senderAccess { get; set; }

        public abstract bool validateInputs(string messageBody);
        public abstract string extractSender(string messageBody);

        public abstract string encodeMessage();

        public string keywordReplace(string messageBody)
        {

            foreach (Keyword item in keywordList)
            {
                //https://stackoverflow.com/questions/6143642/way-to-have-string-replace-only-hit-whole-words
                string pattern = @"\b" + item.keyAccess + @"\b";
                messageBody = Regex.Replace(messageBody, pattern, item.keyAccess + " <" + item.keyWordAccess + ">", RegexOptions.IgnoreCase);
            }
            return messageBody;
        }

        
    }
}
