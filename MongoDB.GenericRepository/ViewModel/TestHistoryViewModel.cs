using System;

namespace MongoDB.GenericRepository.ViewModel
{
    public class TestHistoryViewModel
    {
        public string UnqueID { get; set; }
        public string Description { get; set; }
        public string ParentTestType { get; set; }
        public string[] TestName { get; set; }
        public string CaseID { get; set; }
        public decimal Amount { get; set; }
        public string AllTestDone { get; set; }

        public string Status { get; set; }
        public string DateStart { get; set; } = new DateTime().ToShortDateString();
        public string DateEnd { get; set; }
        public bool ShouldCommit { get; set; } = true;
    }
}
