using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.GenericRepository.Utility;
using MongoDB.GenericRepository.ViewModel;
using System;

namespace MongoDB.GenericRepository.Model
{
    public class UnitsCategory
    {
        public UnitsCategory(UnitsCategoryViewModel UR)
        {
            Id = Guid.NewGuid();
            UnqueID = CommonMethods.GetUniqueIDShorter();//new Random().Next(10, 500).ToString();
            Name = UR.Name;
            Description = UR.Description;
            Status = UR.Status;
            DateStart = UR.DateStart;
            DateEnd = UR.DateEnd;
        }

        public UnitsCategory(string updateUniqueaID, UnitsCategoryViewModel UR)
        {
            //Id = id;
            UnqueID = updateUniqueaID;
            Name = UR.Name;
            Description = UR.Description;
            Status = UR.Status;
            DateStart = UR.DateStart;
            DateEnd = UR.DateEnd;
        }
        //[BsonId]
        //[BsonIgnoreIfDefault]
        public Guid Id { get; set; }
        public string UnqueID { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; }
        public string DateStart { get; set; } = new DateTime().ToShortDateString();
        public string DateEnd { get; set; }
    }
}
