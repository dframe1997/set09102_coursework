using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseworkApplication
{
    class Sms : Message
    {
        public Sms(string messageHeader, string messageBody)
        {
            this.messageHeader = messageHeader;
            this.sender = extractSender(messageBody);

            messageBody = messageBody.Substring(messageBody.IndexOf(" ")).Substring(1); //Removes the sender. Second substring removes space at beginning, cannot do -1 on the first substring for some reason

            if (validateInputs(messageBody))
            {
                keywordList.Add(new Keyword("Hello", "Hello World"));
                this.messageBody = keywordReplace(messageBody);
            }
            else
            {
                throw new Exception("Message too long. Please stay below 140 characters (currently " + (messageBody.Length-2) + ")"); //-2 for line ending characters
            }
        }

        public override string messageHeaderAccess {
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
                return messageBody.Substring(0, messageBody.IndexOf(" "));
            }
            catch
            {
                throw new Exception("Please include a phone number, followed by a space and then a message of up to 140 characters.");
            }
        }

        public override bool validateInputs(string messageBody)
        {
            if (messageBody.Length > 142) //2 for the end of string characters /r and /n
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
