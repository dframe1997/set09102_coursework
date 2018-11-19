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
        string oldSubject = "";
        Boolean isSIR = false;
        DataManager dataManager = new DataManager();

        public MainWindow()
        {
            InitializeComponent();
            dataManager.readFromCSV();
        }

        private void btn_clear_Click(object sender, RoutedEventArgs e)
        {
            tbx_sender.Text = "";
            if(!isSIR) tbx_subject.Text = "";

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
                        tbx_output.Document.Blocks.Add(new Paragraph(new Run("Header: " + mySms.messageHeaderAccess)));
                        tbx_output.Document.Blocks.Add(new Paragraph(new Run("Sender: " + mySms.senderAccess)));
                        tbx_output.Document.Blocks.Add(new Paragraph(new Run("Body: " + mySms.messageBodyAccess)));
                        tbx_output.Document.Blocks.Add(new Paragraph(new Run("Type: " + mySms.GetType().ToString())));
                        break;
                    case "Tweet":
                        header = generateID("T");
                        Tweet myTweet = new Tweet(header, senderName, body, dataManager, true);
                        messageType = "Tweet";
                        tbx_output.Document.Blocks.Clear();
                        tbx_output.Document.Blocks.Add(new Paragraph(new Run("Header: " + myTweet.messageHeaderAccess)));
                        tbx_output.Document.Blocks.Add(new Paragraph(new Run("Sender: " + myTweet.senderAccess)));
                        tbx_output.Document.Blocks.Add(new Paragraph(new Run("Body: " + myTweet.messageBodyAccess)));
                        tbx_output.Document.Blocks.Add(new Paragraph(new Run("Type: " + myTweet.GetType().ToString())));
                        break;
                    case "Email":
                        header = generateID("E");
                        if (isSIR)
                        {
                            SIR myEmail = new SIR(header, senderName, subject, body, dataManager, true);
                            messageType = "Email";
                            tbx_output.Document.Blocks.Clear();
                            tbx_output.Document.Blocks.Add(new Paragraph(new Run("Header: " + myEmail.messageHeaderAccess)));
                            tbx_output.Document.Blocks.Add(new Paragraph(new Run("Sender: " + myEmail.senderAccess)));
                            tbx_output.Document.Blocks.Add(new Paragraph(new Run("Subject: " + myEmail.subjectAccess)));
                            tbx_output.Document.Blocks.Add(new Paragraph(new Run("Body: " + myEmail.messageBodyAccess)));
                            tbx_output.Document.Blocks.Add(new Paragraph(new Run("Type: " + myEmail.GetType().ToString())));
                        }
                        else
                        {
                            Email myEmail = new Email(header, senderName, subject, body, dataManager, true);
                            messageType = "Email";
                            tbx_output.Document.Blocks.Clear();
                            tbx_output.Document.Blocks.Add(new Paragraph(new Run("Header: " + myEmail.messageHeaderAccess)));
                            tbx_output.Document.Blocks.Add(new Paragraph(new Run("Sender: " + myEmail.senderAccess)));
                            tbx_output.Document.Blocks.Add(new Paragraph(new Run("Subject: " + myEmail.subjectAccess)));
                            tbx_output.Document.Blocks.Add(new Paragraph(new Run("Body: " + myEmail.messageBodyAccess)));
                            tbx_output.Document.Blocks.Add(new Paragraph(new Run("Type: " + myEmail.GetType().ToString())));
                        }
                        break;
                    default:
                        messageType = "Unknown";
                        tbx_output.Document.Blocks.Clear();
                        tbx_output.Document.Blocks.Add(new Paragraph(new Run("In the header box, please specify the type of message you wish to send: Email, Sms or Tweet.")));
                        break;     
                }
                tbx_sender.Text = "";
                if (!isSIR) tbx_subject.Text = "";

                tbx_content.Document.Blocks.Clear();
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

        private void Cbx_SignificantIncidentReport_Checked(object sender, RoutedEventArgs e)
        {
            if(drop_messageType.Text == "Email")
            {
                oldSubject = tbx_subject.Text;
                tbx_subject.Text = "SIR " + DateTime.Now.ToString("d/M/yy").ToString();
                tbx_subject.IsEnabled = false;
            }
            isSIR = true;
        }

        private void Cbx_SignificantIncidentReport_Unchecked(object sender, RoutedEventArgs e)
        {
            if(drop_messageType.Text == "Email")
            {
                tbx_subject.Text = oldSubject;
                tbx_subject.IsEnabled = true;
            }
            isSIR = false;
        }
    }
}
