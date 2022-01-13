
using MongoDB.GenericRepository.Model;
using System;
using System.Collections.Generic;

namespace MongoDB.GenericRepository.ViewModel
{
    public class GroupTestsViewModel
    {
        public string UnqueID { get; set; }
        public bool ShouldCommit { get; set; } = true;
        public string DateStart { get; set; } = new DateTime().ToShortDateString();
        public string Status { get; set; }
        public string DateEnd { get; set; }
        public string GroupName { get; set; }
        public string Description { get; set; }
        public string Discount { get; set; }
        public string ActualPrice { get; set; }
        public string TotalPrice { get; set; }
        public string CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public List<Name> names { get; set; }
    }
}
