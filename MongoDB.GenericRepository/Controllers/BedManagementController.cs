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
    public class BedManagementController : ControllerBase
    {
        private readonly IBedManagementRepository _bedManagementRepository;
        private readonly IUnitOfWork _uow;

        public BedManagementController(IBedManagementRepository bedManagementRepository, IUnitOfWork uow)
        {
            _bedManagementRepository = bedManagementRepository;
            _uow = uow;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<BedManagement>>> Get()
        {
            var bedManagements = await _bedManagementRepository.GetAll();
            return Ok(bedManagements);
        }
        [HttpGet("{UnqueID}")]
        public async Task<ActionResult<BedManagement>> Get(string UnqueID)
        {
            var bedManagement = await _bedManagementRepository.GetById(UnqueID);
            return Ok(bedManagement);
        }
        [Route("[action]/{DateStart}")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BedManagement>>> GetByDateStart(string DateStart)
        {
            var bedManagements = await _bedManagementRepository.GetAll();
            var available = from c in bedManagements where c.DateStart == DateStart select c;


            //DateTime frmdt = Convert.ToDateTime("2020-11-27T18:30:00.000Z");
            //DateTime frmdt = Convert.ToDateTime(DateStart);
            //string frmdtString = frmdt.ToString("dd-MM-yyy");
            //Console.WriteLine(frmdtString);
            var availableFilter = bedManagements.Where(c => c.DateStart != null);
            //var totalAvailable = availableFilter.Where(c => Convert.ToDateTime(c.ReferralMaster.appointment).ToString("dd-MM-yyy") == frmdtString);
            var totalAvailable = availableFilter.Where(c => c.DateStart.Trim() == DateStart.Trim());
            //var availableCars = bedManagements.Where(c => c.IsAvailable).ToArray();
            return Ok(totalAvailable); // bedManagement.PatientDetails.;
        }

        //[HttpGet("{DateStart}")]
        //public async Task<ActionResult<BedManagement>> Get(string DateStart)
        //{
        //    var bedManagement = await _bedManagementRepository.GetById(UnqueID);
        //    return Ok(bedManagement);
        //}

        [HttpGet("{id}/{UnqueID}")]
        public async Task<string> GetName(int id, string UnqueID)
        {
            var bedManagement = await _bedManagementRepository.GetById(UnqueID);
            return "PatientName"; // bedManagement.PatientDetails.;
        }


        [HttpGet("{UnqueID}/{DateStart}")]
        public async Task<string> GetName(string UnqueID, string DateStart)
        {
            var bedManagement = await _bedManagementRepository.GetByIdDateStartOnly(DateStart);
            return "PatientName"; // bedManagement.PatientDetails.;
        }









        [HttpPost, Route("PostSimulatingError")]
        public IActionResult PostSimulatingError([FromBody] BedManagementViewModel value)
        {
            var bedManagement = new BedManagement(value);
            _bedManagementRepository.Add(bedManagement);

            // The bedManagement will not be added
            return BadRequest();
        }

        [HttpPost]
        public async Task<ActionResult<BedManagement>> Post([FromBody] BedManagementViewModel value)
        {

            var bedManagement = new BedManagement(value);
            // var bedManagement = new BedManagement(value.Notes, value.Name, value.UnqueID);
            _bedManagementRepository.Add(bedManagement);

            // it will be null
            var testBedManagement = await _bedManagementRepository.GetById(bedManagement.UnqueID);

            // If everything is ok then:
            await _uow.Commit();

            // The bedManagement will be added only after commit
            testBedManagement = await _bedManagementRepository.GetById(bedManagement.UnqueID);

            return Ok(testBedManagement);
        }

        [HttpPut("{UnqueID}")]
        public async Task<ActionResult<BedManagement>> Put(string UnqueID, [FromBody] BedManagementViewModel value)
        {
            //var bedManagement1 = await _bedManagementRepository.GetById(UnqueID);

            ////string aa = new CommonUtility(_bedManagementRepository, _uow).GetID(UnqueID).ToString();
            ////var a = _bedManagementRepository.GetById(value.UnqueID, value.UnqueID);
            //var bedManagement = new BedManagement(bedManagement1.Id, value.Notes, value.Name, value.UnqueID);

            //_bedManagementRepository.Update(bedManagement, UnqueID);
            var bedManagement = new BedManagement(UnqueID, value);

            _bedManagementRepository.Update(bedManagement, bedManagement.UnqueID);

            await _uow.Commit();

            return Ok(await _bedManagementRepository.GetById(value.UnqueID));
        }
        [HttpPut]
        [Route("UpdateBedStatus/{id}/{BedCategory}/{BedName}/{status}/{OccupiedBy}/{OPDIPDID}/{CaseUniqueID}/{AssignedDoctor}")]
        public async Task<ActionResult<BedManagement>> UpdateBedStatus(string id, string BedCategory, string BedName,string status,string OccupiedBy,string OPDIPDID,string CaseUniqueID,string  AssignedDoctor)
        {
            var bedManagement1 = await _bedManagementRepository.GetById(id);
            

            bedManagement1.teachers.Where(l => l.name.Equals(BedCategory)).ToList()[0].batches.Where(r=>r.name==BedName).ToList()
                .ForEach(i => { i.OccupySatus = status;i.OccupiedBy = OccupiedBy; i.OPDIPDID = OPDIPDID; i.CaseUniqueID = CaseUniqueID; i.AssignedDoctor = AssignedDoctor; });
            //k[0].batches.ForEach(i => i.name = status);

            //bedManagement1.CreatedBy = "liku"+BedName;
            //var bedManagement = new BedManagement(UnqueID, bedManagement1);

            _bedManagementRepository.Update(bedManagement1, bedManagement1.UnqueID);

            await _uow.Commit();

            return Ok(await _bedManagementRepository.GetById(bedManagement1.UnqueID));
        }

        [HttpPut]
        [Route("UpdateBedStatusDuringAllCaseDelete/{id}/{CaseUniqueID}")]
        public async Task<ActionResult<BedManagement>> UpdateBedStatusDuringAllCaseDelete(string id, string CaseUniqueID)
        {
            var bedManagement1 = await _bedManagementRepository.GetById(id);
            bool find = false;
            for (int j= 0; j < bedManagement1.teachers.Count; j++)
            {
                for(int i=0;i< bedManagement1.teachers[j].batches.Count;i++)
                {
                    string id1 = bedManagement1.teachers[j].batches[i].CaseUniqueID;
                    if(id1 == CaseUniqueID)
                    {
                        find = true;
                        bedManagement1.teachers[j].batches[i].OccupySatus = "OPEN";
                        bedManagement1.teachers[j].batches[i].OccupiedBy = "";
                        bedManagement1.teachers[j].batches[i].OPDIPDID = "";
                        bedManagement1.teachers[j].batches[i].CaseUniqueID = "";
                        bedManagement1.teachers[j].batches[i].AssignedDoctor = "";
                        break;
                    }
                    
                }
                if(find == true)
                {
                    break;
                }
            }

            bool aa = find;
            //bedManagement1.teachers.bat.Where(l => l.CaseUniqueID.Equals(CaseUniqueID)).ToList()[0].batches.Where(r => r.name == BedName).ToList()
            //    .ForEach(i => { i.OccupySatus = status; i.OccupiedBy = OccupiedBy; i.OPDIPDID = OPDIPDID; i.CaseUniqueID = CaseUniqueID; i.AssignedDoctor = AssignedDoctor; });
            //k[0].batches.ForEach(i => i.name = status);

            //bedManagement1.CreatedBy = "liku"+BedName;
            //var bedManagement = new BedManagement(UnqueID, bedManagement1);
            if (find == true)
            {
                _bedManagementRepository.Update(bedManagement1, bedManagement1.UnqueID);

                await _uow.Commit();
            }

            return Ok();
        }


        [HttpDelete("{UnqueID}")]
        public async Task<ActionResult> Delete(string UnqueID)
        {
            _bedManagementRepository.Remove(UnqueID);

            // it won't be null
            var testBedManagement = await _bedManagementRepository.GetById(UnqueID);

            // If everything is ok then:
            await _uow.Commit();

            // not it must by null
            //testBedManagement = await _bedManagementRepository.GetById("153");

            return Ok();
        }
    }
}
