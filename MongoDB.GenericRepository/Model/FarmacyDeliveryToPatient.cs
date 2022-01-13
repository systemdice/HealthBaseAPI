
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.GenericRepository.Utility;
using MongoDB.GenericRepository.ViewModel;
using System;
using System.Collections.Generic;

namespace MongoDB.GenericRepository.Model
{
    public class FarmacyDeliveryToPatient
    {
        public FarmacyDeliveryToPatient()
        {
        }
          public FarmacyDeliveryToPatient(FarmacyDeliveryToPatientViewModel UR)
        {
            Id = Guid.NewGuid();
            UnqueID = CommonMethods.GetUniqueIDShorter(); //new Random().Next(10, 500).ToString();
            DateStart = DateTime.Now.ToString("dd-MM-yyyy");  // UR.DateStart;
            Patientid = UR.Patientid;
            CaseID = UR.CaseID;
            name = UR.name;
            description = UR.description;
            BillingDate = UR.BillingDate;
            BilledBy = UR.BilledBy;
            CustomerName = UR.CustomerName;
            Address = UR.Address;
            PhoneNumber = UR.PhoneNumber;
            priorityStatus = UR.priorityStatus;
            bedDetails = UR.bedDetails;
            refferDoctor = UR.refferDoctor;
            DoctorPercentage = UR.DoctorPercentage;
            department = UR.department;
            extra = UR.extra;
            teachers = UR.teachers;
            PharmacyStoreName = UR.PharmacyStoreName;
            DeliveredTo = UR.DeliveredTo;
            DeliveredHospital = UR.DeliveredHospital;
            ModeOfDespach = UR.ModeOfDespach;
            BillNo = UR.BillNo;
            PaymentMode = UR.PaymentMode;
            PaymentAmount = UR.PaymentAmount;
            CeditStatus = UR.CeditStatus;
            IPDOPDId = UR.IPDOPDId;
            PaymentStatus = UR.PaymentStatus;
            GrossPurchasePriceOnthisBill = UR.GrossPurchasePriceOnthisBill;
            GrossSalePriceOnthisBill = UR.GrossSalePriceOnthisBill;
            GrossProffitPriceOnthisBill = UR.GrossProffitPriceOnthisBill;
            GrossGSTPriceOnthisBill = UR.GrossGSTPriceOnthisBill;
            GrossCGSTPriceOnthisBill = UR.GrossCGSTPriceOnthisBill;
            GrossSGSTPriceOnthisBill = UR.GrossSGSTPriceOnthisBill;
            BillingMonth = UR.BillingMonth;
            BillingYear = UR.BillingYear;

        }

