using Microsoft.AspNetCore.Mvc;
using MongoDB.GenericRepository.Interfaces;
using MongoDB.GenericRepository.Model;
using MongoDB.GenericRepository.ViewModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
//using System.Net.Mail;
using Microsoft.AspNetCore.Hosting;
using System.Net;
using System.Net.Mail;

namespace MongoDB.GenericRepository.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportRepository _reportRepository;
        private readonly IExpensesRepository _expenseRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ILeaveMangementRepository _leaveMangementRepository;
        private readonly INewModifyCaseRepository _newModifyCaseRepository;
        private readonly IFarmacyDeliveryToPatientRepository _farmacyDeliveryToPatientRepository;
        private readonly IEmailSender _emailSender;
        private readonly IUnitOfWork _uow;
        //IHostingEnvironment env = null;

        public ReportController(IEmailSender emailSender, IReportRepository reportRepository, IExpensesRepository expenseRepository,
                                 ICategoryRepository categoryRepository, INewModifyCaseRepository newModifyCaseRepository,
                                 ILeaveMangementRepository leaveMangementRepository,
                                 IFarmacyDeliveryToPatientRepository farmacyDeliveryToPatientRepository, IUnitOfWork uow)
        {
            _reportRepository = reportRepository;
            _expenseRepository = expenseRepository;
            _categoryRepository = categoryRepository;
            _leaveMangementRepository = leaveMangementRepository;
            _newModifyCaseRepository = newModifyCaseRepository;
            _farmacyDeliveryToPatientRepository = farmacyDeliveryToPatientRepository;
            _emailSender = emailSender;
            _uow = uow;
            //this.env = env;
        }

        [HttpGet]
        public async Task<ActionResult<List<PieChartModel>>> Get()
        {
            var reports = await _reportRepository.GetAll();
            var expense = await _expenseRepository.GetAll();
            var category = await _categoryRepository.GetAll();
            var results = (from p in expense where p.BusinessType=="Expense" || p.BusinessType==null 
                           join pi in category                            
                           on p.ExpenseCategory equals pi.CategoryName
                           group p by pi.CategoryName into groupData
                           select new PieChartModel
                           {
                               Category = groupData.Key,
                               Amount = groupData.Sum(x => x.ExpenseAmount)
                           }).ToList();
            
            return Ok(results);
        }
        [HttpGet]
        [Route("getExpCategoryWIse/{BussinessType}")]
        public async Task<ActionResult<List<PieChartModel>>> getExpCategoryWIse(string BussinessType)
        {
            var reports = await _reportRepository.GetAll();
            var expense = await _expenseRepository.GetAll();
            var category = await _categoryRepository.GetAll();
            var results = (from p in expense
                           where p.BusinessType == BussinessType
                           join pi in category
                           on p.ExpenseCategory equals pi.CategoryName
                           group p by pi.CategoryName into groupData
                           select new PieChartModel
                           {
                               Category = groupData.Key,
                               Amount = groupData.Sum(x => x.ExpenseAmount)
                           }).ToList();

            return Ok(results);
        }

        [HttpGet]
        [Route("getExpPerMonth/{BussinessType}")]
        public async Task<ActionResult<List<BarChartModel>>> getExpPerMonth(string BussinessType)
        {
            var reports = await _reportRepository.GetAll();
            var expense = await _expenseRepository.GetAll();
            var category = await _categoryRepository.GetAll();
            //var results = (from p in expense
            //               join pi in category on p.CategoryName equals pi.Name
            //               group p by pi.Name into groupData
            //               select new PieChartModel
            //               {
            //                   Category = groupData.Key,
            //                   Amount = groupData.Sum(x => x.ExpenseAmount)
            //               }).ToList();

            //return Ok(results);
            var expenses = (from exp in expense
                            where exp.BusinessType == BussinessType
                            group exp by new { month = Convert.ToDateTime(exp.Date).Month, year = Convert.ToDateTime(exp.Date).Year } into groupData
                            select new BarChartModel
                            {
                                Month = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(groupData.Key.month)+", "+ groupData.Key.year.ToString(),//groupData.Key.month.ToString(),
                                Year = groupData.Key.year.ToString(),//groupData.Key.month.ToString(),
                                Amount = groupData.Sum(x => x.ExpenseAmount)
                            }

                            )
                            .OrderBy(i => i.Year).ToList();

            var expenses1 = (from exp in expense.OrderBy(x => x.Date) //.OrderByDescending(x => x.Date)
                             where exp.BusinessType == BussinessType                            
                             group exp by new { month = Convert.ToDateTime(exp.Date).Month, year = Convert.ToDateTime(exp.Date).Year } into groupData
                            select new BarChartModel
                            {
                                //dt = DateTime.Parse(groupData.Key.dt),
                                Month = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(groupData.Key.month) + ", " + groupData.Key.year.ToString(),//groupData.Key.month.ToString(),
                                Year = groupData.Key.year.ToString(),//groupData.Key.month.ToString(),
                                Amount = groupData.Sum(x => x.ExpenseAmount)
                            }

                            )
                            //.OrderBy(i => i.dt)
                            .ToList();

            var prof = expense
    .GroupBy(l => l.BusinessType)
    .Select(cl => new ProfitLost
    {
        ProductName = cl.First().BusinessType,
        Quantity = cl.Count().ToString(),
        Price = cl.Sum(c => c.ExpenseAmount).ToString(),
    }).ToList();

            var expenses2 = (from exp in expense.OrderBy(x => x.Date) //.OrderByDescending(x => x.Date)
                             //where exp.BusinessType == BussinessType
                             group exp by new { month = Convert.ToDateTime(exp.Date).Month, year = Convert.ToDateTime(exp.Date).Year, businessType = exp.BusinessType } into groupData
                             select new BarChartModel
                             {
                                 //dt = DateTime.Parse(groupData.Key.dt),
                                 Month = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(groupData.Key.month) + ", " + groupData.Key.year.ToString(),//groupData.Key.month.ToString(),
                                 Year = groupData.Key.year.ToString(),//groupData.Key.month.ToString(),
                                 Amount = groupData.Sum(x => x.ExpenseAmount),
                                 BusinessType = groupData.Key.businessType
                             }

                            )
                            //.OrderBy(i => i.dt)
                            .ToList();



            return expenses1;
        }

        [HttpGet]
        [Route("getExpPerMonth/{BussinessType}/{FilterTypeMonth}/{FilterTypeYear}/{FilterTypeAll}")]
        public async Task<ActionResult<List<BarChartModel>>> getExpenseIncomeFilter(string BussinessType,string FilterTypeMonth, string FilterTypeYear, string FilterTypeAll)
        {
            var reports = await _reportRepository.GetAll();
            var expense = await _expenseRepository.GetAll();
            var category = await _categoryRepository.GetAll();
            dynamic expenses = null;
            if (FilterTypeAll == "Filter")
            {
                expenses = (from exp in expense//.OrderBy(x => x.Date) //.OrderByDescending(x => x.Date)
                                               //where exp.BusinessType == BussinessType
                            group exp by new { btype = exp.BusinessType } into groupData
                            select new BarChartModel
                            {
                                //dt = DateTime.Parse(groupData.Key.dt),
                                //Month = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(groupData.Key.month) + ", " + groupData.Key.year.ToString(),//groupData.Key.month.ToString(),
                                Year = groupData.Key.btype.ToString(),//groupData.Key.month.ToString(),
                                Amount = groupData.Sum(x => x.ExpenseAmount)
                            }
                            )
                            .ToList();
                
            }
            if(FilterTypeAll == "MonthYear")
            {
                var expensesdd = (from exp in expense//.OrderBy(x => x.Date) //.OrderByDescending(x => x.Date)
                                                     //where exp.BusinessType == BussinessType
                                  group exp by new { month = Convert.ToDateTime(exp.Date).Month, year = Convert.ToDateTime(exp.Date).Year, btype = exp.BusinessType } into groupData
                                  select new BarChartModel
                                  {
                                      //dt = DateTime.Parse(groupData.Key.dt),
                                      //Month = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(groupData.Key.month) + ", " + groupData.Key.year.ToString(),//groupData.Key.month.ToString(),
                                      Month = groupData.Key.month.ToString(),
                                      Year = groupData.Key.year.ToString(),//groupData.Key.month.ToString(),
                                      Amount = groupData.Sum(x => x.ExpenseAmount),
                                      BusinessType = groupData.Key.btype
                                  }
                           )
                           .ToList();

                expenses = expensesdd.Where(o => o.Year == FilterTypeYear && o.Month == FilterTypeMonth).ToList();
                var wantedPeople = from n in expensesdd
                               where n.Year == FilterTypeYear && n.Month == FilterTypeMonth
                                   select n;
            }

    //        var prof = expense
    //.GroupBy(l => l.BusinessType)
    //.Select(cl => new ProfitLost
    //{
    //    ProductName = cl.First().BusinessType,
    //    Quantity = cl.Count().ToString(),
    //    Price = cl.Sum(c => c.ExpenseAmount).ToString(),
    //}).ToList();

            //var expenses2 = (from exp in expense.OrderBy(x => x.Date) //.OrderByDescending(x => x.Date)
            //                                                          //where exp.BusinessType == BussinessType
            //                 group exp by new { month = Convert.ToDateTime(exp.Date).Month, year = Convert.ToDateTime(exp.Date).Year, businessType = exp.BusinessType } into groupData
            //                 select new BarChartModel
            //                 {
            //                     //dt = DateTime.Parse(groupData.Key.dt),
            //                     Month = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(groupData.Key.month) + ", " + groupData.Key.year.ToString(),//groupData.Key.month.ToString(),
            //                     Year = groupData.Key.year.ToString(),//groupData.Key.month.ToString(),
            //                     Amount = groupData.Sum(x => x.ExpenseAmount),
            //                     BusinessType = groupData.Key.businessType
            //                 }

            //                )
            //                //.OrderBy(i => i.dt)
            //                .ToList();



            return expenses;

        }

        #region "for health module"
        [HttpPost]
        [Route("email")]
        public async Task email()
        {
            await _emailSender.SendEmailAsync("debasmitsamal@gmail.com", "subject",
                         $"Enter email body here");
        }

        [HttpPost]
        [Route("send-email")]
        public void SendEmail([FromBody]JObject objData)
        {
            try
            {
                SmtpClient client = new SmtpClient("smtp.gmail.com");
                client.Port = 587;
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential("systemdice@gmail.com", "systemdice@1618");

                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress("systemdice@gmail.com");
                mailMessage.To.Add("debasmitsamal@gmail.com");
                mailMessage.Body = "body";
                mailMessage.Subject = "subject";
                client.Send(mailMessage);

                SmtpClient client1 = new SmtpClient("smtp.ionos.com");
                client1.Port = 587;
                client1.EnableSsl = true;
                client1.UseDefaultCredentials = false;
                client1.Credentials = new NetworkCredential("dsamal@systemdice.com", "Debasmit@1618");

                MailMessage mailMessage1 = new MailMessage();
                mailMessage1.From = new MailAddress("contacts@systemdice.com");
                mailMessage1.To.Add("debasmitsamal@gmail.com");
                mailMessage1.Body = "body";
                mailMessage1.Subject = "subject";
                client1.Send(mailMessage1);
            }
            catch (Exception ex)
            {

            }
            //SmtpClient client = new SmtpClient("smtp.gmail.com");
            //client.Port = 465;
            //client.UseDefaultCredentials = false;
            //client.Credentials = new NetworkCredential("systemdice@gmail.com", "systemdice@1618");

            //MailMessage mailMessage = new MailMessage();
            //mailMessage.From = new MailAddress("systemdice@gmail.com");
            //mailMessage.To.Add("debasmitsamal@gmail.com");
            //mailMessage.Body = "body";
            //mailMessage.Subject = "subject";
            //client.Send(mailMessage);
            // MimeMessage message = new MimeMessage();

            // MailboxAddress from = new MailboxAddress("Admin",
            // "serverdataconsumption@serverdata.com");
            // message.From.Add(from);

            // MailboxAddress to = new MailboxAddress("User",
            // "debasmitsamal@gmail.com");
            // message.To.Add(to);
            // MailboxAddress to1 = new MailboxAddress("User",
            //"daskumar@live.com");
            // message.To.Add(to1);
            // MailboxAddress to2 = new MailboxAddress("User",
            //"ddas@systemdice.com");
            // message.To.Add(to2);

            // message.Subject = "Space consumption warning!!";
            // BodyBuilder bodyBuilder = new BodyBuilder();
            // bodyBuilder.HtmlBody = "<h3>Hello SystemDICE!</h3> <br/> Thanks for using our product – we hope we were able to meet your expectations. Just to let you know that your subscription expired 01/20/2021 and you won’t be able to log in any more. <br/><br/> But it’s not too late! You can gain immediate access to your saved data and preferences by renewing . If you renew within the next seven days, you’ll also be able to take advantage of our product.<br/><br/> Thank you,<br/>IONOS";
            // //bodyBuilder.TextBody = "Hello World!";
            // //bodyBuilder.Attachments.Add(env.WebRootPath + "\\file.png");
            // message.Body = bodyBuilder.ToMessageBody();

            // SmtpClient client = new SmtpClient();
            // client.Connect("smtp.gmail.com", 465, true);
            // client.Authenticate("systemdice@gmail.com", "systemdice@1618");

            // client.Send(message);
            // client.Disconnect(true);
            // client.Dispose();
        }
        [HttpGet]
        [Route("getCaseCountPerMonth")]
        public async Task<ActionResult<List<BarChartModel>>> getCaseCountPerMonth()
        {
            //var reports = await _reportRepository.GetAll();
            //var expense = await _expenseRepository.GetAll();
            //var category = await _categoryRepository.GetAll();
            var caseDetails = await _newModifyCaseRepository.GetAllCase();
            //var results = (from p in expense
            //               join pi in category on p.CategoryName equals pi.Name
            //               group p by pi.Name into groupData
            //               select new PieChartModel
            //               {
            //                   Category = groupData.Key,
            //                   Amount = groupData.Sum(x => x.ExpenseAmount)
            //               }).ToList();

            //return Ok(results);
            //try
            //{
            //    var expenses = (from exp in caseDetails
            //                    group exp by new { month = Convert.ToDateTime(exp.DateStart).Month, year = Convert.ToDateTime(exp.DateStart).Year } into groupData
            //                    select new BarChartModel
            //                    {
            //                        Month = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(groupData.Key.month) + ", " + groupData.Key.year.ToString(),//groupData.Key.month.ToString(),
            //                        Year = groupData.Key.year.ToString(),//groupData.Key.month.ToString(),
            //                                                             //Amount = groupData.Sum(x => x.ExpenseAmount)
            //                                                             //Amount = groupData.Count()
            //                    }

            //                                )
            //                                .OrderBy(i => i.Year).ToList();
            //}
            //catch (Exception ex)
            //{
            //    string p = ex.Message;
               
            //}
            //try
            //{
            //    CultureInfo invariantCulture1 = CultureInfo.InvariantCulture;
            //    DateTime fromDateTime1 = DateTime.ParseExact("14-01-2021", "dd-MM-yyyy", invariantCulture1);
            //    var expensespp1 = (from exp in caseDetails
            //                    group exp by new { month = DateTime.ParseExact(exp.DateStart.Replace("-", "/"), "dd/MM/yyyy", invariantCulture1).Month, year = DateTime.ParseExact(exp.DateStart.Replace("-", "/"), "dd/MM/yyyy", invariantCulture1).Year } into groupData
            //                    select new BarChartModel
            //                    {
            //                        Month = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(groupData.Key.month) + ", " + groupData.Key.year.ToString(),//groupData.Key.month.ToString(),
            //                        Year = groupData.Key.year.ToString(),//groupData.Key.month.ToString(),
            //                                                             //Amount = groupData.Sum(x => x.ExpenseAmount)
            //                        Amount = groupData.Count()
            //                    }

            //                                )
            //                                .OrderBy(i => i.Year).ToList();
            //}
            //catch (Exception ex)
            //{
            //    string p = ex.Message;

            //}
            CultureInfo invariantCulture = CultureInfo.InvariantCulture;
            DateTime fromDateTime = DateTime.ParseExact("14-01-2021", "dd-MM-yyyy", invariantCulture);
            var expensespp = (from exp in caseDetails.Where(a => a.CaseStatus != "SoftDelete")
                              where exp.DateStart != ""
                              group exp by new { month = DateTime.ParseExact(exp.DateStart.Replace("-", "/"), "dd/MM/yyyy", invariantCulture).Month, year = DateTime.ParseExact(exp.DateStart.Replace("-", "/"), "dd/MM/yyyy", invariantCulture).Year } into groupData
                              select new BarChartModel
                              {
                                  Month = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(groupData.Key.month) + ", " + groupData.Key.year.ToString(),//groupData.Key.month.ToString(),
                                  Year = groupData.Key.year.ToString(),//groupData.Key.month.ToString(),
                                                                       //Amount = groupData.Sum(x => x.ExpenseAmount)
                                  Amount = groupData.Count()
                              }

                                            )
                                            .OrderBy(i => i.Year).ToList();


            //var expenses1 = (from exp in caseDetails.OrderBy(x => x.DateStart) //.OrderByDescending(x => x.Date)                             
            //                 group exp by new { month = Convert.ToDateTime(exp.DateStart).Month, year = Convert.ToDateTime(exp.DateStart).Year } into groupData
            //                 select new BarChartModel
            //                 {
            //                     //dt = DateTime.Parse(groupData.Key.dt),
            //                     Month = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(groupData.Key.month) + ", " + groupData.Key.year.ToString(),//groupData.Key.month.ToString(),
            //                     Year = groupData.Key.year.ToString(),//groupData.Key.month.ToString(),
            //                                                          //Amount = groupData.Sum(x => x.ExpenseAmount)
            //                     Amount = groupData.Count()
            //                 }

            //                )
            //                //.OrderBy(i => i.dt)
            //                .ToList();

    //        var prof = expense
    //.GroupBy(l => l.BusinessType)
    //.Select(cl => new ProfitLost
    //{
    //    ProductName = cl.First().BusinessType,
    //    Quantity = cl.Count().ToString(),
    //    Price = cl.Sum(c => c.ExpenseAmount).ToString(),
    //}).ToList();

            //try
            //{
            //    var expenses2 = (from exp in expense.OrderBy(x => x.Date) //.OrderByDescending(x => x.Date)
            //                                                              //where exp.BusinessType == BussinessType
            //                     group exp by new { month = DateTime.ParseExact(exp.Date.Replace("-", "/"), "dd/MM/yyyy", invariantCulture).Month, year = DateTime.ParseExact(exp.Date.Replace("-", "/"), "dd/MM/yyyy", invariantCulture).Year, businessType = exp.BusinessType } into groupData
            //                     select new BarChartModel
            //                     {
            //                         //dt = DateTime.Parse(groupData.Key.dt),
            //                         Month = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(groupData.Key.month) + ", " + groupData.Key.year.ToString(),//groupData.Key.month.ToString(),
            //                         Year = groupData.Key.year.ToString(),//groupData.Key.month.ToString(),
            //                         Amount = groupData.Sum(x => x.ExpenseAmount),
            //                         BusinessType = groupData.Key.businessType
            //                     }

            //                                )
            //                                //.OrderBy(i => i.dt)
            //                                .ToList();
            //}
            //catch (Exception ex)
            //{

            //}

            



            return expensespp;
        }

        [HttpGet]
        [Route("getExpenseIncomeFromTo/{dateFrom}/{dateTo}")]
        public async Task<ActionResult<List<ProfitLost>>> getExpenseIncomeFromTo(string dateFrom, string dateTo)
        {

            CultureInfo invariantCulture1 = CultureInfo.InvariantCulture;
            var StartDate = DateTime.ParseExact(dateFrom.Replace("-", "/"), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            var EndDate = DateTime.ParseExact(dateTo.Replace("-", "/"), "dd/MM/yyyy", CultureInfo.InvariantCulture);

            int dayLength = (EndDate - StartDate).Days + 1;

            var result = new string[dayLength];
            for (int i = 0; i < Convert.ToInt32(dayLength); i++)
            {
                //var r7 = DateTime.ParseExact(DateTime.Now.AddDays(-i).ToString("dd/MM/yyyy"), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                //string pp = DateTime.Now.AddDays(-i).ToString("dd/MM/yyyy").ToString();
                //string pp1 = DateTime.Now.AddDays(-i).ToString("dd-MM-yyyy").ToString();
                //result[i] = EndDate.AddDays(-i).ToString("yyyy-MM-dd").ToString();
                result[i] = EndDate.AddDays(-i).ToString("MM/dd/yyyy").ToString();

            }
            var expense = await _newModifyCaseRepository.GetAllExpenses1("", "Range", result);
            //var reports = await _reportRepository.GetAll();
            //var expense = await _expenseRepository.GetAllExpenses1();
            //var category = await _categoryRepository.GetAll();
            //var caseDetails = await _newModifyCaseRepository.GetAll();

            //CultureInfo invariantCulture1 = CultureInfo.InvariantCulture;
            //DateTime fromDateTime1 = DateTime.ParseExact(StartDate, "dd-MM-yyyy", invariantCulture1);
            //DateTime fromDateTime2 = DateTime.ParseExact(EndDate, "dd-MM-yyyy", invariantCulture1);

            //StartDate = fromDateTime1.ToString();
            //EndDate = fromDateTime2.ToString();

            //var appointmentNoShow = from a in expense
            //                        where Convert.ToDateTime(a.Date) >= Convert.ToDateTime(StartDate) && Convert.ToDateTime(a.Date) <= Convert.ToDateTime(EndDate)
            //                        group a by Convert.ToDateTime(a.Date) into groupRecords
            //                        select new ProfitLost
            //                        {
            //                            Price = groupRecords.First().Date,
            //                            ProductName = groupRecords.First().BusinessType,
            //                            Quantity = groupRecords.Count().ToString()
            //                        };

            //var appointmentNoShow1 = from a in expense
            //                        where Convert.ToDateTime(a.Date) >= Convert.ToDateTime(StartDate) && Convert.ToDateTime(a.Date) <= Convert.ToDateTime(EndDate)
            //                         group a by new
            //                         {
            //                             a.Date,
            //                             a.ExpenseCategory
            //                         } into gcs
            //                         select new ProfitLost
            //                         {
            //                             Price = gcs.Key.Date,
            //                             ProductName = gcs.Key.ExpenseCategory,
            //                             Quantity = gcs.Count().ToString()
            //                         };

            //CultureInfo invariantCulture1 = CultureInfo.InvariantCulture;
            //DateTime fromDateTime1 = DateTime.ParseExact("14-01-2021", "dd-MM-yyyy", invariantCulture1);
            

            var prof = expense
                .OrderByDescending(s => s.Date)
                .Where(a => Convert.ToDateTime(a.Date) >= Convert.ToDateTime(StartDate) && Convert.ToDateTime(a.Date) <= Convert.ToDateTime(EndDate))
    .GroupBy(l => new { l.Date, l.CategoryName })    
    .Select(cl => new ProfitLost
    {
        ProductName = cl.First().BusinessType,
        Quantity = cl.Count().ToString(),
        Price = cl.Sum(c => c.ExpenseAmount).ToString(),
        DateUse = cl.First().Date
    }).ToList();

           


            return prof;
        }

        [HttpGet]
        [Route("getAllLeaveFromTo/{StartDate}")]
        public async Task<ActionResult<List<ProfitLost>>> getAllLeaveFromTo(string StartDate)
        {

            //try
            //{
            //    CultureInfo invariantCulture11 = CultureInfo.InvariantCulture;
            //    DateTime startDate = DateTime.ParseExact(StartDate, "dd-MM-yyyy", invariantCulture11);
            //    DateTime endDate = DateTime.ParseExact(EndDate, "dd-MM-yyyy", invariantCulture11);
            //    //DateTime startDate = new DateTime(2011, 1, 1); 06-05-2021
            //    //DateTime endDate = new DateTime(2013, 1, 1);
            //    //DateTime inRange = new DateTime(2021, 5, 5);
            //    //DateTime outRange = new DateTime(2021, 7, 7);
            //    DateTime inRange = DateTime.ParseExact("06-05-2021", "dd-MM-yyyy", invariantCulture11);
            //    DateTime outRange = DateTime.ParseExact("09-05-2021", "dd-MM-yyyy", invariantCulture11);
            //    DateRange range = new DateRange(startDate, endDate);
            //    bool yes = range.WithInRange(inRange);
            //    bool no = range.WithInRange(outRange);
            //}
            //catch (Exception ex)
            //{

            //    throw;
            //}

            CultureInfo invariantCulture11 = CultureInfo.InvariantCulture;
            DateTime startDate = DateTime.ParseExact(StartDate, "dd-MM-yyyy", invariantCulture11);
            //DateTime endDate = DateTime.ParseExact(EndDate, "dd-MM-yyyy", invariantCulture11);
            //DateTime startDate = new DateTime(2011, 1, 1); 06-05-2021
            //DateTime endDate = new DateTime(2013, 1, 1);
            //DateTime inRange = new DateTime(2021, 5, 5);
            //DateTime outRange = new DateTime(2021, 7, 7);
            DateTime inRange = DateTime.ParseExact("06-05-2021", "dd-MM-yyyy", invariantCulture11);
            DateTime outRange = DateTime.ParseExact("09-05-2021", "dd-MM-yyyy", invariantCulture11);
           // DateRange range = new DateRange(startDate, endDate);
            //bool yes = range.WithInRange(inRange);
            //bool no = range.WithInRange(outRange);

            //var reports = await _reportRepository.GetAll();
            //var expense = await _leaveMangementRepository.GetAll();
            //var category = await _categoryRepository.GetAll();
            //var caseDetails = await _newModifyCaseRepository.GetAll();

            CultureInfo invariantCulture1 = CultureInfo.InvariantCulture;
            DateTime fromDateTime1 = DateTime.ParseExact(StartDate, "dd-MM-yyyy", invariantCulture1);
            //DateTime fromDateTime2 = DateTime.ParseExact(EndDate, "dd-MM-yyyy", invariantCulture1);

            StartDate = fromDateTime1.ToString();
            //EndDate = fromDateTime2.ToString();
            var leaves = await _leaveMangementRepository.GetAll();
            List<ProfitLost> prof=null;// = null;
            //DateRange range = new DateRange(DateTime.ParseExact(dateFrom, "dd-MM-yyyy", invariantCulture11), DateTime.ParseExact(dateTo, "dd-MM-yyyy", invariantCulture11));
            try
            {
                
                var prof1 = leaves
                                .OrderByDescending(s => s.DateStart)
                                .Where(a => new DateRange(DateTime.ParseExact(a.dateFrom, "yyyy-MM-dd", invariantCulture11), DateTime.ParseExact(a.dateTo, "yyyy-MM-dd", invariantCulture11)).WithInRange(startDate) == true)
                    //.GroupBy(l => new { l.DateStart, l.FirstName })
                    .Select(cl => new ProfitLost
                    {
                        ProductName = cl.FirstName,
                        CreditStatus = cl.DateStart,
                        Price = cl.SingleDayLeave.ToString(),
                        DateUse = DateTime.ParseExact(cl.dateFrom, "yyyy-MM-dd", invariantCulture11) .ToString("dd/MM/yyyy") + " to " + DateTime.ParseExact(cl.dateTo, "yyyy-MM-dd", invariantCulture11).ToString("dd/MM/yyyy")
                    }).ToList();

                prof = prof1;
            }
            catch (Exception ex)
            {

                //hrow;
            }
            




            return prof;
        }


        [HttpGet]
        [Route("getAllLeave")]
        public async Task<ActionResult<List<ProfitLost>>> getAllLeave()
        {

            CultureInfo invariantCulture11 = CultureInfo.InvariantCulture;

            var leaves = await _leaveMangementRepository.GetAll();
            List<ProfitLost> prof = null;// = null;
            //DateRange range = new DateRange(DateTime.ParseExact(dateFrom, "dd-MM-yyyy", invariantCulture11), DateTime.ParseExact(dateTo, "dd-MM-yyyy", invariantCulture11));
            try
            {

                var prof1 = leaves
                                .OrderByDescending(s => s.DateStart)                                
                    .Select(cl => new ProfitLost
                    {
                        ProductName = cl.FirstName,
                        CreditStatus = cl.DateStart,
                        Price = cl.SingleDayLeave.ToString(),
                        DateUse = DateTime.ParseExact(cl.dateFrom, "yyyy-MM-dd", invariantCulture11).ToString("dd/MM/yyyy") + " to " + DateTime.ParseExact(cl.dateTo, "yyyy-MM-dd", invariantCulture11).ToString("dd/MM/yyyy")
                    }).ToList();

                prof = prof1;
            }
            catch (Exception ex)
            {

                //hrow;
            }





            return prof;
        }


        [HttpGet]
        [Route("getAllExpenseIncomeFromTo/{StartDate}/{EndDate}")]
        public async Task<ActionResult<List<ProfitLost>>> getAllExpenseIncomeFromTo(string StartDate,string EndDate)
        {
            //var reports = await _reportRepository.GetAll();
            var expense = await _expenseRepository.GetAll();
            //var category = await _categoryRepository.GetAll();
            //var caseDetails = await _newModifyCaseRepository.GetAll();

            CultureInfo invariantCulture1 = CultureInfo.InvariantCulture;
            DateTime fromDateTime1 = DateTime.ParseExact(StartDate, "dd-MM-yyyy", invariantCulture1);
            DateTime fromDateTime2 = DateTime.ParseExact(EndDate, "dd-MM-yyyy", invariantCulture1);

            StartDate = fromDateTime1.ToString();
            EndDate = fromDateTime2.ToString();

            
            

            var prof = expense
                .OrderByDescending(s => s.Date)
                //.OrderByDescending(s => DateTime.ParseExact(s.Date, "dd-MM-yyyy", invariantCulture1))
                .Where(a => Convert.ToDateTime(a.Date) >= Convert.ToDateTime(StartDate) && Convert.ToDateTime(a.Date) <= Convert.ToDateTime(EndDate))      
    .Select(cl => new ProfitLost
    {
        ProductName = cl.BusinessType,
        Quantity = cl.ExpenseCategory.ToString(),
        Price = cl.ExpenseAmount.ToString(),
        DateUse = cl.Date
    }).ToList();

           


            return prof;
        }


        [HttpGet]
        [Route("getBillForPharmacy/{StartDate}/{EndDate}/{username}")]
        public async Task<ActionResult<List<ProfitLost>>> getBillForPharmacy(string StartDate, string EndDate,string username)
        {
            //var reports = await _reportRepository.GetAll();
            //var expense = await _expenseRepository.GetAll();
            //var category = await _categoryRepository.GetAll();
            //var caseDetails = await _newModifyCaseRepository.GetAll();
            var farmacytoPatient = await _farmacyDeliveryToPatientRepository.GetAllPharmaFilter(username,"");

            CultureInfo invariantCulture1 = CultureInfo.InvariantCulture;
            DateTime fromDateTime1 = DateTime.ParseExact(StartDate, "dd-MM-yyyy", invariantCulture1);
            DateTime fromDateTime2 = DateTime.ParseExact(EndDate, "dd-MM-yyyy", invariantCulture1);

            StartDate = fromDateTime1.ToString();
            EndDate = fromDateTime2.ToString();

            //var appointmentNoShow = from a in expense
            //                        where  DateTime.ParseExact(StartDate, "dd-MM-yyyy", invariantCulture1) >= Convert.ToDateTime(StartDate) && Convert.ToDateTime(a.Date) <= Convert.ToDateTime(EndDate)
            //                        group a by Convert.ToDateTime(a.Date) into groupRecords
            //                        select new ProfitLost
            //                        {
            //                            Price = groupRecords.First().Date,
            //                            ProductName = groupRecords.First().BusinessType,
            //                            Quantity = groupRecords.Count().ToString()
            //                        };

            //var appointmentNoShow1 = from a in expense
            //                         where Convert.ToDateTime(a.Date) >= Convert.ToDateTime(StartDate) && Convert.ToDateTime(a.Date) <= Convert.ToDateTime(EndDate)
            //                         group a by new
            //                         {
            //                             a.Date,
            //                             a.ExpenseCategory
            //                         } into gcs
            //                         select new ProfitLost
            //                         {
            //                             Price = gcs.Key.Date,
            //                             ProductName = gcs.Key.ExpenseCategory,
            //                             Quantity = gcs.Count().ToString()
            //                         };

            //CultureInfo invariantCulture1 = CultureInfo.InvariantCulture;
            try
            {
                DateTime date1 = DateTime.ParseExact("2021-04-27T19:09:07.242Z".Substring(0, "2021-04-27T19:09:07.242Z".IndexOf("T")), "yyyy-MM-dd", invariantCulture1);  //DateTime.ParseExact("2021-04-15T14:53:57.572Z", "yyyy -MM-dd", invariantCulture1);
                DateTime date2 = DateTime.ParseExact("2021-04-15T14:53:57.572Z", "dd/MM/yyyy", null);
            }
            catch (Exception ex)
            {

                //throw;
            }
            DateTime fromDateTime11 = DateTime.ParseExact("14-01-2021", "dd-MM-yyyy", invariantCulture1);
            
            IEnumerable<ProfitLost> prof;
            try
            {

                prof = farmacytoPatient
                .OrderByDescending(s => DateTime.ParseExact(s.BillingDate.Substring(0, s.BillingDate.IndexOf("T")), "yyyy-MM-dd", invariantCulture1))
                .Where(a => a.PharmacyStoreName== username && DateTime.ParseExact(a.BillingDate.Substring(0, a.BillingDate.IndexOf("T")), "yyyy-MM-dd", invariantCulture1) >= Convert.ToDateTime(StartDate) && DateTime.ParseExact(a.BillingDate.Substring(0, a.BillingDate.IndexOf("T")), "yyyy-MM-dd", invariantCulture1) <= Convert.ToDateTime(EndDate))
                //.OrderByDescending(s => Convert.ToDateTime(s.DateStart))
                //.Where(a => Convert.ToDateTime(a.DateStart) >= Convert.ToDateTime(StartDate) && Convert.ToDateTime(a.DateStart) <= Convert.ToDateTime(EndDate))
    .GroupBy(l => new { l.BilledBy,l.CustomerName , l.BillingDate })
    .Select(cl => new ProfitLost
    {
        ProductName = cl.First().PharmacyStoreName,
        Quantity = cl.Count().ToString(),
        //Price = cl.Sum(c =>  Convert.ToInt32(c.teachers.total)).ToString(),
        //Price = cl.Sum(u => u.teachers.Sum(o => Convert.ToDecimal(o.total))).ToString(),
        Price = cl.Sum(u => u.teachers.Sum(r =>
        {
            return r.total != null ? Convert.ToDecimal(r.total) : 0;
        })    ).ToString(),
        //Price = cl.Sum(c => Convert.ToInt32(c.teachers.total)).ToString(),
        DateUse = cl.First().BillingDate,
        CustomerName = cl.First().CustomerName,
        Billed = cl.First().CaseID,
        CustomerIPDOPDID = cl.First().UnqueID,
        CreditStatus = cl.First().CeditStatus,
        BillPaidStatus = cl.First().DeliveredHospital //this need to be change to paid status

    }).ToList();


    //            prof = farmacytoPatient
    //            .OrderByDescending(s => s.DateStart)
    //            .Where(a => Convert.ToDateTime(a.DateStart) >= Convert.ToDateTime(StartDate) && Convert.ToDateTime(a.DateStart) <= Convert.ToDateTime(EndDate))
    //.GroupBy(l => new { l.BilledBy })
    //.Select(cl => new ProfitLost
    //{
    //    ProductName = cl.First().BilledBy,
    //    Quantity = cl.Count().ToString(),
    //    Price = cl.Sum(c => Convert.ToInt32( c.teachers.First().total)).ToString(),
    //    DateUse = cl.First().DateStart
    //}).ToList();
            }
            catch (Exception ex)
            {

                throw;
            }




            return prof.ToList();
        }

        #endregion
        //
        [HttpGet("{UnqueID}")]
        public async Task<ActionResult<Report>> Get(string UnqueID)
        {
            var report = await _reportRepository.GetById(UnqueID);
            return Ok(report);
        }
        [Route("[action]/{DateStart}")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Report>>> GetByDateStart(string DateStart)
        {
            var reports = await _reportRepository.GetAll();
            var available = from c in reports where c.DateStart == DateStart select c;


            //DateTime frmdt = Convert.ToDateTime("2020-11-27T18:30:00.000Z");
            //DateTime frmdt = Convert.ToDateTime(DateStart);
            //string frmdtString = frmdt.ToString("dd-MM-yyy");
            //Console.WriteLine(frmdtString);
            var availableFilter = reports.Where(c => c.DateStart != null);
            //var totalAvailable = availableFilter.Where(c => Convert.ToDateTime(c.ReferralMaster.appointment).ToString("dd-MM-yyy") == frmdtString);
            var totalAvailable = availableFilter.Where(c => c.DateStart.Trim() == DateStart.Trim());
            //var availableCars = reports.Where(c => c.IsAvailable).ToArray();
            return Ok(totalAvailable); // report.PatientDetails.;
        }

        //[HttpGet("{DateStart}")]
        //public async Task<ActionResult<Report>> Get(string DateStart)
        //{
        //    var report = await _reportRepository.GetById(UnqueID);
        //    return Ok(report);
        //}

        [HttpGet("{id}/{UnqueID}")]
        public async Task<string> GetName(int id, string UnqueID)
        {
            var report = await _reportRepository.GetById(UnqueID);
            return "PatientName"; // report.PatientDetails.;
        }


        [HttpGet("{UnqueID}/{DateStart}")]
        public async Task<string> GetName(string UnqueID, string DateStart)
        {
            var report = await _reportRepository.GetByIdDateStartOnly(DateStart);
            return "PatientName"; // report.PatientDetails.;
        }









        [HttpPost, Route("PostSimulatingError")]
        public IActionResult PostSimulatingError([FromBody] ReportViewModel value)
        {
            var report = new Report(value);
            _reportRepository.Add(report);

            // The report will not be added
            return BadRequest();
        }

        [HttpPost]
        public async Task<ActionResult<Report>> Post([FromBody] ReportViewModel value)
        {

            var report = new Report(value);
            // var report = new Report(value.Notes, value.Name, value.UnqueID);
            _reportRepository.Add(report);

            // it will be null
            var testReport = await _reportRepository.GetById(report.UnqueID);

            // If everything is ok then:
            await _uow.Commit();

            // The report will be added only after commit
            testReport = await _reportRepository.GetById(report.UnqueID);

            return Ok(testReport);
        }

        [HttpPut("{UnqueID}")]
        public async Task<ActionResult<Report>> Put(string UnqueID, [FromBody] ReportViewModel value)
        {
            //var report1 = await _reportRepository.GetById(UnqueID);

            ////string aa = new CommonUtility(_reportRepository, _uow).GetID(UnqueID).ToString();
            ////var a = _reportRepository.GetById(value.UnqueID, value.UnqueID);
            //var report = new Report(report1.Id, value.Notes, value.Name, value.UnqueID);

            //_reportRepository.Update(report, UnqueID);
            var report = new Report(UnqueID, value);

            _reportRepository.Update(report, report.UnqueID);

            await _uow.Commit();

            return Ok(await _reportRepository.GetById(value.UnqueID));
        }

        [HttpDelete("{UnqueID}")]
        public async Task<ActionResult> Delete(string UnqueID)
        {
            _reportRepository.Remove(UnqueID);

            // it won't be null
            var testReport = await _reportRepository.GetById(UnqueID);

            // If everything is ok then:
            await _uow.Commit();

            // not it must by null
            //testReport = await _reportRepository.GetById("153");

            return Ok();
        }
    }

    public interface IRange
    {
        DateTime Start { get; }
        DateTime End { get; }
        bool WithInRange(DateTime value);
        bool WithInRange(IRange range);
    }

    public class DateRange : IRange
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public DateRange(DateTime start, DateTime end)
        {
            Start = start;
            End = end;
        }

        public bool WithInRange(DateTime value)
        {
            return (Start <= value) && (value <= End);
        }

        public bool WithInRange(IRange range)
        {
            return (Start <= range.Start) && (range.End <= End);
        }
    }
}
