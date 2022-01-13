using Microsoft.AspNetCore.Mvc;
using MongoDB.GenericRepository.Interfaces;
using MongoDB.GenericRepository.Model;
using MongoDB.GenericRepository.Utility;
using MongoDB.GenericRepository.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace MongoDB.GenericRepository.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TimesheetController : ControllerBase
    {
        private readonly ITimesheetRepository _timesheetRepository;
        private readonly IUnitOfWork _uow;

        public TimesheetController(ITimesheetRepository timesheetRepository, IUnitOfWork uow)
        {
            _timesheetRepository = timesheetRepository;
            _uow = uow;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Timesheet>>> Get()
        {
           
            var timesheets = await _timesheetRepository.GetAll();
            return Ok(timesheets);
        }
        [HttpGet("{UnqueID}")]
        public async Task<ActionResult<Timesheet>> Get(string UnqueID)
        {
            var timesheet = await _timesheetRepository.GetById(UnqueID);
            return Ok(timesheet);
        }

        [HttpGet("{id}/{UnqueID}")]
        public async Task<string> GetName(int id, string UnqueID)
        {
            var timesheet = await _timesheetRepository.GetById(UnqueID);
            return timesheet.WorkCategory;
        }








        [HttpPost, Route("PostSimulatingError")]
        public IActionResult PostSimulatingError([FromBody] TimesheetViewModel value)
        {
            var timesheet = new Timesheet(value);
            _timesheetRepository.Add(timesheet);

            // The timesheet will not be added
            return BadRequest();
        }

        [HttpPost]
        public async Task<ActionResult<Timesheet>> Post([FromBody] TimesheetViewModel value)
        {

            var timesheet = new Timesheet(value);
            // var timesheet = new Timesheet(value.Notes, value.Name, value.UnqueID);
            _timesheetRepository.Add(timesheet);

            // it will be null
            var testTimesheet = await _timesheetRepository.GetById(timesheet.UnqueID);

            // If everything is ok then:
            await _uow.Commit();

            // The timesheet will be added only after commit
            testTimesheet = await _timesheetRepository.GetById(timesheet.UnqueID);

            return Ok(testTimesheet);
        }

        [HttpPut("{UnqueID}")]
        public async Task<ActionResult<Timesheet>> Put(string UnqueID, [FromBody] TimesheetViewModel value)
        {
            //var timesheet1 = await _timesheetRepository.GetById(UnqueID);

            ////string aa = new CommonUtility(_timesheetRepository, _uow).GetID(UnqueID).ToString();
            ////var a = _timesheetRepository.GetById(value.UnqueID, value.UnqueID);
            //var timesheet = new Timesheet(timesheet1.Id, value.Notes, value.Name, value.UnqueID);

            //_timesheetRepository.Update(timesheet, UnqueID);
            var timesheet = new Timesheet(UnqueID, value);

            _timesheetRepository.Update(timesheet, timesheet.UnqueID);

            await _uow.Commit();

            return Ok(await _timesheetRepository.GetById(value.UnqueID));
        }

        [HttpDelete("{UnqueID}")]
        public async Task<ActionResult> Delete(string UnqueID)
        {
            _timesheetRepository.Remove(UnqueID);

            // it won't be null
            var testTimesheet = await _timesheetRepository.GetById(UnqueID);

            // If everything is ok then:
            await _uow.Commit();

            // not it must by null
            //testTimesheet = await _timesheetRepository.GetById("153");

            return Ok();
        }
    }
}
