using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoDB.GenericRepository.ViewModel
{
    public class TimesheetViewModel
    {
        public string UnqueID { get; set; }
        public bool ShouldCommit { get; set; } = true;

        public decimal TimeSpent { get; set; }

        public string Date { get; set; }

        public string WorkingDate { get; set; }
        public string WorkerName { get; set; }
        public string WorkerEmail { get; set; }
        public string WorkerID { get; set; }
        public string WorkerJOBStatus { get; set; }

        public string WorkCategory { get; set; }
        public string WorkSubCategory { get; set; }

        public string Notes { get; set; }

        public string SubmitTo { get; set; }

        public string ApprovalStatus { get; set; }
        public string ApprovalReason { get; set; }
        public string Others { get; set; }
        public string BillingSatus { get; set; }
        public string LegalEntry { get; set; }
    }
}
