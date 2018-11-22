using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace CourseworkApplication
{
    public sealed class DataManager
    {
        public List<Keyword> keywordList;
        public string csvPath = @"..\textwords.csv";
        public string listString;

        public List<SIRItem> SIRList = new List<SIRItem>();
        public List<string> hashtagList = new List<string>();
        public List<string> quarantineList = new List<string>();
        public List<string> mentionList = new List<string>();

        private static readonly Lazy<DataManager> dataManager = new Lazy<DataManager>(() => new DataManager());

        public static DataManager Instance
        {
            get
            {
                return dataManager.Value;
            }
        }

        public void setCSVPath(string newpath)
        {
            this.csvPath = newpath;
        }

        public void readFromCSV()
        {
            using (var reader = new StreamReader(csvPath))
            {
                List<Keyword> newlist = new List<Keyword>();

                while (!reader.EndOfStream)
                {
                    
                    string line = reader.ReadLine();
                    var keyValue = line.Split(',');
                    Keyword k = new Keyword(keyValue[0], keyValue[1]);
                    newlist.Add(k);
                }
                keywordList = newlist;
            }
        }

        public void saveToFile(Message message)
        {
            var jset = new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.All };
            List<Message> messageList = new List<Message>();
            string json;

            if (File.Exists(@"..\output.json"))
            {
                using (var reader = new StreamReader(@"..\output.json"))
                {
                    json = reader.ReadLine();
                    messageList = JsonConvert.DeserializeObject<List<Message>>(json, jset);
                }
            }
            
            using (StreamWriter file = new StreamWriter(@"..\output.json"))
            {
                messageList.Add(message);
                json = JsonConvert.SerializeObject(messageList, jset);
                file.WriteLine(json);
            }
        }

        public string generateListString()
        {
            listString = "";
            listString += @"SIR List
";
            foreach(SIRItem s in SIRList)
            {
                listString += " sortCode: " + s.sortCodeAccess + ", natureOfIncident: " + s.natureOfIncidentAccess + @"
";
            }
            listString += @"Hashtag List
";
            foreach (string h in hashtagList)
            {
                listString += " #" + h + @"
";
            }
            
            listString += @"Quarantine List
";
            foreach (string q in quarantineList)
            {
                listString += " " + q + @"
                    ";
            }
            listString += @"Mention List
";
            foreach (string m in mentionList)
            {
                listString += " @" + m + @"
";
            }
            return listString;
        }
    }
}
