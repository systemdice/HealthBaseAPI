using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoDB.GenericRepository.ViewModel
{
    public class DeptInvestigationViewModel
    {
        public string UnqueID { get; set; }
        public string TestCode { get; set; }
        public string Name { get; set; }
        public string Fees { get; set; }
        public decimal RevenueshareAmountFor { get; set; }
        public string[] Gender { get; set; }
        public string[] TestBookingAllowed { get; set; }
        public string[] Testslinked { get; set; }
        public string Count { get; set; }
        public string Panels { get; set; }
        public string Remarks { get; set; }
        public string ReferenceRange { get; set; }
        public string[] Unit { get; set; }
        public string[] ParameterDetails { get; set; }
        public string DateStart { get; set; } = new DateTime().ToShortDateString();
        public Boolean Active { get; set; }
        public string EnteredBy { get; set; }
    }
}
