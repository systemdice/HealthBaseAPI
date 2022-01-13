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
    public class TestHistoryController : ControllerBase
    {
        private readonly ITestHistoryRepository _testHistoryRepository;
        private readonly IUnitOfWork _uow;

        public TestHistoryController(ITestHistoryRepository testHistoryRepository, IUnitOfWork uow)
        {
            _testHistoryRepository = testHistoryRepository;
            _uow = uow;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TestHistory>>> Get()
        {
            var testHistorys = await _testHistoryRepository.GetAll();
            return Ok(testHistorys);
        }
        [HttpGet("{UnqueID}")]
        public async Task<ActionResult<TestHistory>> Get(string UnqueID)
        {
            var testHistory = await _testHistoryRepository.GetById(UnqueID);
            return Ok(testHistory);
        }

        [HttpGet("{id}/{UnqueID}")]
        public async Task<string> GetName(int id, string UnqueID)
        {
            var testHistory = await _testHistoryRepository.GetById(UnqueID);
            return testHistory.Description;
        }








        [HttpPost, Route("PostSimulatingError")]
        public IActionResult PostSimulatingError([FromBody] TestHistoryViewModel value)
        {
            var testHistory = new TestHistory(value);
            _testHistoryRepository.Add(testHistory);

            // The testHistory will not be added
            return BadRequest();
        }

        [HttpPost]
        public async Task<ActionResult<TestHistory>> Post([FromBody] TestHistoryViewModel value)
        {

            var testHistory = new TestHistory(value);
            // var testHistory = new TestHistory(value.Notes, value.Name, value.UnqueID);
            _testHistoryRepository.Add(testHistory);

            // it will be null
            var testTestHistory = await _testHistoryRepository.GetById(testHistory.UnqueID);

            // If everything is ok then:
            await _uow.Commit();

            // The testHistory will be added only after commit
            testTestHistory = await _testHistoryRepository.GetById(testHistory.UnqueID);

            return Ok(testTestHistory);
        }

        [HttpPut("{UnqueID}")]
        public async Task<ActionResult<TestHistory>> Put(string UnqueID, [FromBody] TestHistoryViewModel value)
        {
            //var testHistory1 = await _testHistoryRepository.GetById(UnqueID);

            ////string aa = new CommonUtility(_testHistoryRepository, _uow).GetID(UnqueID).ToString();
            ////var a = _testHistoryRepository.GetById(value.UnqueID, value.UnqueID);
            //var testHistory = new TestHistory(testHistory1.Id, value.Notes, value.Name, value.UnqueID);

            //_testHistoryRepository.Update(testHistory, UnqueID);
            var testHistory = new TestHistory(UnqueID, value);

            _testHistoryRepository.Update(testHistory, testHistory.UnqueID);

            await _uow.Commit();

            return Ok(await _testHistoryRepository.GetById(value.UnqueID));
        }

        [HttpDelete("{UnqueID}")]
        public async Task<ActionResult> Delete(string UnqueID)
        {
            _testHistoryRepository.Remove(UnqueID);

            // it won't be null
            var testTestHistory = await _testHistoryRepository.GetById(UnqueID);

            // If everything is ok then:
            await _uow.Commit();

            // not it must by null
            //testTestHistory = await _testHistoryRepository.GetById("153");

            return Ok();
        }
    }
}
