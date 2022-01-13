using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.GenericRepository.Utility;
using MongoDB.GenericRepository.ViewModel;
using System;

namespace MongoDB.GenericRepository.Model
{
    public class ReferralMaster
    {
        public ReferralMaster(ReferralMasterViewModel UR)
        {
            Id = Guid.NewGuid();
            UnqueID = CommonMethods.GetUniqueIDShorter();// Random().Next(10, 500).ToString();
            FirstName = UR.FirstName;
            LastName = UR.LastName;
            ContactNumber = UR.ContactNumber;
            Description = UR.Description;
            Status = UR.Status;
            Department  = UR.Department;
            fees  = UR.fees;
            Commission = UR.Commission;
            Discount  = UR.Discount;
            Experience = UR.Experience;
            Degree  = UR.Degree;
            Email  = UR.Email;
            Address  = UR.Address;
            Title  = UR.Title;
            DateStart  = DateTime.Now.ToString("dd-MM-yyyy");
            DateEnd  = UR.DateEnd;
            UserName = UR.UserName;
            StaffType = UR.StaffType;
            StaffGender = UR.StaffGender;


        }

        public ReferralMaster(string updateUniqueaID, ReferralMasterViewModel UR)
        {
            //Id = id;
            UnqueID = updateUniqueaID;
            FirstName = UR.FirstName;
            LastName = UR.LastName;
            ContactNumber = UR.ContactNumber;
            Description = UR.Description;
            Status = UR.Status;
            Department  = UR.Department;
            fees  = UR.fees;
            Commission = UR.Commission;
            Discount  = UR.Discount;
            Experience= UR.Experience;
            Degree  = UR.Degree;
            Email  = UR.Email;
            Address  = UR.Address;
            Title  = UR.Title;
            DateStart  = DateTime.Now.ToString("dd-MM-yyyy");
            DateEnd  = UR.DateEnd;
            UserName = UR.UserName;
            StaffType = UR.StaffType;
            StaffGender = UR.StaffGender;

        }

        //[BsonId]
        //[BsonIgnoreIfDefault]
        public Guid Id { get; set; }
        public string UnqueID { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string ContactNumber { get; set; }
    public string Description { get; set; }
    public bool Status { get; set; }
    public string Department { get; set; }
    public decimal fees { get; set; }
        public string Commission { get; set; }
        public decimal Discount { get; set; }
        public string Experience { get; set; }

        public string Degree { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }
    public string Title { get; set; }
    public string DateStart { get; set; } = DateTime.Now.ToString("dd-MM-yyyy");
        public string DateEnd { get; set; }
        public string UserName { get; set; }
        public string  StaffType { get; set; }
        public string StaffGender { get; set; }

    }

    public class SingleReferralMaster
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ContactNumber { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
        public string Department { get; set; }
        public decimal fees { get; set; }
        public string Commission { get; set; }

        public decimal Discount { get; set; }
        public string Experience { get; set; }
        public string Degree { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Title { get; set; }
        public string DateStart { get; set; } = DateTime.Now.ToString("dd-MM-yyyy");
        public string DateEnd { get; set; }

        public string doctorname { get; set; }
        public string department { get; set; }
        public string appointment { get; set; }
        public string time { get; set; }
        public string UserName { get; set; }
        public string StaffType { get; set; }
        public string StaffGender { get; set; }

    }
}
