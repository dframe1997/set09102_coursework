using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CourseworkApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string header;
        string senderName;
        string subject;
        string body;
        string messageType;
        DataManager dataManager = new DataManager();

        public MainWindow()
        {
            InitializeComponent();
            dataManager.readFromCSV();
        }

        private void btn_clear_Click(object sender, RoutedEventArgs e)
        {
            tbx_content.Document.Blocks.Clear();
        }

        private void btn_submit_Click(object sender, RoutedEventArgs e)
        {        
            body = new TextRange(tbx_content.Document.ContentStart, tbx_content.Document.ContentEnd).Text;
            header = drop_messageType.Text;
            senderName = tbx_sender.Text;
            subject = tbx_subject.Text;

            try {
                switch (header)
                {
                    case "SMS":
                        header = generateID("S");
                        Sms mySms = new Sms(header, senderName, body, dataManager, true);
                        messageType = "Sms";
                        tbx_output.Document.Blocks.Clear();
                        tbx_output.Document.Blocks.Add(new Paragraph(new Run(mySms.messageHeaderAccess)));
                        tbx_output.Document.Blocks.Add(new Paragraph(new Run(mySms.messageBodyAccess)));
                        tbx_output.Document.Blocks.Add(new Paragraph(new Run(mySms.GetType().ToString())));
                        break;
                    case "Tweet":
                        header = "T" + drop_messageType.Text;
                        Tweet myTweet = new Tweet(header, senderName, body, dataManager);
                        messageType = "Tweet";
                        tbx_output.Document.Blocks.Clear();
                        tbx_output.Document.Blocks.Add(new Paragraph(new Run(myTweet.messageHeaderAccess)));
                        tbx_output.Document.Blocks.Add(new Paragraph(new Run(myTweet.messageBodyAccess)));
                        tbx_output.Document.Blocks.Add(new Paragraph(new Run(myTweet.GetType().ToString())));
                        break;
                    case "Email":
                        header = "E" + drop_messageType.Text;
                        Email myEmail = new Email(header, senderName, subject, body, dataManager);
                        messageType = "Email";
                        tbx_output.Document.Blocks.Clear();
                        tbx_output.Document.Blocks.Add(new Paragraph(new Run(myEmail.messageHeaderAccess)));
                        tbx_output.Document.Blocks.Add(new Paragraph(new Run(myEmail.messageBodyAccess)));
                        tbx_output.Document.Blocks.Add(new Paragraph(new Run(myEmail.GetType().ToString())));
                        break;
                    default:
                        messageType = "Unknown";
                        tbx_output.Document.Blocks.Clear();
                        tbx_output.Document.Blocks.Add(new Paragraph(new Run("In the header box, please specify the type of message you wish to send: E for email, S for sms or T for tweet followed by a 9 digit message ID.")));
                        break;
                }
            }
            catch(Exception ex)
            {
                tbx_output.Document.Blocks.Clear();
                tbx_output.Document.Blocks.Add(new Paragraph(new Run(ex.Message)));
            }
            
        }

        private void drop_messageType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(drop_messageType.SelectedValue.ToString().Substring(drop_messageType.SelectedValue.ToString().IndexOf(" ")).Substring(1) == "Email")
            {
                tbx_subject.IsEnabled = true;
            }
            else
            {
                tbx_subject.IsEnabled = false;
            }
        }

        private string generateID(string startChar)
        {
            string messageID = startChar;
            Random random = new Random();
            //https://stackoverflow.com/questions/7055489/how-to-generate-a-random-10-digit-number-in-c
            for (int i = 0; i < 9; i++)
            {
                messageID += random.Next(0, 9).ToString();
            }
            return messageID;
        }
    }
}
