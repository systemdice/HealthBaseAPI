using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoDB.GenericRepository.ViewModel
{
    public class UserRegistrationViewModel
    {
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
        public bool ShouldCommit { get; set; } = true;
    }
}
