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
    public class ReferralMasterController : ControllerBase
    {
        private readonly IReferralMasterRepository _referralMasterRepository;
        private readonly IUnitOfWork _uow;

        public ReferralMasterController(IReferralMasterRepository referralMasterRepository, IUnitOfWork uow)
        {
            _referralMasterRepository = referralMasterRepository;
            _uow = uow;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReferralMaster>>> Get()
        {
            var referralMasters = await _referralMasterRepository.GetAll();
            var doctorList = from c in referralMasters
                             where c.StaffType != null && c.StaffType == "Doctor"
                             select new
                             {
                                 UnqueID = c.UnqueID,
                                 FirstName = c.FirstName,
                                 LastName = c.LastName,
                                 Department = c.Department,
                                 fees = c.fees,
                                 Commission = c.Commission,
                                 Discount = c.Discount,
                                 UserName = c.UserName,
                                 StaffType = c.StaffType

        };
            return Ok(doctorList);
        }

        [HttpGet]
        [Route("getStaffType/{StaffType}")]

        public async Task<ActionResult<IEnumerable<ReferralMaster>>> getStaffType(string StaffType)
        {
            //var farmacyDeliveryToPatient = await _farmacyDeliveryToPatientRepository.GetById(UnqueID);
            //return Ok(farmacyDeliveryToPatient);

            var filterData = await _referralMasterRepository.GetStaffType(StaffType);
            var staffType = from c in filterData where c.StaffType != null && c.StaffType == StaffType
                            select new
                            {
                                UnqueID = c.UnqueID,
                                FirstName = c.FirstName,
                                LastName = c.LastName,
                                Department = c.Department,
                                fees = c.fees,
                                Commission = c.Commission,
                                Discount = c.Discount,
                                UserName = c.UserName,
                                StaffType = c.StaffType

                            };
            return Ok(staffType);
        }

        [HttpGet]
        [Route("getAllStaffType")]

        public async Task<ActionResult<IEnumerable<ReferralMaster>>> getAllStaffType()
        {
            //var farmacyDeliveryToPatient = await _farmacyDeliveryToPatientRepository.GetById(UnqueID);
            //return Ok(farmacyDeliveryToPatient);

            var filterDataMain = await _referralMasterRepository.GetAll();
            var filterData = from c in filterDataMain
                             where c.StaffType != null 
                            select new
                            {
                                UnqueID = c.UnqueID,
                                FirstName = c.FirstName,
                                LastName = c.LastName,
                                Department = c.Department,
                                fees = c.fees,
                                Commission = c.Commission,
                                Discount = c.Discount,
                                UserName = c.UserName,
                                StaffType = c.StaffType,                               
            ContactNumber = c.ContactNumber,
            Experience = c.Experience,
            Degree = c.Degree,
            Email = c.Email,
            Address = c.Address,
            Title = c.Title
            

        };
            return Ok(filterData);
        }

        [HttpGet("{UnqueID}")]
        public async Task<ActionResult<ReferralMaster>> Get(string UnqueID)
        {
            var referralMaster = await _referralMasterRepository.GetById(UnqueID);
            return Ok(referralMaster);
        }

        [HttpGet("{id}/{UnqueID}")]
        public async Task<string> GetName(int id, string UnqueID)
        {
            var referralMaster = await _referralMasterRepository.GetById(UnqueID);
            return referralMaster.FirstName;
        }








        [HttpPost, Route("PostSimulatingError")]
        public IActionResult PostSimulatingError([FromBody] ReferralMasterViewModel value)
        {
            var referralMaster = new ReferralMaster(value);
            _referralMasterRepository.Add(referralMaster);

            // The referralMaster will not be added
            return BadRequest();
        }

        [HttpPost]
        public async Task<ActionResult<ReferralMaster>> Post([FromBody] ReferralMasterViewModel value)
        {

            var referralMaster = new ReferralMaster(value);
            // var referralMaster = new ReferralMaster(value.Notes, value.Name, value.UnqueID);
            _referralMasterRepository.Add(referralMaster);

            // it will be null
            var testReferralMaster = await _referralMasterRepository.GetById(referralMaster.UnqueID);

            // If everything is ok then:
            await _uow.Commit();

            // The referralMaster will be added only after commit
            testReferralMaster = await _referralMasterRepository.GetById(referralMaster.UnqueID);

            return Ok(testReferralMaster);
        }

        [HttpPut("{UnqueID}")]
        public async Task<ActionResult<ReferralMaster>> Put(string UnqueID, [FromBody] ReferralMasterViewModel value)
        {
            //var referralMaster1 = await _referralMasterRepository.GetById(UnqueID);

            ////string aa = new CommonUtility(_referralMasterRepository, _uow).GetID(UnqueID).ToString();
            ////var a = _referralMasterRepository.GetById(value.UnqueID, value.UnqueID);
            //var referralMaster = new ReferralMaster(referralMaster1.Id, value.Notes, value.Name, value.UnqueID);

            //_referralMasterRepository.Update(referralMaster, UnqueID);
            var referralMaster = new ReferralMaster(UnqueID, value);

            _referralMasterRepository.Update(referralMaster, referralMaster.UnqueID);

            await _uow.Commit();

            return Ok(await _referralMasterRepository.GetById(value.UnqueID));
        }

        [HttpDelete("{UnqueID}")]
        public async Task<ActionResult> Delete(string UnqueID)
        {
            _referralMasterRepository.Remove(UnqueID);

            // it won't be null
            var testReferralMaster = await _referralMasterRepository.GetById(UnqueID);

            // If everything is ok then:
            await _uow.Commit();

            // not it must by null
            //testReferralMaster = await _referralMasterRepository.GetById("153");

            return Ok();
        }
    }
}
