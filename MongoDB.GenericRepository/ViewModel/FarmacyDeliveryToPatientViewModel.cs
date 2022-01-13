using MongoDB.GenericRepository.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoDB.GenericRepository.ViewModel
{
    public class FarmacyDeliveryToPatientViewModel
    {
        public string UnqueID { get; set; }
        public bool ShouldCommit { get; set; } = true;

        public string DateStart { get; set; } = new DateTime().ToString("dd-MM-yyyy");
        public string Patientid { get; set; }
        public string CaseID { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string BillingDate { get; set; } = new DateTime().ToString("dd-MM-yyyy");
        public string BilledBy { get; set; }
        public string CustomerName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string priorityStatus { get; set; }
        public string bedDetails { get; set; }
        public string refferDoctor { get; set; }
        public string DoctorPercentage { get; set; }
        public string department { get; set; }
        public string extra { get; set; }
        public List<Teacher> teachers { get; set; }

        public string PharmacyStoreName { get; set; }
        public string DeliveredTo { get; set; }
        public string DeliveredHospital { get; set; }
        public string ModeOfDespach { get; set; }
        public string BillNo { get; set; }
        public string PaymentMode { get; set; }
        public string PaymentAmount { get; set; }
        public string CeditStatus { get; set; }
        public string IPDOPDId { get; set; }
        public string PaymentStatus { get; set; }

        public string GrossSalePriceOnthisBill { get; set; }
        public string GrossPurchasePriceOnthisBill { get; set; }
        public string GrossProffitPriceOnthisBill { get; set; }
        public string GrossGSTPriceOnthisBill { get; set; }
        public string GrossCGSTPriceOnthisBill { get; set; }
        public string GrossSGSTPriceOnthisBill { get; set; }
        public string BillingMonth { get; set; }
        public string BillingYear { get; set; }


    }
}
