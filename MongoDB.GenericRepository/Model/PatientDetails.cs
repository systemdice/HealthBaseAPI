using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.GenericRepository.ViewModel;
using System;
using MongoDB.GenericRepository.Utility;

namespace MongoDB.GenericRepository.Model
{
    public class PatientDetails
    {
        public PatientDetails(PatientDetailsViewModel UR)
        {
            Id = Guid.NewGuid();
            UnqueID = CommonMethods.GetUniqueIDShorter();// Random().Next(10, 500).ToString();
            Title = UR.Title;
            UserName = UR.UserName;
            FirstName = UR.FirstName;
            LastName = UR.LastName;
            Year = UR.Year;
            Month = UR.Month;
            Days = UR.Days;

            Gender = UR.Gender;
            Email = UR.Email;
            ContactNumber = UR.ContactNumber;
            Address = UR.Address;
            Status = UR.Status;
            AppointmentID= UR.AppointmentID;
            CaseID= UR.CaseID;
            BedID= UR.BedID;
            TestID= UR.TestID;
            NewID= UR.NewID;
            OtherID= UR.OtherID;
            Doctorfees = UR.Doctorfees;
            HospitalDiscount = UR.HospitalDiscount;
            GrandTotal = UR.GrandTotal;
            TreatmentContinue = UR.TreatmentContinue;
            AssignDoctor = UR.AssignDoctor;
            DOB = UR.DOB;
            Relationship = UR.Relationship;
            Pregnancy = UR.Pregnancy;
            PatientCategory = UR.PatientCategory;
            RefferDoctorName = UR.RefferDoctorName;
            PermananetAddress = UR.PermananetAddress;
            OfficeAddress = UR.OfficeAddress;
            MaritalStatus = UR.MaritalStatus;
            CO = UR.CO;
            Religion = UR.Religion;
            Occupation = UR.Occupation;
            BloodGroup = UR.BloodGroup;
            Allergy = UR.Allergy;
            AssignedPharma = UR.AssignedPharma;
            AssignedDept = UR.AssignedDept;
            Height = UR.Height;
            Weight = UR.Weight;
            Temperature = UR.Temperature;
            RespiratoryRate = UR.RespiratoryRate;
            RhType = UR.RhType;
            BPReading = UR.BPReading;
            FatherName = UR.FatherName;
            MotherName = UR.MotherName;
            AdvPayment = UR.AdvPayment;
        }

        public PatientDetails(string updateUniqueaID, PatientDetailsViewModel UR)
        {
            //Id = id;
            UnqueID = updateUniqueaID;
            Title = UR.Title;
            UserName = UR.UserName;
            FirstName = UR.FirstName;
            LastName = UR.LastName;
            Year = UR.Year;
            Month = UR.Month;
            Days = UR.Days;

            Gender = UR.Gender;
            Email = UR.Email;
            ContactNumber = UR.ContactNumber;
            Address = UR.Address;
            Status = UR.Status;
            AppointmentID = UR.AppointmentID;
            CaseID = UR.CaseID;
            BedID = UR.BedID;
            TestID = UR.TestID;
            NewID = UR.NewID;
            OtherID = UR.OtherID;

            Doctorfees = UR.Doctorfees;
            HospitalDiscount = UR.HospitalDiscount;
            GrandTotal = UR.GrandTotal;
            TreatmentContinue = UR.TreatmentContinue;
            AssignDoctor = UR.AssignDoctor;
            DOB = UR.DOB;
            Relationship = UR.Relationship;
            Pregnancy = UR.Pregnancy;
            PatientCategory = UR.PatientCategory;
            RefferDoctorName = UR.RefferDoctorName;
            PermananetAddress = UR.PermananetAddress;
            OfficeAddress = UR.OfficeAddress;
            MaritalStatus = UR.MaritalStatus;
            CO = UR.CO;
            Religion = UR.Religion;
            Occupation = UR.Occupation;
            BloodGroup = UR.BloodGroup;
            Allergy = UR.Allergy;
            Height = UR.Height;
            Weight = UR.Weight;
            Temperature = UR.Temperature;
            RespiratoryRate = UR.RespiratoryRate;
            RhType = UR.RhType;
            BPReading = UR.BPReading;
            FatherName = UR.FatherName;
            MotherName = UR.MotherName;
            UR.AdvPayment = UR.AdvPayment;
    }
    
    //[BsonId]
    //[BsonIgnoreIfDefault]
    public Guid Id { get; set; }
        public string UnqueID { get; set; }
        public string Title { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Year { get; set; } = "0";
        public string Month { get; set; } = "0";
        public string Days { get; set; } = "0";

        public string Gender { get; set; }
        public string Email { get; set; }
        public string ContactNumber { get; set; }
        public string Address { get; set; }
        public string Status { get; set; }

        public string AppointmentID { get; set; }
        public string CaseID { get; set; }
        public string BedID { get; set; }
        public string TestID { get; set; }
        public string NewID { get; set; }
        public string OtherID { get; set; }

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

    }

    public class SinglePatientDetails
    {       
        public string Title { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int Days { get; set; }

        public string Gender { get; set; }
        public string Email { get; set; }
        public string ContactNumber { get; set; }
        public string Address { get; set; }
        public string Status { get; set; }
        public string AppointmentID { get; set; }
        public string CaseID { get; set; }
        public string BedID { get; set; }
        public string TestID { get; set; }
        public string NewID { get; set; }
        public string OtherID { get; set; }

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

    }
}
