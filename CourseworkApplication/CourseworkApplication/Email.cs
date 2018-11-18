using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CourseworkApplication
{
    class Email: Message
    {
        string subject;
        public Email(string messageHeaderAccess, string senderAccess, string subject, string messageBodyAccess, DataManager dataManager)
        {
            this.messageHeader = messageHeaderAccess;
            this.dataManager = dataManager;

            if(sender != "" && subject != "")
            {
                this.sender = senderAccess;
                this.subject = subject;
            }
            else
            {
                throw new Exception("Please include an email address in the sender box and a subject with up to 20 characters in the subject box.");
            }

            messageBodyAccess = messageBodyAccess.Substring(21);

            if (validateInputs(messageBodyAccess))
            {
                this.messageBody = removeURLS(messageBodyAccess);
                this.dataManager.saveToFile(this);
            }
            else
            {
                throw new Exception("Message too long. Please stay below 1028 characters (currently " + (messageBodyAccess.Length - 2) + ")"); //-2 for line ending characters
            }
        }

        public string removeURLS(string messageBody)
        {
            //https://stackoverflow.com/questions/2013124/regex-matching-up-to-the-first-occurrence-of-a-character
            string pattern = @"http[^ ]*";
            foreach(Match match in Regex.Matches(messageBody, pattern))
            {
                dataManager.quarantineList.Add(match.Value);
            }
            messageBody = Regex.Replace(messageBody, pattern, "<URL Quarantined>", RegexOptions.IgnoreCase);
            return messageBody;
        }

        public override string messageHeaderAccess
        {
            get
            {
                return messageHeader;
            }
            set
            {
                messageHeader = value;
            }
        }

        public override string messageBodyAccess
        {
            get
            {
                return messageBody;
            }
            set
            {
                messageBody = value;
            }
        }

        public override string senderAccess
        {
            get
            {
                return sender;
            }
            set
            {
                sender = value;
            }
        }

        public override bool validateInputs(string messageBody)
        {
            if (messageBody.Length > 1030)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public override string encodeMessage()
        {
            return "";
        }
    }
}
