using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.GenericRepository.Utility;
using MongoDB.GenericRepository.ViewModel;
using System;
using Twilio.Rest.Api.V2010.Account;

namespace MongoDB.GenericRepository.Model
{
    public class Timesheet
    {
        public Timesheet(TimesheetViewModel UR)
        {
            Id = Guid.NewGuid();
            UnqueID = CommonMethods.GetUniqueIDShorter(); //new Random().Next(10, 500).ToString();            

            TimeSpent = UR.TimeSpent;

            Date = UR.Date;
            WorkingDate = UR.WorkingDate;
            WorkerName = UR.WorkerName;
            WorkerEmail = UR.WorkerEmail;
            WorkerID = UR.WorkerID;
            WorkerJOBStatus = UR.WorkerJOBStatus;

            WorkCategory = UR.WorkCategory;
            WorkSubCategory = UR.WorkSubCategory;

            Notes = UR.Notes;

            SubmitTo = UR.SubmitTo;

            ApprovalStatus = UR.ApprovalStatus;
            ApprovalReason = UR.ApprovalReason;
            Others = UR.Others;
            BillingSatus = UR.BillingSatus;
            LegalEntry = UR.LegalEntry;

        }

        public Timesheet(string updateUniqueaID, TimesheetViewModel UR)
        {
            //Id = id;
            UnqueID = updateUniqueaID;
            TimeSpent = UR.TimeSpent;

            Date = UR.Date;
            WorkingDate = UR.WorkingDate;
            WorkerName = UR.WorkerName;
            WorkerEmail = UR.WorkerEmail;
            WorkerID = UR.WorkerID;
            WorkerJOBStatus = UR.WorkerJOBStatus;

            WorkCategory = UR.WorkCategory;
            WorkSubCategory = UR.WorkSubCategory;

            Notes = UR.Notes;

            SubmitTo = UR.SubmitTo;

            ApprovalStatus = UR.ApprovalStatus;
            ApprovalReason = UR.ApprovalReason;
            Others = UR.Others;
            BillingSatus = UR.BillingSatus;
            LegalEntry = UR.LegalEntry;
        }

        //[BsonId]
        //[BsonIgnoreIfDefault]
        public Guid Id { get; set; }
        public string UnqueID { get; set; }

        public decimal TimeSpent { get; set; } = 0;

        public string Date { get; set; }

        public string WorkingDate { get; set; }
        public string WorkerName { get; set; }
        public string WorkerEmail { get; set; }
        public string WorkerID { get; set; }
        public string WorkerJOBStatus { get; set; }
        public string WorkCategory { get; set; }
        public string WorkSubCategory { get; set; }

        public string Notes { get; set; }
        
        public string SubmitTo { get; set; }

        public string ApprovalStatus { get; set; }
        public string ApprovalReason { get; set; }
        public string Others { get; set; }
        public string BillingSatus { get; set; }
        public string LegalEntry { get; set; }

    }
}
