using System.Net;
using System.Net.Mail;
using Tickets.Models;

namespace Tickets.EmailConfig
{
    public static class EmailSetting
    {
        public static void  SendMail(Email Email)
        {

            try
            {
                var config = new SmtpClient("smtp.gmail.com", 587);
                config.EnableSsl = true;

                config.Credentials = new NetworkCredential("bishoy.bishoy2001@gmail.com", "bnjajyztdzbficuj");
                config.Send("bishoy.bishoy2001@gmail.com", Email.To, Email.Title, Email.Body);
            }
            catch(Exception ex) { throw ex; }
     
        }
    }
}
