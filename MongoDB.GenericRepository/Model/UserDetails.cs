using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.GenericRepository.ViewModel;
using System;
using MongoDB.GenericRepository.Utility;

namespace MongoDB.GenericRepository.Model
{
    public class UserDetails
    {
        public UserDetails(UserDetailsViewModel UR)
        {
            Id = Guid.NewGuid();
            UnqueID = CommonMethods.GetUniqueIDShorter();// Random().Next(10, 500).ToString();
            DateStart = DateTime.Now.ToString("dd-MM-yyyy");
            Title = UR.Title;
            UserName = UR.UserName;
            Password = UR.Password;
            OldPassword = UR.OldPassword;
            FirstName = UR.FirstName;
            LastName = UR.LastName;
            DOB = UR.DOB;

            Gender = UR.Gender;
            Email = UR.Email;
            ContactNumber = UR.ContactNumber;
            Address = UR.Address;
            Status = UR.Status;

            Designation = UR.Designation;
            Role = UR.Role;
        MultiRoles = UR.MultiRoles;
        MenuItems = UR.MenuItems;
        CenterName = UR.CenterName;
            SecretCode = UR.SecretCode;
            Location = UR.Location;
            RefferDoctorName = UR.RefferDoctorName;
            ReportingManagerName = UR.ReportingManagerName;
            ReportingManagerRole = UR.ReportingManagerRole;
            ProfilePicName  = UR.ProfilePicName;

        }

        public UserDetails(string updateUniqueaID, UserDetailsViewModel UR)
        {
            //Id = id;
            UnqueID = updateUniqueaID;
            DateStart = DateTime.Now.ToString("dd-MM-yyyy");
            Title = UR.Title;
            UserName = UR.UserName;
            Password = UR.Password;
            OldPassword = UR.OldPassword;
            FirstName = UR.FirstName;
            LastName = UR.LastName;
            DOB = UR.DOB;

            Gender = UR.Gender;
            Email = UR.Email;
            ContactNumber = UR.ContactNumber;
            Address = UR.Address;
            Status = UR.Status;

            Designation = UR.Designation;
            Role = UR.Role;
            MultiRoles = UR.MultiRoles;
            MenuItems = UR.MenuItems;
            CenterName = UR.CenterName;
            SecretCode = UR.SecretCode;
            Location = UR.Location;
            RefferDoctorName = UR.RefferDoctorName;
            ReportingManagerName = UR.ReportingManagerName;
            ReportingManagerRole = UR.ReportingManagerRole;
            ProfilePicName = UR.ProfilePicName;


        }
    
    //[BsonId]
    //[BsonIgnoreIfDefault]
    public Guid Id { get; set; }
        public string UnqueID { get; set; }
        public string DateStart { get; set; }
        public string Title { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string OldPassword { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DOB { get; set; }

        public string Gender { get; set; }
        public string Email { get; set; }
        public string ContactNumber { get; set; }
        public string Address { get; set; }
        public string Status { get; set; }

        public string Designation { get; set; }
        public string Role { get; set; }
        public string[] MultiRoles { get; set; }
        public string[] MenuItems { get; set; }
        public string CenterName { get; set; }
        public string SecretCode { get; set; }
        public string Location { get; set; }
        public string RefferDoctorName { get; set; }
        public string ReportingManagerName { get; set; }
        public string ReportingManagerRole { get; set; }
        public string ProfilePicName { get; set; }

    }

    public class LoginAndPasswordResetModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string NewPassword { get; set; }
        public string OldPassword { get; set; }
        public string SecretCode { get; set; }
    }

    public class ReturnUserModel
    {
        public string UserName { get; set; }
        //public string Password { get; set; }
        public string Role{ get; set; }
        public string[] MultiRoles { get; set; }
        public string CenterName { get; set; }
    }

}
