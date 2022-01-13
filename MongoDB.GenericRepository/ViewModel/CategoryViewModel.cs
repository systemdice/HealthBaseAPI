using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoDB.GenericRepository.ViewModel
{
    public class CategoryViewModel
    {
        public string UnqueID { get; set; }
        public bool ShouldCommit { get; set; } = true;

        public string Notes { get; set; }

        public string CategoryName { get; set; }
        public string CategoryType { get; set; }
        public string CategoryStatus { get; set; }

        public string Date { get; set; }
    }
}
