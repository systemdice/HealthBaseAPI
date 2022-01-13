using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.GenericRepository.Utility;
using MongoDB.GenericRepository.ViewModel;
using System;

namespace MongoDB.GenericRepository.Model
{
    public class TestHistory
    {
        public TestHistory(TestHistoryViewModel UR)
        {
            Id = Guid.NewGuid();
            UnqueID = CommonMethods.GetUniqueIDShorter();//new Random().Next(10, 500).ToString();
            Description = UR.Description;
            ParentTestType = UR.ParentTestType;
            TestName = UR.TestName;
        CaseID  = UR.CaseID;
         Amount = UR.Amount;
        AllTestDone  = UR.AllTestDone;

        Status  = UR.Status;
        DateStart  = UR.DateStart;
        DateEnd  = UR.DateEnd;
        }

        public TestHistory(string updateUniqueaID, TestHistoryViewModel UR)
        {
            //Id = id;
            UnqueID = updateUniqueaID;
            Description = UR.Description;
            ParentTestType = UR.ParentTestType;
            TestName = UR.TestName;
            CaseID = UR.CaseID;
            Amount = UR.Amount;
            AllTestDone = UR.AllTestDone;

            Status = UR.Status;
            DateStart = UR.DateStart;
            DateEnd = UR.DateEnd;
        }
        //[BsonId]
        //[BsonIgnoreIfDefault]
        public Guid Id { get; set; }
        public string UnqueID { get; set; }
        public string Description { get; set; }
        public string  ParentTestType { get; set; }
        public string[] TestName { get; set; }
        public string CaseID { get; set; }
        public decimal Amount { get; set; }
        public string AllTestDone { get; set; }

        public string Status { get; set; }
        public string DateStart { get; set; } = new DateTime().ToShortDateString();
        public string DateEnd { get; set; }
    }
}
