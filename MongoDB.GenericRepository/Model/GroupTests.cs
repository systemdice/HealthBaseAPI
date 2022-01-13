
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.GenericRepository.Utility;
using MongoDB.GenericRepository.ViewModel;
using System;
using System.Collections.Generic;

namespace MongoDB.GenericRepository.Model
{
    public class GroupTests
    {
        public GroupTests(GroupTestsViewModel UR)
        {
            Id = Guid.NewGuid();
            UnqueID = CommonMethods.GetUniqueIDShorter();//new Random().Next(10, 500).ToString();
            names = UR.names;
            Status = UR.Status;
            DateStart = UR.DateStart;
            DateEnd = UR.DateEnd;
            Status = UR.Status;
            GroupName = UR.GroupName;
            Description = UR.Description;
            Discount = UR.Discount;
            ActualPrice = UR.ActualPrice;
            TotalPrice = UR.TotalPrice;
            CreatedDate = UR.CreatedDate;
            CreatedBy = UR.CreatedBy;
        }

        public GroupTests(string updateUniqueaID, GroupTestsViewModel UR)
        {
            //Id = id;
            UnqueID = updateUniqueaID;
            names = UR.names;
            Status = UR.Status;
            DateStart = UR.DateStart;
            DateEnd = UR.DateEnd;
            Status = UR.Status;
            GroupName = UR.GroupName;
            Description = UR.Description;
            Discount = UR.Discount;
            ActualPrice = UR.ActualPrice;
            TotalPrice = UR.TotalPrice;
            CreatedDate = UR.CreatedDate;
            CreatedBy = UR.CreatedBy;
    }
        //[BsonId]
        //[BsonIgnoreIfDefault]
        public Guid Id { get; set; }
        public string UnqueID { get; set; }
        public string DateStart { get; set; } = new DateTime().ToShortDateString();
        public string Status { get; set; }
        public string DateEnd { get; set; }
        public string GroupName { get; set; }
        public string Description { get; set; }
        public string Discount { get; set; }
        public string ActualPrice { get; set; }
        public string TotalPrice { get; set; }
        public string CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public List<Name> names { get; set; }
    }
    

}
