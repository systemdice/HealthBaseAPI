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
    public class TestsCategoryController : ControllerBase
    {
        private readonly ITestsCategoryRepository _testsCategoryRepository;
        private readonly IUnitOfWork _uow;

        public TestsCategoryController(ITestsCategoryRepository testsCategoryRepository, IUnitOfWork uow)
        {
            _testsCategoryRepository = testsCategoryRepository;
            _uow = uow;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TestsCategory>>> Get()
        {
            var testsCategorys = await _testsCategoryRepository.GetAll();
            return Ok(testsCategorys);
        }
        [HttpGet("{UnqueID}")]
        public async Task<ActionResult<TestsCategory>> Get(string UnqueID)
        {
            var testsCategory = await _testsCategoryRepository.GetById(UnqueID);
            return Ok(testsCategory);
        }

        [HttpGet("{id}/{UnqueID}")]
        public async Task<string> GetName(int id, string UnqueID)
        {
            var testsCategory = await _testsCategoryRepository.GetById(UnqueID);
            return testsCategory.Name;
        }








        [HttpPost, Route("PostSimulatingError")]
        public IActionResult PostSimulatingError([FromBody] TestsCategoryViewModel value)
        {
            var testsCategory = new TestsCategory(value);
            _testsCategoryRepository.Add(testsCategory);

            // The testsCategory will not be added
            return BadRequest();
        }

        [HttpPost]
        public async Task<ActionResult<TestsCategory>> Post([FromBody] TestsCategoryViewModel value)
        {

            var testsCategory = new TestsCategory(value);
           // var testsCategory = new TestsCategory(value.Notes, value.Name, value.UnqueID);
            _testsCategoryRepository.Add(testsCategory);

            // it will be null
            var testTestsCategory = await _testsCategoryRepository.GetById(testsCategory.UnqueID);

            // If everything is ok then:
            await _uow.Commit();

            // The testsCategory will be added only after commit
            testTestsCategory = await _testsCategoryRepository.GetById(testsCategory.UnqueID);

            return Ok(testTestsCategory);
        }

        [HttpPut("{UnqueID}")]
        public async Task<ActionResult<TestsCategory>> Put(string UnqueID, [FromBody] TestsCategoryViewModel value)
        {
            //var testsCategory1 = await _testsCategoryRepository.GetById(UnqueID);

            ////string aa = new CommonUtility(_testsCategoryRepository, _uow).GetID(UnqueID).ToString();
            ////var a = _testsCategoryRepository.GetById(value.UnqueID, value.UnqueID);
            //var testsCategory = new TestsCategory(testsCategory1.Id, value.Notes, value.Name, value.UnqueID);

            //_testsCategoryRepository.Update(testsCategory, UnqueID);
            var testsCategory = new TestsCategory(UnqueID, value);

            _testsCategoryRepository.Update(testsCategory, testsCategory.UnqueID);

            await _uow.Commit();

            return Ok(await _testsCategoryRepository.GetById(value.UnqueID));
        }

        [HttpDelete("{UnqueID}")]
        public async Task<ActionResult> Delete(string UnqueID)
        {
            _testsCategoryRepository.Remove(UnqueID);

            // it won't be null
            var testTestsCategory = await _testsCategoryRepository.GetById(UnqueID);

            // If everything is ok then:
            await _uow.Commit();

            // not it must by null
            //testTestsCategory = await _testsCategoryRepository.GetById("153");

            return Ok();
        }
    }
}
