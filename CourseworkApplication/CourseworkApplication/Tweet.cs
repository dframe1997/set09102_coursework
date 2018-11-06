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
            keywordList.Add(new Keyword("Hello", "Hello World"));
            this.messageBody = keywordReplace(messageBody);
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
