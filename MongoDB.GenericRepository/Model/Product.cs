using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.GenericRepository.Utility;
using System;

namespace MongoDB.GenericRepository.Model
{
    public class Product
    {
        public Product(string description, string name, string unqueID)
        {
            Description = description;
            Name = name;
            UnqueID = CommonMethods.GetUniqueIDShorter();// Random().Next(10, 500).ToString();
            Id = Guid.NewGuid();
        }

        public Product(Guid id, string description,string name, string unqueID)
        {
            Id = id ;
            Description = description;
            Name = name;
            UnqueID = unqueID;
        }
        //[BsonId]
        //[BsonIgnoreIfDefault]
        public Guid Id { get; set; }
        public string UnqueID { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
    }
}
