
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.GenericRepository.Utility;
using MongoDB.GenericRepository.ViewModel;

namespace MongoDB.GenericRepository.Model
{
    public class BedManagement
    {

        public BedManagement(BedManagementViewModel UR)
        {
            Id = Guid.NewGuid();
            UnqueID = CommonMethods.GetUniqueIDShorter();//new Random().Next(10, 500).ToString();
            DateStart = DateTime.Now.ToString("dd-MM-yyyy"); // UR.DateStart;  
            teachers = UR.teachers;
            CreatedBy = UR.CreatedBy;

        }

        public BedManagement(string updateUniqueaID, BedManagementViewModel UR)
        {
            //Id = id;
            UnqueID = updateUniqueaID;
            DateStart = DateTime.Now.ToString("dd-MM-yyyy"); // UR.DateStart;
            teachers = UR.teachers;
            CreatedBy = UR.CreatedBy;

        }
        //[BsonId]
        //[BsonIgnoreIfDefault]
        public Guid Id { get; set; }
        public string UnqueID { get; set; }
        public string DateStart { get; set; }
        public List<BedCategory> teachers { get; set; }
        public string CreatedBy { get; set; }




    }

    public class BedCategory
    {
        public string name { get; set; }
        public List<BedNames> batches { get; set; }
    }
    public class BedNames
    {
        public string name { get; set; }
        public string price { get; set; }
        public string OccupySatus { get; set; }

        public string OccupiedBy { get; set; }
        public string OPDIPDID { get; set; }
        public string CaseUniqueID { get; set; }
        public string AssignedDoctor { get; set; }
    }
}
