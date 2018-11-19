using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseworkApplication
{
    public class Tweet: Message
    {
        public Tweet(string messageHeaderAccess, string senderAccess, string messageBodyAccess, DataManager dataManagerAccess, Boolean saveAfterCreation)
        {
            this.messageHeader = messageHeaderAccess;

            if (dataManager != null)
            {
                this.dataManager = dataManagerAccess;
            }
            else
            {
                DataManager dataManager = new DataManager();
                dataManager.readFromCSV();
                this.dataManager = dataManager;
            }

            this.keywordList = this.dataManager.keywordList;

            if (senderAccess != "")
            {
                
                if (validateSender(senderAccess))
                {
                    this.sender = senderAccess;
                }
                else
                {
                    throw new Exception("In the sender box, please include a twitter ID of up to 15 characters (not including @).");
                }
            }
            else
            {
                throw new Exception("In the sender box, please include a twitter ID of up to 15 characters (not including @).");
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
                throw new Exception("Message too long. Please stay below 140 characters (currently " + (messageBodyAccess.Length - 2) + ")"); //-2 for line ending characters
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

        /*public override string subjectAccess
        {
            get
            {
                return "N/A";
            }
            set
            {

            }
        }*/

        public override bool validateBody(string messageBody)
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

        public override bool validateSender(string sender)
        {
            if (sender.Length > 16 || sender.Substring(0,1) != "@")
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