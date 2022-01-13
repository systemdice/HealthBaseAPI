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
    public class ExpensesController : ControllerBase
    {
        private readonly IExpensesRepository _expensesRepository;
        private readonly IUnitOfWork _uow;

        public ExpensesController(IExpensesRepository expensesRepository, IUnitOfWork uow)
        {
            _expensesRepository = expensesRepository;
            _uow = uow;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Expenses>>> Get()
        {
            var expensess = await _expensesRepository.GetAll();
            return Ok(expensess);
        }
        [HttpGet("{UnqueID}")]
        public async Task<ActionResult<Expenses>> Get(string UnqueID)
        {
            var expenses = await _expensesRepository.GetById(UnqueID);
            return Ok(expenses);
        }

        [HttpGet("{id}/{UnqueID}")]
        public async Task<string> GetName(int id, string UnqueID)
        {
            var expenses = await _expensesRepository.GetById(UnqueID);
            return expenses.CategoryName;
        }








        [HttpPost, Route("PostSimulatingError")]
        public IActionResult PostSimulatingError([FromBody] ExpensesViewModel value)
        {
            var expenses = new Expenses(value);
            _expensesRepository.Add(expenses);

            // The expenses will not be added
            return BadRequest();
        }

        [HttpPost]
        public async Task<ActionResult<Expenses>> Post([FromBody] ExpensesViewModel value)
        {

            var expenses = new Expenses(value);
            // var expenses = new Expenses(value.Notes, value.Name, value.UnqueID);
            _expensesRepository.Add(expenses);

            // it will be null
            var testExpenses = await _expensesRepository.GetById(expenses.UnqueID);

            // If everything is ok then:
            await _uow.Commit();

            // The expenses will be added only after commit
            testExpenses = await _expensesRepository.GetById(expenses.UnqueID);

            return Ok(testExpenses);
        }

        [HttpPut("{UnqueID}")]
        public async Task<ActionResult<Expenses>> Put(string UnqueID, [FromBody] ExpensesViewModel value)
        {
            //var expenses1 = await _expensesRepository.GetById(UnqueID);

            ////string aa = new CommonUtility(_expensesRepository, _uow).GetID(UnqueID).ToString();
            ////var a = _expensesRepository.GetById(value.UnqueID, value.UnqueID);
            //var expenses = new Expenses(expenses1.Id, value.Notes, value.Name, value.UnqueID);

            //_expensesRepository.Update(expenses, UnqueID);
            var expenses = new Expenses(UnqueID, value);

            _expensesRepository.Update(expenses, expenses.UnqueID);

            await _uow.Commit();

            return Ok(await _expensesRepository.GetById(value.UnqueID));
        }

        [HttpDelete("{UnqueID}")]
        public async Task<ActionResult> Delete(string UnqueID)
        {
            _expensesRepository.Remove(UnqueID);

            // it won't be null
            var testExpenses = await _expensesRepository.GetById(UnqueID);

            // If everything is ok then:
            await _uow.Commit();

            // not it must by null
            //testExpenses = await _expensesRepository.GetById("153");

            return Ok();
        }
    }
}
