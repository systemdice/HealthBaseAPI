using MongoDB.GenericRepository.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoDB.GenericRepository.ViewModel
{
    public class NewModifyCaseViewModel
    {
        public string UnqueID { get; set; }
        public bool ShouldCommit { get; set; } = true;
        public string IPDOPDId { get; set; }
        public string CaseLife { get; set; }
        public string IPDOPDConversionStatus { get; set; }
        public string IPDOPDConversionFrom { get; set; }

        public string PatientID { get; set; }
        public string CaseStatus { get; set; }
        public string UserName { get; set; }
        public string UserRole { get; set; }
        public string Location { get; set; }
        public string ModifiedBy { get; set; }
        public string PaymentHistoryID { get; set; }

        public string DateStart { get; set; } = new DateTime().ToShortDateString();
        public Home Home { get; set; }
        //public HomeFew HomeFew { get; set; }
        public PaymentHistorySingle[] PaymentHistory { get; set; }
        public TestNameWithCase TestNameWithCase { get; set; }

        public BedandAdmissionHistorySingle[] BedandAdmissionHistory { get; set; }
        public int opdipd { get; set; }
        public string OPDkimbaIPD { get; set; }
        public OPD OPD { get; set; }
        public DoctortoPatientCommentMedicine DoctortoPatientCommentMedicine { get; set; }
        public DischargeNote DischargeNote { get; set; }
        public DailyExpense[] DailyExpense { get; set; }
        public PharmacyManualEntry[] PharmacyManualEntry { get; set; }
        public Nebulization[] Nebulization { get; set; }
        public DoctorVisit[] DoctorVisit { get; set; }
        public NurseVisit[] NurseVisit { get; set; }
        public DoctortoPatientCommentMedicineReDevelop[] DoctortoPatientCommentMedicineReDevelop { get; set; }
        public AmbulanceVist[] AmbulanceVisit { get; set; }
        public AmbulanceVist[] AmbulanceVist { get; set; }

        public BedDetailsVisit[] BedDetailsVisit { get; set; }
        public VaccineDetailsVisit[] VaccineDetailsVisit { get; set; }

        public OTDetails[] OTDetails { get; set; }

    }
}
