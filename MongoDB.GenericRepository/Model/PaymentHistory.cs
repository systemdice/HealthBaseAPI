using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.GenericRepository.Utility;
using MongoDB.GenericRepository.ViewModel;
using System;

namespace MongoDB.GenericRepository.Model
{
    public class PaymentHistory
    {
        public PaymentHistory(PaymentHistoryViewModel UR)
        {
            Id = Guid.NewGuid();
            UnqueID = CommonMethods.GetUniqueIDShorter();//new Random().Next(10, 500).ToString();
            Description = UR.Description;
            CaseID = UR.CaseID;
        Status = UR.Status;
        DateStart  = DateTime.Now.ToString("dd-MM-yyyy hh:mm"); //UR.DateStart;
        Type  = UR.Type;
        Amount = UR.Amount;
            Discount = UR.Discount;
            PaidAmount = UR.PaidAmount;
            Balance = UR.Balance;
            CollectionChargePaid  = UR.CollectionChargePaid;
        ReceivedBy  = UR.ReceivedBy;
        CenterName  = UR.CenterName;
        OPDCharge = UR.OPDCharge;
            DoctorCharge = UR.DoctorCharge;
            NurseCharge = UR.NurseCharge;
            LabTestCharge = UR.LabTestCharge;
            FarmaCharge = UR.FarmaCharge;
            BedCharge = UR.BedCharge;
            OTCharge = UR.OTCharge;
            OtherCharge = UR.OtherCharge;
            RegdCharge = UR.RegdCharge;
            EarlierPayment = UR.EarlierPayment;
            VaccinationCharge = UR.VaccinationCharge;
            PharmacyManualEntryCharge = UR.VaccinationCharge;
            NebulizationCharge = UR.VaccinationCharge;
        }

        public PaymentHistory(string updateUniqueaID, PaymentHistoryViewModel UR)
        {
            //Id = id;
            UnqueID = updateUniqueaID;
            Description = UR.Description;
            CaseID = UR.CaseID;
            Status = UR.Status;
            DateStart = DateTime.Now.ToString("dd-MM-yyyy hh:mm"); //UR.DateStart;
            Type = UR.Type;
            Amount = UR.Amount;
            Discount = UR.Discount;
            PaidAmount = UR.PaidAmount;
            Balance = UR.Balance;
            CollectionChargePaid = UR.CollectionChargePaid;
            ReceivedBy = UR.ReceivedBy;
            CenterName = UR.CenterName;
            OPDCharge = UR.OPDCharge;
            DoctorCharge = UR.DoctorCharge;
            NurseCharge = UR.NurseCharge;
            LabTestCharge = UR.LabTestCharge;
            FarmaCharge = UR.FarmaCharge;
            BedCharge = UR.BedCharge;
            OTCharge = UR.OTCharge;
            OtherCharge = UR.OtherCharge;
            RegdCharge = UR.RegdCharge;
            EarlierPayment = UR.EarlierPayment;
            VaccinationCharge = UR.VaccinationCharge;
            PharmacyManualEntryCharge = UR.VaccinationCharge;
            NebulizationCharge = UR.VaccinationCharge;
        }
        //[BsonId]
        //[BsonIgnoreIfDefault]
        public Guid Id { get; set; }
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
    }
}
