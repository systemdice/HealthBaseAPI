using System;

namespace MongoDB.GenericRepository.ViewModel
{
    public class PatientDetailsViewModel
    {
        public string UnqueID { get; set; }
        public string Title { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Year { get; set; }
        public string Month { get; set; }
        public string Days { get; set; }

        public string Gender { get; set; }
        public string Email { get; set; }
        public string ContactNumber { get; set; }
        public string Address { get; set; }
        public string Status { get; set; }

       public string AppointmentID{ get; set; }
       public string CaseID{ get; set; }
       public string BedID{ get; set; }
       public string TestID{ get; set; }
       public string NewID{ get; set; }
       public string OtherID{ get; set; }

        public string AssignDoctor { get; set; }

        public string Doctorfees { get; set; }
        public string HospitalDiscount { get; set; }
        public string GrandTotal { get; set; }
        public string TreatmentContinue { get; set; }

        public string DOB { get; set; }
        public string Relationship { get; set; }
        public string Pregnancy { get; set; }
        public string PatientCategory { get; set; }
        public string RefferDoctorName { get; set; }
        public string PermananetAddress { get; set; }
        public string OfficeAddress { get; set; }
        public string MaritalStatus { get; set; }
        public string CO { get; set; }
        public string Religion { get; set; }
        public string Occupation { get; set; }
        public string BloodGroup { get; set; }
        public string Allergy { get; set; }
        public string AssignedPharma { get; set; }
        public string AssignedDept { get; set; }
        public string Height { get; set; }
        public string Weight { get; set; }
        public string Temperature { get; set; }
        public string RespiratoryRate { get; set; }
        public string RhType { get; set; }
        public string BPReading { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string AdvPayment { get; set; }
        public bool ShouldCommit { get; set; } = true;
    }
}
