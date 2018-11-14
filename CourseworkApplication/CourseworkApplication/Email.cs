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
        public Email(string messageHeaderAccess, string messageBodyAccess, DataManager dataManager)
        {
            this.messageHeader = messageHeaderAccess;
            this.sender = extractSender(messageBodyAccess);
            this.dataManager = dataManager;

            messageBodyAccess = messageBodyAccess.Substring(messageBodyAccess.IndexOf(" ")).Substring(1);

            this.subject = extractSubject(messageBodyAccess);

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

        public override string extractSender(string messageBody)
        {
            try
            {
                sender = messageBody.Substring(0, messageBody.IndexOf(" "));
                return sender;
            }
            catch
            {
                throw new Exception("Please include an email address, a space, a subject with exactly 20 characters, another space, then your message of up to 1028 characters.");
            }
        }

        public string extractSubject(string messageBody)
        {
            try
            {
                subject = messageBody.Substring(0, 20);
                string remainingMessage = messageBody.Substring(21);
                return subject;
            }
            catch
            {
                throw new Exception("Please include a subject with exactly 20 characters, a space and then your message of up to 1080 characters.");
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