        public FarmacyDeliveryToPatient(string updateUniqueaID, FarmacyDeliveryToPatientViewModel UR)
        {
            //Id = id;
            UnqueID = updateUniqueaID;
            DateStart = DateTime.Now.ToString("dd-MM-yyyy");  // UR.DateStart;
            Patientid = UR.Patientid;
            CaseID = UR.CaseID;
            name = UR.name;
            description = UR.description;
             BillingDate = UR.BillingDate;
             BilledBy = UR.BilledBy;
             CustomerName = UR.CustomerName;
             Address = UR.Address;
             PhoneNumber = UR.PhoneNumber;
             priorityStatus = UR.priorityStatus;
             bedDetails = UR.bedDetails;
             refferDoctor = UR.refferDoctor;
            DoctorPercentage = UR.DoctorPercentage;
            department = UR.department;
             extra = UR.extra;
            teachers = UR.teachers;
            PharmacyStoreName = UR.PharmacyStoreName;
            DeliveredTo = UR.DeliveredTo;
            DeliveredHospital = UR.DeliveredHospital;
            ModeOfDespach = UR.ModeOfDespach;
            BillNo = UR.BillNo;
            PaymentMode = UR.PaymentMode;
            PaymentAmount = UR.PaymentAmount;
            CeditStatus = UR.CeditStatus;
            IPDOPDId = UR.IPDOPDId;
            PaymentStatus = UR.PaymentStatus;
            GrossPurchasePriceOnthisBill = UR.GrossPurchasePriceOnthisBill;
            GrossSalePriceOnthisBill = UR.GrossSalePriceOnthisBill;
            GrossProffitPriceOnthisBill = UR.GrossProffitPriceOnthisBill;
            GrossGSTPriceOnthisBill = UR.GrossGSTPriceOnthisBill;
            GrossCGSTPriceOnthisBill = UR.GrossCGSTPriceOnthisBill;
            GrossSGSTPriceOnthisBill = UR.GrossSGSTPriceOnthisBill;
            BillingMonth = UR.BillingMonth;
            BillingYear = UR.BillingYear;

        }
        //[BsonId]
        //[BsonIgnoreIfDefault]
        public Guid Id { get; set; }
        public string UnqueID { get; set; }
        public string IPDOPDId { get; set; }
        public string PaymentStatus { get; set; }
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
        public string GrossSalePriceOnthisBill { get; set; }
        public string GrossPurchasePriceOnthisBill { get; set; }
        public string GrossProffitPriceOnthisBill { get; set; }
        public string GrossGSTPriceOnthisBill { get; set; }
        public string GrossCGSTPriceOnthisBill { get; set; }
        public string GrossSGSTPriceOnthisBill { get; set; }
        public string BillingMonth { get; set; }
        public string BillingYear { get; set; }


    }

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class Teacher
    {
        public string name { get; set; }
        public string unit { get; set; }
        public string price { get; set; }
        public string Saleprice { get; set; }
        public string Unitprice { get; set; }
        public string discount { get; set; }
        public string CGST { get; set; }
        public string SGST { get; set; }
        public string IGST { get; set; }
        public string CGSTVal { get; set; }
        public string SGSTVal { get; set; }
        public string IGSTVal { get; set; }
        public string tax { get; set; }
        public string total { get; set; }
        public string inventoryID { get; set; }
        public string expDate { get; set; }
        public string HSNCode { get; set; }
        public string UOM { get; set; }
        public string BatchNumber { get; set; }
        public string expiryDate { get; set; }

        public string TotalPurchasePrice { get; set; }
        public string NetProffit { get; set; }
        public string TotalGST { get; set; }
        public string BillingMonth { get; set; }
        public string BillingYear { get; set; }


    }


    public class FaramactToPatientFew
    {
     
        public string IPDOPDId { get; set; }        
        
        public string CaseID { get; set; }
        
        public List<Teacher> teachers { get; set; }
        
    }

    public class FaramaBillFew
    {

        public string UnqueID { get; set; }
        public string IPDOPDId { get; set; }
        public string PaymentStatus { get; set; }
        public string DateStart { get; set; } = new DateTime().ToString("dd-MM-yyyy");
        public string CaseID { get; set; }
        public string name { get; set; }
        public string BillingDate { get; set; } = new DateTime().ToString("dd-MM-yyyy");
        public string BilledBy { get; set; }
        public string CustomerName { get; set; }
        public string BillNo { get; set; }
        public string PaymentAmount { get; set; }
        public string CeditStatus { get; set; }
        public string PharmacyStoreName { get; set; }
        public string GrossSalePriceOnthisBill { get; set; }
        public string GrossPurchasePriceOnthisBill { get; set; }
        public string GrossProffitPriceOnthisBill { get; set; }
        public string GrossGSTPriceOnthisBill { get; set; }
        public string GrossCGSTPriceOnthisBill { get; set; }
        public string GrossSGSTPriceOnthisBill { get; set; }
        public string BillingMonth { get; set; }
        public string BillingYear { get; set; }
        public string refferDoctor { get; set; }
        public string DoctorPercentage { get; set; }

    }






}





