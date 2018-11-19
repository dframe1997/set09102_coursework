using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseworkApplication
{
    public class Keyword
    {
        private string key;
        private string keyword;

        public Keyword(string key, string keyword)
        {
            this.key = key;
            this.keyword = keyword;
        }

        public string keyAccess
        {
            get
            {
                return key;
            }
            set
            {
                key = value;
            }
        }

        public string keyWordAccess
        {
            get
            {
                return keyword;
            }
            set
            {
                keyword = value;
            }
        }
    }
}
