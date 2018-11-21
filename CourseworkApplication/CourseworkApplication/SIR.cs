using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace CourseworkApplication
{
    public class SIR: Email
    {
        public string sortCode = "";
        public string natureOfIncident = "";
        private string[] possibleIncidents = new[] { "Theft", "Staff Attack", "ATM Theft", "Raid", "Customer Attack", "Staff Abuse", "Bomb Threat", "Terrorism", "Suspicious Incident", "Intelligence", "Cash Loss" };

        public SIR(string messageHeaderAccess, string senderAccess, string subjectAccess, string messageBodyAccess, DataManager dataManagerAccess, Boolean saveAfterCreation) : base(messageHeaderAccess, senderAccess, subjectAccess, messageBodyAccess, dataManagerAccess, saveAfterCreation){}

        public override bool validateBody(string messageBodyAccess)
        {
            if(this.sortCode == "") this.sortCode = messageBodyAccess.Split(new[] { Environment.NewLine }, StringSplitOptions.None)[0];

            Regex rgx = new Regex(@"..-..-..");

            if (!rgx.IsMatch(this.sortCode))
            {
                throw new Exception("Please include a valid sort code on the first line of the message.");
            }

            this.natureOfIncident = messageBodyAccess.Split(new[] { Environment.NewLine }, StringSplitOptions.None)[1];

            if (!this.possibleIncidents.Contains(this.natureOfIncident, StringComparer.OrdinalIgnoreCase))
            {
                throw new Exception("Please provide a valid nature of incident on the second line of the message.");
            }

            //https://stackoverflow.com/questions/4940124/how-can-i-delete-the-first-n-lines-in-a-string-in-c
            var lines = Regex.Split(messageBodyAccess, "\r\n|\r|\n").Skip(2);
            messageBodyAccess = string.Join(Environment.NewLine, lines.ToArray());

            if (messageBodyAccess.Length > 1030)
            {
                throw new Exception("Message too long. Please stay below 1028 characters (currently " + (messageBodyAccess.Length - 2) + ")"); //-2 for line ending characters
            }
            else if (messageBodyAccess == "")
            {
                throw new Exception("Please include a message after your sort code and nature of incident.");
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
    }
}
