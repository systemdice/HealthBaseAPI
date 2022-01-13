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
    public class LeaveMangementController : ControllerBase
    {
        private readonly ILeaveMangementRepository _leaveMangementRepository;
        private readonly IUnitOfWork _uow;

        public LeaveMangementController(ILeaveMangementRepository leaveMangementRepository, IUnitOfWork uow)
        {
            _leaveMangementRepository = leaveMangementRepository;
            _uow = uow;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LeaveMangement>>> Get()
        {
            var leaveMangements = await _leaveMangementRepository.GetAll();
            //var doctorList = from c in leaveMangements where c.approved != null && c.StaffType == "Doctor" select c;
            return Ok(leaveMangements);
        }

        [HttpGet]
        [Route("getStaffType/{StaffType}")]

        public async Task<ActionResult<IEnumerable<LeaveMangement>>> getStaffType(string StaffType)
        {
            //var farmacyDeliveryToPatient = await _farmacyDeliveryToPatientRepository.GetById(UnqueID);
            //return Ok(farmacyDeliveryToPatient);

            var filterData = await _leaveMangementRepository.GetAll();
            var staffType = from c in filterData where c.StaffType != null && c.StaffType == StaffType select c;
            return Ok(staffType);
        }

        [HttpGet]
        [Route("getAllStaffType")]

        public async Task<ActionResult<IEnumerable<LeaveMangement>>> getAllStaffType()
        {
            //var farmacyDeliveryToPatient = await _farmacyDeliveryToPatientRepository.GetById(UnqueID);
            //return Ok(farmacyDeliveryToPatient);

            var filterData = await _leaveMangementRepository.GetAll();
            return Ok(filterData);
        }

        [HttpGet("{UnqueID}")]
        public async Task<ActionResult<LeaveMangement>> Get(string UnqueID)
        {
            var leaveMangement = await _leaveMangementRepository.GetById(UnqueID);
            return Ok(leaveMangement);
        }

        [HttpGet("{id}/{UnqueID}")]
        public async Task<string> GetName(int id, string UnqueID)
        {
            var leaveMangement = await _leaveMangementRepository.GetById(UnqueID);
            return leaveMangement.FirstName;
        }








        [HttpPost, Route("PostSimulatingError")]
        public IActionResult PostSimulatingError([FromBody] LeaveMangementViewModel value)
        {
            var leaveMangement = new LeaveMangement(value);
            _leaveMangementRepository.Add(leaveMangement);

            // The leaveMangement will not be added
            return BadRequest();
        }

        [HttpPost]
        public async Task<ActionResult<LeaveMangement>> Post([FromBody] LeaveMangementViewModel value)
        {

            var leaveMangement = new LeaveMangement(value);
            // var leaveMangement = new LeaveMangement(value.Notes, value.Name, value.UnqueID);
            _leaveMangementRepository.Add(leaveMangement);

            // it will be null
            var testLeaveMangement = await _leaveMangementRepository.GetById(leaveMangement.UnqueID);

            // If everything is ok then:
            await _uow.Commit();

            // The leaveMangement will be added only after commit
            testLeaveMangement = await _leaveMangementRepository.GetById(leaveMangement.UnqueID);

            return Ok(testLeaveMangement);
        }

        [HttpPut("{UnqueID}")]
        public async Task<ActionResult<LeaveMangement>> Put(string UnqueID, [FromBody] LeaveMangementViewModel value)
        {
            //var leaveMangement1 = await _leaveMangementRepository.GetById(UnqueID);

            ////string aa = new CommonUtility(_leaveMangementRepository, _uow).GetID(UnqueID).ToString();
            ////var a = _leaveMangementRepository.GetById(value.UnqueID, value.UnqueID);
            //var leaveMangement = new LeaveMangement(leaveMangement1.Id, value.Notes, value.Name, value.UnqueID);

            //_leaveMangementRepository.Update(leaveMangement, UnqueID);
            var leaveMangement = new LeaveMangement(UnqueID, value);

            _leaveMangementRepository.Update(leaveMangement, leaveMangement.UnqueID);

            await _uow.Commit();

            return Ok(await _leaveMangementRepository.GetById(value.UnqueID));
        }

        [HttpDelete("{UnqueID}")]
        public async Task<ActionResult> Delete(string UnqueID)
        {
            _leaveMangementRepository.Remove(UnqueID);

            // it won't be null
            var testLeaveMangement = await _leaveMangementRepository.GetById(UnqueID);

            // If everything is ok then:
            await _uow.Commit();

            // not it must by null
            //testLeaveMangement = await _leaveMangementRepository.GetById("153");

            return Ok();
        }
    }
}




