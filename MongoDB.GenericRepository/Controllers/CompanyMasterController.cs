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
    public class CompanyMasterController : ControllerBase
    {
        private readonly ICompanyMasterRepository _companyMasterRepository;
        private readonly IUnitOfWork _uow;

        public CompanyMasterController(ICompanyMasterRepository companyMasterRepository, IUnitOfWork uow)
        {
            _companyMasterRepository = companyMasterRepository;
            _uow = uow;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CompanyMaster>>> Get()
        {
            var companyMasters = await _companyMasterRepository.GetAll();
            return Ok(companyMasters);
        }
        [HttpGet("{UnqueID}")]
        public async Task<ActionResult<CompanyMaster>> Get(string UnqueID)
        {
            var companyMaster = await _companyMasterRepository.GetById(UnqueID);
            return Ok(companyMaster);
        }
        [Route("[action]/{DateStart}")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CompanyMaster>>> GetByDateStart(string DateStart)
        {
            var companyMasters = await _companyMasterRepository.GetAll();
            var available = from c in companyMasters where c.DateStart == DateStart select c;


            //DateTime frmdt = Convert.ToDateTime("2020-11-27T18:30:00.000Z");
            //DateTime frmdt = Convert.ToDateTime(DateStart);
            //string frmdtString = frmdt.ToString("dd-MM-yyy");
            //Console.WriteLine(frmdtString);
            var availableFilter = companyMasters.Where(c => c.DateStart != null);
            //var totalAvailable = availableFilter.Where(c => Convert.ToDateTime(c.ReferralMaster.appointment).ToString("dd-MM-yyy") == frmdtString);
            var totalAvailable = availableFilter.Where(c => c.DateStart.Trim() == DateStart.Trim());
            //var availableCars = companyMasters.Where(c => c.IsAvailable).ToArray();
            return Ok(totalAvailable); // companyMaster.PatientDetails.;
        }

        //[HttpGet("{DateStart}")]
        //public async Task<ActionResult<CompanyMaster>> Get(string DateStart)
        //{
        //    var companyMaster = await _companyMasterRepository.GetById(UnqueID);
        //    return Ok(companyMaster);
        //}

        [HttpGet("{id}/{UnqueID}")]
        public async Task<string> GetName(int id, string UnqueID)
        {
            var companyMaster = await _companyMasterRepository.GetById(UnqueID);
            return "PatientName"; // companyMaster.PatientDetails.;
        }


        [HttpGet("{UnqueID}/{DateStart}")]
        public async Task<string> GetName(string UnqueID, string DateStart)
        {
            var companyMaster = await _companyMasterRepository.GetByIdDateStartOnly(DateStart);
            return "PatientName"; // companyMaster.PatientDetails.;
        }









        [HttpPost, Route("PostSimulatingError")]
        public IActionResult PostSimulatingError([FromBody] CompanyMasterViewModel value)
        {
            var companyMaster = new CompanyMaster(value);
            _companyMasterRepository.Add(companyMaster);

            // The companyMaster will not be added
            return BadRequest();
        }

        [HttpPost]
        public async Task<ActionResult<CompanyMaster>> Post([FromBody] CompanyMasterViewModel value)
        {

            var companyMaster = new CompanyMaster(value);
            // var companyMaster = new CompanyMaster(value.Notes, value.Name, value.UnqueID);
            _companyMasterRepository.Add(companyMaster);

            // it will be null
            var testCompanyMaster = await _companyMasterRepository.GetById(companyMaster.UnqueID);

            // If everything is ok then:
            await _uow.Commit();

            // The companyMaster will be added only after commit
            testCompanyMaster = await _companyMasterRepository.GetById(companyMaster.UnqueID);

            return Ok(testCompanyMaster);
        }

        [HttpPut("{UnqueID}")]
        public async Task<ActionResult<CompanyMaster>> Put(string UnqueID, [FromBody] CompanyMasterViewModel value)
        {
            //var companyMaster1 = await _companyMasterRepository.GetById(UnqueID);

            ////string aa = new CommonUtility(_companyMasterRepository, _uow).GetID(UnqueID).ToString();
            ////var a = _companyMasterRepository.GetById(value.UnqueID, value.UnqueID);
            //var companyMaster = new CompanyMaster(companyMaster1.Id, value.Notes, value.Name, value.UnqueID);

            //_companyMasterRepository.Update(companyMaster, UnqueID);
            var companyMaster = new CompanyMaster(UnqueID, value);

            _companyMasterRepository.Update(companyMaster, companyMaster.UnqueID);

            await _uow.Commit();

            return Ok(await _companyMasterRepository.GetById(value.UnqueID));
        }

        [HttpDelete("{UnqueID}")]
        public async Task<ActionResult> Delete(string UnqueID)
        {
            _companyMasterRepository.Remove(UnqueID);

            // it won't be null
            var testCompanyMaster = await _companyMasterRepository.GetById(UnqueID);

            // If everything is ok then:
            await _uow.Commit();

            // not it must by null
            //testCompanyMaster = await _companyMasterRepository.GetById("153");

            return Ok();
        }
    }
}
