using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoDB.GenericRepository.ViewModel
{
    public class LeaveMangementViewModel
    {
        public string UnqueID { get; set; }
        public bool ShouldCommit { get; set; } = true;

        public string DateStart { get; set; } = new DateTime().ToString("dd-MM-yyyy");
        public string name { get; set; }
        public string description { get; set; }
        public string BillingDate { get; set; } = new DateTime().ToString("dd-MM-yyyy");
        public string StaffType { get; set; }
        public string FirstName { get; set; }

        public string leaveId { get; set; }
        public string leaveReason { get; set; }
        public string dateFrom { get; set; }
        public string dateTo { get; set; }
        public string approved { get; set; }
        public string deniedReason { get; set; }
        public string status { get; set; }
        public string createdAt { get; set; }
        public string NofDays { get; set; }
        public string SingleDayLeave { get; set; }
        public string ReportingManager { get; set; }
        public string ReportingManagerEmail { get; set; }

    }
}
