
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.GenericRepository.Utility;
using MongoDB.GenericRepository.ViewModel;
using System;
using System.Collections.Generic;

namespace MongoDB.GenericRepository.Model
{
    public class LabTestIndividual
    {
        public LabTestIndividual(LabTestIndividualViewModel UR)
        {
            Id = Guid.NewGuid();
            UnqueID = CommonMethods.GetUniqueIDShorter();//new Random().Next(10, 500).ToString();
            DateStart = DateTime.Now.ToString("dd-MM-yyyy"); // UR.DateStart;
            CategoryName = UR.CategoryName;
            CaseID = UR.CaseID;
            TestName = UR.TestName;
            TestPrice = UR.TestPrice;
            CreatedBy = UR.CreatedBy;
            UserName = UR.UserName;
            UserRole = UR.UserRole;
            Location = UR.Location;
            ModifiedBy = UR.ModifiedBy;
            ReportStatus = UR.ReportStatus;
            BarCodeKey = UR.BarCodeKey;
            QRCodeKey = UR.QRCodeKey;
            ParentTest = UR.ParentTest;
            MoreDetails = UR.MoreDetails;
            test = UR.test;
            //DateStart = UR.PatientID; 

        }

        public LabTestIndividual(string updateUniqueaID, LabTestIndividualViewModel UR)
        {
            //Id = id;
            UnqueID = updateUniqueaID;
            DateStart = DateTime.Now.ToString("dd-MM-yyyy"); // UR.DateStart;
            CategoryName = UR.CategoryName;
            CaseID = UR.CaseID;
            TestName = UR.TestName;
            TestPrice = UR.TestPrice;
            CreatedBy = UR.CreatedBy;
            UserName = UR.UserName;
            UserRole = UR.UserRole;
            Location = UR.Location;
            ModifiedBy = UR.ModifiedBy;
            ReportStatus = UR.ReportStatus;
            BarCodeKey = UR.BarCodeKey;
            QRCodeKey = UR.QRCodeKey;
            ParentTest = UR.ParentTest;
            MoreDetails = UR.MoreDetails;
            test = UR.test;
            //DateStart = UR.PatientID;

        }
        //[BsonId]
        //[BsonIgnoreIfDefault]
        public Guid Id { get; set; }
        public string UnqueID { get; set; }
        public string DateStart { get; set; }
        public string CaseID { get; set; }
        public string TestName { get; set; }
        public string CategoryName { get; set; }
        public string TestPrice { get; set; }
        public string CreatedBy { get; set; }
        public string UserName { get; set; }
        public string UserRole { get; set; }
        public string Location { get; set; }
        public string ModifiedBy { get; set; }
        public string ReportStatus { get; set; }
        public string BarCodeKey { get; set; }
        public string QRCodeKey { get; set; }
        public string ParentTest { get; set; }
        public string MoreDetails { get; set; }
        public List<GenralRefTest> test { get; set; }



    }







    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class GenralRefTest
    {
        public string ParamterName { get; set; }
        public string InputValue { get; set; }
        public string Unit { get; set; }
        public string GeneralRef { get; set; }
        
    }

   

}
