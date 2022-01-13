using MongoDB.GenericRepository.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoDB.GenericRepository.ViewModel
{
    public class LabTestMasterViewModel
    {
        public string UnqueID { get; set; }
        public bool ShouldCommit { get; set; } = true;
        public string DateStart { get; set; }
        public string CategoryName { get; set; }
        public string CategoryType { get; set; }
        public string ShortName { get; set; }
        public string TestName { get; set; }
        public string TestPrice { get; set; }
        public string Discount { get; set; }
        public string CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public List<Parameter> test { get; set; }
    }
}
