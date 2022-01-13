using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.GenericRepository.Utility;
using MongoDB.GenericRepository.ViewModel;
using System;

namespace MongoDB.GenericRepository.Model
{
    public class Vaccinations
    {
        public Vaccinations(VaccinationsViewModel UR)
        {
            Id = Guid.NewGuid();
            UnqueID = CommonMethods.GetUniqueIDShorter();//new Random().Next(10, 500).ToString();
            VSID = UR.VSID;
            SlNO = UR.SlNO;
            Description = UR.Description;
            Name = UR.Name;
            Status = UR.Status;
            DateStart = DateTime.Now.ToString("dd-MM-yyyy"); 
            UpdateDate = DateTime.Now.ToString("dd-MM-yyyy"); 
            DateEnd = UR.DateEnd;
            AddUpdateBy = UR.AddUpdateBy;

            VaccineCategory = UR.VaccineCategory;
            BrandName = UR.BrandName;
            CompanyName = UR.CompanyName;
            VaccineName = UR.VaccineName;
            Quantity = UR.Quantity;
            Unit = UR.Unit;
            BatchNumber = UR.BatchNumber;
            Expiry = UR.Expiry;
            ExpiryMonth = UR.ExpiryMonth;
            ExpiryYear = UR.ExpiryYear;
            ExpiryStatus = UR.ExpiryStatus;
            ExpiryDayRemaining = UR.ExpiryDayRemaining;
            MRP = UR.MRP;
            Amount = UR.Amount;
            Other = UR.Other;
            hsn = UR.hsn;
            mfg = UR.mfg;
            free = UR.free;
            discount = UR.discount;
            cgst = UR.cgst;
            sgst = UR.sgst;
            gst = UR.gst;
        }

        public Vaccinations(string updateUniqueaID, VaccinationsViewModel UR)
        {
            //Id = id;
            UnqueID = updateUniqueaID;
            SlNO = UR.SlNO;
            VSID = UR.VSID;
            Description = UR.Description;
            Name = UR.Name;
            Status = UR.Status;
            DateStart = UR.DateStart;
            UpdateDate = DateTime.Now.ToString("dd-MM-yyyy"); //UR.UpdateDate;
            DateEnd = UR.DateEnd;
            AddUpdateBy = UR.AddUpdateBy;

            VaccineCategory = UR.VaccineCategory;
            BrandName = UR.BrandName;
            CompanyName = UR.CompanyName;
            VaccineName = UR.VaccineName;
            Quantity = UR.Quantity;
            Unit = UR.Unit;
            BatchNumber = UR.BatchNumber;
            Expiry = UR.Expiry;
            ExpiryMonth = UR.ExpiryMonth;
            ExpiryYear = UR.ExpiryYear;
            ExpiryStatus = UR.ExpiryStatus;
            ExpiryDayRemaining = UR.ExpiryDayRemaining;
            MRP = UR.MRP;
            Amount = UR.Amount;
            Other = UR.Other;
        hsn  = UR.hsn;
        mfg  = UR.mfg;
            free = UR.free;
            discount = UR.discount;
            cgst = UR.cgst;
            sgst = UR.sgst;
            gst = UR.gst;
        }
        //[BsonId]
        //[BsonIgnoreIfDefault]
        public Guid Id { get; set; }
        public string UnqueID { get; set; }
        public string VSID { get; set; }
        public string SlNO { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public string DateStart { get; set; } = new DateTime().ToShortDateString();
        public string UpdateDate { get; set; }
        public string DateEnd { get; set; }
        public string AddUpdateBy { get; set; }

        public string VaccineCategory { get; set; }
        public string BrandName { get; set; }
        public string CompanyName { get; set; }
        public string VaccineName { get; set; }
        public string Quantity { get; set; }
        public string Unit { get; set; }
        public string BatchNumber { get; set; }
        public string Expiry { get; set; }
        public string ExpiryMonth  { get; set; }
    public string ExpiryYear { get; set; }
        public string ExpiryStatus { get; set; }
        public string ExpiryDayRemaining { get; set; }
        public string MRP { get; set; }
        public string Amount { get; set; }
        public string Other { get; set; }
        public string hsn { get; set; }
        public string mfg { get; set; }
        public string free { get; set; }
        public string discount { get; set; }
        public string cgst { get; set; }
        public string sgst { get; set; }
        public string gst { get; set; }
    }
}
