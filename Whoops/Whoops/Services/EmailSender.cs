using System.Net;
using System.Net.Mail;

namespace Whoops.Services
{
    public class EmailSender
    {
        SmtpClient _client;

        public EmailSender()
        {
            _client = new SmtpClient("localhost", 587)
            {
                //EnableSsl = true,
                Credentials = new NetworkCredential("dontreply@FunWebsite.com", "1234"),
            };
            _client.Timeout = 5 * 60 * 1000;
        }
        public void SendConfirmation(string sendTo, string text,string subject)
        {
            _client.Send("dontreply@FunWebsite.com", sendTo,"Confirm your email",text);
        }
    }
}
