using Microsoft.AspNetCore.Mvc;
using MongoDB.GenericRepository.Interfaces;
using MongoDB.GenericRepository.Model;
using MongoDB.GenericRepository.Utility;
using MongoDB.GenericRepository.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;

namespace MongoDB.GenericRepository.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FollowupController : ControllerBase
    {
        private readonly IFollowupRepository _followupRepository;
        private readonly IUnitOfWork _uow;

        public FollowupController(IFollowupRepository followupRepository, IUnitOfWork uow)
        {
            _followupRepository = followupRepository;
            _uow = uow;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Followup>>> Get()
        {
            var followups = await _followupRepository.GetAll();
            return Ok(followups);
        }
        [HttpGet("{UnqueID}")]
        public async Task<ActionResult<Followup>> Get(string UnqueID)
        {
            var followup = await _followupRepository.GetById(UnqueID);
            return Ok(followup);
        }

        [HttpGet("{id}/{UnqueID}")]
        public async Task<string> GetName(int id, string UnqueID)
        {
            var followup = await _followupRepository.GetById(UnqueID);
            return followup.BusinessType;
        }








        [HttpPost, Route("PostSimulatingError")]
        public IActionResult PostSimulatingError([FromBody] FollowupViewModel value)
        {
            var followup = new Followup(value);
            _followupRepository.Add(followup);

            // The followup will not be added
            return BadRequest();
        }

        [HttpPost]
        public async Task<ActionResult<Followup>> Post([FromBody] FollowupViewModel value)
        {

            using (var httpClient = new HttpClient())
            {
                //https://www.yogihosting.com/aspnet-core-consume-api/#read
                string apiString = "https://2factor.in/API/R1/?module=TRANS_SMS&apikey=32d0a507-5c0c-11e7-94da-0200cd936042&to=8904292294&from=NUAAPP&templatename=Doctor+Appoitment&var1=Debasmit&var2=123456";
                using (var response = await httpClient.GetAsync(apiString))
                {
                    //Below one line code need to uncommented for sending sms.
                    //string apiResponse = await response.Content.ReadAsStringAsync();
                }
            }

            //https://2factor.in/API/R1/?module=TRANS_SMS&apikey=API_KEY&to=CLIENT_NUMBER&from=SENDER_ID&templatename=TemplateName&var1=VAR1_VALUE&var2=VAR2_VALUE

            var followup = new Followup(value);
            // var followup = new Followup(value.Notes, value.Name, value.UnqueID);
            _followupRepository.Add(followup);

            // it will be null
            var testFollowup = await _followupRepository.GetById(followup.UnqueID);

            // If everything is ok then:
            await _uow.Commit();

            // The followup will be added only after commit
            testFollowup = await _followupRepository.GetById(followup.UnqueID);

            return Ok(testFollowup);
        }

        [HttpPut("{UnqueID}")]
        public async Task<ActionResult<Followup>> Put(string UnqueID, [FromBody] FollowupViewModel value)
        {
            //var followup1 = await _followupRepository.GetById(UnqueID);

            ////string aa = new CommonUtility(_followupRepository, _uow).GetID(UnqueID).ToString();
            ////var a = _followupRepository.GetById(value.UnqueID, value.UnqueID);
            //var followup = new Followup(followup1.Id, value.Notes, value.Name, value.UnqueID);

            //_followupRepository.Update(followup, UnqueID);
            var followup = new Followup(UnqueID, value);

            _followupRepository.Update(followup, followup.UnqueID);

            await _uow.Commit();

            return Ok(await _followupRepository.GetById(value.UnqueID));
        }

        [HttpDelete("{UnqueID}")]
        public async Task<ActionResult> Delete(string UnqueID)
        {
            _followupRepository.Remove(UnqueID);

            // it won't be null
            var testFollowup = await _followupRepository.GetById(UnqueID);

            // If everything is ok then:
            await _uow.Commit();

            // not it must by null
            //testFollowup = await _followupRepository.GetById("153");

            return Ok();
        }
    }
}
