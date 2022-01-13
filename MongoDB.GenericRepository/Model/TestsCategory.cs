using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.GenericRepository.Utility;
using MongoDB.GenericRepository.ViewModel;
using System;

namespace MongoDB.GenericRepository.Model
{
    public class TestsCategory
    {
        public TestsCategory(TestsCategoryViewModel UR)
        {
            Id = Guid.NewGuid();
            UnqueID = CommonMethods.GetUniqueIDShorter();//new Random().Next(10, 500).ToString();
            Name   = UR.Name;
            ShortName   = UR.ShortName;
            Category   = UR.Category;
            Fee   = UR.Fee;
            Method   = UR. Method;
            Instrument   = UR. Instrument;
            ResultTypeDoc   = UR.ResultTypeDoc;
            Notes   = UR. Notes;
            Comments   = UR. Comments;
            Interpretation   = UR.Interpretation;
            Parameters = UR.Parameters;
            ReferenceRange = UR.ReferenceRange;
        }

        public TestsCategory(string updateUniqueaID, TestsCategoryViewModel UR)
        {
            //Id = id;
            UnqueID = updateUniqueaID;
            Name = UR.Name;
            ShortName = UR.ShortName;
            Category = UR.Category;
            Fee = UR.Fee;
            Method = UR.Method;
            Instrument = UR.Instrument;
            ResultTypeDoc = UR.ResultTypeDoc;
            Notes = UR.Notes;
            Comments = UR.Comments;
            Interpretation = UR.Interpretation;
            Parameters = UR.Parameters;
            ReferenceRange = UR.ReferenceRange;
        }
        //[BsonId]
        //[BsonIgnoreIfDefault]
        public Guid Id { get; set; }
        public string UnqueID { get; set; }
        public string Name { get; set; }
        public string ShortName{ get; set; }
        public string Category{ get; set; }
        public decimal Fee{ get; set; }
        public string Method{ get; set; }
        public string Instrument{ get; set; }
        public bool ResultTypeDoc{ get; set; }

        public string Notes { get; set; }
        public string Comments{ get; set; }
        public string Interpretation{ get; set; }

        public Parameters[] Parameters{ get; set; }
        public ReferenceRange[] ReferenceRange { get; set; }


    }
}
