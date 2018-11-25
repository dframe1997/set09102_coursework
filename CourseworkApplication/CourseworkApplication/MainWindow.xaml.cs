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
        DataManager dataManager = DataManager.Instance;
        int currentVisibleMessage = 0;

        public MainWindow()
        {
            InitializeComponent();
            dataManager.readFromCSV();
            dataManager.readFromJSON();
            if(dataManager.messageList.Count > 0)
            {
                showMessage(0);
            }
            if(dataManager.messageList.Count > 1)
            {
                btn_next.IsEnabled = true;
            }

            tbx_lists.Document.Blocks.Clear();
            tbx_lists.Document.Blocks.Add(new Paragraph(new Run(dataManager.generateListString())));
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

            dataManager.messageList.Clear();
            dataManager.hashtagList.Clear();
            dataManager.mentionList.Clear();
            dataManager.quarantineList.Clear();
            dataManager.SIRList.Clear();

            try {
                switch (header)
                {
                    case "SMS":
                        Sms mySms = new Sms("", senderName, body, true);
                        messageType = "Sms";

                        tbx_output.Document.Blocks.Clear();
                        tbx_output.Document.Blocks.Add(new Paragraph(new Run("Header: " + mySms.messageHeaderAccess)));
                        tbx_output.Document.Blocks.Add(new Paragraph(new Run("Sender: " + mySms.senderAccess)));
                        tbx_output.Document.Blocks.Add(new Paragraph(new Run("Body: " + mySms.messageBodyAccess)));
                        tbx_output.Document.Blocks.Add(new Paragraph(new Run("Type: " + mySms.GetType().ToString())));
                        break;
                    case "Tweet":
                        Tweet myTweet = new Tweet("", senderName, body, true);
                        messageType = "Tweet";

                        tbx_output.Document.Blocks.Clear();
                        tbx_output.Document.Blocks.Add(new Paragraph(new Run("Header: " + myTweet.messageHeaderAccess)));
                        tbx_output.Document.Blocks.Add(new Paragraph(new Run("Sender: " + myTweet.senderAccess)));
                        tbx_output.Document.Blocks.Add(new Paragraph(new Run("Body: " + myTweet.messageBodyAccess)));
                        tbx_output.Document.Blocks.Add(new Paragraph(new Run("Type: " + myTweet.GetType().ToString())));
                        break;
                    case "Email":
                        if (isSIR)
                        {
                            SIR mySir = new SIR("", "", "", senderName, subject, body, true);
                            messageType = "Email";

                            tbx_output.Document.Blocks.Clear();
                            tbx_output.Document.Blocks.Add(new Paragraph(new Run("Header: " + mySir.messageHeaderAccess)));
                            tbx_output.Document.Blocks.Add(new Paragraph(new Run("Sender: " + mySir.senderAccess)));
                            tbx_output.Document.Blocks.Add(new Paragraph(new Run("Subject: " + mySir.subjectAccess)));
                            tbx_output.Document.Blocks.Add(new Paragraph(new Run("Sort Code: " + mySir.sortCodeAccess)));
                            tbx_output.Document.Blocks.Add(new Paragraph(new Run("Nature of incident: " + mySir.natureOfIncidentAccess)));
                            tbx_output.Document.Blocks.Add(new Paragraph(new Run("Body: " + mySir.censoredBodyAccess)));
                            tbx_output.Document.Blocks.Add(new Paragraph(new Run("Type: " + mySir.GetType().ToString())));
                        }
                        else
                        {
                            Email myEmail = new Email("", senderName, subject, body, true);
                            messageType = "Email";

                            tbx_output.Document.Blocks.Clear();
                            tbx_output.Document.Blocks.Add(new Paragraph(new Run("Header: " + myEmail.messageHeaderAccess)));
                            tbx_output.Document.Blocks.Add(new Paragraph(new Run("Sender: " + myEmail.senderAccess)));
                            tbx_output.Document.Blocks.Add(new Paragraph(new Run("Subject: " + myEmail.subjectAccess)));
                            tbx_output.Document.Blocks.Add(new Paragraph(new Run("Body: " + myEmail.censoredBodyAccess)));
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

                currentVisibleMessage = dataManager.messageList.Count - 1;
                btn_next.IsEnabled = false;

                if (dataManager.messageList.Count > 1)
                {
                    btn_prev.IsEnabled = true;
                }

                tbx_lists.Document.Blocks.Clear();
                tbx_lists.Document.Blocks.Add(new Paragraph(new Run(dataManager.generateListString())));
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
                cbx_SignificantIncidentReport.IsEnabled = true;
            }
            else
            {
                tbx_subject.IsEnabled = false;
                cbx_SignificantIncidentReport.IsEnabled = false;
            }
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

        private void showMessage(int messageID)
        {
            dynamic m = dataManager.messageList[messageID];
            
            tbx_output.Document.Blocks.Clear();
            tbx_output.Document.Blocks.Add(new Paragraph(new Run("Header: " + m.messageHeaderAccess)));
            tbx_output.Document.Blocks.Add(new Paragraph(new Run("Sender: " + m.senderAccess)));

            if (dataManager.messageList[messageID] is Email)
            {
                tbx_output.Document.Blocks.Add(new Paragraph(new Run("Subject: " + m.subjectAccess)));
                if (dataManager.messageList[messageID] is SIR)
                {
                    tbx_output.Document.Blocks.Add(new Paragraph(new Run("Sort Code: " + m.sortCodeAccess)));
                    tbx_output.Document.Blocks.Add(new Paragraph(new Run("Nature of incident: " + m.natureOfIncidentAccess)));
                }
                tbx_output.Document.Blocks.Add(new Paragraph(new Run("Body: " + m.censoredBodyAccess)));
            }
            else
            {
                tbx_output.Document.Blocks.Add(new Paragraph(new Run("Body: " + m.messageBodyAccess)));
            }         
            
            tbx_output.Document.Blocks.Add(new Paragraph(new Run("Type: " + m.GetType().ToString())));
        }

        private void btn_next_Click(object sender, RoutedEventArgs e)
        {
            currentVisibleMessage++;
            if (dataManager.messageList.Count <= currentVisibleMessage+1)
            {
                btn_next.IsEnabled = false;
            }
            btn_prev.IsEnabled = true;
            showMessage(currentVisibleMessage);
        }

        private void btn_prev_Click(object sender, RoutedEventArgs e)
        {
            currentVisibleMessage--;
            if (currentVisibleMessage <= 0)
            {
                btn_prev.IsEnabled = false;
            }
            btn_next.IsEnabled = true;
            showMessage(currentVisibleMessage);
        }
    }
}
