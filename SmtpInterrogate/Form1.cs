using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Windows.Forms;

namespace SmtpInterrogate
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void buttonSend_Click_1(object sender, EventArgs e)
        {
            SendEmail();
        }

        private void buttonClearResults_Click_1(object sender, EventArgs e)
        {
            resultMessage.Text = "";
        }

        private void buttonClearForm_Click_1(object sender, EventArgs e)
        {
            host.Text = "";
            port.Text = "";
            useSsl.Checked = false;
            username.Text = "";
            password.Text = "";
            from.Text = "";
            to.Text = "";
            subject.Text = "";
            body.Text = "";
        }

        protected void SendEmail()
        {
            resultMessage.Text += "Sending email(s)...\r\n";

            SmtpClient smtpClient = new SmtpClient();
            MailMessage message = new MailMessage();

            smtpClient.Host = host.Text;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.EnableSsl = useSsl.Checked;
            smtpClient.Port = Convert.ToInt32(port.Text);
            smtpClient.Timeout = Convert.ToInt32(timeoutTime.Text);
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.UseDefaultCredentials = useDefaultCreds.Checked;

            if (false == string.IsNullOrEmpty(username.Text))
            {
                smtpClient.Credentials = new NetworkCredential(username.Text, password.Text);
            }

            MailAddress fromAddress = new MailAddress(from.Text);

            message.From = fromAddress;
            message.Subject = subject.Text;
            message.IsBodyHtml = false;
            message.Body = body.Text;

            List<string> emailToList = new List<string>();

            emailToList = to.Text.Split(',').ToList();

            foreach (string emailTo in emailToList)
            {
                message.To.Add(emailTo);
            }

            try
            {
                smtpClient.Send(message);
                resultMessage.Text += "Success!\r\n";
            }
            catch (Exception ex)
            {
                resultMessage.Text += "Error: " + ex.ToString() + "\r\n";
            }
        }

    }
}
