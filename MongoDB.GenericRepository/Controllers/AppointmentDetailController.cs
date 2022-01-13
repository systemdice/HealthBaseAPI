using Microsoft.AspNetCore.Mvc;
using MongoDB.GenericRepository.Interfaces;
using MongoDB.GenericRepository.Model;
using MongoDB.GenericRepository.Utility;
using MongoDB.GenericRepository.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Twilio;
using Twilio.Types;
using Twilio.Rest.Api.V2010.Account;
using System.IO;
using System.Net;
using System.Text;


namespace MongoDB.GenericRepository.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AppointmentDetailController : ControllerBase
    {
        private readonly IAppointmentDetailRepository _appointmentDetailRepository;
        private readonly IUnitOfWork _uow;

        public AppointmentDetailController(IAppointmentDetailRepository appointmentDetailRepository, IUnitOfWork uow)
        {
            _appointmentDetailRepository = appointmentDetailRepository;
            _uow = uow;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppointmentDetail>>> Get()
        {
            var appointmentDetails = await _appointmentDetailRepository.GetAll();
            return Ok(appointmentDetails);
        }
        [HttpGet]
        [Route("getWaitingAppointmnet")]
        public async Task<ActionResult<IEnumerable<AppointmentDetail>>> getWaitingAppointmnet()
        {
            var appointmentDetails = await _appointmentDetailRepository.GetAll();
            var filterAppointments = appointmentDetails.Where(a => (a.AppointmentStatus == null || a.AppointmentStatus == "Waiting"));
            return Ok(filterAppointments);
        }
        [HttpGet("{UnqueID}")]
        public async Task<ActionResult<AppointmentDetail>> Get(string UnqueID)
        {
            var appointmentDetail = await _appointmentDetailRepository.GetById(UnqueID);
            return Ok(appointmentDetail);
        }
        [Route("[action]/{DateStart}")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppointmentDetail>>> GetByDateStart(string DateStart)
        {
            var appointmentDetails = await _appointmentDetailRepository.GetAll();
            var available = from c in appointmentDetails where c.DateStart == DateStart select c;


            //DateTime frmdt = Convert.ToDateTime("2020-11-27T18:30:00.000Z");
            //DateTime frmdt = Convert.ToDateTime(DateStart);
            //string frmdtString = frmdt.ToString("dd-MM-yyy");
            //Console.WriteLine(frmdtString);
            var availableFilter = appointmentDetails.Where(c => c.ReferralMaster?.appointment?.Trim() != string.Empty);
            //var totalAvailable = availableFilter.Where(c => Convert.ToDateTime(c.ReferralMaster.appointment).ToString("dd-MM-yyy") == frmdtString);
           // var totalAvailable = availableFilter.Where(c => c.ReferralMaster.appointment.Trim() == DateStart.Trim());
            var totalAvailable = availableFilter.Where(c => c.ReferralMaster?.appointment?.Trim() == DateStart.Trim());
            var totalAvailable2 = availableFilter.Where(c => c.ReferralMaster.appointment != null && c.ReferralMaster.appointment.Trim() == DateStart.Trim());
            //var totalAvailable3 = availableFilter.Where(c => c.ReferralMaster.appointment.Trim() == DateStart.Trim());
            //myClass.Where(x => x.MyOtherObject?.Name == "Name").ToList();
            //var availableCars = appointmentDetails.Where(c => c.IsAvailable).ToArray();
            return Ok(totalAvailable); // appointmentDetail.PatientDetails.;
        }

        //[HttpGet("{DateStart}")]
        //public async Task<ActionResult<AppointmentDetail>> Get(string DateStart)
        //{
        //    var appointmentDetail = await _appointmentDetailRepository.GetById(UnqueID);
        //    return Ok(appointmentDetail);
        //}

        [HttpGet("{id}/{UnqueID}")]
        public async Task<string> GetName(int id, string UnqueID)
        {
            var appointmentDetail = await _appointmentDetailRepository.GetById(UnqueID);
            return "PatientName"; // appointmentDetail.PatientDetails.;
        }


        [HttpGet("{UnqueID}/{DateStart}")]
        public async Task<string> GetName(string UnqueID,string DateStart)
        {
            var appointmentDetail = await _appointmentDetailRepository.GetByIdDateStartOnly(DateStart);
            return "PatientName"; // appointmentDetail.PatientDetails.;
        }









        [HttpPost, Route("PostSimulatingError")]
        public IActionResult PostSimulatingError([FromBody] AppointmentDetailViewModel value)
        {
            var appointmentDetail = new AppointmentDetail(value);
            _appointmentDetailRepository.Add(appointmentDetail);

            // The appointmentDetail will not be added
            return BadRequest();
        }

        [HttpPost]
        public async Task<ActionResult<AppointmentDetail>> Post([FromBody] AppointmentDetailViewModel value)
        {

            var appointmentDetail = new AppointmentDetail(value);
            // var appointmentDetail = new AppointmentDetail(value.Notes, value.Name, value.UnqueID);
            _appointmentDetailRepository.Add(appointmentDetail);

            // it will be null
            var testAppointmentDetail = await _appointmentDetailRepository.GetById(appointmentDetail.UnqueID);

            // If everything is ok then:
            await _uow.Commit();

            // The appointmentDetail will be added only after commit
            testAppointmentDetail = await _appointmentDetailRepository.GetById(appointmentDetail.UnqueID);
            #region "send sms"
            try
            {
                //var client = new RestClient("http://2factor.in/API/V1/{api_key}/ADDON_SERVICES/SEND/TSMS");
                //var request = new RestRequest(Method.POST);
                //request.AddParameter("undefined", "{\"From\": \"{SenderId}\",\"To\": \"{CommaSeparatedContacts}\", \"Msg\": \"{MessageBody}\", \"SendAt\": \"{OptionScheduleTime}\"}", ParameterType.RequestBody);
                //IRestResponse response = client.Execute(request);
            }
            catch (Exception ex )
            {

                //throw;
            }
            try
            {
                // Find your Account Sid and Token at twilio.com/console
                // and set the environment variables. See http://twil.io/secure
                //string accountSid = Environment.GetEnvironmentVariable("AC061a374ed80ce9d98d1932d5e1258675");
                //string authToken = Environment.GetEnvironmentVariable("552375853318f54f95518df0c02f4fe2");
                //////const string accountSid = "AC061a374ed80ce9d98d1932d5e1258675";
                //////const string authToken = "552375853318f54f95518df0c02f4fe2";
                //////TwilioClient.Init(accountSid, authToken);

                //TwilioClient.Init(accountSid, authToken);

                //var message = MessageResource.Create(
                //    body: "Thank you!!! Your appointmentID is 126554,Date:03/03/2021, Nuasakala, Keonjhar",
                //    from: new Twilio.Types.PhoneNumber("+18106786111"),
                //    to: new Twilio.Types.PhoneNumber("+919920329044")
                //);
               ////// var message1 = MessageResource.Create(
               //////    body: "Dear "+ appointmentDetail.PatientDetails.FirstName + ", Thank you so much!!! Your appointmentID:"+ testAppointmentDetail.UnqueID + ", Date:"+ appointmentDetail.ReferralMaster.appointment + ", Nuasakala, Keonjhar",
               //////    from: new Twilio.Types.PhoneNumber("+18106786111"),
               //////    to: new Twilio.Types.PhoneNumber("+918904292294")
               //////);
            }
            catch (Exception exx)
            {

                //throw;
            }

            #endregion

            return Ok(testAppointmentDetail);
        }

        [HttpPut("{UnqueID}")]
        public async Task<ActionResult<AppointmentDetail>> Put(string UnqueID, [FromBody] AppointmentDetailViewModel value)
        {
            //var appointmentDetail1 = await _appointmentDetailRepository.GetById(UnqueID);

            ////string aa = new CommonUtility(_appointmentDetailRepository, _uow).GetID(UnqueID).ToString();
            ////var a = _appointmentDetailRepository.GetById(value.UnqueID, value.UnqueID);
            //var appointmentDetail = new AppointmentDetail(appointmentDetail1.Id, value.Notes, value.Name, value.UnqueID);

            //_appointmentDetailRepository.Update(appointmentDetail, UnqueID);
            var appointmentDetail = new AppointmentDetail(UnqueID, value);

            _appointmentDetailRepository.Update(appointmentDetail, appointmentDetail.UnqueID);

            await _uow.Commit();

            return Ok(await _appointmentDetailRepository.GetById(value.UnqueID));
        }

        [HttpDelete("{UnqueID}")]
        public async Task<ActionResult> Delete(string UnqueID)
        {
            _appointmentDetailRepository.Remove(UnqueID);

            // it won't be null
            var testAppointmentDetail = await _appointmentDetailRepository.GetById(UnqueID);

            // If everything is ok then:
            await _uow.Commit();

            // not it must by null
            //testAppointmentDetail = await _appointmentDetailRepository.GetById("153");

            return Ok();
        }
    }
}
