using System;
using MongoDB.GenericRepository.Model;

namespace MongoDB.GenericRepository.ViewModel
{
    public class UserDetailsViewModel
    {
        public string UnqueID { get; set; }
        public string DateStart { get; set; } = new DateTime().ToString("dd-MM-yyyy");


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
        public bool ShouldCommit { get; set; } = true;

        public static explicit operator UserDetailsViewModel(UserDetails v)
        {
            throw new NotImplementedException();
        }
    }
}

