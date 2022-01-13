
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
    public class GroupTestsController : ControllerBase
    {
        private readonly IGroupTestsRepository _groupTestsRepository;
        private readonly IUnitOfWork _uow;

        public GroupTestsController(IGroupTestsRepository groupTestsRepository, IUnitOfWork uow)
        {
            _groupTestsRepository = groupTestsRepository;
            _uow = uow;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GroupTests>>> Get()
        {
            var groupTestss = await _groupTestsRepository.GetAll();
            return Ok(groupTestss);
        }
        [HttpGet("{UnqueID}")]
        public async Task<ActionResult<GroupTests>> Get(string UnqueID)
        {
            var groupTests = await _groupTestsRepository.GetById(UnqueID);
            return Ok(groupTests);
        }

        [HttpGet("{id}/{UnqueID}")]
        public async Task<string> GetName(int id, string UnqueID)
        {
            var groupTests = await _groupTestsRepository.GetById(UnqueID);
            return groupTests.UnqueID;
        }








        [HttpPost, Route("PostSimulatingError")]
        public IActionResult PostSimulatingError([FromBody] GroupTestsViewModel value)
        {
            var groupTests = new GroupTests(value);
            _groupTestsRepository.Add(groupTests);

            // The groupTests will not be added
            return BadRequest();
        }

        [HttpPost]
        public async Task<ActionResult<GroupTests>> Post([FromBody] GroupTestsViewModel value)
        {

            var groupTests = new GroupTests(value);
            // var groupTests = new GroupTests(value.Notes, value.Name, value.UnqueID);
            _groupTestsRepository.Add(groupTests);

            // it will be null
            var testGroupTests = await _groupTestsRepository.GetById(groupTests.UnqueID);

            // If everything is ok then:
            await _uow.Commit();

            // The groupTests will be added only after commit
            testGroupTests = await _groupTestsRepository.GetById(groupTests.UnqueID);

            return Ok(testGroupTests);
        }

        [HttpPut("{UnqueID}")]
        public async Task<ActionResult<GroupTests>> Put(string UnqueID, [FromBody] GroupTestsViewModel value)
        {
            //var groupTests1 = await _groupTestsRepository.GetById(UnqueID);

            ////string aa = new CommonUtility(_groupTestsRepository, _uow).GetID(UnqueID).ToString();
            ////var a = _groupTestsRepository.GetById(value.UnqueID, value.UnqueID);
            //var groupTests = new GroupTests(groupTests1.Id, value.Notes, value.Name, value.UnqueID);

            //_groupTestsRepository.Update(groupTests, UnqueID);
            var groupTests = new GroupTests(UnqueID, value);

            _groupTestsRepository.Update(groupTests, groupTests.UnqueID);

            await _uow.Commit();

            return Ok(await _groupTestsRepository.GetById(value.UnqueID));
        }

        [HttpDelete("{UnqueID}")]
        public async Task<ActionResult> Delete(string UnqueID)
        {
            _groupTestsRepository.Remove(UnqueID);

            // it won't be null
            var testGroupTests = await _groupTestsRepository.GetById(UnqueID);

            // If everything is ok then:
            await _uow.Commit();

            // not it must by null
            //testGroupTests = await _groupTestsRepository.GetById("153");

            return Ok();
        }
    }
}
