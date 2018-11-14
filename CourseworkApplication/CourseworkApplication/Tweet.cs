using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseworkApplication
{
    class Tweet: Message
    {
        public Tweet(string messageHeader, string messageBody)
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
                throw new Exception("Message too long. Please stay below 140 characters (currently " + (messageBody.Length - 2) + ")"); //-2 for line ending characters
            }
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
                if (sender.Substring(0, 1) == "@" && sender.Length <= 16) //15 plus the @
                {
                    return sender;
                }
                else
                {
                    throw new Exception("Please include a twitter ID of up to 15 characters (not including @), followed by a space, then your message of up to 140 characters.");
                }
            }
            catch
            {
                throw new Exception("Please include a twitter ID of up to 15 characters (not including @), followed by a space, then your message of up to 140 characters.");
            }
        }

        public override bool validateInputs(string messageBody)
        {
            if(messageBody.Length > 142)
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
