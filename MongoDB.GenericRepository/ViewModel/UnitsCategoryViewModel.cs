using System;

namespace MongoDB.GenericRepository.ViewModel
{
    public class UnitsCategoryViewModel
    {
        public string Description { get; set; }
        public string Name { get; set; }
        public string UnqueID { get; set; }

        public bool Status { get; set; } = false;

        public bool ShouldCommit { get; set; } = true;

        public string DateStart { get; set; } = new DateTime().ToShortDateString();
        public string DateEnd { get; set; }
    }
}
