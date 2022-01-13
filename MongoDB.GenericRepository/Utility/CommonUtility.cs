using MongoDB.GenericRepository.Interfaces;
using MongoDB.GenericRepository.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;

namespace MongoDB.GenericRepository.Utility
{
    public class CommonUtility
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _uow;
        public CommonUtility()
        {
        }
            public CommonUtility(IProductRepository productRepository, IUnitOfWork uow)
        {
            _productRepository = productRepository;
            _uow = uow;
        }
        public string GetID(string UnqueID)
        {
            var product = _productRepository.GetByIdOnly(UnqueID);
            return product.ToString();
        }

        public void SendEmail(string toAddres, string passBody,string passSubject)
        {
            try
            {
                SmtpClient client = new SmtpClient("smtp.gmail.com");
                client.Port = 587;
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential("systemdice@gmail.com", "systemdice@1618");

                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress("systemdice@gmail.com");
                mailMessage.To.Add(toAddres);
                mailMessage.Body = passBody;
                mailMessage.Subject = passSubject;
                mailMessage.IsBodyHtml = true;
                client.Send(mailMessage);

                
            }
            catch (Exception ex)
            {
                SmtpClient client1 = new SmtpClient("smtp.ionos.com");
                client1.Port = 587;
                client1.EnableSsl = true;
                client1.UseDefaultCredentials = false;
                client1.Credentials = new NetworkCredential("dsamal@systemdice.com", "Debasmit@1618");

                MailMessage mailMessage1 = new MailMessage();
                mailMessage1.From = new MailAddress("contacts@systemdice.com");
                mailMessage1.To.Add(toAddres);
                mailMessage1.Body = passBody;
                mailMessage1.Subject = passSubject;
                mailMessage1.IsBodyHtml = true;
                client1.Send(mailMessage1);
            }
            //SmtpClient client = new SmtpClient("smtp.gmail.com");
            //client.Port = 465;
            //client.UseDefaultCredentials = false;
            //client.Credentials = new NetworkCredential("systemdice@gmail.com", "systemdice@1618");

            //MailMessage mailMessage = new MailMessage();
            //mailMessage.From = new MailAddress("systemdice@gmail.com");
            //mailMessage.To.Add("debasmitsamal@gmail.com");
            //mailMessage.Body = "body";
            //mailMessage.Subject = "subject";
            //client.Send(mailMessage);
            // MimeMessage message = new MimeMessage();

            // MailboxAddress from = new MailboxAddress("Admin",
            // "serverdataconsumption@serverdata.com");
            // message.From.Add(from);

            // MailboxAddress to = new MailboxAddress("User",
            // "debasmitsamal@gmail.com");
            // message.To.Add(to);
            // MailboxAddress to1 = new MailboxAddress("User",
            //"daskumar@live.com");
            // message.To.Add(to1);
            // MailboxAddress to2 = new MailboxAddress("User",
            //"ddas@systemdice.com");
            // message.To.Add(to2);

            // message.Subject = "Space consumption warning!!";
            // BodyBuilder bodyBuilder = new BodyBuilder();
            // bodyBuilder.HtmlBody = "<h3>Hello SystemDICE!</h3> <br/> Thanks for using our product – we hope we were able to meet your expectations. Just to let you know that your subscription expired 01/20/2021 and you won’t be able to log in any more. <br/><br/> But it’s not too late! You can gain immediate access to your saved data and preferences by renewing . If you renew within the next seven days, you’ll also be able to take advantage of our product.<br/><br/> Thank you,<br/>IONOS";
            // //bodyBuilder.TextBody = "Hello World!";
            // //bodyBuilder.Attachments.Add(env.WebRootPath + "\\file.png");
            // message.Body = bodyBuilder.ToMessageBody();

            // SmtpClient client = new SmtpClient();
            // client.Connect("smtp.gmail.com", 465, true);
            // client.Authenticate("systemdice@gmail.com", "systemdice@1618");

            // client.Send(message);
            // client.Disconnect(true);
            // client.Dispose();
        }
    }
}
