using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;
using System.IO;

namespace CryptoTerminal.Core
{
   //Done
    public static class Email
    {
        private static string mail;

        public static string Mail
        {
            get { return mail; }
            set { mail = value; }
        }

        public static void SendSignal(string mail, string messege)
        {
            if (mail != "" && mail.Contains("@") && mail.Contains(".") && (mail.Length >= 5))
            {
                MailAddress from = new MailAddress("supertrendbot@outlook.com", "SuperTrendSignal");
                // кому отправляем
                MailAddress to = new MailAddress(mail);
                // создаем объект сообщения
                MailMessage m = new MailMessage(from, to);
                // тема письма
                m.Subject = "Сингал от бота";
                // текст письма
                m.Body = "<h2>" + messege + "</h2>";
                // письмо представляет код html
                m.IsBodyHtml = true;
                // адрес smtp-сервера и порт, с которого будем отправлять письмо
                SmtpClient smtp = new SmtpClient("smtp.outlook.com", 587);
                // логин и пароль
                smtp.Credentials = new NetworkCredential("supertrendbot@outlook.com", "12042003A");
                smtp.EnableSsl = true;
                smtp.Send(m);
            }


        }
    }
    
}
