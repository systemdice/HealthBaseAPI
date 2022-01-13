using System;

namespace MongoDB.GenericRepository.ViewModel
{
    public class VaccinationsViewModel
    {
        public string UnqueID { get; set; }
        public bool ShouldCommit { get; set; } = true;
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
        public string ExpiryMonth { get; set; }
        public string ExpiryYear { get; set; }
        public string ExpiryStatus { get; set; }
        public string ExpiryDayRemaining { get; set; }
        public string MRP { get; set; }
        public string Amount { get; set; }
        public string Other { get; set; }

        public string hsn{ get; set; }
      public string mfg{ get; set; }
      public string free{ get; set; }
      public string discount{ get; set; }
      public string cgst{ get; set; }
      public string sgst{ get; set; }
      public string gst{ get; set; }

    }
}
