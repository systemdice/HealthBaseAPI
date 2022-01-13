using MongoDB.GenericRepository.Utility;
using MongoDB.GenericRepository.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoDB.GenericRepository.Model
{
    public class UserRegistration
    {

        public UserRegistration(UserRegistrationViewModel UR)
        {
            Id = Guid.NewGuid();
            UnqueID = CommonMethods.GetUniqueIDShorter(); //new Random().Next(10, 500).ToString();
            Username = UR.Username;
            Password = UR.Password;
            FirstName = UR.FirstName;
            LastName = UR.LastName;
            Token = UR.Token;
            Category = UR.Category;
            Country = UR.Country;
            State = UR.State;
            Active = UR.Active;
            Gender = UR.Gender;
            ContactNumber = UR.ContactNumber;
            Role = UR.Role;
            EmailAddress = UR.EmailAddress;
            Address = UR.Address;
            DateStart = UR.DateStart;
            DateEnd = UR.DateEnd;
        }

        public UserRegistration(string updateUniqueaID, UserRegistrationViewModel UR)
        {
            //Id = id;
            UnqueID = updateUniqueaID;
            Username = UR.Username;
            Password = UR.Password;
            FirstName = UR.FirstName;
            LastName = UR.LastName;
            Token = UR.Token;
            Category = UR.Category;
            Country = UR.Country;
            State = UR.State;
            Active = UR.Active;
            Gender = UR.Gender;
            ContactNumber = UR.ContactNumber;
            Role = UR.Role;
            EmailAddress = UR.EmailAddress;
            Address = UR.Address;
            DateStart = UR.DateStart;
            DateEnd = UR.DateEnd;
        }

       
        public Guid Id { get; set; }
        public string UnqueID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Token { get; set; }
        public string Category { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public Boolean Active { get; set; }
        public string Gender { get; set; }
        public string ContactNumber { get; set; }
        public string Role { get; set; }
        public string EmailAddress { get; set; }
        public string Address { get; set; }
        public string DateStart { get; set; } = new DateTime().ToShortDateString();
        public string DateEnd { get; set; }
    }
}
