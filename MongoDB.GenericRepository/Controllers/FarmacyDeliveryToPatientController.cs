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
    public class FarmacyDeliveryToPatientController : ControllerBase
    {
        private readonly IFarmacyDeliveryToPatientRepository _farmacyDeliveryToPatientRepository;
        private readonly IUnitOfWork _uow;

        public FarmacyDeliveryToPatientController(IFarmacyDeliveryToPatientRepository farmacyDeliveryToPatientRepository, IUnitOfWork uow)
        {
            _farmacyDeliveryToPatientRepository = farmacyDeliveryToPatientRepository;
            _uow = uow;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FarmacyDeliveryToPatient>>> Get()
        {
            var farmacyDeliveryToPatients = await _farmacyDeliveryToPatientRepository.GetAll();

            return Ok(farmacyDeliveryToPatients);
        }
        [HttpGet]
        [Route("getFarmDataNamewise/{storeName}")]
        public async Task<ActionResult<IEnumerable<FarmacyDeliveryToPatient>>> getFarmDataNamewise(string storeName)
        {
            //var farmacyDeliveryToPatients = await _farmacyDeliveryToPatientRepository.GetAll();
            var farmacyDeliveryToPatients = await _farmacyDeliveryToPatientRepository.GetAllPharmaFilter(storeName, "");
            var objfarmacyDeliveryToPatients = from c in farmacyDeliveryToPatients //.OrderBy(t => Convert.ToInt32((t.CaseID=="" || t.CaseID==null)?"0": t.CaseID))
            where c.PharmacyStoreName == storeName && c.Patientid != "SoftDelete"
                                               select new FaramaBillFew
                                {
                                    //UnqueID = c.UnqueID,
                                    UnqueID = c.UnqueID,
                                    IPDOPDId = c.IPDOPDId,
                                    PaymentStatus = c.PaymentStatus,
                                    DateStart = c.DateStart,
                                    CaseID = c.CaseID,
                                    name = c.name,
                                    BillingDate = c.BillingDate,
                                    BilledBy = c.BilledBy,
                                    CustomerName = c.CustomerName,
                                    BillNo = c.BillNo,
                                   PaymentAmount = c.PaymentAmount,
                                   CeditStatus= c.CeditStatus,
                                   PharmacyStoreName=c.PharmacyStoreName,
                                    GrossSalePriceOnthisBill = c.GrossSalePriceOnthisBill,
                                    GrossPurchasePriceOnthisBill = c.GrossPurchasePriceOnthisBill,
                                    GrossProffitPriceOnthisBill = c.GrossProffitPriceOnthisBill,
                                    GrossGSTPriceOnthisBill = c.GrossGSTPriceOnthisBill,
                                    GrossCGSTPriceOnthisBill = c.GrossCGSTPriceOnthisBill,
                                    GrossSGSTPriceOnthisBill = c.GrossSGSTPriceOnthisBill,
                                    BillingMonth = c.BillingMonth,
                                    BillingYear = c.BillingYear
                                };
            return Ok(objfarmacyDeliveryToPatients);
        }


        [HttpGet]
        [Route("getCashFarmDataNamewise/{storeName}")]
        public async Task<ActionResult<IEnumerable<FarmacyDeliveryToPatient>>> getCashFarmDataNamewise(string storeName)
        {
            //var farmacyDeliveryToPatients = await _farmacyDeliveryToPatientRepository.GetAll();
            var farmacyDeliveryToPatients = await _farmacyDeliveryToPatientRepository.GetAllPharmaFilter(storeName, "OnlyCash");
            var objfarmacyDeliveryToPatients = from c in farmacyDeliveryToPatients.OrderBy(t => Convert.ToInt32(t.CaseID))
                                               where c.PharmacyStoreName == storeName && c.Patientid != "SoftDelete"
                                               select new FaramaBillFew
                                               {
                                                   //UnqueID = c.UnqueID,
                                                   UnqueID = c.UnqueID,
                                                   IPDOPDId = c.IPDOPDId,
                                                   PaymentStatus = c.PaymentStatus,
                                                   DateStart = c.DateStart,
                                                   CaseID = c.CaseID,
                                                   name = c.name,
                                                   BillingDate = c.BillingDate,
                                                   BilledBy = c.BilledBy,
                                                   CustomerName = c.CustomerName,
                                                   BillNo = c.BillNo,
                                                   PaymentAmount = c.PaymentAmount,
                                                   CeditStatus = c.CeditStatus,
                                                   PharmacyStoreName = c.PharmacyStoreName,
                                                   GrossSalePriceOnthisBill = c.GrossSalePriceOnthisBill,
                                                   GrossPurchasePriceOnthisBill = c.GrossPurchasePriceOnthisBill,
                                                   GrossProffitPriceOnthisBill = c.GrossProffitPriceOnthisBill,
                                                   GrossGSTPriceOnthisBill = c.GrossGSTPriceOnthisBill,
                                                   GrossCGSTPriceOnthisBill = c.GrossCGSTPriceOnthisBill,
                                                   GrossSGSTPriceOnthisBill = c.GrossSGSTPriceOnthisBill,
                                                   BillingMonth = c.BillingMonth,
                                                   BillingYear = c.BillingYear
                                               };
            return Ok(objfarmacyDeliveryToPatients);
        }


        [HttpGet]
        [Route("getFarmaComision/{storeName}")]
        public async Task<ActionResult<IEnumerable<FarmacyDeliveryToPatient>>> getFarmaComision(string storeName)
        {
            //var farmacyDeliveryToPatients = await _farmacyDeliveryToPatientRepository.GetAll();
            var farmacyDeliveryToPatients = await _farmacyDeliveryToPatientRepository.GetAllPharmaFilter(storeName, "");
            var objfarmacyDeliveryToPatients = from c in farmacyDeliveryToPatients
                                               where c.PharmacyStoreName == storeName && c.Patientid != "SoftDelete"
                                               select new FaramaBillFew
                                               {
                                                   //UnqueID = c.UnqueID,
                                                   UnqueID = c.UnqueID,
                                                   DateStart = c.DateStart,
                                                   CaseID = c.CaseID,
                                                   name = c.name,
                                                   BillingDate = c.BillingDate,
                                                   BilledBy = c.BilledBy,
                                                   CustomerName = c.CustomerName,
                                                   BillNo = c.BillNo,
                                                   PaymentAmount = c.PaymentAmount,
                                                   refferDoctor = c.refferDoctor,
                                                   DoctorPercentage = c.DoctorPercentage,
                                                   PharmacyStoreName = c.PharmacyStoreName,
                                                   GrossSalePriceOnthisBill = c.GrossSalePriceOnthisBill,
                                                   BillingMonth = c.BillingMonth,
                                                   BillingYear = c.BillingYear
                                               };
            return Ok(objfarmacyDeliveryToPatients);
        }
        [HttpGet("{UnqueID}")]
        public async Task<ActionResult<FarmacyDeliveryToPatient>> Get(string UnqueID)
        {
            var farmacyDeliveryToPatient = await _farmacyDeliveryToPatientRepository.GetById(UnqueID);
            return Ok(farmacyDeliveryToPatient);
        }
        [HttpGet]
        [Route("getFarmDataCasewise/{caseID}/{storeName}")]
        public async Task<ActionResult<FarmacyDeliveryToPatient>> getFarmDataCasewise(string caseID,string storeName)
        {
            //var farmacyDeliveryToPatient = await _farmacyDeliveryToPatientRepository.GetById(UnqueID);
            //return Ok(farmacyDeliveryToPatient);

            var filterData = await _farmacyDeliveryToPatientRepository.GetAllPharmaFilter(storeName, caseID);
            //var farmacyDeliveryToPatient = from c in filterData where (c.CaseID == caseID && c.PharmacyStoreName == storeName) select c;
            var farmacyDeliveryToPatient = from c in filterData where (c.CaseID == caseID && c.PharmacyStoreName == storeName && c.Patientid != "SoftDelete") select c;
            return Ok(farmacyDeliveryToPatient);
        }

        [HttpGet]
        [Route("getByCaseIdEditFreshCase/{caseID}")]
        public async Task<ActionResult<FarmacyDeliveryToPatient>> getByCaseIdEditFreshCase(string caseID)
        {
            //var farmacyDeliveryToPatient = await _farmacyDeliveryToPatientRepository.GetById(UnqueID);
            //return Ok(farmacyDeliveryToPatient);

            //var filterData = await _farmacyDeliveryToPatientRepository.GetAll();
            var filterData = await _farmacyDeliveryToPatientRepository.GetAllPharmaFilter("", caseID);
            var farmacyDeliveryToPatient = from c in filterData
                                           where (c.CaseID == caseID && c.Patientid != "SoftDelete")
                                           select new FaramactToPatientFew
                                           {
                                               IPDOPDId = c.IPDOPDId,
                                               CaseID = c.CaseID,
                                               teachers = c.teachers
                                           };
                                          
            return Ok(farmacyDeliveryToPatient);
        }

        [Route("[action]/{DateStart}")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FarmacyDeliveryToPatient>>> GetByDateStart(string DateStart)
        {
            var farmacyDeliveryToPatients = await _farmacyDeliveryToPatientRepository.GetAll();
            var available = from c in farmacyDeliveryToPatients where c.DateStart == DateStart select c;


            //DateTime frmdt = Convert.ToDateTime("2020-11-27T18:30:00.000Z");
            //DateTime frmdt = Convert.ToDateTime(DateStart);
            //string frmdtString = frmdt.ToString("dd-MM-yyy");
            //Console.WriteLine(frmdtString);
            var availableFilter = farmacyDeliveryToPatients.Where(c => c.Patientid != null);
            //var totalAvailable = availableFilter.Where(c => Convert.ToDateTime(c.ReferralMaster.appointment).ToString("dd-MM-yyy") == frmdtString);
            var totalAvailable = availableFilter.Where(c => c.Patientid.Trim() == DateStart.Trim());
            //var availableCars = farmacyDeliveryToPatients.Where(c => c.IsAvailable).ToArray();
            return Ok(totalAvailable); // farmacyDeliveryToPatient.PatientDetails.;
        }

        //[HttpGet("{DateStart}")]
        //public async Task<ActionResult<FarmacyDeliveryToPatient>> Get(string DateStart)
        //{
        //    var farmacyDeliveryToPatient = await _farmacyDeliveryToPatientRepository.GetById(UnqueID);
        //    return Ok(farmacyDeliveryToPatient);
        //}

        [HttpGet("{id}/{UnqueID}")]
        public async Task<string> GetName(int id, string UnqueID)
        {
            var farmacyDeliveryToPatient = await _farmacyDeliveryToPatientRepository.GetById(UnqueID);
            return "PatientName"; // farmacyDeliveryToPatient.PatientDetails.;
        }


        [HttpGet("{UnqueID}/{DateStart}")]
        public async Task<string> GetName(string UnqueID, string DateStart)
        {
            var farmacyDeliveryToPatient = await _farmacyDeliveryToPatientRepository.GetByIdDateStartOnly(DateStart);
            return "PatientName"; // farmacyDeliveryToPatient.PatientDetails.;
        }


        [HttpPost, Route("PostSimulatingError")]
        public IActionResult PostSimulatingError([FromBody] FarmacyDeliveryToPatientViewModel value)
        {
            var farmacyDeliveryToPatient = new FarmacyDeliveryToPatient(value);
            _farmacyDeliveryToPatientRepository.Add(farmacyDeliveryToPatient);

            // The farmacyDeliveryToPatient will not be added
            return BadRequest();
        }

        //[HttpGet]
        ////[Route("getLabIndividualByCaseID/{caseID}/{testName}")] 
        //[Route("getLatestCashOrCreditID/{CashOrCredit}")]
        //public async Task<ActionResult<string>> getLatestCashOrCreditID(string CashOrCredit)
        //{
        //    string latestId = "000";
        //    var newModifyCases = await _farmacyDeliveryToPatientRepository.GetAll();
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
        public ActionResult<returnSingleData> getLatestCashOrCreditID(string CashOrCredit, string PharmaName)
        {
            var farmacyDeliveryToPatient = new FarmacyDeliveryToPatient();
            long pCredit = _farmacyDeliveryToPatientRepository.GetCount(farmacyDeliveryToPatient, CashOrCredit, PharmaName);
            long pCash = _farmacyDeliveryToPatientRepository.GetCount(farmacyDeliveryToPatient, CashOrCredit, PharmaName);
            //long pCash = _farmacyDeliveryToPatientRepository.GetCount(farmacyDeliveryToPatient, "Credit", PharmaName);
            string CreditrecordCountString = (pCredit == 0 ? 1 : pCredit + 1).ToString();
            string CashrecordCountString = (pCash == 0 ? 1 : pCash + 1).ToString();


            returnSingleData returnSingleDataObj = new returnSingleData();
            //var newModifyCases = await _farmacyDeliveryToPatientRepository.GetAll();
            //string CreditrecordCountString = (newModifyCases.Where(a => a.CeditStatus == "Credit").Count() == 0 ? 1 : newModifyCases.Where(a => a.CeditStatus == "Credit" && a.PharmacyStoreName == PharmaName).Count() + 1).ToString();
            //string CashrecordCountString = (newModifyCases.Where(a => a.CeditStatus == "Cash").Count() == 0 ? 1 : newModifyCases.Where(a => a.CeditStatus == "Cash" && a.PharmacyStoreName == PharmaName).Count() + 1).ToString();
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

            //var farmacyDeliveryToPatient = new FarmacyDeliveryToPatient(value);

            // var farmacyDeliveryToPatient = new FarmacyDeliveryToPatient(value.Notes, value.Name, value.UnqueID);
            //_farmacyDeliveryToPatientRepository.Add(farmacyDeliveryToPatient);

            // it will be null
            //var testFarmacyDeliveryToPatient = await _farmacyDeliveryToPatientRepository.GetById("42119187");
            //testFarmacyDeliveryToPatient.BillNo = BillNo

            // If everything is ok then:
            //await _uow.Commit();

            // The farmacyDeliveryToPatient will be added only after commit
            //testFarmacyDeliveryToPatient = await _farmacyDeliveryToPatientRepository.GetById("42119187");

            return Ok(returnSingleDataObj);
            
        }

        [HttpPost]
        public async Task<ActionResult<FarmacyDeliveryToPatient>> Post([FromBody] FarmacyDeliveryToPatientViewModel value)
        {
            

            var newModifyCases = await _farmacyDeliveryToPatientRepository.GetAllPharmaFilter(value.PharmacyStoreName,"");
            //string CreditrecordCountString = (newModifyCases.Where(a => a.CeditStatus == "Credit").Count() == 0 ? 1 : newModifyCases.Where(a => a.CeditStatus == "Credit").Count() + 1).ToString();
            //string CashrecordCountString = (newModifyCases.Where(a => a.CeditStatus == "Cash").Count() == 0 ? 1 : newModifyCases.Where(a => a.CeditStatus == "Cash").Count() + 1).ToString();

            string CreditrecordCountString = (newModifyCases.Where(a => a.CeditStatus == "Credit").Count() == 0 ? 1 : newModifyCases.Where(a => a.CeditStatus == "Credit" ).Count() + 1).ToString();
            string CashrecordCountString = (newModifyCases.Where(a => a.CeditStatus == "Cash").Count() == 0 ? 1 : newModifyCases.Where(a => a.CeditStatus == "Cash").Count() + 1).ToString();


            if (value.CeditStatus == "Credit")
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
                value.BillNo = "CR" + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString().Substring(DateTime.Now.Year.ToString().Length - 2) + "-" + CreditrecordCountString;
                //value.CaseID = CreditrecordCountString;
            }
            else if (value.CeditStatus == "Cash")
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
                value.BillNo = "CA" + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString().Substring(DateTime.Now.Year.ToString().Length - 2) + "-" + CashrecordCountString;
                value.CaseID = CashrecordCountString;
            }

            var farmacyDeliveryToPatient = new FarmacyDeliveryToPatient(value);

            // var farmacyDeliveryToPatient = new FarmacyDeliveryToPatient(value.Notes, value.Name, value.UnqueID);
            _farmacyDeliveryToPatientRepository.Add(farmacyDeliveryToPatient);

            // it will be null
            var testFarmacyDeliveryToPatient = await _farmacyDeliveryToPatientRepository.GetById(farmacyDeliveryToPatient.UnqueID);

            // If everything is ok then:
            await _uow.Commit();

            // The farmacyDeliveryToPatient will be added only after commit
            testFarmacyDeliveryToPatient = await _farmacyDeliveryToPatientRepository.GetById(farmacyDeliveryToPatient.UnqueID);

            return Ok(testFarmacyDeliveryToPatient);
        }

        [HttpPut("{UnqueID}")]
        public async Task<ActionResult<FarmacyDeliveryToPatient>> Put(string UnqueID, [FromBody] FarmacyDeliveryToPatientViewModel value)
        {
            //var farmacyDeliveryToPatient1 = await _farmacyDeliveryToPatientRepository.GetById(UnqueID);

            ////string aa = new CommonUtility(_farmacyDeliveryToPatientRepository, _uow).GetID(UnqueID).ToString();
            ////var a = _farmacyDeliveryToPatientRepository.GetById(value.UnqueID, value.UnqueID);
            //var farmacyDeliveryToPatient = new FarmacyDeliveryToPatient(farmacyDeliveryToPatient1.Id, value.Notes, value.Name, value.UnqueID);

            //_farmacyDeliveryToPatientRepository.Update(farmacyDeliveryToPatient, UnqueID);
            var farmacyDeliveryToPatient = new FarmacyDeliveryToPatient(UnqueID, value);

            _farmacyDeliveryToPatientRepository.Update(farmacyDeliveryToPatient, farmacyDeliveryToPatient.UnqueID);

            await _uow.Commit();

            return Ok(await _farmacyDeliveryToPatientRepository.GetById(farmacyDeliveryToPatient.UnqueID));
        }

        [HttpDelete("{UnqueID}")]
        public async Task<ActionResult> Delete(string UnqueID)
        {
           // _farmacyDeliveryToPatientRepository.Remove(UnqueID);

            // it won't be null
            //var testFarmacyDeliveryToPatient = await _farmacyDeliveryToPatientRepository.GetById(UnqueID);

            // If everything is ok then:
            //await _uow.Commit();

            // not it must by null
            //testFarmacyDeliveryToPatient = await _farmacyDeliveryToPatientRepository.GetById("153");
            //await _uow.Commit();

            // it won't be null
            var testFarmacyDeliveryToPatient = await _farmacyDeliveryToPatientRepository.GetById(UnqueID);


            testFarmacyDeliveryToPatient.Patientid = "SoftDelete";
            //var newModifyCase = new NewModifyCase(UnqueID, testNewModifyCase);

            _farmacyDeliveryToPatientRepository.Update(testFarmacyDeliveryToPatient, testFarmacyDeliveryToPatient.UnqueID);

            await _uow.Commit();



            return Ok();
        }
    }
}
