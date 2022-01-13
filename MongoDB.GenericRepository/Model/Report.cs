using MongoDB.GenericRepository.Utility;
using MongoDB.GenericRepository.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoDB.GenericRepository.Model
{
    public class Report
    {
        public Report(ReportViewModel UR)
        {
            Id = Guid.NewGuid();
            UnqueID = CommonMethods.GetUniqueIDShorter(); //new Random().Next(10, 500).ToString();
            DateStart = DateTime.Now.ToString("dd-MM-yyyy"); // UR.DateStart;
            PieChartModel = UR.PieChartModel;
            BarChartModel = UR.BarChartModel;
        }

        public Report(string updateUniqueaID, ReportViewModel UR)
        {
            //Id = id;
            UnqueID = updateUniqueaID;
            DateStart = DateTime.Now.ToString("dd-MM-yyyy");  // UR.DateStart;
            PieChartModel = UR.PieChartModel;
            BarChartModel = UR.BarChartModel;
        }
        //[BsonId]
        //[BsonIgnoreIfDefault]
        public Guid Id { get; set; }
        public string UnqueID { get; set; }

        public string DateStart { get; set; } = new DateTime().ToShortDateString();
        public PieChartModel PieChartModel { get; set; }
        public BarChartModel BarChartModel { get; set; }
    }

    public class PieChartModel
    {
        public string Category { get; set; }
        public decimal? Amount { get; set; }
    }

    public class BarChartModel
    {
        public string Month { get; set; }
        public decimal? Amount { get; set; }
        public string Year { get; set; }

        public DateTime dt { get; set; }
        public string BusinessType { get; set; }
    }

    public class OPDIPDModel
    {
        public string Month { get; set; }
        public decimal? Amount { get; set; }
        public int RecordCount { get; set; }
        public string BillingMonth { get; set; }
        public string BillingYear { get; set; }

        public DateTime dt { get; set; }
        public string OPDIPDType { get; set; }
        public string OPDCount { get; set; }
        public string IPDCount { get; set; }
        public string VSCount { get; set; }
    }

    public class ProfitLost
    {

        public ProfitLost() { }
        public string DateUse { get; set; }
        public string ProductName { get; set; }
        public string Price { get; set; }
        public string Quantity { get; set; }
        public string Billed { get; set; }
        public string CustomerName { get; set; }
        public string CustomerIPDOPDID { get; set; }
        public string CreditStatus { get; set; }
        public string BillPaidStatus { get; set; }
    }
}
