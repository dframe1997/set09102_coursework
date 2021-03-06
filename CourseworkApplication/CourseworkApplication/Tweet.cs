﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CourseworkApplication
{
    public class Tweet: Message
    {
        public Tweet(string messageHeaderAccess, string senderAccess, string messageBodyAccess, Boolean saveAfterCreation)
        {
            this.messageHeader = messageHeaderAccess;

            dataManager.readFromCSV();
            this.keywordList = this.dataManager.keywordList;

            if (senderAccess != "")
            { 
                if (validateSender(senderAccess))
                {
                    this.sender = senderAccess;
                }
                else
                {
                    throw new Exception("In the sender box, please include a twitter ID of up to 15 characters (not including @).");
                }
            }
            else
            {
                throw new Exception("In the sender box, please include a twitter ID of up to 15 characters (not including @).");
            }
            if (validateBody(messageBodyAccess))
            {
                this.messageBody = keywordReplace(messageBodyAccess);

                this.dataManager.messageNum++;

                this.messageHeader = "T" + this.dataManager.messageNum.ToString();

                //https://stackoverflow.com/questions/13869642/grabing-hashtagged-word-from-a-string-using-regex
                var regex = new Regex(@"(?<=#)\w+");
                var matches = regex.Matches(this.messageBody);

                foreach (Match m in matches)
                {
                    if (dataManager.hashtagList.FirstOrDefault(item => item.hashtagTextAccess == m.Value) != null)
                    {
                        dataManager.hashtagList.First(item => item.hashtagTextAccess == m.Value).instancesAccess += 1;
                    }
                    else
                    {
                        this.dataManager.hashtagList.Add(new Hashtag(m.Value, 1));
                    }
                    this.dataManager.hashtagList.Sort((i, j) => i.instancesAccess.CompareTo(j.instancesAccess));
                    this.dataManager.hashtagList.Reverse();
                }

                regex = new Regex(@"(?<=@)\w+");
                matches = regex.Matches(this.messageBody);

                foreach (Match m in matches)
                {
                    this.dataManager.mentionList.Add(m.Value);
                }

                if (saveAfterCreation)
                {
                    this.dataManager.saveToFile(this);
                }
            }
            else
            {
                throw new Exception("Message too long. Please stay below 140 characters (currently " + (messageBodyAccess.Length - 2) + ")"); //-2 for line ending characters
            }
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

        public override bool validateBody(string messageBody)
        {
            messageBody = Regex.Replace(messageBody, @" ?\<.{0,100}?\>", string.Empty); //Up to 100 characters will be ignored for acronyms
            if (messageBody.Length > 142)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public override bool validateSender(string sender)
        {
            if (sender.Length > 16 || sender.Substring(0,1) != "@")
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}