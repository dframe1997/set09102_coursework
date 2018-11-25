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
        public int messageNum;
        public List<Message> messageList = new List<Message>();

        public List<SIRItem> SIRList = new List<SIRItem>();
        public List<Hashtag> hashtagList = new List<Hashtag>();
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
            
            string json;
            /*messageNum = 0; //All the messages will be read in again, so we need to set the counter backward - not to 0 as this new one isn't in the file yet, so we set it to 1.

            if (File.Exists(@"..\output.json"))
            {
                messageList.Clear();

                using (var reader = new StreamReader(@"..\output.json"))
                {
                    json = reader.ReadLine();
                    messageList = JsonConvert.DeserializeObject<List<Message>>(json, jset);
                }
            }*/

            readFromJSON();
            
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
                listString += " sortCode: " + s.sortCodeAccess + ", NOI: " + s.natureOfIncidentAccess + @"
";
            }
            listString += @"
Hashtag List
";
            foreach (Hashtag h in hashtagList)
            {
                listString += " #" + h.hashtagTextAccess + @"
";
            }
            
            listString += @"
Quarantine List
";
            foreach (string q in quarantineList)
            {
                listString += " " + q + @"
";
            }
            listString += @"
Mention List
";
            foreach (string m in mentionList)
            {
                listString += " @" + m + @"
";
            }
            return listString;
        }

        public List<Message> readFromJSON()
        {
            messageNum = 0; //All the messages will be read in again, so we need to set the counter backward to 0.
            
            if (File.Exists(@"..\output.json"))
            {
                string json;
                var jset = new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.All };
                using (var reader = new StreamReader(@"..\output.json"))
                {
                    json = reader.ReadLine();
                    messageList = JsonConvert.DeserializeObject<List<Message>>(json, jset);
                }
            }
            return messageList;
        }
    }  
}
