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
        string body;
        string messageType;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btn_clear_Click(object sender, RoutedEventArgs e)
        {
            tbx_header.Text = "";
            tbx_content.Document.Blocks.Clear();
        }

        private void btn_submit_Click(object sender, RoutedEventArgs e)
        {
            header = tbx_header.Text;
            body = new TextRange(tbx_content.Document.ContentStart, tbx_content.Document.ContentEnd).Text;
            Message myMessage;

            switch (header.Substring(0, 1))
            {
                case "S":
                    myMessage = new Sms(header, body);
                    messageType = "Sms";
                    tbx_output.Document.Blocks.Clear();
                    tbx_output.Document.Blocks.Add(new Paragraph(new Run(myMessage.messageHeaderAccess)));
                    tbx_output.Document.Blocks.Add(new Paragraph(new Run(myMessage.messageBodyAccess)));
                    tbx_output.Document.Blocks.Add(new Paragraph(new Run(myMessage.GetType().ToString())));
                    break;
                case "T":
                    myMessage = new Tweet(header, body);
                    messageType = "Tweet";
                    tbx_output.Document.Blocks.Clear();
                    tbx_output.Document.Blocks.Add(new Paragraph(new Run(myMessage.messageHeaderAccess)));
                    tbx_output.Document.Blocks.Add(new Paragraph(new Run(myMessage.messageBodyAccess)));
                    tbx_output.Document.Blocks.Add(new Paragraph(new Run(myMessage.GetType().ToString())));
                    break;
                case "E":
                    myMessage = new Email(header, body);
                    messageType = "Email";
                    tbx_output.Document.Blocks.Clear();
                    tbx_output.Document.Blocks.Add(new Paragraph(new Run(myMessage.messageHeaderAccess)));
                    tbx_output.Document.Blocks.Add(new Paragraph(new Run(myMessage.messageBodyAccess)));
                    tbx_output.Document.Blocks.Add(new Paragraph(new Run(myMessage.GetType().ToString())));
                    break;
                default:
                    messageType = "Unknown";
                    break;
            }

            
        }
    }
}
