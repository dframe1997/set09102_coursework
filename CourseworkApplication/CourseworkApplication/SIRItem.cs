using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseworkApplication
{
    public class SIRItem
    {
        private string sortCode;
        private string natureOfIncident;

        public SIRItem(string sortCode, string natureOfIncident)
        {
            this.sortCode = sortCode;
            this.natureOfIncident = natureOfIncident;
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
