using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.GenericRepository.Utility;
using MongoDB.GenericRepository.ViewModel;
using System;

namespace MongoDB.GenericRepository.Model
{
    public class Followup
    {
        public Followup(FollowupViewModel UR)
        {
            Id = Guid.NewGuid();
            UnqueID = CommonMethods.GetUniqueIDShorter(); //new Random().Next(10, 500).ToString();            
            AgreementProcess = UR.AgreementProcess;
            WorkStatus = UR.WorkStatus;
            BusinessType = UR.BusinessType;
            ExpenseCategory = UR.ExpenseCategory;
            UserName = UR.UserName;
            ClientName = UR.ClientName;
            ClientLocation = UR.ClientLocation;
            FollowupStartDate = UR.FollowupStartDate;
            ClientAddress = UR.ClientAddress;
            ClientContactName = UR.ClientContactName;
            ClientContactNumber = UR.ClientContactNumber;
            ClientWebsite = UR.ClientWebsite;
            FollowupCategory = UR.FollowupCategory;
            ClientReply = UR.ClientReply;
            ExperienceNotes = UR.ExperienceNotes;
            DemoRequired = UR.DemoRequired;
            DemoDate = UR.DemoDate;
            DemoStatus = UR.DemoStatus;
            DemoAssignedTo = UR.DemoAssignedTo;
            InstallationRequired = UR.InstallationRequired;
            InstallationRequiredDate = UR.InstallationRequiredDate;
            InstallationStatus = UR.InstallationStatus;
            InstallationAssignedTo = UR.InstallationAssignedTo;
            BasePrice = UR.BasePrice;
            ExpectedPrice = UR.ExpectedPrice;
            ActualPrice = UR.ActualPrice;
            CommisionPrice = UR.CommisionPrice;
            SupportAssigned = UR.SupportAssigned;
            AgreementJob = UR.AgreementJob;
            Signup = UR.Signup;
            ClosedBy = UR.ClosedBy;
            CurrentUserRole = UR.CurrentUserRole;
            ExperienceNotesHL = UR.ExperienceNotesHL;
            UpdatedBy = UR.UpdatedBy;
            UpdatedRole = UR.UpdatedRole;
            CategoryName = UR.CategoryName;
            RenewalStatus = UR.RenewalStatus;

        }

        public Followup(string updateUniqueaID, FollowupViewModel UR)
        {
            //Id = id;
            UnqueID = updateUniqueaID;
            AgreementProcess = UR.AgreementProcess;
            WorkStatus = UR.WorkStatus;
            BusinessType = UR.BusinessType;
            ExpenseCategory = UR.ExpenseCategory;
            UserName = UR.UserName;
            ClientName = UR.ClientName;
            ClientLocation = UR.ClientLocation;
            FollowupStartDate = UR.FollowupStartDate;
            ClientAddress = UR.ClientAddress;
            ClientContactName = UR.ClientContactName;
            ClientContactNumber = UR.ClientContactNumber;
            ClientWebsite = UR.ClientWebsite;
            FollowupCategory = UR.FollowupCategory;
            ClientReply = UR.ClientReply;
            ExperienceNotes = UR.ExperienceNotes;
            DemoRequired = UR.DemoRequired;
            DemoDate = UR.DemoDate;
            DemoStatus = UR.DemoStatus;
            DemoAssignedTo = UR.DemoAssignedTo;
            InstallationRequired = UR.InstallationRequired;
            InstallationRequiredDate = UR.InstallationRequiredDate;
            InstallationStatus = UR.InstallationStatus;
            InstallationAssignedTo = UR.InstallationAssignedTo;
            BasePrice = UR.BasePrice;
            ExpectedPrice = UR.ExpectedPrice;
            ActualPrice = UR.ActualPrice;
            CommisionPrice = UR.CommisionPrice;
            SupportAssigned = UR.SupportAssigned;
            AgreementJob = UR.AgreementJob;
            Signup = UR.Signup;
            ClosedBy = UR.ClosedBy;
            CurrentUserRole = UR.CurrentUserRole;
            ExperienceNotesHL = UR.ExperienceNotesHL;
            UpdatedBy = UR.UpdatedBy;
            UpdatedRole = UR.UpdatedRole;
            CategoryName = UR.CategoryName;
            RenewalStatus = UR.RenewalStatus;
        }

        //[BsonId]
        //[BsonIgnoreIfDefault]
        public Guid Id { get; set; }
        public string UnqueID { get; set; }

        public string ClosedBy { get; set; }
        public string CurrentUserRole { get; set; }
        public string WorkStatus { get; set; }
        public string AgreementProcess { get; set; }
        public string BusinessType { get; set; }
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
        public string CategoryName { get; set; }
        public string RenewalStatus { get; set; }
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
        public string ExperienceNotesHL { get; set; }
        public string UpdatedBy { get; set; }
        public string UpdatedRole { get; set; }
         
        

    }
}
