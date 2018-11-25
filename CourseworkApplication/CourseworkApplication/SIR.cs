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
        private string sortCode = "";
        private string natureOfIncident = "";
        private string[] possibleIncidents = new[] { "Theft", "Staff Attack", "ATM Theft", "Raid", "Customer Attack", "Staff Abuse", "Bomb Threat", "Terrorism", "Suspicious Incident", "Intelligence", "Cash Loss" };
        private bool comeFromEmailConstructor = true;
        private bool comingFromJSON = false;

        public SIR(string sortCodeAccess, string natureOfIncidentAccess, string messageHeaderAccess, string senderAccess, string subjectAccess, string messageBodyAccess, Boolean saveAfterCreation) : base(messageHeaderAccess, senderAccess, subjectAccess, messageBodyAccess, saveAfterCreation){
            this.sortCode = sortCodeAccess;
            this.natureOfIncident = natureOfIncidentAccess;

            validateBody(messageBodyAccess);
        }

        public override bool validateBody(string messageBodyAccess)
        {
            if (!comeFromEmailConstructor)
            {
                if (this.sortCode == "")
                {
                    this.sortCode = messageBodyAccess.Split(new[] { Environment.NewLine }, StringSplitOptions.None)[0];
                }
                else
                {
                    comingFromJSON = true;
                }

                Regex rgx = new Regex(@"..-..-..");

                if (!rgx.IsMatch(this.sortCode))
                {
                    throw new Exception("Please include a valid sort code on the first line of the message.");
                }

                if (this.natureOfIncident == "") this.natureOfIncident = messageBodyAccess.Split(new[] { Environment.NewLine }, StringSplitOptions.None)[1];

                if (!this.possibleIncidents.Contains(this.natureOfIncident, StringComparer.OrdinalIgnoreCase))
                {
                    throw new Exception("Please provide a valid nature of incident on the second line of the message.");
                }

                if (!comingFromJSON)
                {
                    //https://stackoverflow.com/questions/4940124/how-can-i-delete-the-first-n-lines-in-a-string-in-c
                    var lines = Regex.Split(messageBodyAccess, "\r\n|\r|\n").Skip(2);
                    messageBodyAccess = string.Join(Environment.NewLine, lines.ToArray());
                }

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
                    this.censoredBody = removeURLS(messageBodyAccess);
                    this.messageBody = messageBodyAccess;

                    this.dataManager.messageNum++;

                    this.messageHeader = "E" + this.dataManager.messageNum.ToString();

                    this.dataManager.SIRList.Add(new SIRItem(this.sortCode, this.natureOfIncident));

                    if (saveAfterCreation)
                    {
                        this.dataManager.saveToFile(this);
                    }

                    return true;
                }
            }
            else
            {
                comeFromEmailConstructor = false;
                return false;
            }
        }

        public string sortCodeAccess
        {
            get
            {
                return sortCode;
            }
            set
            {
                sortCode = value;
            }
        }

        public string natureOfIncidentAccess
        {
            get
            {
                return natureOfIncident;
            }
            set
            {
                natureOfIncident = value;
            }
        }
    }
}
