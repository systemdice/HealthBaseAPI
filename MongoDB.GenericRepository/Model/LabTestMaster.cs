
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.GenericRepository.Utility;
using MongoDB.GenericRepository.ViewModel;
using System;
using System.Collections.Generic;

namespace MongoDB.GenericRepository.Model
{
    public class LabTestMaster
    {
        public LabTestMaster(LabTestMasterViewModel UR)
        {
            Id = Guid.NewGuid();
            UnqueID = CommonMethods.GetUniqueIDShorter();//new Random().Next(10, 500).ToString();
            DateStart = DateTime.Now.ToString("dd-MM-yyyy"); // UR.DateStart;
            CategoryName = UR.CategoryName;
            CategoryType = UR.CategoryType;
            ShortName = UR.ShortName;
            TestName = UR.TestName;
            TestPrice = UR.TestPrice;
            Discount = UR.Discount;
            CreatedDate = UR.CreatedDate;
            CreatedBy = UR.CreatedBy;
            test = UR.test;
            //DateStart = UR.PatientID; 

        }

        public LabTestMaster(string updateUniqueaID, LabTestMasterViewModel UR)
        {
            //Id = id;
            UnqueID = updateUniqueaID;
            DateStart = DateTime.Now.ToString("dd-MM-yyyy"); // UR.DateStart;
            CategoryName = UR.CategoryName;
            CategoryType = UR.CategoryType;
            ShortName = UR.ShortName;
            TestName = UR.TestName;
            TestPrice = UR.TestPrice;
            Discount = UR.Discount;
            CreatedDate = UR.CreatedDate;
            CreatedBy = UR.CreatedBy;
            test = UR.test;
            //DateStart = UR.PatientID;

        }
        //[BsonId]
        //[BsonIgnoreIfDefault]
        public Guid Id { get; set; }
        public string UnqueID { get; set; }
        public string DateStart { get; set; }
        public string CategoryName { get; set; }
        public string CategoryType { get; set; }
        public string ShortName { get; set; }
        public string TestName { get; set; }
        public string TestPrice { get; set; }
        public string Discount { get; set; }
        public string CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public List<Parameter> test { get; set; }



    }
  

    public class Parameter
    {
        public string ParamterName { get; set; }
        public string InputValue { get; set; }
        public string Unit { get; set; }
        
        
        public List<ReferenceRangeLabTest> batches { get; set; }
    }

    public class ReferenceRangeLabTest
    {
        public string GenderCategory { get; set; }
        public string MinRange { get; set; }
        public string MaxRange { get; set; }
        public string CommonRange { get; set; }
        public string PreganancyCase { get; set; }
        
    }

}

