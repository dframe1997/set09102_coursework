using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseworkApplication
{
    public class Sms : Message
    {
        public Sms(string messageHeaderAccess, string senderAccess, string messageBodyAccess, Boolean saveAfterCreation)
        {
            this.messageHeader = messageHeaderAccess;                     
            dataManager.readFromCSV();
            this.keywordList = dataManager.keywordList;

            if (senderAccess != "")
            {
                if (validateSender(senderAccess))
                {
                    this.sender = senderAccess;
                }
                else
                {
                    throw new Exception("Please ensure that your phone number is between 8 and 16 digits, and that it is a number.");
                }
            }
            else
            {
                throw new Exception("Please include a phone number in the sender box.");
            }

            if (validateBody(messageBodyAccess))
            {
                this.messageBody = keywordReplace(messageBodyAccess);
                if (saveAfterCreation)
                {
                    this.dataManager.saveToFile(this);
                }
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

        public override bool validateBody(string messageBody)
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

        public override bool validateSender(string sender)
        {
            if (sender.Length > 16 || sender.Length < 8 || !double.TryParse(sender, out double number)) //Maximum/minimum(including country code) characters in a international phone number and checking that the input is a number
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
