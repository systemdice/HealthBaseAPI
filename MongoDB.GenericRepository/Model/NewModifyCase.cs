
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.GenericRepository.Utility;
using MongoDB.GenericRepository.ViewModel;
using System;
using System.Collections.Generic;

namespace MongoDB.GenericRepository.Model
{
    public class NewModifyCase
    {
        public NewModifyCase()
        {

        }
        public NewModifyCase(NewModifyCaseViewModel UR)
        {
            Id = Guid.NewGuid();
            UnqueID = CommonMethods.GetUniqueIDShorter();//new Random().Next(10, 500).ToString();
            DateStart = CommonMethods.CreateISTTimezone(); // DateTime.Now.ToString("dd-MM-yyyy"); // UR.DateStart;
            OPDkimbaIPD = UR.OPDkimbaIPD;
            IPDOPDId = UR.IPDOPDId;
            CaseLife = UR.CaseLife;
            IPDOPDConversionStatus = UR.IPDOPDConversionStatus;
            IPDOPDConversionFrom = UR.IPDOPDConversionFrom;

            PatientID = UR.PatientID;
            PaymentHistoryID = UR.PatientID;
            CaseStatus = UR.CaseStatus;
            UserName = UR.UserName;
            UserRole = UR.UserRole;
            Location = UR.Location;
            ModifiedBy = UR.ModifiedBy;
            //DateStart = UR.PatientID; 
            Home = UR.Home;
            //HomeFew = UR.HomeFew;
            PaymentHistory = UR.PaymentHistory;
            TestNameWithCase = UR.TestNameWithCase;
            BedandAdmissionHistory = UR.BedandAdmissionHistory;
            opdipd = UR.opdipd;
            OPD =UR.OPD;
            DailyExpense = UR.DailyExpense;
            Nebulization = UR.Nebulization;
            DoctorVisit = UR.DoctorVisit;
            NurseVisit = UR.NurseVisit;            
            DoctortoPatientCommentMedicineReDevelop = UR.DoctortoPatientCommentMedicineReDevelop;
            AmbulanceVisit = UR.AmbulanceVisit;
            BedDetailsVisit = UR.BedDetailsVisit;
            VaccineDetailsVisit = UR.VaccineDetailsVisit;
            OTDetails = UR.OTDetails;
            DoctortoPatientCommentMedicine = UR.DoctortoPatientCommentMedicine;
            DischargeNote = UR.DischargeNote;
            Nebulization = UR.Nebulization;
            PharmacyManualEntry = UR.PharmacyManualEntry;
            
            
        }

        public NewModifyCase(string updateUniqueaID, NewModifyCaseViewModel UR)
        {
            //Id = id;
            UnqueID = updateUniqueaID;
            DateStart = UR.DateStart;// DateTime.Now.ToString("dd-MM-yyyy"); // UR.DateStart;
            OPDkimbaIPD = UR.OPDkimbaIPD;
            IPDOPDId = UR.IPDOPDId;
            CaseLife = UR.CaseLife;
            IPDOPDConversionStatus = UR.IPDOPDConversionStatus;
            IPDOPDConversionFrom = UR.IPDOPDConversionFrom;

            PatientID = UR.PatientID;
            PaymentHistoryID = UR.PatientID;
            //DateStart = UR.PatientID;
            CaseStatus = UR.CaseStatus;
            UserName = UR.UserName;
            UserRole = UR.UserRole;
            Location = UR.Location;
            ModifiedBy = UR.ModifiedBy;
            Home = UR.Home;
            //HomeFew = UR.HomeFew;
            PaymentHistory = UR.PaymentHistory;
            TestNameWithCase = UR.TestNameWithCase;
            BedandAdmissionHistory = UR.BedandAdmissionHistory;
            opdipd = UR.opdipd;
            OPD = UR.OPD;
            DailyExpense = UR.DailyExpense;
            PharmacyManualEntry = UR.PharmacyManualEntry;
            Nebulization = UR.Nebulization;
            DoctorVisit = UR.DoctorVisit;
            NurseVisit = UR.NurseVisit;
        DoctortoPatientCommentMedicineReDevelop = UR.DoctortoPatientCommentMedicineReDevelop;
            AmbulanceVisit = UR.AmbulanceVisit;
            BedDetailsVisit = UR.BedDetailsVisit;
            VaccineDetailsVisit = UR.VaccineDetailsVisit;
            OTDetails = UR.OTDetails;
            DoctortoPatientCommentMedicine = UR.DoctortoPatientCommentMedicine;
            DischargeNote = UR.DischargeNote;
            Nebulization = UR.Nebulization;
            PharmacyManualEntry = UR.PharmacyManualEntry;

        }
        //[BsonId]
        //[BsonIgnoreIfDefault]
        public Guid Id { get; set; }
        public string UnqueID { get; set; }
        public string IPDOPDId { get; set; }
        public string PatientID { get; set; }
        public string PaymentHistoryID { get; set; }
        public string CaseLife { get; set; }
        public string IPDOPDConversionStatus { get; set; }
        public string IPDOPDConversionFrom { get; set; }
        public string CaseStatus { get; set; }
        public string UserName { get; set; }
        public string UserRole { get; set; }
        public string Location { get; set; }
        public string ModifiedBy { get; set; }
        public string DateStart { get; set; } = new DateTime().ToShortDateString();
    public Home Home { get; set; }
        //public HomeFew HomeFew { get; set; }
    public PaymentHistorySingle[] PaymentHistory { get; set; }
        public TestNameWithCase TestNameWithCase { get; set; }
        public BedandAdmissionHistorySingle[] BedandAdmissionHistory { get; set; }
        public int opdipd { get; set; }
        public string OPDkimbaIPD { get; set; }
    public OPD OPD { get; set; }
        public DailyExpense[] DailyExpense { get; set; }
        public PharmacyManualEntry[] PharmacyManualEntry { get; set; }
        public Nebulization[] Nebulization { get; set; }
        public DoctorVisit[] DoctorVisit { get; set; }
        public NurseVisit[] NurseVisit { get; set; }
        public DoctortoPatientCommentMedicineReDevelop[] DoctortoPatientCommentMedicineReDevelop { get; set; }
        public AmbulanceVist[] AmbulanceVisit { get; set; }
        public BedDetailsVisit[] BedDetailsVisit { get; set; }
        public VaccineDetailsVisit[] VaccineDetailsVisit { get; set; }
        public OTDetails[] OTDetails { get; set; }
        public DoctortoPatientCommentMedicine DoctortoPatientCommentMedicine { get; set; }
        public DischargeNote DischargeNote { get; set; }


    }
    public class NewModifyCaseFew
    {
        public string UnqueID { get; set; }
        public string IPDOPDId { get; set; }
    }
        public class OPDChoice
    {
        public string name { get; set; }
        public bool val { get; set; }
    }

