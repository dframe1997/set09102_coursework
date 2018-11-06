using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseworkApplication
{
    class Email: Message
    {
        public Email(string messageHeader, string messageBody)
        {
            this.messageHeader = messageHeader;
            this.messageBody = messageBody;
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

        public override string encodeMessage()
        {
            return "";
        }
    }
}
