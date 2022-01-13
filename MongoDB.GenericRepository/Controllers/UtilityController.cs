using Microsoft.AspNetCore.Mvc;
using MongoDB.GenericRepository.Interfaces;
using MongoDB.GenericRepository.Model;
using MongoDB.GenericRepository.Utility;
using MongoDB.GenericRepository.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Collections.Specialized;
using System.Web;
using ExportFile.Models;
using System.Text;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Hosting;
using System.Net;
using System.Net.Mail;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using System.Net.Http;

namespace MongoDB.GenericRepository.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UtilityController : ControllerBase
    {
        private readonly IEmailSender _emailSender;
        private readonly IUnitOfWork _uow;

        public UtilityController(IEmailSender emailSender, IUnitOfWork uow)
        {
            _emailSender = emailSender;
            _uow = uow;
        }

        [HttpPost]
        [Route("email")]
        public async Task email()
        {
            await _emailSender.SendEmailAsync("debasmitsamal@gmail.com", "subject",
                         $"Enter email body here");
        }

        [HttpPost]
        [Route("send-email")]
        public string SendEmail([FromBody]EmailSMS objData)
        {
            string mailSendSuccess = "Pass";
            try
            {
                if (objData.SelectedDomainAccount == "g")
                {
                    #region "from gmail account"
                    SmtpClient client = new SmtpClient("smtp.gmail.com");
                    client.Port = 587;
                    client.EnableSsl = true;
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential("systemdice@gmail.com", "systemdice@1618");

                    MailMessage mailMessage = new MailMessage();
                    mailMessage.From = new MailAddress("systemdice@gmail.com");
                    mailMessage.To.Add("debasmitsamal@gmail.com");
                    mailMessage.Body = "body";
                    mailMessage.Subject = "subject";
                    client.Send(mailMessage);
                    #endregion
                }
                else if (objData.SelectedDomainAccount == "s")
                {
                    #region "from systemdice account"
                    SmtpClient client1 = new SmtpClient("smtp.ionos.com");
                    client1.Port = 587;
                    client1.EnableSsl = true;
                    client1.UseDefaultCredentials = false;
                    client1.Credentials = new NetworkCredential("dsamal@systemdice.com", "Debasmit@1618");

                    MailMessage mailMessage1 = new MailMessage();
                    mailMessage1.From = new MailAddress("contacts@systemdice.com");
                    mailMessage1.To.Add("debasmitsamal@gmail.com");
                    mailMessage1.Body = "body";
                    mailMessage1.Subject = "subject";
                    client1.Send(mailMessage1);
                    #endregion
                }
                else
                {
                    mailSendSuccess = "smtp not found";
                }
            }
            catch (Exception ex)
            {
                mailSendSuccess = "Failed";
            }
            return mailSendSuccess;
        }

        [HttpPost]
        public async Task<ActionResult<string>> Post([FromBody] SMSTemplate value)
        {
            string smsSuccess = "Pass";
            try
            {
                using (var httpClient = new HttpClient())
                {
                    //https://www.yogihosting.com/aspnet-core-consume-api/#read
                    //https://2factor.in/API/R1/?module=TRANS_SMS&apikey=API_KEY&to=CLIENT_NUMBER&from=SENDER_ID&templatename=TemplateName&var1=VAR1_VALUE&var2=VAR2_VALUE

                    string apiString = "https://2factor.in/API/R1/?module=TRANS_SMS&apikey=32d0a507-5c0c-11e7-94da-0200cd936042&to=8904292294&from=NUAAPP&templatename=Doctor+Appoitment&var1=Debasmit&var2=123456";
                    using (var response = await httpClient.GetAsync(apiString))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        //reservationList = JsonConvert.DeserializeObject<List<Reservation>>(apiResponse);
                    }
                }
            }
            catch (Exception ex)
            {
                smsSuccess = "Failed";
            }

            return Ok(smsSuccess);
        }



    }
}