    public class OPD
    {
        public string title { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string DoctorFeedback { get; set; }
        public bool converttoIPD { get; set; }
        public OPDChoice[] OPDChoice { get; set; }
    }
    public class DoctortoPatientCommentMedicineReDevelop
    {
      public string name { get; set; }
      public string ExpenseDescription { get; set; }
      public string Amount { get; set; }
      public string expDate { get; set; }
        public string DoctorComments { get; set; }
        public string DoctorCommentsHL { get; set; }
        public string MedicineNames { get; set; }
        public string MedicineNamesHL { get; set; }
        public string PatientHistory { get; set; }
        public string PatientFinding { get; set; }
        public string PatientDiagnosis { get; set; }
        public string PatientPathoLabTestAdvice { get; set; }
        public Medicines[] medicines { get; set; }
    }
    public class DoctortoPatientCommentMedicine
    {
        public string DoctorComments { get; set; }
        public string DoctorCommentsHL { get; set; }
        public string MedicineNames { get; set; }        
        public string MedicineNamesHL { get; set; }
        public string PatientHistory { get; set; }
        public string PatientFinding { get; set; }
        public string PatientDiagnosis { get; set; }
        public string PatientPathoLabTestAdvice { get; set; }
    }
    public class Medicines
    {

        public string IndMedicineName { get; set; }
        public string unit { get; set; }
        public string noOfTimes { get; set; }
        public string noOfDays { get; set; }
        public string whichTime { get; set; }
    }
    public class DischargeNote
    {
        public string AdviceNote { get; set; }
        public string FinalDiagnosis { get; set; }
        public string Referal { get; set; }
        public string DischargeType { get; set; }
        public string Other { get; set; }

    }
    public class DailyExpense
    {        
        public string name { get; set; }
        public string ExpenseDescription { get; set; }
        public string Amount { get; set; }
        public string expDate { get; set; }
    }
    public class PharmacyManualEntry
    {
        public string name { get; set; }
        public string ExpenseDescription { get; set; }
        public string Amount { get; set; }
        public string expDate { get; set; }
    }
    public class Nebulization
    {
        public string name { get; set; }
        public string ExpenseDescription { get; set; }
        public string Amount { get; set; }
        public string expDate { get; set; }
    }
    public class DoctorVisit
    {
        public string name { get; set; }
        public string ExpenseDescription { get; set; }
        public string Amount { get; set; }
        public string expDate { get; set; }
    }
    public class NurseVisit
    {

        public string name { get; set; }
        public string ExpenseDescription { get; set; }
        public string Amount { get; set; }
        public string expDate { get; set; }
         public string  task1 { get; set; }
         public string  task2 { get; set; }
         public string  task3 { get; set; }
         public string  task4 { get; set; }
         public string  task5 { get; set; }
         public string  task6 { get; set; }
         public string  task7 { get; set; }
         public string  task8 { get; set; }    
         public string  task9 { get; set; }
         public string task10 { get; set; }
        public string drug { get; set; }
        public string dose { get; set; }
        public string frequency { get; set; }
        public string applytime { get; set; }
        public string others { get; set; }
    }

    public class AmbulanceVist
    {
        
