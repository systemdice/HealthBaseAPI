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
    public class PrintBillController : ControllerBase
    {
        private readonly IPrintBillRepository _printBillRepository;
        private readonly IUnitOfWork _uow;

        public PrintBillController(IPrintBillRepository printBillRepository, IUnitOfWork uow)
        {
            _printBillRepository = printBillRepository;
            _uow = uow;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PrintBill>>> Get()
        {
            var printBills = await _printBillRepository.GetAll();
            return Ok(printBills);
        }
        [HttpGet("{UnqueID}")]
        public async Task<ActionResult<PrintBill>> Get(string UnqueID)
        {
            var printBill = await _printBillRepository.GetById(UnqueID);
            return Ok(printBill);
        }
        

        [Route("[action]/{DateStart}")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PrintBill>>> GetByDateStart(string DateStart)
        {
            var printBills = await _printBillRepository.GetAll();
            var available = from c in printBills where c.Date == DateStart select c;


            //DateTime frmdt = Convert.ToDateTime("2020-11-27T18:30:00.000Z");
            //DateTime frmdt = Convert.ToDateTime(DateStart);
            //string frmdtString = frmdt.ToString("dd-MM-yyy");
            //Console.WriteLine(frmdtString);
            var availableFilter = printBills.Where(c => c.UnqueID != null);
            //var totalAvailable = availableFilter.Where(c => Convert.ToDateTime(c.ReferralMaster.appointment).ToString("dd-MM-yyy") == frmdtString);
            var totalAvailable = availableFilter.Where(c => c.UnqueID.Trim() == DateStart.Trim());
            //var availableCars = printBills.Where(c => c.IsAvailable).ToArray();
            return Ok(totalAvailable); // printBill.PatientDetails.;
        }

        //[HttpGet("{DateStart}")]
        //public async Task<ActionResult<PrintBill>> Get(string DateStart)
        //{
        //    var printBill = await _printBillRepository.GetById(UnqueID);
        //    return Ok(printBill);
        //}

        [HttpGet("{id}/{UnqueID}")]
        public async Task<string> GetName(int id, string UnqueID)
        {
            var printBill = await _printBillRepository.GetById(UnqueID);
            return "PatientName"; // printBill.PatientDetails.;
        }


        [HttpGet("{UnqueID}/{DateStart}")]
        public async Task<string> GetName(string UnqueID, string DateStart)
        {
            var printBill = await _printBillRepository.GetByIdDateStartOnly(DateStart);
            return "PatientName"; // printBill.PatientDetails.;
        }









        [HttpPost, Route("PostSimulatingError")]
        public IActionResult PostSimulatingError([FromBody] PrintBillViewModel value)
        {
            var printBill = new PrintBill(value);
            _printBillRepository.Add(printBill);

            // The printBill will not be added
            return BadRequest();
        }

        //[HttpGet]
        ////[Route("getLabIndividualByCaseID/{caseID}/{testName}")] 
        //[Route("getLatestCashOrCreditID/{CashOrCredit}")]
        //public async Task<ActionResult<string>> getLatestCashOrCreditID(string CashOrCredit)
        //{
        //    string latestId = "000";
        //    var newModifyCases = await _printBillRepository.GetAll();
        //    string CreditrecordCountString = (newModifyCases.Where(a => a.CeditStatus == "Credit").Count() == 0 ? 1 : newModifyCases.Where(a => a.CeditStatus == "Credit").Count() + 1).ToString();
        //    string CashrecordCountString = (newModifyCases.Where(a => a.CeditStatus == "Cash").Count() == 0 ? 1 : newModifyCases.Where(a => a.CeditStatus == "Cash").Count() + 1).ToString();
        //    if (CashOrCredit == "Credit")
        //    {

        //        if (CreditrecordCountString.Length == 0)
        //        {
        //            CreditrecordCountString = "000" + CreditrecordCountString;
        //        }
        //        else if (CreditrecordCountString.Length == 1)
        //        {
        //            CreditrecordCountString = "000" + CreditrecordCountString;
        //        }
        //        else if (CreditrecordCountString.Length == 2)
        //        {
        //            CreditrecordCountString = "00" + CreditrecordCountString;
        //        }
        //        else if (CreditrecordCountString.Length == 3)
        //        {
        //            CreditrecordCountString = "0" + CreditrecordCountString;
        //        }
        //        latestId = "CR" + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString().Substring(DateTime.Now.Year.ToString().Length - 2) + "-" + CreditrecordCountString;
        //        //value.CaseID = CreditrecordCountString;
        //    }
        //    else if (CashOrCredit == "Cash")
        //    {
        //        if (CashrecordCountString.Length == 0)
        //        {
        //            CashrecordCountString = "0000" + CashrecordCountString;
        //        }
        //        else if (CashrecordCountString.Length == 1)
        //        {
        //            CashrecordCountString = "000" + CashrecordCountString;
        //        }
        //        else if (CashrecordCountString.Length == 2)
        //        {
        //            CashrecordCountString = "00" + CashrecordCountString;
        //        }
        //        else if (CashrecordCountString.Length == 3)
        //        {
        //            CashrecordCountString = "0" + CashrecordCountString;
        //        }
        //        latestId = "CA" + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString().Substring(DateTime.Now.Year.ToString().Length - 2) + "-" + CashrecordCountString;

        //    }
        //    return latestId;
        //}

        [HttpPost]
        [Route("getLatestCashOrCreditID/{CashOrCredit}/{PharmaName}")]
        public async Task<ActionResult<returnSingleData>> getLatestCashOrCreditID(string CashOrCredit, string PharmaName)
        {

            returnSingleData returnSingleDataObj = new returnSingleData();
            var newModifyCases = await _printBillRepository.GetAll();
            string CreditrecordCountString = (newModifyCases.Where(a => a.PrintBillStatus == "Credit").Count() == 0 ? 1 : newModifyCases.Where(a => a.PrintBillStatus == "Credit").Count() + 1).ToString();
            string CashrecordCountString = (newModifyCases.Where(a => a.PrintBillStatus == "Cash").Count() == 0 ? 1 : newModifyCases.Where(a => a.PrintBillStatus == "Cash" ).Count() + 1).ToString();
            string BillNo = "";

            if (CashOrCredit == "Credit")
            {

                if (CreditrecordCountString.Length == 0)
                {
                    CreditrecordCountString = "000" + CreditrecordCountString;
                }
                else if (CreditrecordCountString.Length == 1)
                {
                    CreditrecordCountString = "000" + CreditrecordCountString;
                }
                else if (CreditrecordCountString.Length == 2)
                {
                    CreditrecordCountString = "00" + CreditrecordCountString;
                }
                else if (CreditrecordCountString.Length == 3)
                {
                    CreditrecordCountString = "0" + CreditrecordCountString;
                }
                BillNo = "CR" + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString().Substring(DateTime.Now.Year.ToString().Length - 2) + "-" + CreditrecordCountString;
                //value.CaseID = CreditrecordCountString;
            }
            else if (CashOrCredit == "Cash")
            {
                if (CashrecordCountString.Length == 0)
                {
                    CashrecordCountString = "0000" + CashrecordCountString;
                }
                else if (CashrecordCountString.Length == 1)
                {
                    CashrecordCountString = "000" + CashrecordCountString;
                }
                else if (CashrecordCountString.Length == 2)
                {
                    CashrecordCountString = "00" + CashrecordCountString;
                }
                else if (CashrecordCountString.Length == 3)
                {
                    CashrecordCountString = "0" + CashrecordCountString;
                }
                BillNo = "CA" + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString().Substring(DateTime.Now.Year.ToString().Length - 2) + "-" + CashrecordCountString;
                //value.CaseID = CashrecordCountString;
            }

            returnSingleDataObj.NexttoBeIDNameetc = BillNo;

            //var printBill = new PrintBill(value);

            // var printBill = new PrintBill(value.Notes, value.Name, value.UnqueID);
            //_printBillRepository.Add(printBill);

            // it will be null
            //var testPrintBill = await _printBillRepository.GetById("42119187");
            //testPrintBill.BillNo = BillNo

            // If everything is ok then:
            //await _uow.Commit();

            // The printBill will be added only after commit
            //testPrintBill = await _printBillRepository.GetById("42119187");

            return Ok(returnSingleDataObj);
        }

        [HttpPost]
        public async Task<ActionResult<PrintBill>> Post([FromBody] PrintBillViewModel value)
        {


            var newModifyCases = await _printBillRepository.GetAll();
            //string CreditrecordCountString = (newModifyCases.Where(a => a.CeditStatus == "Credit").Count() == 0 ? 1 : newModifyCases.Where(a => a.CeditStatus == "Credit").Count() + 1).ToString();
            //string CashrecordCountString = (newModifyCases.Where(a => a.CeditStatus == "Cash").Count() == 0 ? 1 : newModifyCases.Where(a => a.CeditStatus == "Cash").Count() + 1).ToString();

            string CreditrecordCountString = (newModifyCases.Count() == 0 ? 1 : newModifyCases.Count() + 1).ToString();
            //string CashrecordCountString = (newModifyCases.Where(a => a.PrintBillStatus == "Cash").Count() == 0 ? 1 : newModifyCases.Where(a => a.PrintBillStatus == "Cash" ).Count() + 1).ToString();


            if (CreditrecordCountString.Length == 0)
            {
                CreditrecordCountString = "000" + CreditrecordCountString;
            }
            else if (CreditrecordCountString.Length == 1)
            {
                CreditrecordCountString = "000" + CreditrecordCountString;
            }
            else if (CreditrecordCountString.Length == 2)
            {
                CreditrecordCountString = "00" + CreditrecordCountString;
            }
            else if (CreditrecordCountString.Length == 3)
            {
                CreditrecordCountString = "0" + CreditrecordCountString;
            }
            value.BillID = value.BillID; //"CR" + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString().Substring(DateTime.Now.Year.ToString().Length - 2) + "-" + CreditrecordCountString;
            value.BillNo = CreditrecordCountString;

            var printBill = new PrintBill(value);

            // var printBill = new PrintBill(value.Notes, value.Name, value.UnqueID);
            _printBillRepository.Add(printBill);

            // it will be null
            var testPrintBill = await _printBillRepository.GetById(printBill.UnqueID);

            // If everything is ok then:
            await _uow.Commit();

            // The printBill will be added only after commit
            testPrintBill = await _printBillRepository.GetById(printBill.UnqueID);

            return Ok(testPrintBill);
        }



        [HttpPut("{UnqueID}")]
        public async Task<ActionResult<PrintBill>> Put(string UnqueID, [FromBody] PrintBillViewModel value)
        {
            //var printBill1 = await _printBillRepository.GetById(UnqueID);

            ////string aa = new CommonUtility(_printBillRepository, _uow).GetID(UnqueID).ToString();
            ////var a = _printBillRepository.GetById(value.UnqueID, value.UnqueID);
            //var printBill = new PrintBill(printBill1.Id, value.Notes, value.Name, value.UnqueID);

            //_printBillRepository.Update(printBill, UnqueID);
            var printBill = new PrintBill(UnqueID, value);

            _printBillRepository.Update(printBill, printBill.UnqueID);

            await _uow.Commit();

            return Ok(await _printBillRepository.GetById(printBill.UnqueID));
        }

        [HttpDelete("{UnqueID}")]
        public async Task<ActionResult> Delete(string UnqueID)
        {
            _printBillRepository.Remove(UnqueID);

            // it won't be null
            var testPrintBill = await _printBillRepository.GetById(UnqueID);

            // If everything is ok then:
            await _uow.Commit();

            // not it must by null
            //testPrintBill = await _printBillRepository.GetById("153");

            return Ok();
        }
    }
}
