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
    public class AddQuestionController : ControllerBase
    {
        private readonly IAddQuestionRepository _addQuestionRepository;
        private readonly IUnitOfWork _uow;

        public AddQuestionController(IAddQuestionRepository addQuestionRepository, IUnitOfWork uow)
        {
            _addQuestionRepository = addQuestionRepository;
            _uow = uow;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AddQuestion>>> Get()
        {
            var addQuestions = await _addQuestionRepository.GetAll();
            return Ok(addQuestions);
        }
        [HttpGet("{UnqueID}")]
        public async Task<ActionResult<AddQuestion>> Get(string UnqueID)
        {
            var addQuestion = await _addQuestionRepository.GetById(UnqueID);
            return Ok(addQuestion);
        }
        [Route("[action]/{DateStart}")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AddQuestion>>> GetByDateStart(string DateStart)
        {
            var addQuestions = await _addQuestionRepository.GetAll();
            var available = from c in addQuestions where c.DateStart == DateStart select c;


            //DateTime frmdt = Convert.ToDateTime("2020-11-27T18:30:00.000Z");
            //DateTime frmdt = Convert.ToDateTime(DateStart);
            //string frmdtString = frmdt.ToString("dd-MM-yyy");
            //Console.WriteLine(frmdtString);
            var availableFilter = addQuestions.Where(c => c.questions != null);
            //var totalAvailable = availableFilter.Where(c => Convert.ToDateTime(c.ReferralMaster.appointment).ToString("dd-MM-yyy") == frmdtString);
            var totalAvailable = availableFilter.Where(c => c.DateStart.Trim() == DateStart.Trim());
            //var availableCars = addQuestions.Where(c => c.IsAvailable).ToArray();
            return Ok(totalAvailable); // addQuestion.PatientDetails.;
        }

        //[HttpGet("{DateStart}")]
        //public async Task<ActionResult<AddQuestion>> Get(string DateStart)
        //{
        //    var addQuestion = await _addQuestionRepository.GetById(UnqueID);
        //    return Ok(addQuestion);
        //} 

        [HttpGet("{id}/{UnqueID}")]
        public async Task<string> GetName(int id, string UnqueID)
        {
            var addQuestion = await _addQuestionRepository.GetById(UnqueID);
            return "PatientName"; // addQuestion.PatientDetails.;
        }


        [HttpGet("{UnqueID}/{DateStart}")]
        public async Task<string> GetName(string UnqueID, string DateStart)
        {
            var addQuestion = await _addQuestionRepository.GetByIdDateStartOnly(DateStart);
            return "PatientName"; // addQuestion.PatientDetails.;
        }









        [HttpPost, Route("PostSimulatingError")]
        public IActionResult PostSimulatingError([FromBody] AddQuestionViewModel value)
        {
            var addQuestion = new AddQuestion(value);
            _addQuestionRepository.Add(addQuestion);

            // The addQuestion will not be added
            return BadRequest();
        }

        [HttpPost]
        public async Task<ActionResult<AddQuestion>> Post([FromBody] AddQuestionViewModel value)
        {

            var addQuestion = new AddQuestion(value);
            // var addQuestion = new AddQuestion(value.Notes, value.Name, value.UnqueID);
            _addQuestionRepository.Add(addQuestion);

            // it will be null
            var testAddQuestion = await _addQuestionRepository.GetById(addQuestion.UnqueID);

            // If everything is ok then:
            await _uow.Commit();

            // The addQuestion will be added only after commit
            testAddQuestion = await _addQuestionRepository.GetById(addQuestion.UnqueID);

            return Ok(testAddQuestion);
        }

        [HttpPut("{UnqueID}")]
        public async Task<ActionResult<AddQuestion>> Put(string UnqueID, [FromBody] AddQuestionViewModel value)
        {
            //var addQuestion1 = await _addQuestionRepository.GetById(UnqueID);

            ////string aa = new CommonUtility(_addQuestionRepository, _uow).GetID(UnqueID).ToString();
            ////var a = _addQuestionRepository.GetById(value.UnqueID, value.UnqueID);
            //var addQuestion = new AddQuestion(addQuestion1.Id, value.Notes, value.Name, value.UnqueID);

            //_addQuestionRepository.Update(addQuestion, UnqueID);
            var addQuestion = new AddQuestion(UnqueID, value);

            _addQuestionRepository.Update(addQuestion, addQuestion.UnqueID);

            await _uow.Commit();

            return Ok(await _addQuestionRepository.GetById(value.UnqueID));
        }

        [HttpDelete("{UnqueID}")]
        public async Task<ActionResult> Delete(string UnqueID)
        {
            _addQuestionRepository.Remove(UnqueID);

            // it won't be null
            var testAddQuestion = await _addQuestionRepository.GetById(UnqueID);

            // If everything is ok then:
            await _uow.Commit();

            // not it must by null
            //testAddQuestion = await _addQuestionRepository.GetById("153");

            return Ok();
        }
    }
}
