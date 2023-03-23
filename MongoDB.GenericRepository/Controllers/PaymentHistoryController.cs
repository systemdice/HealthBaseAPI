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
    public class PaymentHistoryController : ControllerBase
    {
        private readonly IPaymentHistoryRepository _paymentHistoryRepository;
        private readonly IUnitOfWork _uow;

        public PaymentHistoryController(IPaymentHistoryRepository paymentHistoryRepository, IUnitOfWork uow)
        {
            _paymentHistoryRepository = paymentHistoryRepository;
            _uow = uow;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PaymentHistory>>> Get()
        {
            var paymentHistorys = await _paymentHistoryRepository.GetAll();
            return Ok(paymentHistorys);
        }
        [HttpGet]
        [Route("getPaymentHistoryByCaseID/{caseID}")]
        public async Task<ActionResult<List<PaymentHistory>>> getPaymentHistoryByCaseID(string caseID)
        {
            var reports = await _paymentHistoryRepository.GetAllPaymentHistory(caseID);

            var expenses1 = (from s in reports
                             orderby s.DateStart
                             where s.CaseID == caseID

                             select s).ToList();
            return expenses1;
        }

        [HttpGet("{UnqueID}")]
        public async Task<ActionResult<PaymentHistory>> Get(string UnqueID)
        {
            var paymentHistory = await _paymentHistoryRepository.GetById(UnqueID);
            return Ok(paymentHistory);
        }

        [HttpGet("{id}/{UnqueID}")]
        public async Task<string> GetName(int id, string UnqueID)
        {
            var paymentHistory = await _paymentHistoryRepository.GetById(UnqueID);
            return paymentHistory.Description;
        }








        [HttpPost, Route("PostSimulatingError")]
        public IActionResult PostSimulatingError([FromBody] PaymentHistoryViewModel value)
        {
            var paymentHistory = new PaymentHistory(value);
            _paymentHistoryRepository.Add(paymentHistory);

            // The paymentHistory will not be added
            return BadRequest();
        }

        [HttpPost]
        public async Task<ActionResult<PaymentHistory>> Post([FromBody] PaymentHistoryViewModel value)
        {

            var paymentHistory = new PaymentHistory(value);
            // var paymentHistory = new PaymentHistory(value.Notes, value.Name, value.UnqueID);
            _paymentHistoryRepository.Add(paymentHistory);

            // it will be null
            var testPaymentHistory = await _paymentHistoryRepository.GetById(paymentHistory.UnqueID);

            // If everything is ok then:
            await _uow.Commit();

            // The paymentHistory will be added only after commit
            testPaymentHistory = await _paymentHistoryRepository.GetById(paymentHistory.UnqueID);

            return Ok(testPaymentHistory);
        }

        [HttpPut("{UnqueID}")]
        public async Task<ActionResult<PaymentHistory>> Put(string UnqueID, [FromBody] PaymentHistoryViewModel value)
        {
            //var paymentHistory1 = await _paymentHistoryRepository.GetById(UnqueID);

            ////string aa = new CommonUtility(_paymentHistoryRepository, _uow).GetID(UnqueID).ToString();
            ////var a = _paymentHistoryRepository.GetById(value.UnqueID, value.UnqueID);
            //var paymentHistory = new PaymentHistory(paymentHistory1.Id, value.Notes, value.Name, value.UnqueID);

            //_paymentHistoryRepository.Update(paymentHistory, UnqueID);
            var paymentHistory = new PaymentHistory(UnqueID, value);

            _paymentHistoryRepository.Update(paymentHistory, paymentHistory.UnqueID);

            await _uow.Commit();

            return Ok(await _paymentHistoryRepository.GetById(value.UnqueID));
        }

        [HttpDelete("{UnqueID}")]
        public async Task<ActionResult> Delete(string UnqueID)
        {
            _paymentHistoryRepository.Remove(UnqueID);

            // it won't be null
            var testPaymentHistory = await _paymentHistoryRepository.GetById(UnqueID);

            // If everything is ok then:
            await _uow.Commit();

            // not it must by null
            //testPaymentHistory = await _paymentHistoryRepository.GetById("153");

            return Ok();
        }
    }
}
