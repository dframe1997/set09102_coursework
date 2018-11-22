using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CourseworkApplication
{
    public class Email: Message
    {
        protected string subject;
        protected Boolean saveAfterCreation;

        public Email(string messageHeaderAccess, string senderAccess, string subjectAccess, string messageBodyAccess, Boolean saveAfterCreation)
        {
            this.messageHeader = messageHeaderAccess;
            this.saveAfterCreation = saveAfterCreation;

            if(senderAccess != "" && subjectAccess != "")
            {
                if (validateSender(senderAccess))
                {
                    this.sender = senderAccess;
                }
                else
                {
                    throw new Exception("Please provide a valid email address.");
                }
                
                if(subjectAccess.Length > 20)
                {
                    throw new Exception("Your subject is too long, the limit is 20 characters.");
                }
                this.subject = subjectAccess;
            }
            else
            {
                throw new Exception("Please include an email address in the sender box and a subject with up to 20 characters in the subject box.");
            }

            validateBody(messageBodyAccess);
        }

        public string removeURLS(string messageBody)
        {
            string pattern = @"http[^ ]*";
            if (saveAfterCreation)
            {
                //https://stackoverflow.com/questions/2013124/regex-matching-up-to-the-first-occurrence-of-a-character
                foreach (Match match in Regex.Matches(messageBody, pattern))
                {
                    this.dataManager.quarantineList.Add(match.Value);
                }
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

        public string subjectAccess
        {
            get
            {
                return subject;
            }
            set
            {
                subject = value;
            }
        }

        public override bool validateBody(string messageBodyAccess)
        {
            if (messageBodyAccess.Length > 1030)
            {
                throw new Exception("Message too long. Please stay below 1028 characters (currently " + (messageBodyAccess.Length - 2) + ")"); //-2 for line ending characters
            }
            else if (messageBodyAccess == "")
            {
                throw new Exception("Please include a message.");
            }
            else
            {
                this.messageBody = removeURLS(messageBodyAccess);
                if (saveAfterCreation)
                {
                    this.dataManager.saveToFile(this);
                }
                
                return true;
            }
        }

        public override bool validateSender(string sender)
        {
            Regex rgx = new Regex(@".*@.*\..{2,4}$");
            if (!rgx.IsMatch(sender))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
