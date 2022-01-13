using System;

namespace MongoDB.GenericRepository.ViewModel
{
    public class ReferralMasterViewModel
    {
        public string UnqueID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ContactNumber { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
        public string Department { get; set; }
        public decimal fees { get; set; }
        public string Commission { get; set; }
        public decimal Discount { get; set; }
        public string Experience { get; set; }
        public string Degree { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Title { get; set; }
        public string DateStart { get; set; } = new DateTime().ToString("dd-MM-yyyy");
        public string DateEnd { get; set; }
        public string UserName { get; set; }
        public string StaffType { get; set; }
        public string StaffGender { get; set; }
        public bool ShouldCommit { get; set; } = true;
    }
}
