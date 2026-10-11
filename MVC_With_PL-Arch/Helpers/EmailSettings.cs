using DAL_Arch.Models;
using System.Net;
using System.Net.Mail;

namespace MVC_With_PL_Arch.Helpers
{
    public static class EmailSettings
    {
        public static void SendEmail(Email email)
        {
            var Client = new SmtpClient("smtp.gmail.com", 587);
            Client.EnableSsl = true;
            Client.Credentials = new NetworkCredential("mohamednasser13200@gmail.com", "74007158");
            Client.Send("mohamednasser13200@gmail.com", email.To, email.Subject, email.Body);
        }
    }
}
