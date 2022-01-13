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
    public class LabTestMasterController : ControllerBase
    {
        private readonly ILabTestMasterRepository _labTestMasterRepository;
        private readonly IUnitOfWork _uow;

        public LabTestMasterController(ILabTestMasterRepository labTestMasterRepository, IUnitOfWork uow)
        {
            _labTestMasterRepository = labTestMasterRepository;
            _uow = uow;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LabTestMaster>>> Get()
        {
            var labTestMasters = await _labTestMasterRepository.GetAll();
            labTestMasters = labTestMasters.OrderBy(s => s.TestName);
            return Ok(labTestMasters);
        }
        [HttpGet("{UnqueID}")]
        public async Task<ActionResult<LabTestMaster>> Get(string UnqueID)
        {
            var labTestMaster = await _labTestMasterRepository.GetById(UnqueID);
            return Ok(labTestMaster);
        }
        [HttpGet]
        [Route("getTestDetail/{TestName}")]
        public async Task<ActionResult<List<LabTestMaster>>> getTestDetail(string TestName)

        {
            var reports = await _labTestMasterRepository.GetAll();
            var res = reports.Where(c => c.TestName.Trim() == TestName.Trim());
            return res.ToList();
        }
        [Route("[action]/{DateStart}")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LabTestMaster>>> GetByDateStart(string DateStart)
        {
            var labTestMasters = await _labTestMasterRepository.GetAll();
            var available = from c in labTestMasters where c.DateStart == DateStart select c;


            //DateTime frmdt = Convert.ToDateTime("2020-11-27T18:30:00.000Z");
            //DateTime frmdt = Convert.ToDateTime(DateStart);
            //string frmdtString = frmdt.ToString("dd-MM-yyy");
            //Console.WriteLine(frmdtString);
            var availableFilter = labTestMasters.Where(c => c.DateStart.Trim() != string.Empty);
            //var totalAvailable = availableFilter.Where(c => Convert.ToDateTime(c.ReferralMaster.appointment).ToString("dd-MM-yyy") == frmdtString);
            var totalAvailable = availableFilter.Where(c => c.DateStart.Trim() == DateStart.Trim());
            //var availableCars = labTestMasters.Where(c => c.IsAvailable).ToArray();
            return Ok(totalAvailable); // labTestMaster.PatientDetails.;
        }

        //[HttpGet("{DateStart}")]
        //public async Task<ActionResult<LabTestMaster>> Get(string DateStart)
        //{
        //    var labTestMaster = await _labTestMasterRepository.GetById(UnqueID);
        //    return Ok(labTestMaster);
        //}

        [HttpGet("{id}/{UnqueID}")]
        public async Task<string> GetName(int id, string UnqueID)
        {
            var labTestMaster = await _labTestMasterRepository.GetById(UnqueID);
            return "PatientName"; // labTestMaster.PatientDetails.;
        }


        [HttpGet("{UnqueID}/{DateStart}")]
        public async Task<string> GetName(string UnqueID, string DateStart)
        {
            var labTestMaster = await _labTestMasterRepository.GetByIdDateStartOnly(DateStart);
            return "PatientName"; // labTestMaster.PatientDetails.;
        }









        [HttpPost, Route("PostSimulatingError")]
        public IActionResult PostSimulatingError([FromBody] LabTestMasterViewModel value)
        {
            var labTestMaster = new LabTestMaster(value);
            _labTestMasterRepository.Add(labTestMaster);

            // The labTestMaster will not be added
            return BadRequest();
        }

        [HttpPost]
        public async Task<ActionResult<LabTestMaster>> Post([FromBody] LabTestMasterViewModel value)
        {

            var labTestMaster = new LabTestMaster(value);
            // var labTestMaster = new LabTestMaster(value.Notes, value.Name, value.UnqueID);
            _labTestMasterRepository.Add(labTestMaster);

            // it will be null
            var testLabTestMaster = await _labTestMasterRepository.GetById(labTestMaster.UnqueID);

            // If everything is ok then:
            await _uow.Commit();

            // The labTestMaster will be added only after commit
            testLabTestMaster = await _labTestMasterRepository.GetById(labTestMaster.UnqueID);

            return Ok(testLabTestMaster);
        }

        [HttpPut("{UnqueID}")]
        public async Task<ActionResult<LabTestMaster>> Put(string UnqueID, [FromBody] LabTestMasterViewModel value)
        {
            //var labTestMaster1 = await _labTestMasterRepository.GetById(UnqueID);

            ////string aa = new CommonUtility(_labTestMasterRepository, _uow).GetID(UnqueID).ToString();
            ////var a = _labTestMasterRepository.GetById(value.UnqueID, value.UnqueID);
            //var labTestMaster = new LabTestMaster(labTestMaster1.Id, value.Notes, value.Name, value.UnqueID);

            //_labTestMasterRepository.Update(labTestMaster, UnqueID);
            var labTestMaster = new LabTestMaster(UnqueID, value);

            _labTestMasterRepository.Update(labTestMaster, labTestMaster.UnqueID);

            await _uow.Commit();

            return Ok(await _labTestMasterRepository.GetById(value.UnqueID));
        }

        [HttpDelete("{UnqueID}")]
        public async Task<ActionResult> Delete(string UnqueID)
        {
            _labTestMasterRepository.Remove(UnqueID);

            // it won't be null
            var testLabTestMaster = await _labTestMasterRepository.GetById(UnqueID);

            // If everything is ok then:
            await _uow.Commit();

            // not it must by null
            //testLabTestMaster = await _labTestMasterRepository.GetById("153");

            return Ok();
        }
    }
}
