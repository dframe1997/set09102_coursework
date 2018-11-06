using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseworkApplication
{
    abstract class Message
    {
        protected string messageHeader;
        protected string messageBody;
        protected string sender;

        public abstract string messageHeaderAccess { get; set; }
        public abstract string messageBodyAccess { get; set; }
        public abstract string senderAccess { get; set; }

        public abstract string encodeMessage();
    }
}
