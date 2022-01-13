using MongoDB.GenericRepository.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoDB.GenericRepository.ViewModel
{
    public class BedManagementViewModel
    {
        public string UnqueID { get; set; }
        public bool ShouldCommit { get; set; } = true;
        public string DateStart { get; set; }
        public List<BedCategory> teachers { get; set; }
        public string CreatedBy { get; set; }
    }
}
