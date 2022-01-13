using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoDB.GenericRepository.ViewModel
{
    public class FollowupViewModel
    {
        public string UnqueID { get; set; }
        public bool ShouldCommit { get; set; } = true;
        public string WorkStatus { get; set; }
        public string BusinessType { get; set; }


        public string CategoryName { get; set; }
        public string RenewalStatus { get; set; }
        public string AgreementProcess { get; set; }



        public string ExpenseCategory { get; set; }
        public string UserName { get; set; }
        public string ClientName { get; set; }
        public string ClientLocation { get; set; }
        public DateTime FollowupStartDate { get; set; }
        public string ClientAddress { get; set; }
        public string ClientContactName { get; set; }
        public string ClientContactNumber { get; set; }
        public string ClientWebsite { get; set; }
        public string FollowupCategory { get; set; }
        public string ClientReply { get; set; }
        public string ExperienceNotes { get; set; }
        public string DemoRequired { get; set; }
        public DateTime DemoDate { get; set; }
        public string DemoStatus { get; set; }
        public string DemoAssignedTo { get; set; }
        public string InstallationRequired { get; set; }
        public DateTime InstallationRequiredDate { get; set; }
        public string InstallationStatus { get; set; }
        public string InstallationAssignedTo { get; set; }
        public string BasePrice { get; set; }
        public string ExpectedPrice { get; set; }
        public string ActualPrice { get; set; }
        public string CommisionPrice { get; set; }
        public string SupportAssigned { get; set; }
        public string AgreementJob { get; set; }
        public string Signup { get; set; }
        public string ClosedBy { get; set; }
        public string CurrentUserRole { get; set; }
        public string ExperienceNotesHL { get; set; }

        public string UpdatedBy { get; set; }
        public string UpdatedRole { get; set; }

    }
}
