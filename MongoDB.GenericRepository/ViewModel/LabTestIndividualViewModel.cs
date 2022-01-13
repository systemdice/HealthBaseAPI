using MongoDB.GenericRepository.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoDB.GenericRepository.ViewModel
{
    public class LabTestIndividualViewModel
    {
        public string UnqueID { get; set; }
        public bool ShouldCommit { get; set; } = true;
        public string DateStart { get; set; }
        public string CaseID { get; set; }
        public string TestName { get; set; }
        public string CategoryName { get; set; }
        public string TestPrice { get; set; }
        public string CreatedBy { get; set; }
        public string UserName { get; set; }
        public string UserRole { get; set; }
        public string Location { get; set; }
        public string ModifiedBy { get; set; }
        public string ReportStatus { get; set; }
        public string BarCodeKey { get; set; }
        public string QRCodeKey { get; set; }
        public string ParentTest { get; set; }
        public string MoreDetails { get; set; }
        public List<GenralRefTest> test { get; set; }
    }
}
