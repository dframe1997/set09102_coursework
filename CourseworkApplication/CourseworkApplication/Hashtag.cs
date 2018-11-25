using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseworkApplication
{
    public class Hashtag
    {
        private string hashtagText;
        private int instances;

        public Hashtag(string hashtagText, int instances)
        {
            this.hashtagText = hashtagText;
            this.instances = instances;
        }

        public string hashtagTextAccess
        {
            get
            {
                return hashtagText;
            }
            set
            {
                hashtagText = value;
            }
        }

        public int instancesAccess
        {
            get
            {
                return instances;
            }
            set
            {
                instances = value;
            }
        }
    }
}
