using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseworkApplication
{
    class Sms : Message
    {
        public Sms(string messageHeaderAccess, string senderAccess, string messageBodyAccess, DataManager dataManager)
        {
            this.messageHeader = messageHeaderAccess;
            this.dataManager = dataManager;
            this.keywordList = this.dataManager.keywordList;

            if (senderAccess != "")
            {
                
                this.sender = extractSender(messageBodyAccess);
            }
            else
            {
                throw new Exception("Please include a phone number in the sender box.");
            }

            if (validateInputs(messageBodyAccess))
            {
                this.messageBody = keywordReplace(messageBodyAccess);
                this.dataManager.saveToFile(this);
            }
            else
            {
                throw new Exception("Message too long. Please stay below 140 characters (currently " + (messageBodyAccess.Length-2) + ")"); //-2 for line ending characters
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