        public string name { get; set; }
        public string ExpenseDescription { get; set; }
        public string Amount { get; set; }
        public string expDate { get; set; }
        public string perkm { get; set; }
      public string totalkm { get; set; }
      public string total { get; set; }
      public string drivername { get; set; }
       
    }
    public class BedDetailsVisit
    {
      public string  BedCategory { get; set; }
      public string  BedName { get; set; }
        public string  StartDate { get; set; }
      public string EndDate { get; set; }
       public string BedPrice { get; set; }
       public string BedDescription { get; set; }
        public string NofDays { get; set; }
       public string AdmittedBy { get; set; }
       public string ShiftingReason { get; set; }
       public string BedCurrentStatus { get; set; }
        public string BedForceRelease { get; set; }
        public string Other { get; set; }
        
    }
    public class VaccineDetailsVisit
    {
        public string VaccineCategory { get; set; }
        public string BrandName { get; set; }
        public string CompanyName { get; set; }
        public string VaccineName { get; set; }
        public string Quantity { get; set; }
        public string Unit { get; set; }
        public string BatchNumber { get; set; }
        public string Expiry { get; set; }
        public string MRP { get; set; }
        public string Amount { get; set; }
        public string StartDate { get; set; }
        public string NextVaccineDate { get; set; }
        public string VaccinePrice { get; set; }
        public string VaccineDescription { get; set; }
        public string NofDays { get; set; }
        public string DoctorAssigned { get; set; }
        public string DoctorPrice { get; set; }
        public string NurseAssigned { get; set; }
        public string NursePrice { get; set; }
        public string CurrentStatus { get; set; }
        public string TotalPrice { get; set; }
        public string Other { get; set; }



    }
    public class PaymentHistorySingle
    {
        public string UnqueID { get; set; }
        public string Description { get; set; }
        public string CaseID { get; set; }
        public string Name { get; set; }

        public string Status { get; set; }
        public string DateStart { get; set; } = DateTime.Now.ToString("dd-MM-yyyy");

        public string Time { get; set; }
        public string Type { get; set; }

        public double Amount { get; set; }
        public double Discount { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal Balance { get; set; }
        public string EarlierPayment { get; set; }

        public string CollectionChargePaid { get; set; }

        public string ReceivedBy { get; set; }
        public string CenterName { get; set; }
        public string DateEnd { get; set; }
        public string OPDCharge{ get; set; }
        public string DoctorCharge { get; set; }
        public string NurseCharge { get; set; }
        public string LabTestCharge{ get; set; }
    public string FarmaCharge { get; set; }
        public string BedCharge { get; set; }
        public string DailyExpense { get; set; }
        public string OTCharge { get; set; }
        public string OtherCharge { get; set; }
        public string RegdCharge{ get; set; }
        public string VaccinationCharge  { get; set; }
        public string PharmacyManualEntryCharge { get; set; }
        public string NebulizationCharge { get; set; }
        public bool ShouldCommit { get; set; } = true;
    }
    public class PaymentHistorySingleFew
    {
        

        public double Amount { get; set; }       
        public decimal PaidAmount { get; set; }
        public decimal Balance { get; set; }
        
        public string ReceivedBy { get; set; }
       
    }
    public class BedandAdmissionHistorySingle
    {
        public string UnqueID { get; set; }
        public string Description { get; set; }
        public string BedNumber { get; set; }
        public string BedPrice { get; set; }
        public string Upgrade { get; set; } = "No";
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Nofdays { get; set; }
        public string TotalPrice { get; set; }
        public string DoctorName { get; set; }
        public string SupportStaff { get; set; }
        public string DoctorComments { get; set; }
        public string OperationRequired { get; set; }
        public string OperationDescription { get; set; }

    }
    public class Home
    {
        public object UnqueID { get; set; }
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
        public string DOB { get; set; }
        public string Relationship { get; set; }
        public string Status { get; set; }
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
    public class HomeFew
    {
        public object UnqueID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Year { get; set; } = "0";
        public string Month { get; set; } = "0";
        public string Days { get; set; } = "0";
        public string Gender { get; set; }
        
        public string ContactNumber { get; set; }
        public string AssignedPharma { get; set; }

    }

    public class OTDetails
    {
        public string UnqueID { get; set; }
        public string OperationName { get; set; }
        public string Date { get; set; }
        public string OperationType { get; set; }
        public string ConsultationDoctor { get; set; }
        public string AsstDoctor1 { get; set; }
         public string AsstDoctor2 { get; set; }
         public string Nurse1 { get; set; }
        public string Nurse2 { get; set; }
         public string Helper1 { get; set; }
        public string Helper2 { get; set; }
        public string AnastheticPerson1 { get; set; }
        public string AnastheticType1 { get; set; }
         public string AnastheticPerson2 { get; set; }
         public string AnastheticType2 { get; set; }
         public string OTTechnician1 { get; set; }
        public string OTTechnician2 { get; set; }
         public string OTTechnicianAsst1 { get; set; }
         public string OTTechnicianAsst2 { get; set; }
         public string Result { get; set; }
        public string  Remarks { get; set; }
        public string  TotalPrice { get; set; }
        public string OperationMajorMinor { get; set; }
        public string OperationNote { get; set; }
    }
}

