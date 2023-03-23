using System;

namespace MongoDB.GenericRepository.ViewModel
{
    public class PaymentHistoryViewModel
    {
        public string UnqueID { get; set; }
        public string Description { get; set; }
        public string CaseID { get; set; }
        
        public string Status { get; set; }
        public string DateStart { get; set; } = new DateTime().ToShortDateString();

        public string Type { get; set; }

        public double Amount { get; set; }
        public double Discount { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal Balance { get; set; }
        public string CollectionChargePaid { get; set; }
        public string ReceivedBy { get; set; }
        public string CenterName { get; set; }
        public string OPDCharge { get; set; }
        public string DoctorCharge { get; set; }
        public string NurseCharge { get; set; }
        public string LabTestCharge { get; set; }
        public string FarmaCharge { get; set; }
        public string BedCharge { get; set; }
        public string OTCharge { get; set; }
        public string OtherCharge { get; set; }
        public string RegdCharge { get; set; }
        public string EarlierPayment { get; set; }
        public string VaccinationCharge { get; set; }
        public string PharmacyManualEntryCharge { get; set; }
        public string NebulizationCharge { get; set; }
        public bool ShouldCommit { get; set; } = true;
    }
}
