using System;

namespace MongoDB.GenericRepository.ViewModel
{
    public class CategoryMasterViewModel
    {
        public string UnqueID { get; set; }
        public string Order { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
        public string DateStart { get; set; } = new DateTime().ToShortDateString();
        public string DateEnd { get; set; }
        public bool ShouldCommit { get; set; } = true;
    }
}
