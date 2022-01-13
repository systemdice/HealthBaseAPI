using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.GenericRepository.Utility;
using MongoDB.GenericRepository.ViewModel;
using System;

namespace MongoDB.GenericRepository.Model
{
    public class Category
    {
        public Category(CategoryViewModel UR)
        {
            Id = Guid.NewGuid();
            UnqueID = CommonMethods.GetUniqueIDShorter(); //new Random().Next(10, 500).ToString();            
            Notes = UR.Notes;
            CategoryName = UR.CategoryName;
            CategoryType = UR.CategoryType;
            CategoryStatus = UR.CategoryStatus;
            Date = UR.Date;

        }

        public Category(string updateUniqueaID, CategoryViewModel UR)
        {
            //Id = id;
            UnqueID = updateUniqueaID;
            Notes = UR.Notes;
            CategoryName = UR.CategoryName;
            CategoryType = UR.CategoryType;
            CategoryStatus = UR.CategoryStatus;
            Date = UR.Date;
        }

        //[BsonId]
        //[BsonIgnoreIfDefault]
        public Guid Id { get; set; }
        public string UnqueID { get; set; }

        public string Notes { get; set; }

        public string CategoryName { get; set; }
        public string CategoryType { get; set; }
        public string CategoryStatus { get; set; }

        public string Date { get; set; }

       

    }
}

