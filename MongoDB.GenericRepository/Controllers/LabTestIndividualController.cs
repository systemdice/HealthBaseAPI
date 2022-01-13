using Microsoft.AspNetCore.Mvc;
using MongoDB.GenericRepository.Interfaces;
using MongoDB.GenericRepository.Model;
using MongoDB.GenericRepository.Utility;
using MongoDB.GenericRepository.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoDB.GenericRepository.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LabTestIndividualController : ControllerBase
    {
        private readonly ILabTestIndividualRepository _labTestIndividualRepository;
        private readonly IUnitOfWork _uow;

        public LabTestIndividualController(ILabTestIndividualRepository labTestIndividualRepository, IUnitOfWork uow)
        {
            _labTestIndividualRepository = labTestIndividualRepository;
            _uow = uow;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LabTestIndividual>>> Get()
        {
            var labTestIndividuals = await _labTestIndividualRepository.GetAll();
            return Ok(labTestIndividuals);
        }
        [HttpGet("{UnqueID}")]
        public async Task<ActionResult<LabTestIndividual>> Get(string UnqueID)
        {
            var labTestIndividual = await _labTestIndividualRepository.GetById(UnqueID);
            return Ok(labTestIndividual);
        }
        
        [Route("[action]/{DateStart}")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LabTestIndividual>>> GetByDateStart(string DateStart)
        {
            var labTestIndividuals = await _labTestIndividualRepository.GetAll();
            var available = from c in labTestIndividuals where c.DateStart == DateStart select c;


            //DateTime frmdt = Convert.ToDateTime("2020-11-27T18:30:00.000Z");
            //DateTime frmdt = Convert.ToDateTime(DateStart);
            //string frmdtString = frmdt.ToString("dd-MM-yyy");
            //Console.WriteLine(frmdtString);
            var availableFilter = labTestIndividuals.Where(c => c.DateStart.Trim() != string.Empty);
            //var totalAvailable = availableFilter.Where(c => Convert.ToDateTime(c.ReferralMaster.appointment).ToString("dd-MM-yyy") == frmdtString);
            var totalAvailable = availableFilter.Where(c => c.DateStart.Trim() == DateStart.Trim());
            //var availableCars = labTestIndividuals.Where(c => c.IsAvailable).ToArray();
            return Ok(totalAvailable); // labTestIndividual.PatientDetails.;
        }

        //[HttpGet("{DateStart}")]
        //public async Task<ActionResult<LabTestIndividual>> Get(string DateStart)
        //{
        //    var labTestIndividual = await _labTestIndividualRepository.GetById(UnqueID);
        //    return Ok(labTestIndividual);
        //}

        [HttpGet("{id}/{UnqueID}")]
        public async Task<string> GetName(int id, string UnqueID)
        {
            var labTestIndividual = await _labTestIndividualRepository.GetById(UnqueID);
            return "PatientName"; // labTestIndividual.PatientDetails.;
        }


        [HttpGet("{UnqueID}/{DateStart}")]
        public async Task<string> GetName(string UnqueID, string DateStart)
        {
            var labTestIndividual = await _labTestIndividualRepository.GetByIdDateStartOnly(DateStart);
            return "PatientName"; // labTestIndividual.PatientDetails.;
        }


        [HttpGet]
        [Route("getLabIndividualByCaseID/{caseID}/{testName}/{parentTest}")]

        public async Task<ActionResult<IEnumerable<LabTestIndividual>>> getLabIndividualByCaseID(string caseID,string testName,string parentTest)
        {
            //var farmacyDeliveryToPatient = await _farmacyDeliveryToPatientRepository.GetById(UnqueID);
            //return Ok(farmacyDeliveryToPatient);

            var filterData = await _labTestIndividualRepository.GetAll();
            var labTestIndividual = from c in filterData where c.CaseID == caseID && c.TestName== testName && c.ParentTest== parentTest select c;
            return Ok(labTestIndividual);
        }

        [HttpGet]
        [Route("getAllLabIndividualByCaseID/{caseID}/{testReportingStatus}/{parentTest}")]

        public async Task<ActionResult<IEnumerable<LabTestIndividual>>> getAllLabIndividualByCaseID(string caseID, string testReportingStatus, string parentTest)
        {
            //var farmacyDeliveryToPatient = await _farmacyDeliveryToPatientRepository.GetById(UnqueID);
            //return Ok(farmacyDeliveryToPatient);

            var filterData = await _labTestIndividualRepository.GetAll();
            var labTestIndividual = from c in filterData where c.CaseID == caseID && c.ReportStatus == testReportingStatus && c.ParentTest == parentTest select c;
            return Ok(labTestIndividual);
        }

        [HttpGet]
        [Route("getLabIndividualByStatus")]
        public async Task<ActionResult<IEnumerable<LabTestIndividual>>> getLabIndividualByStatus(string caseID)
        {
            //var farmacyDeliveryToPatient = await _farmacyDeliveryToPatientRepository.GetById(UnqueID);
            //return Ok(farmacyDeliveryToPatient);

            var filterData = await _labTestIndividualRepository.GetAll();
            var labTestIndividual = from c in filterData where c.ReportStatus != "Final" select c;
            return Ok(labTestIndividual.ToList());
        }






        [HttpPost, Route("PostSimulatingError")]
        public IActionResult PostSimulatingError([FromBody] LabTestIndividualViewModel value)
        {
            var labTestIndividual = new LabTestIndividual(value);
            _labTestIndividualRepository.Add(labTestIndividual);

            // The labTestIndividual will not be added
            return BadRequest();
        }

        [HttpPost]
        public async Task<ActionResult<LabTestIndividual>> Post([FromBody] LabTestIndividualViewModel value)
        {

            var labTestIndividual = new LabTestIndividual(value);
            // var labTestIndividual = new LabTestIndividual(value.Notes, value.Name, value.UnqueID);
            _labTestIndividualRepository.Add(labTestIndividual);

            // it will be null
            var testLabTestIndividual = await _labTestIndividualRepository.GetById(labTestIndividual.UnqueID);

            // If everything is ok then:
            await _uow.Commit();

            // The labTestIndividual will be added only after commit
            testLabTestIndividual = await _labTestIndividualRepository.GetById(labTestIndividual.UnqueID);

            return Ok(testLabTestIndividual);
        }

        [HttpPut("{UnqueID}")]
        public async Task<ActionResult<LabTestIndividual>> Put(string UnqueID, [FromBody] LabTestIndividualViewModel value)
        {
            //var labTestIndividual1 = await _labTestIndividualRepository.GetById(UnqueID);

            ////string aa = new CommonUtility(_labTestIndividualRepository, _uow).GetID(UnqueID).ToString();
            ////var a = _labTestIndividualRepository.GetById(value.UnqueID, value.UnqueID);
            //var labTestIndividual = new LabTestIndividual(labTestIndividual1.Id, value.Notes, value.Name, value.UnqueID);

            //_labTestIndividualRepository.Update(labTestIndividual, UnqueID);
            var labTestIndividual = new LabTestIndividual(UnqueID, value);

            _labTestIndividualRepository.Update(labTestIndividual, labTestIndividual.UnqueID);

            await _uow.Commit();

            return Ok(await _labTestIndividualRepository.GetById(value.UnqueID));
        }

        [HttpDelete("{UnqueID}")]
        public async Task<ActionResult> Delete(string UnqueID)
        {
            _labTestIndividualRepository.Remove(UnqueID);

            // it won't be null
            var testLabTestIndividual = await _labTestIndividualRepository.GetById(UnqueID);

            // If everything is ok then:
            await _uow.Commit();

            // not it must by null
            //testLabTestIndividual = await _labTestIndividualRepository.GetById("153");

            return Ok();
        }
    }
}
