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
    public class AvailabilityController : ControllerBase
    {
        private readonly IAvailabilityRepository _availabilityRepository;
        private readonly IUnitOfWork _uow;

        public AvailabilityController(IAvailabilityRepository availabilityRepository, IUnitOfWork uow)
        {
            _availabilityRepository = availabilityRepository;
            _uow = uow;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Availability>>> Get()
        {
            var availabilitys = await _availabilityRepository.GetAll();
            return Ok(availabilitys);
        }
        [HttpGet("{UnqueID}")]
        public async Task<ActionResult<Availability>> Get(string UnqueID)
        {
            var availability = await _availabilityRepository.GetById(UnqueID);
            return Ok(availability);
        }
        
        //[HttpGet("{DateStart}")]
        //public async Task<ActionResult<Availability>> Get(string DateStart)
        //{
        //    var availability = await _availabilityRepository.GetById(UnqueID);
        //    return Ok(availability);
        //}

        [HttpGet("{id}/{UnqueID}")]
        public async Task<string> GetName(int id, string UnqueID)
        {
            var availability = await _availabilityRepository.GetById(UnqueID);
            return "PatientName"; // availability.PatientDetails.;
        }


        [HttpGet("{UnqueID}/{DateStart}")]
        public async Task<string> GetName(string UnqueID, string DateStart)
        {
            var availability = await _availabilityRepository.GetByIdDateStartOnly(DateStart);
            return "PatientName"; // availability.PatientDetails.;
        }

        [Route("[action]/{paramVal}")]
        [HttpGet]
        public async Task<Availability> Getanything(string paramVal)
        {
            var availability = await _availabilityRepository.Getanything(paramVal);
            return availability; // "PatientName"; // availability.PatientDetails.;
        }






        [HttpPost, Route("PostSimulatingError")]
        public IActionResult PostSimulatingError([FromBody] AvailabilityViewModel value)
        {
            var availability = new Availability(value);
            _availabilityRepository.Add(availability);

            // The availability will not be added
            return BadRequest();
        }

        [HttpPost]
        public async Task<ActionResult<Availability>> Post([FromBody] AvailabilityViewModel value)
        {

            var availability = new Availability(value);
            // var availability = new Availability(value.Notes, value.Name, value.UnqueID);
            _availabilityRepository.Add(availability);

            // it will be null
            var testAvailability = await _availabilityRepository.GetById(availability.UnqueID);

            // If everything is ok then:
            await _uow.Commit();

            // The availability will be added only after commit
            testAvailability = await _availabilityRepository.GetById(availability.UnqueID);

            return Ok(testAvailability);
        }

        [HttpPut("{UnqueID}")]
        public async Task<ActionResult<Availability>> Put(string UnqueID, [FromBody] AvailabilityViewModel value)
        {
            //var availability1 = await _availabilityRepository.GetById(UnqueID);

            ////string aa = new CommonUtility(_availabilityRepository, _uow).GetID(UnqueID).ToString();
            ////var a = _availabilityRepository.GetById(value.UnqueID, value.UnqueID);
            //var availability = new Availability(availability1.Id, value.Notes, value.Name, value.UnqueID);

            //_availabilityRepository.Update(availability, UnqueID);
            var availability = new Availability(UnqueID, value);

            _availabilityRepository.Update(availability, availability.UnqueID);

            await _uow.Commit();

            return Ok(await _availabilityRepository.GetById(value.UnqueID));
        }

        [HttpDelete("{UnqueID}")]
        public async Task<ActionResult> Delete(string UnqueID)
        {
            _availabilityRepository.Remove(UnqueID);

            // it won't be null
            var testAvailability = await _availabilityRepository.GetById(UnqueID);

            // If everything is ok then:
            await _uow.Commit();

            // not it must by null
            //testAvailability = await _availabilityRepository.GetById("153");

            return Ok();
        }
    }
}
