using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoDB.GenericRepository.Model
{
    public class NotifyData
    {
    }
    public class NotifyPendingTestData
    {
        
        public string CaseID { get; set; }
        public string TestName { get; set; }
        public string AssignedTo { get; set; }
        public string RefferDoctorName { get; set; }
        public string CollectionCenter { get; set; }
        public string DateStart { get; set; }
        public string ParentTest { get; set; }
        public string ProgressStatus { get; set; }
        public string Location { get; set; }
    }
    public class BeddReport
    {
        public string UnqueID { get; set; }
        public string CaseID { get; set; }
        public string BedCategory { get; set; }
        public string BedName { get; set; }
        public string PatientName { get; set; }
        public string OPDIPDid { get; set; }
        public string DateStart { get; set; }
    }
    public class returnSingleData
    {
        public string NexttoBeIDNameetc { get; set; }
    }
}
