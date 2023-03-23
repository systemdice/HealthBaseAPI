using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MongoDB.GenericRepository.Interfaces;
using MongoDB.GenericRepository.Model;
using MongoDB.GenericRepository.Utility;
using MongoDB.GenericRepository.ViewModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDB.GenericRepository.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class NewModifyCaseController : ControllerBase
    {
        private readonly INewModifyCaseRepository _newModifyCaseRepository;
        private readonly IPaymentHistoryRepository _paymentHistoryRepository;
        private readonly ILabTestIndividualRepository _labTestIndividualRepository;
        private readonly IFarmacyDeliveryToPatientRepository _farmacyDeliveryToPatient;
        private readonly IConfiguration _configuration;

        private readonly IUnitOfWork _uow;

        public NewModifyCaseController(INewModifyCaseRepository newModifyCaseRepository, ILabTestIndividualRepository labTestIndividualRepository,
                                                IFarmacyDeliveryToPatientRepository farmacyDeliveryToPatient,
                                                IPaymentHistoryRepository paymentHistoryRepository, 
                                                IUnitOfWork uow, IConfiguration configuration)
        {
            _newModifyCaseRepository = newModifyCaseRepository;
            _labTestIndividualRepository = labTestIndividualRepository;
            _farmacyDeliveryToPatient = farmacyDeliveryToPatient;
            _paymentHistoryRepository = paymentHistoryRepository;
            _uow = uow;
            _configuration = configuration;
        }

        [HttpGet]
        [Route("getAdmitted")]
        public async Task<ActionResult<IEnumerable<NewModifyCase>>> getAdmitted()
        {
            var newModifyCases = await _newModifyCaseRepository.GetAllCase();

            var p = newModifyCases.Where(a => a.CaseStatus != "SoftDelete");
            var sumEachCust = from c in p
                                  //let sum = 0.0m
                              from o in c.BedDetailsVisit
                              select new
                              {

                                  CaseID = c.UnqueID,
                                  BedCategory =  o.BedCategory,
                                  BedName = o.BedName,
                                  DateStart = c.DateStart,
                                  PatientName = c.Home.FirstName,
                                  OPDIPDid = c.IPDOPDId,
                                  BedForceRelease = o.BedForceRelease


                              };

            var lst = (from lst1 in sumEachCust.Where(x=> x.BedName != "" && x.BedForceRelease != "Release")
                       
                       //&& x.MachineID == lst1.MachineID)
                       //select lst1
                       select new BeddReport
                       {
                           //dt = DateTime.Parse(groupData.Key.dt),
                           CaseID = lst1.CaseID,
                           BedCategory = lst1.BedCategory,
                           BedName = lst1.BedName,
                           DateStart = lst1.DateStart,
                           PatientName = lst1.PatientName,
                           OPDIPDid = lst1.OPDIPDid

                       }

                       ).ToList();
            return Ok(lst);
        }

        [HttpGet]
        [Route("getAllTodaysCase")]
        public async Task<ActionResult<IEnumerable<NewModifyCase>>> getAllTodaysCase()
        {
            var newModifyCases = await _newModifyCaseRepository.GetAllCase();
            var p = newModifyCases.Where(a => a.CaseStatus != "SoftDelete");
            //var objModifyCase = from c in p
            //                    select new
            //                    {
            //                        UnqueID = c.UnqueID,
            //                        DateStart = c.DateStart, // DateTime.Now.ToString("dd-MM-yyyy"); // UR.DateStart;
            //                        OPDkimbaIPD = c.OPDkimbaIPD,
            //                        IPDOPDId = c.IPDOPDId,
            //                        CaseLife = c.CaseLife,
            //                        IPDOPDConversionStatus = c.IPDOPDConversionStatus,
            //                        IPDOPDConversionFrom = c.IPDOPDConversionFrom,

            //                        PatientID = c.PatientID,
            //                        PaymentHistoryID = c.PatientID,
            //                        CaseStatus = c.CaseStatus,
            //                        UserName = c.UserName,
            //                        UserRole = c.UserRole,
            //                        Location = c.Location,
            //                        ModifiedBy = c.ModifiedBy,
            //                        //DateStart = UR.PatientID; 
            //                        Home = c.Home,
            //                        PaymentHistory = c.PaymentHistory,
            //                        TestNameWithCase = c.TestNameWithCase,

            //                        opdipd = c.opdipd,
            //                        OPD = c.OPD
            //                    };
            CultureInfo invariantCulture1 = CultureInfo.InvariantCulture;

            //try
            //{
            //    //var a2 = DateTime.ParseExact(new DateTime().ToShortDateString().Replace("-", "/"), "dd/MM/yyyy", invariantCulture1);
            //    //var a1 = DateTime.ParseExact("01-04-2021".Replace(" - ", "/"), "dd/MM/yyyy", invariantCulture1);
            //    //var k = DateTime.ParseExact("24/01/2013", "dd/MM/yyyy", CultureInfo.InvariantCulture);
            //    //var h = DateTime.ParseExact("24/01/2013", "dd/MM/yyyy", invariantCulture1);
            //    //var r = DateTime.ParseExact("24/01/2013", "dd/MM/yyyy", CultureInfo.InvariantCulture);
            //    //var r1 = DateTime.ParseExact("04/05/2021", "dd/MM/yyyy", CultureInfo.InvariantCulture);
            //    var r5 = DateTime.ParseExact(DateTime.Now.AddDays(1).ToString("dd/MM/yyyy"), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            //    //var r2 = DateTime.ParseExact(DateTime.Now.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);

            //    newModifyCases = newModifyCases.Where(a => a.CaseStatus != "SoftDelete" && DateTime.ParseExact(a.DateStart.Replace("-", "/"), "dd/MM/yyyy", CultureInfo.InvariantCulture) == r5).ToList();
            //    //newModifyCases = newModifyCases.Where(a => a.CaseStatus != "SoftDelete" && (a.DateStart.Replace("-", "/") == DateTime.Now.ToString("dd/MM/yyyy"))).ToList();
            //}
            //catch (Exception ex)
            //{

            //    //throw;
            //}

            var r5 = DateTime.ParseExact(DateTime.Now.ToString("dd/MM/yyyy"), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            //var r2 = DateTime.ParseExact(DateTime.Now.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);

            newModifyCases = newModifyCases.Where(a => a.CaseStatus != "SoftDelete" && DateTime.ParseExact(a.DateStart.Replace("-", "/"), "dd/MM/yyyy", CultureInfo.InvariantCulture) == r5).ToList();

            var objModifyCase = from c in newModifyCases
                                select new
                                {
                                    //UnqueID = c.UnqueID,
                                    //DateStart = c.DateStart, // DateTime.Now.ToString("dd-MM-yyyy"); // UR.DateStart;
                                    //OPDkimbaIPD = c.OPDkimbaIPD,
                                    //IPDOPDId = c.IPDOPDId,
                                    //CaseLife = c.CaseLife,
                                    //IPDOPDConversionStatus = c.IPDOPDConversionStatus,
                                    //IPDOPDConversionFrom = c.IPDOPDConversionFrom,

                                    //PatientID = c.PatientID,
                                    //PaymentHistoryID = c.PatientID,
                                    //CaseStatus = c.CaseStatus,
                                    //UserName = c.UserName,
                                    //UserRole = c.UserRole,
                                    //Location = c.Location,
                                    //ModifiedBy = c.ModifiedBy,
                                    ////DateStart = UR.PatientID; 
                                    //Home = c.Home,
                                    //PaymentHistory = c.PaymentHistory,
                                    //TestNameWithCase = c.TestNameWithCase,

                                    //opdipd = c.opdipd,
                                    //OPD = c.OPD
                                    UnqueID = c.UnqueID,
                                    DateStart = c.DateStart, // DateTime.Now.ToString("dd-MM-yyyy"); // UR.DateStart;
                                    OPDkimbaIPD = c.OPDkimbaIPD,
                                    IPDOPDId = c.IPDOPDId,
                                    CaseLife = c.CaseLife,
                                    IPDOPDConversionStatus = c.IPDOPDConversionStatus,
                                    IPDOPDConversionFrom = c.IPDOPDConversionFrom,

                                    PatientID = c.PatientID,
                                    PaymentHistoryID = c.PatientID,
                                    CaseStatus = c.CaseStatus,
                                    UserName = c.UserName,
                                    UserRole = c.UserRole,
                                    Location = c.Location,
                                    ModifiedBy = c.ModifiedBy,
                                    //DateStart = UR.PatientID; 
                                    //Home =  new Home {FirstName=c.Home.FirstName,ContactNumber=c.Home.ContactNumber,Year= c.Home.Year, Month = c.Home.Month, Days = c.Home.Days },
                                    //PatientID = c.PatientID,
                                    PaymentHistory = from a1 in c.PaymentHistory select new { Amount = a1.Amount, Balance = a1.Balance, ReceivedBy = a1.ReceivedBy },
                                    //PaymentHistory = [new PaymentHistorySingleFew { Amount = c.PaymentHistory[0].Amount, Balance = c.PaymentHistory[0].Balance, ReceivedBy = c.PaymentHistory[0].ReceivedBy }] ,
                                    TestNameWithCase = c.TestNameWithCase,
                                    Home = new HomeFew { FirstName = c.Home.FirstName, ContactNumber = c.Home.ContactNumber, Gender = c.Home.Gender, Year = c.Home.Year, Month = c.Home.Month, Days = c.Home.Days },
                                    opdipd = c.opdipd,
                                    OPD = c.OPD
                                };

            return Ok(objModifyCase);
        }

      
        [HttpGet]
        [Route("getAnydaysCase/{dateVal}")]
        public async Task<ActionResult<IEnumerable<NewModifyCase>>> getAnydaysCase(string dateVal)
        {
            var newModifyCases = await _newModifyCaseRepository.GetAllCase1(dateVal, "One",null);
            //var p = newModifyCases.Where(a => a.CaseStatus != "SoftDelete");
           

            //CultureInfo invariantCulture1 = CultureInfo.InvariantCulture;

            //try
            //{
            //    //var a2 = DateTime.ParseExact(new DateTime().ToShortDateString().Replace("-", "/"), "dd/MM/yyyy", invariantCulture1);
            //    //var a1 = DateTime.ParseExact("01-04-2021".Replace(" - ", "/"), "dd/MM/yyyy", invariantCulture1);
            //    //var k = DateTime.ParseExact("24/01/2013", "dd/MM/yyyy", CultureInfo.InvariantCulture);
            //    //var h = DateTime.ParseExact("24/01/2013", "dd/MM/yyyy", invariantCulture1);
            //    //var r = DateTime.ParseExact("24/01/2013", "dd/MM/yyyy", CultureInfo.InvariantCulture);
            //    //var r1 = DateTime.ParseExact("04/05/2021", "dd/MM/yyyy", CultureInfo.InvariantCulture);
            //    var r5 = DateTime.ParseExact(DateTime.Now.AddDays(1).ToString("dd/MM/yyyy"), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            //    //var r2 = DateTime.ParseExact(DateTime.Now.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);

            //    newModifyCases = newModifyCases.Where(a => a.CaseStatus != "SoftDelete" && DateTime.ParseExact(a.DateStart.Replace("-", "/"), "dd/MM/yyyy", CultureInfo.InvariantCulture) == r5).ToList();
            //    //newModifyCases = newModifyCases.Where(a => a.CaseStatus != "SoftDelete" && (a.DateStart.Replace("-", "/") == DateTime.Now.ToString("dd/MM/yyyy"))).ToList();
            //}
            //catch (Exception ex)
            //{

            //    //throw;
            //}
            //try
            //{
            //    var r55 = DateTime.ParseExact(dateVal.Replace("-", "/"), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            //    var r2 = DateTime.ParseExact(DateTime.Now.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);

            //    var newModifyCasesResult5 = newModifyCases.Where(a => a.CaseStatus != "SoftDelete" && DateTime.ParseExact(a.DateStart.Replace("-", "/"), "dd/MM/yyyy", CultureInfo.InvariantCulture) == r55).ToList();

            //}
            //catch (Exception ex)
            //{

            //    throw;
            //}
            //var r5 = DateTime.ParseExact(dateVal.Replace("-", "/"), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            //var r2 = DateTime.ParseExact(DateTime.Now.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);

            //var objModifyCase1 = p.Where(a => DateTime.ParseExact(a.DateStart.Replace("-", "/"), "dd/MM/yyyy", CultureInfo.InvariantCulture) == r5).ToList();

            var res = from c in newModifyCases
                      select new
                                {
                          //UnqueID = c.UnqueID,
                          //DateStart = c.DateStart, // DateTime.Now.ToString("dd-MM-yyyy"); // UR.DateStart;
                          //OPDkimbaIPD = c.OPDkimbaIPD,
                          //IPDOPDId = c.IPDOPDId,
                          //CaseLife = c.CaseLife,
                          //IPDOPDConversionStatus = c.IPDOPDConversionStatus,
                          //IPDOPDConversionFrom = c.IPDOPDConversionFrom,

                          //PatientID = c.PatientID,
                          //PaymentHistoryID = c.PatientID,
                          //CaseStatus = c.CaseStatus,
                          //UserName = c.UserName,
                          //UserRole = c.UserRole,
                          //Location = c.Location,
                          //ModifiedBy = c.ModifiedBy,
                          ////DateStart = UR.PatientID; 
                          //Home = c.Home,
                          //PaymentHistory = c.PaymentHistory,
                          //TestNameWithCase = c.TestNameWithCase,

                          //opdipd = c.opdipd,
                          //OPD = c.OPD
                          UnqueID = c.UnqueID,
                          DateStart = c.DateStart, // DateTime.Now.ToString("dd-MM-yyyy"); // UR.DateStart;
                          OPDkimbaIPD = c.OPDkimbaIPD,
                          IPDOPDId = c.IPDOPDId,
                          CaseLife = c.CaseLife,
                          IPDOPDConversionStatus = c.IPDOPDConversionStatus,
                          IPDOPDConversionFrom = c.IPDOPDConversionFrom,

                          PatientID = c.PatientID,
                          PaymentHistoryID = c.PatientID,
                          CaseStatus = c.CaseStatus,
                          UserName = c.UserName,
                          UserRole = c.UserRole,
                          Location = c.Location,
                          ModifiedBy = c.ModifiedBy,
                          //DateStart = UR.PatientID; 
                          //Home =  new Home {FirstName=c.Home.FirstName,ContactNumber=c.Home.ContactNumber,Year= c.Home.Year, Month = c.Home.Month, Days = c.Home.Days },
                          //PatientID = c.PatientID,
                          PaymentHistory = from a1 in c.PaymentHistory select new { Amount = a1.Amount, Balance = a1.Balance, ReceivedBy = a1.ReceivedBy },
                          //PaymentHistory = [new PaymentHistorySingleFew { Amount = c.PaymentHistory[0].Amount, Balance = c.PaymentHistory[0].Balance, ReceivedBy = c.PaymentHistory[0].ReceivedBy }] ,
                          TestNameWithCase = c.TestNameWithCase,
                          Home = new HomeFew { FirstName = c.Home.FirstName, ContactNumber = c.Home.ContactNumber, Gender = c.Home.Gender, Year = c.Home.Year, Month = c.Home.Month, Days = c.Home.Days },
                          opdipd = c.opdipd,
                          OPD = c.OPD
                      };



            return Ok(res);
        }

        [HttpGet]
        [Route("getDateRangeCase/{dateFrom}/{dateTo}")]
        public async Task<ActionResult<IEnumerable<NewModifyCase>>> getDateRangeCase(string dateFrom, string dateTo)
        {
            CultureInfo invariantCulture1 = CultureInfo.InvariantCulture;
            var StartDate = DateTime.ParseExact(dateFrom.Replace("-", "/"), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            var EndDate = DateTime.ParseExact(dateTo.Replace("-", "/"), "dd/MM/yyyy", CultureInfo.InvariantCulture);

            int dayLength = (EndDate - StartDate).Days+1;

            var result = new string[dayLength];
            for (int i = 0; i < Convert.ToInt32(dayLength); i++)
            {
                //var r7 = DateTime.ParseExact(DateTime.Now.AddDays(-i).ToString("dd/MM/yyyy"), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                //string pp = DateTime.Now.AddDays(-i).ToString("dd/MM/yyyy").ToString();
                //string pp1 = DateTime.Now.AddDays(-i).ToString("dd-MM-yyyy").ToString();
                result[i] = EndDate.AddDays(-i).ToString("dd-MM-yyyy").ToString();

            }
            var newModifyCases = await _newModifyCaseRepository.GetAllCase1("", "Range", result);
            //var p = newModifyCases.Where(a => a.CaseStatus != "SoftDelete");


            //CultureInfo invariantCulture1 = CultureInfo.InvariantCulture;

            //try
            //{
            //    //var a2 = DateTime.ParseExact(new DateTime().ToShortDateString().Replace("-", "/"), "dd/MM/yyyy", invariantCulture1);
            //    //var a1 = DateTime.ParseExact("01-04-2021".Replace(" - ", "/"), "dd/MM/yyyy", invariantCulture1);
            //    //var k = DateTime.ParseExact("24/01/2013", "dd/MM/yyyy", CultureInfo.InvariantCulture);
            //    //var h = DateTime.ParseExact("24/01/2013", "dd/MM/yyyy", invariantCulture1);
            //    //var r = DateTime.ParseExact("24/01/2013", "dd/MM/yyyy", CultureInfo.InvariantCulture);
            //    //var r1 = DateTime.ParseExact("04/05/2021", "dd/MM/yyyy", CultureInfo.InvariantCulture);
            //    var r5 = DateTime.ParseExact(DateTime.Now.AddDays(1).ToString("dd/MM/yyyy"), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            //    //var r2 = DateTime.ParseExact(DateTime.Now.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);

            //    newModifyCases = newModifyCases.Where(a => a.CaseStatus != "SoftDelete" && DateTime.ParseExact(a.DateStart.Replace("-", "/"), "dd/MM/yyyy", CultureInfo.InvariantCulture) == r5).ToList();
            //    //newModifyCases = newModifyCases.Where(a => a.CaseStatus != "SoftDelete" && (a.DateStart.Replace("-", "/") == DateTime.Now.ToString("dd/MM/yyyy"))).ToList();
            //}
            //catch (Exception ex)
            //{

            //    //throw;
            //}
            //try
            //{
            //    var r55 = DateTime.ParseExact(dateVal.Replace("-", "/"), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            //    var r2 = DateTime.ParseExact(DateTime.Now.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);

            //    var newModifyCasesResult5 = newModifyCases.Where(a => a.CaseStatus != "SoftDelete" && DateTime.ParseExact(a.DateStart.Replace("-", "/"), "dd/MM/yyyy", CultureInfo.InvariantCulture) == r55).ToList();

            //}
            //catch (Exception ex)
            //{

            //    throw;
            //}
            //var r5 = DateTime.ParseExact(dateVal.Replace("-", "/"), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            //var r2 = DateTime.ParseExact(DateTime.Now.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);

            //var objModifyCase1 = p.Where(a => DateTime.ParseExact(a.DateStart.Replace("-", "/"), "dd/MM/yyyy", CultureInfo.InvariantCulture) == r5).ToList();

            var res = from c in newModifyCases
                      select new
                      {
                          //UnqueID = c.UnqueID,
                          //DateStart = c.DateStart, // DateTime.Now.ToString("dd-MM-yyyy"); // UR.DateStart;
                          //OPDkimbaIPD = c.OPDkimbaIPD,
                          //IPDOPDId = c.IPDOPDId,
                          //CaseLife = c.CaseLife,
                          //IPDOPDConversionStatus = c.IPDOPDConversionStatus,
                          //IPDOPDConversionFrom = c.IPDOPDConversionFrom,

                          //PatientID = c.PatientID,
                          //PaymentHistoryID = c.PatientID,
                          //CaseStatus = c.CaseStatus,
                          //UserName = c.UserName,
                          //UserRole = c.UserRole,
                          //Location = c.Location,
                          //ModifiedBy = c.ModifiedBy,
                          ////DateStart = UR.PatientID; 
                          //Home = c.Home,
                          //PaymentHistory = c.PaymentHistory,
                          //TestNameWithCase = c.TestNameWithCase,

                          //opdipd = c.opdipd,
                          //OPD = c.OPD
                          UnqueID = c.UnqueID,
                          DateStart = c.DateStart, // DateTime.Now.ToString("dd-MM-yyyy"); // UR.DateStart;
                          OPDkimbaIPD = c.OPDkimbaIPD,
                          IPDOPDId = c.IPDOPDId,
                          CaseLife = c.CaseLife,
                          IPDOPDConversionStatus = c.IPDOPDConversionStatus,
                          IPDOPDConversionFrom = c.IPDOPDConversionFrom,

                          PatientID = c.PatientID,
                          PaymentHistoryID = c.PatientID,
                          CaseStatus = c.CaseStatus,
                          UserName = c.UserName,
                          UserRole = c.UserRole,
                          Location = c.Location,
                          ModifiedBy = c.ModifiedBy,
                          //DateStart = UR.PatientID; 
                          //Home =  new Home {FirstName=c.Home.FirstName,ContactNumber=c.Home.ContactNumber,Year= c.Home.Year, Month = c.Home.Month, Days = c.Home.Days },
                          //PatientID = c.PatientID,
                          PaymentHistory = from a1 in c.PaymentHistory select new { Amount = a1.Amount, Balance = a1.Balance, ReceivedBy = a1.ReceivedBy },
                          //PaymentHistory = [new PaymentHistorySingleFew { Amount = c.PaymentHistory[0].Amount, Balance = c.PaymentHistory[0].Balance, ReceivedBy = c.PaymentHistory[0].ReceivedBy }] ,
                          TestNameWithCase = c.TestNameWithCase,
                          Home = new HomeFew { FirstName = c.Home.FirstName, ContactNumber = c.Home.ContactNumber, Gender = c.Home.Gender, Year = c.Home.Year, Month = c.Home.Month, Days = c.Home.Days },
                          opdipd = c.opdipd,
                          OPD = c.OPD
                      };



            return Ok(res);
        }

        //Below controller is for finding the balance sheet for various payment at LAB
        [HttpGet]
        [Route("getDateRangeBalanceSheet/{dateFrom}/{dateTo}")]
        public async Task<ActionResult<IEnumerable<NewModifyCase>>> getDateRangeBalanceSheet(string dateFrom, string dateTo)
        {
            var StartDate = DateTime.ParseExact(dateFrom.Replace("-", "/"), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            var EndDate = DateTime.ParseExact(dateTo.Replace("-", "/"), "dd/MM/yyyy", CultureInfo.InvariantCulture);

            int dayLength = (EndDate - StartDate).Days + 1;

            var result = new string[dayLength];
            for (int i = 0; i < Convert.ToInt32(dayLength); i++)
            {
                 result[i] = EndDate.AddDays(-i).ToString("dd-MM-yyyy").ToString();

            }
            var newModifyCases = await _newModifyCaseRepository.GetAllCase1("", "Range", result);
           
            var res = from c in newModifyCases
                      where c.PatientID != "SoftDelete"
                      select new
                      {                          
                         
                          UnqueID = c.UnqueID,
                          DateStart = c.DateStart, 
                          OPDkimbaIPD = c.OPDkimbaIPD,
                          IPDOPDId = c.IPDOPDId,
                          CaseLife = c.CaseLife,
                          IPDOPDConversionStatus = c.IPDOPDConversionStatus,
                          IPDOPDConversionFrom = c.IPDOPDConversionFrom,

                          PatientID = c.PatientID,
                          PaymentHistoryID = c.PatientID,
                          CaseStatus = c.CaseStatus,
                          UserName = c.UserName,
                          UserRole = c.UserRole,
                          Location = c.Location,
                          ModifiedBy = c.ModifiedBy,
                          PaymentHistory = c.PaymentHistory,
                          //DateStart = UR.PatientID; 
                          //Home =  new Home {FirstName=c.Home.FirstName,ContactNumber=c.Home.ContactNumber,Year= c.Home.Year, Month = c.Home.Month, Days = c.Home.Days },
                          //PatientID = c.PatientID,
                          //PaymentHistory = from a1 in c.PaymentHistory select new { Amount = a1.Amount, Balance = a1.Balance, ReceivedBy = a1.ReceivedBy },
                          //PaymentHistory = [new PaymentHistorySingleFew { Amount = c.PaymentHistory[0].Amount, Balance = c.PaymentHistory[0].Balance, ReceivedBy = c.PaymentHistory[0].ReceivedBy }] ,
                          //TestNameWithCase = c.TestNameWithCase,
                          //Home = new HomeFew { FirstName = c.Home.FirstName, ContactNumber = c.Home.ContactNumber, Gender = c.Home.Gender, Year = c.Home.Year, Month = c.Home.Month, Days = c.Home.Days },
                          opdipd = c.opdipd,
                          OPD = c.OPD
                      };



            return Ok(res);
        }

        [HttpGet]
        [Route("getLastNDaysCase/{noofdays}")]
        public async Task<ActionResult<IEnumerable<NewModifyCase>>> getLastNDaysCase(int noofdays)
        {
            var newModifyCases = await _newModifyCaseRepository.GetAllCase1(noofdays.ToString(), "Many",null);
            //var p = newModifyCases.Where(a1 => a1.CaseStatus != "SoftDelete");

            //CultureInfo invariantCulture1 = CultureInfo.InvariantCulture;

            //var a2 = DateTime.ParseExact(new DateTime().ToShortDateString().Replace("-", "/"), "dd/MM/yyyy", invariantCulture1);
            //var a1 = DateTime.ParseExact("01-04-2021".Replace(" - ", "/"), "dd/MM/yyyy", invariantCulture1);
            //var k = DateTime.ParseExact("24/01/2013", "dd/MM/yyyy", CultureInfo.InvariantCulture);
            //var h = DateTime.ParseExact("24/01/2013", "dd/MM/yyyy", invariantCulture1);
            //var r = DateTime.ParseExact("24/01/2013", "dd/MM/yyyy", CultureInfo.InvariantCulture);
            //var r1 = DateTime.ParseExact("04/05/2021", "dd/MM/yyyy", CultureInfo.InvariantCulture);
            //var r5 = DateTime.ParseExact(DateTime.Now.AddDays(1).ToString("dd/MM/yyyy"), "dd/MM/yyyy", CultureInfo.InvariantCulture); //Usa time matching
            //var r5 = DateTime.ParseExact(DateTime.Now.ToString("dd/MM/yyyy"), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            //var r6 = DateTime.ParseExact(DateTime.Now.AddDays(-noofdays).ToString("dd/MM/yyyy"), "dd/MM/yyyy", CultureInfo.InvariantCulture); //range setting
            //                                                                                                                                  //var r2 = DateTime.ParseExact(DateTime.Now.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);

            //var objModifyCase = p.Where(a => DateTime.ParseExact(a.DateStart.Replace("-", "/"), "dd/MM/yyyy", CultureInfo.InvariantCulture) <= r5
            //&& DateTime.ParseExact(a.DateStart.Replace("-", "/"), "dd/MM/yyyy", CultureInfo.InvariantCulture) > r6).ToList();
            //newModifyCases = newModifyCases.Where(a => a.CaseStatus != "SoftDelete" && (a.DateStart.Replace("-", "/") == DateTime.Now.ToString("dd/MM/yyyy"))).ToList();
            //var a= newModifyCases.Count() == 0 ? null :
            var res = newModifyCases.Count() == 0 ? null : from c in newModifyCases.OrderByDescending(t => DateTime.ParseExact(t.DateStart.Replace("-","/"), "dd/MM/yyyy", CultureInfo.InvariantCulture)) //objModifyCase
                                                           select new
                                {
                        UnqueID = c.UnqueID,
                        DateStart = c.DateStart, // DateTime.Now.ToString("dd-MM-yyyy"); // UR.DateStart;
                        OPDkimbaIPD = c.OPDkimbaIPD,
                        IPDOPDId = c.IPDOPDId,
                        CaseLife = c.CaseLife,
                        IPDOPDConversionStatus = c.IPDOPDConversionStatus,
                        IPDOPDConversionFrom = c.IPDOPDConversionFrom,

                        PatientID = c.PatientID,
                        PaymentHistoryID = c.PatientID,
                        CaseStatus = c.CaseStatus,
                        UserName = c.UserName,
                        UserRole = c.UserRole,
                        Location = c.Location,
                        ModifiedBy = c.ModifiedBy,
                          //DateStart = UR.PatientID; 
                          //Home = new { c.Home.FirstName, c.Home.ContactNumber,c.Home.Days } ,// c.Home ,
                          //PaymentHistory = c.PaymentHistory,
                          //TestNameWithCase = c.TestNameWithCase,
                          PaymentHistory = from a1 in c.PaymentHistory select new { Amount = a1.Amount, Balance = a1.Balance, ReceivedBy = a1.ReceivedBy },
                          //PaymentHistory = [new PaymentHistorySingleFew { Amount = c.PaymentHistory[0].Amount, Balance = c.PaymentHistory[0].Balance, ReceivedBy = c.PaymentHistory[0].ReceivedBy }] ,
                          TestNameWithCase = c.TestNameWithCase,
                          Home = new HomeFew { FirstName = c.Home.FirstName, ContactNumber = c.Home.ContactNumber, Gender = c.Home.Gender, Year = c.Home.Year, Month = c.Home.Month, Days = c.Home.Days },


                          opdipd = c.opdipd,
                        OPD = c.OPD
                        //UnqueID = c.UnqueID,
                        //DateStart = c.DateStart, // DateTime.Now.ToString("dd-MM-yyyy"); // UR.DateStart;
                        //OPDkimbaIPD = c.OPDkimbaIPD,
                        //IPDOPDId = c.IPDOPDId,
                        //CaseLife = c.CaseLife,
                        //IPDOPDConversionStatus = c.IPDOPDConversionStatus,
                        //IPDOPDConversionFrom = c.IPDOPDConversionFrom,

                        //PatientID = c.PatientID,
                        //PaymentHistoryID = c.PatientID,
                        //CaseStatus = c.CaseStatus,
                        //UserName = c.UserName,
                        //UserRole = c.UserRole,
                        //Location = c.Location,
                        //ModifiedBy = c.ModifiedBy,
                        ////DateStart = UR.PatientID; 
                        ////Home =  new Home {FirstName=c.Home.FirstName,ContactNumber=c.Home.ContactNumber,Year= c.Home.Year, Month = c.Home.Month, Days = c.Home.Days },
                        ////PatientID = c.PatientID,
                        //PaymentHistory = from a1 in c.PaymentHistory select new { Amount = a1.Amount, Balance = a1.Balance, ReceivedBy = a1.ReceivedBy },
                        ////PaymentHistory = [new PaymentHistorySingleFew { Amount = c.PaymentHistory[0].Amount, Balance = c.PaymentHistory[0].Balance, ReceivedBy = c.PaymentHistory[0].ReceivedBy }] ,
                        //TestNameWithCase = c.TestNameWithCase,
                        //Home = new HomeFew { FirstName = c.Home.FirstName, ContactNumber = c.Home.ContactNumber, Gender = c.Home.Gender, Year = c.Home.Year, Month = c.Home.Month, Days = c.Home.Days },
                        //opdipd = c.opdipd,
                        //OPD = c.OPD
                    };

            return Ok(res);
        }

        [HttpGet]
        [Route("getTypeData/{type}")]
        public async Task<ActionResult<IEnumerable<NewModifyCase>>> getTypeData(string type)
        {
            var newModifyCases = await _newModifyCaseRepository.GetAllCase();
            //var p = newModifyCases.Where(a1 => a1.CaseStatus != "SoftDelete");
            
            var p = newModifyCases.Where(a => a.CaseStatus != "SoftDelete" && a.OPDkimbaIPD == type).ToList();
            var objModifyCase = from c in p
                                select new
                                {
                                    UnqueID = c.UnqueID,
                                    DateStart = c.DateStart, // DateTime.Now.ToString("dd-MM-yyyy"); // UR.DateStart;
                                    OPDkimbaIPD = c.OPDkimbaIPD,
                                    IPDOPDId = c.IPDOPDId,
                                    CaseLife = c.CaseLife,
                                    IPDOPDConversionStatus = c.IPDOPDConversionStatus,
                                    IPDOPDConversionFrom = c.IPDOPDConversionFrom,

                                    PatientID = c.PatientID,
                                    PaymentHistoryID = c.PatientID,
                                    CaseStatus = c.CaseStatus,
                                    UserName = c.UserName,
                                    UserRole = c.UserRole,
                                    Location = c.Location,
                                    ModifiedBy = c.ModifiedBy,
                                    //DateStart = UR.PatientID; 
                                    Home = c.Home,
                                    PaymentHistory = c.PaymentHistory,
                                    TestNameWithCase = c.TestNameWithCase,

                                    opdipd = c.opdipd,
                                    OPD = c.OPD
                                };

            return Ok(newModifyCases);
        }


        string dateString, format;
        DateTime result;
        CultureInfo provider = CultureInfo.InvariantCulture;

        [HttpGet]
        [Route("getCasewithDoctorPerMonth")]
        public async Task<ActionResult<List<OPDIPDModel>>> getCasewithDoctorPerMonth()
        {
            var expense = await _newModifyCaseRepository.GetAllCase();

           
            var expenses2 = (from exp in expense.OrderBy(x => x.DateStart) //.OrderByDescending(x => x.Date)
                             where exp.DateStart != "" && exp.CaseStatus != "SoftDelete" && exp.Home.RefferDoctorName != null
                             group exp by new { month = DateTime.ParseExact(exp.DateStart.Replace("-", "/"), "dd/MM/yyyy", CultureInfo.InvariantCulture).Month, year = DateTime.ParseExact(exp.DateStart.Replace("-", "/"), "dd/MM/yyyy", CultureInfo.InvariantCulture).Year, businessType = exp.Home.RefferDoctorName } into groupData
                             select new OPDIPDModel
                             {
                                 //dt = DateTime.Parse(groupData.Key.dt),
                                 Month = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(groupData.Key.month) + ", " + groupData.Key.year.ToString(),//groupData.Key.month.ToString(),
                                 BillingMonth = groupData.Key.month.ToString(),
                                 BillingYear = groupData.Key.year.ToString(),//groupData.Key.month.ToString(),
                                 RecordCount = groupData.Count(),
                                 OPDIPDType = groupData.Key.businessType,

                                 OPDCount = groupData.Key.businessType == "OPD" ? groupData.Count().ToString() : "0",
                                 IPDCount = groupData.Key.businessType == "IPD" ? groupData.Count().ToString() : "0",
                                 VSCount = groupData.Key.businessType == "VS" ? groupData.Count().ToString() : "0",

                             }

                                  )
                                  //.OrderBy(i => i.dt)
                                  .ToList();


            return expenses2;
        }

        [HttpGet]
        [Route("getCasewithDoctorAll")]
        public async Task<ActionResult<List<OPDIPDModel>>> getCasewithDoctorAll()
        {
            var expense = await _newModifyCaseRepository.GetAllCase();


            var expenses2 = (from exp in expense.OrderBy(x => x.DateStart) //.OrderByDescending(x => x.Date)
                             where exp.DateStart != "" && exp.CaseStatus != "SoftDelete" && exp.Home.RefferDoctorName !=null
                             group exp by new { businessType = exp.Home.RefferDoctorName } into groupData
                             select new OPDIPDModel
                             {
                                 //dt = DateTime.Parse(groupData.Key.dt),
                                 //Month = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(groupData.Key.month) + ", " + groupData.Key.year.ToString(),//groupData.Key.month.ToString(),
                                 //BillingMonth = groupData.Key.month.ToString(),
                                 //BillingYear = groupData.Key.year.ToString(),//groupData.Key.month.ToString(),
                                 RecordCount = groupData.Count(),
                                 OPDIPDType = groupData.Key.businessType,

                                 //OPDCount = groupData.Key.businessType == "OPD" ? groupData.Count().ToString() : "0",
                                 //IPDCount = groupData.Key.businessType == "IPD" ? groupData.Count().ToString() : "0",
                                 //VSCount = groupData.Key.businessType == "VS" ? groupData.Count().ToString() : "0",

                             }

                                  )
                                  //.OrderBy(i => i.dt)
                                  .ToList();


            return expenses2;
        }

        [HttpGet]
        [Route("getOPDIPDPerMonth")]
        public async Task<ActionResult<List<OPDIPDModel>>> getOPDIPDPerMonth()
        {
            var expense = await _newModifyCaseRepository.GetAllCase();          

    //        var prof = expense.Where(a=> a.CaseStatus != "SoftDelete")
    //.GroupBy(l => l.OPDkimbaIPD)
    //.Select(cl => new ProfitLost
    //{
    //    ProductName = cl.First().OPDkimbaIPD,
    //    Quantity = cl.Count().ToString(),
    //    //Price = cl.Sum(c => c.ExpenseAmount).ToString(), 
    //}).ToList();

            //try {

            //          DateTime dt = DateTime.ParseExact("04/26/2016", "MM/dd/yyyy", CultureInfo.InvariantCulture);
            //          Console.WriteLine(dt.ToString("yyyy-MM-dd"));

            //          DateTime dt1 = DateTime.ParseExact("26/04/2016", "dd/MM/yyyy", CultureInfo.InvariantCulture);
            //          Console.WriteLine(dt1.ToString("yyyy-MM-dd"));

            //          DateTime dt2 = DateTime.ParseExact("26-04-2016", "dd-MM-yyyy", CultureInfo.InvariantCulture);
            //          Console.WriteLine(dt1.ToString("yyyy-MM-dd"));

            //          result = DateTime.ParseExact(dateString, format, provider);
            //   Console.WriteLine("{0} converts to {1}.", dateString, result.ToString());
            //}
            //catch (FormatException) {
            //   Console.WriteLine("{0} is not in the correct format.", dateString);
            //}

            var expenses2 = (from exp in expense.OrderBy(x => x.DateStart) //.OrderByDescending(x => x.Date)
                             where exp.DateStart != "" && exp.CaseStatus != "SoftDelete"
                             group exp by new { month = DateTime.ParseExact(exp.DateStart.Replace("-", "/"), "dd/MM/yyyy", CultureInfo.InvariantCulture).Month, year = DateTime.ParseExact(exp.DateStart.Replace("-", "/"), "dd/MM/yyyy", CultureInfo.InvariantCulture).Year, businessType = exp.OPDkimbaIPD } into groupData
                             select new OPDIPDModel
                             {
                                 //dt = DateTime.Parse(groupData.Key.dt),
                                 Month = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(groupData.Key.month) + ", " + groupData.Key.year.ToString(),//groupData.Key.month.ToString(),
                                 BillingMonth = groupData.Key.month.ToString(),
                                 BillingYear = groupData.Key.year.ToString(),//groupData.Key.month.ToString(),
                                 RecordCount = groupData.Count(),
                                 OPDIPDType = groupData.Key.businessType,

                                OPDCount = groupData.Key.businessType== "OPD"? groupData.Count().ToString():"0",
                                 IPDCount = groupData.Key.businessType == "IPD" ? groupData.Count().ToString() : "0",
                                 VSCount = groupData.Key.businessType == "VS" ? groupData.Count().ToString() : "0",

                             }

                                  )
                                  //.OrderBy(i => i.dt)
                                  .ToList();


            return expenses2;
        }
        [HttpGet]
        [Route("getOPDIPDVSCount")]
        public async Task<ActionResult<List<ProfitLost>>> getOPDIPDVSCount()
        {
            var expense = await _newModifyCaseRepository.GetAllCase();

            var prof = expense.Where(a => a.CaseStatus != "SoftDelete")
    .GroupBy(l => l.OPDkimbaIPD)
    .Select(cl => new ProfitLost
    {
        ProductName = cl.First().OPDkimbaIPD,
        Quantity = cl.Count().ToString(),
        //Price = cl.Sum(c => c.ExpenseAmount).ToString(), 
    }).ToList();

           


            return prof;
        }




        [HttpGet]
        public async Task<ActionResult<IEnumerable<NewModifyCase>>> Get()
        {
            var newModifyCases = await _newModifyCaseRepository.GetAllCase();
            var p = newModifyCases.Where(a => a.CaseStatus != "SoftDelete");
            var objModifyCase = from c in p

                              select new
                              {
                                  UnqueID = c.UnqueID,
            DateStart = c.DateStart, // DateTime.Now.ToString("dd-MM-yyyy"); // UR.DateStart;
            OPDkimbaIPD = c.OPDkimbaIPD,
            IPDOPDId = c.IPDOPDId,
            CaseLife = c.CaseLife,
            IPDOPDConversionStatus = c.IPDOPDConversionStatus,
            IPDOPDConversionFrom = c.IPDOPDConversionFrom,

            PatientID = c.PatientID,
            PaymentHistoryID = c.PatientID,
            CaseStatus = c.CaseStatus,
            UserName = c.UserName,
            UserRole = c.UserRole,
            Location = c.Location,
            ModifiedBy = c.ModifiedBy,
            //DateStart = UR.PatientID; 
            //Home =  new Home {FirstName=c.Home.FirstName,ContactNumber=c.Home.ContactNumber,Year= c.Home.Year, Month = c.Home.Month, Days = c.Home.Days },
            //PatientID = c.PatientID,
            PaymentHistory = from a1 in c.PaymentHistory select new { Amount = a1.Amount, Balance = a1.Balance, ReceivedBy = a1.ReceivedBy },
            //PaymentHistory = [new PaymentHistorySingleFew { Amount = c.PaymentHistory[0].Amount, Balance = c.PaymentHistory[0].Balance, ReceivedBy = c.PaymentHistory[0].ReceivedBy }] ,
            TestNameWithCase = c.TestNameWithCase,
            Home = new HomeFew { FirstName = c.Home.FirstName, ContactNumber = c.Home.ContactNumber,Gender=c.Home.Gender, Year = c.Home.Year, Month = c.Home.Month, Days = c.Home.Days },
            opdipd = c.opdipd,
            OPD = c.OPD
           };

            //return Ok(newModifyCases.Where(a => a.CaseStatus != "SoftDelete"));
            return Ok(objModifyCase);
        }

        [HttpGet]
        [Route("getFarmaCreditDetails/{PharmaName}")]
        public async Task<ActionResult<IEnumerable<NewModifyCase>>> getFarmaCreditDetails(string PharmaName)
        {
            var newModifyCases = await _newModifyCaseRepository.GetAllCase();
            var p = newModifyCases.Where(a => a.CaseStatus != "SoftDelete" && a.Home.AssignedPharma == PharmaName);
            var objModifyCase = from c in p

                                select new
                                {
                                    UnqueID = c.UnqueID,
                                    DateStart = c.DateStart, // DateTime.Now.ToString("dd-MM-yyyy"); // UR.DateStart;
                                    OPDkimbaIPD = c.OPDkimbaIPD,
                                    IPDOPDId = c.IPDOPDId,
                                    CaseLife = c.CaseLife,
                                    IPDOPDConversionStatus = c.IPDOPDConversionStatus,
                                    IPDOPDConversionFrom = c.IPDOPDConversionFrom,

                                    PatientID = c.PatientID,
                                    PaymentHistoryID = c.PatientID,
                                    CaseStatus = c.CaseStatus,
                                    UserName = c.UserName,
                                    UserRole = c.UserRole,
                                    Location = c.Location,
                                    ModifiedBy = c.ModifiedBy,
                                    //DateStart = UR.PatientID; 
                                    //Home =  new Home {FirstName=c.Home.FirstName,ContactNumber=c.Home.ContactNumber,Year= c.Home.Year, Month = c.Home.Month, Days = c.Home.Days },
                                    //PatientID = c.PatientID,
                                    PaymentHistory = from a1 in c.PaymentHistory select new { Amount = a1.Amount, Balance = a1.Balance, ReceivedBy = a1.ReceivedBy },
                                    //PaymentHistory = [new PaymentHistorySingleFew { Amount = c.PaymentHistory[0].Amount, Balance = c.PaymentHistory[0].Balance, ReceivedBy = c.PaymentHistory[0].ReceivedBy }] ,
                                    TestNameWithCase = c.TestNameWithCase,
                                    Home = new HomeFew { FirstName = c.Home.FirstName, LastName = c.Home.LastName, ContactNumber = c.Home.ContactNumber, Gender = c.Home.Gender, Year = c.Home.Year, Month = c.Home.Month, Days = c.Home.Days, AssignedPharma = c.Home.AssignedPharma },
                                    opdipd = c.opdipd,
                                    OPD = c.OPD
                                };

            //return Ok(newModifyCases.Where(a => a.CaseStatus != "SoftDelete"));
            return Ok(objModifyCase);
        }

        [HttpGet]
        [Route("getCount")]
        public async Task<ActionResult<Int32>> getCount()
        {
            var newModifyCases = await _newModifyCaseRepository.GetAllCase();
            var p = newModifyCases.Where(a => a.CaseStatus != "SoftDelete");
           

            return Ok(newModifyCases.Count());
        }


        [HttpGet]
        //[Route("getLabIndividualByCaseID/{caseID}/{testName}")] 
        [Route("getPendingTest")]
        public async Task<ActionResult<IEnumerable<NotifyPendingTestData>>> getPendingTest( )    
        {
            var labTestIndividuals = await _labTestIndividualRepository.GetAllCase();
            var newModifyCases = await _newModifyCaseRepository.GetAllCase();

            var sumEachCust = from c in newModifyCases.Where(a => a.CaseStatus != "SoftDelete")
                              //let sum = 0.0m
                              from o in c.TestNameWithCase.TestType
                              from op in o.names
                              select new
                              {
                                  DateStart = c.DateStart,
                                  testName = op.TestName,
                                  CaseID = c.UnqueID,
                                  RefferDoctorName = c.TestNameWithCase.RefferDoctorName,
                                  CollectionCenter= c.TestNameWithCase.CollectionCenter,
                                  ParentTest = o.parentTest,
                                  Location=c.Location
                              };

            var lst = (from lst1 in sumEachCust
                       where !labTestIndividuals.Any(
                                         x => x.CaseID == lst1.CaseID && x.TestName == lst1.testName && x.ParentTest == lst1.ParentTest)
                       //&& x.MachineID == lst1.MachineID)
                       //select lst1
                       select new NotifyPendingTestData
                       {
                           //dt = DateTime.Parse(groupData.Key.dt),
                           CaseID = lst1.CaseID,
                           TestName = lst1.testName ,
                           DateStart = lst1.DateStart,
                           RefferDoctorName = lst1.RefferDoctorName,
                           CollectionCenter = lst1.CollectionCenter,
                           ParentTest = lst1.ParentTest,
                            Location = lst1.Location

                       }

                       ).ToList();

            //list1 = lst.ToList();
            return Ok(lst);
        }
        [HttpGet]
        //[Route("getLabIndividualByCaseID/{caseID}/{testName}")] 
        [Route("getPendingFarmaJob")]
        public async Task<ActionResult<IEnumerable<NotifyPendingTestData>>> getPendingFarmaJob()
        {
            var farmacyDeliveryToPatient = await _farmacyDeliveryToPatient.GetAll();
            var newModifyCases = await _newModifyCaseRepository.GetAllCase();

            var sumEachCust = from c in newModifyCases.Where(a => a.CaseStatus != "SoftDelete")
                                  //let sum = 0.0m
                                  //from o in c.OPD
                                  //from op in o.names
                              select new
                              {
                                  DateStart = c.DateStart,
                                  CustomerName = c.Home.FirstName,
                                  IPDOPDId = c.IPDOPDId,
                                  CaseID = c.UnqueID,
                                  ContactNumber= c.Home.ContactNumber,
                                  Year=c.Home.Year,
                                  DoctorFeedback = c.OPD.DoctorFeedback,
                                  RefferDoctorName = c.Home.RefferDoctorName
                              };

            var lst = (from lst1 in sumEachCust
                       where !farmacyDeliveryToPatient.Any(
                                         x => x.CaseID == lst1.CaseID)
                       //&& x.MachineID == lst1.MachineID)
                       //select lst1
                       select new 
                       {
                           //dt = DateTime.Parse(groupData.Key.dt),
                           CaseID = lst1.CaseID,
                           CustomerName = lst1.CustomerName,
                           OPDIPDID = lst1.IPDOPDId,
                           Year =lst1.Year,
                           DateStart = lst1.DateStart,
                           RefferDoctorName = lst1.RefferDoctorName,
                           DoctorFeedback = lst1.DoctorFeedback,
                           ContactNumber = lst1.ContactNumber

                       }

                       ).ToList();

            //list1 = lst.ToList();
            return Ok(lst);
        }
        [HttpGet("{UnqueID}")]
        public async Task<ActionResult<NewModifyCase>> Get(string UnqueID)
        {
            var newModifyCase = await _newModifyCaseRepository.GetById(UnqueID);
            //var objModifyCase1 = new NewModifyCaseFew { IPDOPDId = newModifyCase.IPDOPDId };

            return Ok(newModifyCase);
        }

        [HttpGet]
        [Route("getDataWithUniqueorIPOP/{UnqueIDIPOP}")]
        public async Task<ActionResult<NewModifyCase>> getDataWithUniqueorIPOP(string UnqueIDIPOP)
        {
            NewModifyCase nn = new NewModifyCase();
            //var newModifyCase = await _newModifyCaseRepository.GetById(UnqueID);
            //var objModifyCase1 = new NewModifyCaseFew { IPDOPDId = newModifyCase.IPDOPDId };
            var newModifyCase = await _newModifyCaseRepository.GetByOPIPId(UnqueIDIPOP);
            if (newModifyCase != null)
            {
                nn.Home = newModifyCase.Home;
                nn.BedDetailsVisit = newModifyCase.BedDetailsVisit;
                nn.UnqueID = newModifyCase.UnqueID;
                nn.IPDOPDId = newModifyCase.IPDOPDId;
            }
            return Ok(nn);
        }

        [HttpGet]
        //[Route("getLabIndividualByCaseID/{caseID}/{testName}")] 
        [Route("getUniqueIdFromIPOP/{IPOPid}")]
        public async Task<ActionResult<NewModifyCase>> getUniqueIdFromIPOP(string IPOPid)
        {
            NewModifyCase nn = new NewModifyCase();
            nn.UnqueID = "NA";
            nn.IPDOPDId = "NA";
            try
            {
                var newModifyCase = await _newModifyCaseRepository.GetByOPIPId(IPOPid);
                //var objModifyCase1 = new NewModifyCaseFew { IPDOPDId = newModifyCase.IPDOPDId };
                //NewModifyCase nn = new NewModifyCase();
                if (newModifyCase != null)
                {
                    nn.UnqueID = newModifyCase.UnqueID;
                    nn.IPDOPDId = newModifyCase.IPDOPDId;
                }
            }
            catch (Exception ex)
            {

            }
            
            return Ok(nn);
        }

        [Route("[action]/{DateStart}")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NewModifyCase>>> GetByDateStart(string DateStart)
        {
            var newModifyCases = await _newModifyCaseRepository.GetAllCase(); 
            var available = from c in newModifyCases where c.CaseStatus != "SoftDelete" && c.DateStart == DateStart select c;


            //DateTime frmdt = Convert.ToDateTime("2020-11-27T18:30:00.000Z");
            //DateTime frmdt = Convert.ToDateTime(DateStart);
            //string frmdtString = frmdt.ToString("dd-MM-yyy");
            //Console.WriteLine(frmdtString);
            var availableFilter = newModifyCases.Where(c => c.DateStart.Trim() != string.Empty);
            //var totalAvailable = availableFilter.Where(c => Convert.ToDateTime(c.ReferralMaster.appointment).ToString("dd-MM-yyy") == frmdtString);
            var totalAvailable = availableFilter.Where(c => c.DateStart.Trim() == DateStart.Trim());
            //var availableCars = newModifyCases.Where(c => c.IsAvailable).ToArray();
            return Ok(totalAvailable); // newModifyCase.PatientDetails.;
        }

        //[HttpGet("{DateStart}")]
        //public async Task<ActionResult<NewModifyCase>> Get(string DateStart)
        //{
        //    var newModifyCase = await _newModifyCaseRepository.GetById(UnqueID);
        //    return Ok(newModifyCase);
        //}

        [HttpGet("{id}/{UnqueID}")]
        public async Task<string> GetName(int id, string UnqueID)
        {
            var newModifyCase = await _newModifyCaseRepository.GetById(UnqueID);
            return "PatientName"; // newModifyCase.PatientDetails.;
        }

        [Route("getstring/{id}")]
        [HttpGet]
        public ActionResult getstring(string id)
        {
           

            return Ok(id + " from API PatientName");
        }
        



        [HttpGet("{UnqueID}/{DateStart}")]
        public async Task<string> GetName(string UnqueID, string DateStart)
        {
            var newModifyCase = await _newModifyCaseRepository.GetByIdDateStartOnly(DateStart);
            return "PatientName"; // newModifyCase.PatientDetails.;
        }









        [HttpPost, Route("PostSimulatingError")]
        public IActionResult PostSimulatingError([FromBody] NewModifyCaseViewModel value)
        {
            var newModifyCase = new NewModifyCase(value);
            _newModifyCaseRepository.Add(newModifyCase);

            // The newModifyCase will not be added
            return BadRequest();
        }

        [HttpPost]
        public async Task<ActionResult<NewModifyCase>> Post([FromBody] NewModifyCaseViewModel value)
        {
            //var newModifyCases = await _newModifyCaseRepository.GetAllCase();
            var newModifyCaseObj = new NewModifyCase();
            long pIPD = _newModifyCaseRepository.GetOPIPVSCount(newModifyCaseObj, "IPD");
            long pOPD = _newModifyCaseRepository.GetOPIPVSCount(newModifyCaseObj, "OPD");
            long pVS = _newModifyCaseRepository.GetOPIPVSCount(newModifyCaseObj, "VS");

            string IPDrecordCountString = (pIPD == 0 ? 1 : pIPD + 1).ToString();
            string OPDrecordCountString = (pOPD == 0 ? 1 : pOPD + 1).ToString();
            string VSrecordCountString = (pVS == 0 ? 1 : pVS + 1).ToString();

            //string IPDrecordCountString = (newModifyCases.Where(a=>a.OPDkimbaIPD == "IPD") .Count() ==0? 1: newModifyCases.Where(a=>a.OPDkimbaIPD == "IPD") .Count()+1).ToString();
            //string OPDrecordCountString = (newModifyCases.Where(a => a.OPDkimbaIPD == "OPD").Count() == 0 ? 1 : newModifyCases.Where(a => a.OPDkimbaIPD == "OPD").Count() + 1).ToString();
            //string VSrecordCountString = (newModifyCases.Where(a => a.OPDkimbaIPD == "VS").Count() == 0 ? 1 : newModifyCases.Where(a => a.OPDkimbaIPD == "VS").Count() + 1).ToString();


            if (value.OPDkimbaIPD == "IPD")
            {

                if (IPDrecordCountString.Length == 0)
                {
                    IPDrecordCountString = "000" + IPDrecordCountString;
                }
                else if (IPDrecordCountString.Length == 1)
                {
                    IPDrecordCountString = "000" + IPDrecordCountString;
                }
                else if (IPDrecordCountString.Length == 2)
                {
                    IPDrecordCountString = "00" + IPDrecordCountString;
                }
                else if (IPDrecordCountString.Length == 3)
                {
                    IPDrecordCountString = "0" + IPDrecordCountString;
                }
                value.IPDOPDId = "IP" + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString().Substring(DateTime.Now.Year.ToString().Length - 2) + "-" + IPDrecordCountString;
            }
            else if (value.OPDkimbaIPD == "OPD")
            {
                if (OPDrecordCountString.Length == 0)
                {
                    OPDrecordCountString = "0000" + OPDrecordCountString;
                }
                else if (OPDrecordCountString.Length == 1)
                {
                    OPDrecordCountString = "000" + OPDrecordCountString;
                }
                else if (OPDrecordCountString.Length == 2)
                {
                    OPDrecordCountString = "00" + OPDrecordCountString;
                }
                else if (OPDrecordCountString.Length == 3)
                {
                    OPDrecordCountString = "0" + OPDrecordCountString;
                }
                value.IPDOPDId = "OP" + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString().Substring(DateTime.Now.Year.ToString().Length - 2) + "-" + OPDrecordCountString;
            }
            else if (value.OPDkimbaIPD == "VS")
            {
                if (VSrecordCountString.Length == 0)
                {
                    VSrecordCountString = "0000" + VSrecordCountString;
                }
                else if (VSrecordCountString.Length == 1)
                {
                    VSrecordCountString = "000" + VSrecordCountString;
                }
                else if (VSrecordCountString.Length == 2)
                {
                    VSrecordCountString = "00" + VSrecordCountString;
                }
                else if (VSrecordCountString.Length == 3)
                {
                    VSrecordCountString = "0" + VSrecordCountString;
                }
                value.IPDOPDId = "VS" + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString().Substring(DateTime.Now.Year.ToString().Length - 2) + "-" + VSrecordCountString;
            }
            var newModifyCase = new NewModifyCase(value);
            //foreach (var a in newModifyCase.DoctortoPatientCommentMedicineReDevelop)
            //{
            //    if (a.MedicineNames != "")
            //    {
            //        string subject = "Medicine request for CASE: " + newModifyCase.IPDOPDId;
            //        string body = "Hi " + newModifyCase.Home.AssignedPharma + "," + Environment.NewLine + "Request you to supply following medicines for Patient:" + newModifyCase.Home.FirstName + " " + newModifyCase.Home.LastName + " and the Case Id is: " + newModifyCase.IPDOPDId + Environment.NewLine + "Reffered Doctor is" + a.name + Environment.NewLine + "Medicine Names are as below and please provide with your consent" + Environment.NewLine + a.MedicineNames + Environment.NewLine + "Thanks," + Environment.NewLine + "NABA DIGANTA, Primary care hospital" + Environment.NewLine + "Contact No- 9439332561";
            //        //send the email to farmacy
            //        CommonUtility cu = new CommonUtility();
            //        cu.SendEmail("debasmitsamal@gmail.com", body, subject);

            //        a.MedicineNames = "";
            //    }
            //}
            foreach (var a in newModifyCase.DoctortoPatientCommentMedicineReDevelop)
            {
                if (a.medicines != null && a.medicines.Length > 0)
                {
                    string subject = "Medicine request for CASE: " + newModifyCase.IPDOPDId;
                    string body = "Hi " + newModifyCase.Home.AssignedPharma + "," + Environment.NewLine + "Request you to supply following medicines for Patient:" + newModifyCase.Home.FirstName + " " + newModifyCase.Home.LastName + " and the Case Id is: " + newModifyCase.IPDOPDId + Environment.NewLine + "Reffered Doctor is" + a.name + Environment.NewLine + "Medicine Names are as below and please provide with your consent" + Environment.NewLine + a.MedicineNames;
                    //send the email to farmacy
                    StringBuilder sbPresc = new StringBuilder();
                    sbPresc.Append("<table style='border:1px solid black;'>");
                    foreach (var pres in a.medicines)
                    {
                        sbPresc.Append("<tr style='border: 1px solid black;'>");
                        sbPresc.Append("<td>" + pres.IndMedicineName + "</td>");
                        sbPresc.Append("<td>" + pres.unit + "</td>");
                        sbPresc.Append("<td>" + pres.noOfTimes + "</td>");
                        sbPresc.Append("<td>" + pres.noOfDays + "</td>");
                        sbPresc.Append("<td>" + pres.whichTime + "</td>");
                        sbPresc.Append("</tr>");

                    }
                    sbPresc.Append(@"</table> <br/>" + Environment.NewLine + "Thanks & Regards," + Environment.NewLine + "<br/> " + _configuration["EmailSettings:Regards"] + Environment.NewLine + _configuration["EmailSettings:Contact"]);
                    CommonUtility cu = new CommonUtility();

                    //Open below line when Email facility is required for client
                    //cu.SendEmail("debasmitsamal@gmail.com", body + sbPresc.ToString(), subject);

                    a.MedicineNames = "";
                }
            }


            // var newModifyCase = new NewModifyCase(value.Notes, value.Name, value.UnqueID);
            _newModifyCaseRepository.Add(newModifyCase);

            // it will be null
            var testNewModifyCase = await _newModifyCaseRepository.GetById(newModifyCase.UnqueID);

            // If everything is ok then:
            await _uow.Commit();
            
            // The newModifyCase will be added only after commit
            testNewModifyCase = await _newModifyCaseRepository.GetById(newModifyCase.UnqueID);

            

            return Ok(testNewModifyCase);
        }

        [HttpPut("{UnqueID}")]
        public async Task<ActionResult<NewModifyCase>> Put(string UnqueID, [FromBody] NewModifyCaseViewModel value)
        {
            //var newModifyCase1 = await _newModifyCaseRepository.GetById(UnqueID);

            ////string aa = new CommonUtility(_newModifyCaseRepository, _uow).GetID(UnqueID).ToString();
            ////var a = _newModifyCaseRepository.GetById(value.UnqueID, value.UnqueID);
            //var newModifyCase = new NewModifyCase(newModifyCase1.Id, value.Notes, value.Name, value.UnqueID);

            //_newModifyCaseRepository.Update(newModifyCase, UnqueID);
            
            var newModifyCase = new NewModifyCase(UnqueID, value);

            foreach (var a in newModifyCase.DoctortoPatientCommentMedicineReDevelop)
            {
                if (a.medicines != null && a.medicines.Length > 0 )
                {
                    string subject = "Medicine request for CASE: " + newModifyCase.IPDOPDId;
                    string body = "Hi " + newModifyCase.Home.AssignedPharma + "," + Environment.NewLine + "Request you to supply following medicines for Patient:" + newModifyCase.Home.FirstName + " " + newModifyCase.Home.LastName + " and the Case Id is: " + newModifyCase.IPDOPDId + Environment.NewLine + "Reffered Doctor is" + a.name + Environment.NewLine + "Medicine Names are as below and please provide with your consent" + Environment.NewLine + a.MedicineNames ;
                    //send the email to farmacy
                    StringBuilder sbPresc = new StringBuilder();
                    sbPresc.Append("<table style='border:1px solid black;'>");
                    foreach (var pres in a.medicines)
                    {
                        sbPresc.Append("<tr style='border: 1px solid black;'>");
                             sbPresc.Append("<td>"+pres.IndMedicineName + "</td>");
                        sbPresc.Append("<td>" + pres.unit + "</td>");
                        sbPresc.Append("<td>" + pres.noOfTimes + "</td>");
                        sbPresc.Append("<td>" + pres.noOfDays + "</td>");
                        sbPresc.Append("<td>" + pres.whichTime + "</td>");
                        sbPresc.Append("</tr>");

                    }
                    sbPresc.Append(@"</table> <br/>" + Environment.NewLine + "Thanks & Regards," + Environment.NewLine + "<br/> "+ _configuration["EmailSettings:Regards"]  + Environment.NewLine + _configuration["EmailSettings:Contact"]);
                    CommonUtility cu = new CommonUtility();
                   //Open below line when client needs Email facility
                    //cu.SendEmail("debasmitsamal@gmail.com", body+ sbPresc.ToString(), subject);

                    a.MedicineNames = "";
                }
            }

            _newModifyCaseRepository.Update(newModifyCase, newModifyCase.UnqueID);
            var testNewModifyCase = newModifyCase;
            await _uow.Commit();
            

            return Ok(await _newModifyCaseRepository.GetById(value.UnqueID));
        }


        [HttpDelete("{UnqueID}/{status}")]
        public async Task<ActionResult> Delete(string UnqueID,string status)
        {
            //if want to delete permanenntly then uncomment below two line
            // _newModifyCaseRepository.Remove(UnqueID);
            //If everything is ok then:
            await _uow.Commit();

            // it won't be null
            var testNewModifyCase = await _newModifyCaseRepository.GetById(UnqueID);

            
            if(status == "SoftDelete")
            {
                testNewModifyCase.CaseStatus = "SoftDelete";
                //var newModifyCase = new NewModifyCase(UnqueID, testNewModifyCase);

                _newModifyCaseRepository.Update(testNewModifyCase, testNewModifyCase.UnqueID);

                await _uow.Commit();


            }


            //var newPaymentHistoryForDelete = await _paymentHistoryRepository.GetAll();
            //foreach (var T  in newPaymentHistoryForDelete.Where(a=> a.CaseID == UnqueID))
            //{
            //    _paymentHistoryRepository.Remove(T.UnqueID);
            //    await _uow.Commit();
            //}

            _paymentHistoryRepository.RemoveBasedOnCASEID(UnqueID);
            await _uow.Commit();

            //var newTestIndividualForDelete = await _labTestIndividualRepository.GetAll();
            //foreach (var T in newTestIndividualForDelete.Where(a => a.CaseID == UnqueID))
            //{
            //    _labTestIndividualRepository.Remove(T.UnqueID);
            //    await _uow.Commit();
            //}
            _labTestIndividualRepository.RemoveBasedOnCASEID(UnqueID);
            await _uow.Commit();

            //var newFarmacyDeliveryToPatientForDelete = await _farmacyDeliveryToPatient.GetAll();
            //foreach (var T in newFarmacyDeliveryToPatientForDelete.Where(a => a.CaseID == UnqueID))
            //{
            //    _farmacyDeliveryToPatient.Remove(T.UnqueID);
            //    await _uow.Commit();
            //}
            _farmacyDeliveryToPatient.RemoveBasedOnCASEID(UnqueID);
            await _uow.Commit();

            

            // not it must by null
            //testNewModifyCase = await _newModifyCaseRepository.GetById("153");

            return Ok();
        }
    }
}
