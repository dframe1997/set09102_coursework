using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CourseworkApplication
{
    public abstract class Message
    {
        protected string messageHeader;
        protected string messageBody;
        protected string sender;
        protected List<Keyword> keywordList = new List<Keyword>();
        protected DataManager dataManager = DataManager.Instance;

        public abstract string messageHeaderAccess { get; set; }
        public abstract string messageBodyAccess { get; set; }
        public abstract string senderAccess { get; set; }

        public abstract bool validateSender(string sender);
        public abstract bool validateBody(string messageBody);

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
