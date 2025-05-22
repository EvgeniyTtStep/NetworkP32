using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Net.Mail;

namespace Mailer
{
    class Program
    {
        // string prompt(string query)
        // {
        //     Console.WriteLine(query);
        //     return Console.ReadLine();
        // }

        static void Main(string[] args)
        {
            Program app = new Program();
            // заповнюємо поля поштового повідомлення
            app.Dialog();
            // намагаємось надіслати сообщение
            app.SendMail();
        }
        string to;
        string from;
        string subject;
        string body;
        string server;

        void Dialog()
        {
            // to = prompt("Введіть адресу одержувача:");
            // from = prompt("Введіть адресу відправника:");
            // subject = prompt("Введіть тему");
            // body = prompt("Введіть текст повідомлення:");
            // server = prompt("Введіть адресу сервера:");
            
            to = "46program@ukr.net";
            from = "46program@ukr.net";
            subject = "Hello";
            body = "Hello, <br/>" + from + "<br/>" + subject;
            server = "smtp.ukr.net";
        }

        public void SendMail()
        {
            //MailMessage message = new MailMessage(from, to, subject, body);
            MailMessage message = new MailMessage();
            message.From = new MailAddress(from);
            message.To.Add(to);
            message.Subject = subject;
            message.Body = body;
            
            
            SmtpClient client = new SmtpClient(server, 465)
            {
                Credentials = new NetworkCredential("46program@ukr.net", "gQjVe51Pd1z4uqJi"),
                EnableSsl = true

            };
            Console.WriteLine("Порахуйте до 100");
            client.Timeout = 30000;// встановлюємо TimeOut 10000 milliseconds
            //client.UseDefaultCredentials = true;
            try
            {
                client.Send(message);
                Console.WriteLine("Повідомленнянадіслано");
            }
            catch (SmtpException se)
            {
              Console.WriteLine("Повідомлення не надіслано через "+se.Message);
            }
        }
    }
}