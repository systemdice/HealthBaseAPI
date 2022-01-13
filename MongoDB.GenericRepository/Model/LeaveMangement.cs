using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.GenericRepository.Utility;
using MongoDB.GenericRepository.ViewModel;
using System;
using System.Collections.Generic;

namespace MongoDB.GenericRepository.Model
{
    public class LeaveMangement
    {
        public LeaveMangement(LeaveMangementViewModel UR)
        {
            Id = Guid.NewGuid();
            UnqueID = CommonMethods.GetUniqueIDShorter(); //new Random().Next(10, 500).ToString();
            DateStart = DateTime.Now.ToString("dd-MM-yyyy");  // UR.DateStart;
            name = UR.name;
            description = UR.description;
            BillingDate = UR.BillingDate;
            StaffType = UR.StaffType;
            FirstName = UR.FirstName;
            leaveId = UR.leaveId;
            leaveReason = UR.leaveReason;
            dateFrom = UR.dateFrom;
            dateTo = UR.dateTo;
            approved = UR.approved;
            deniedReason = UR.deniedReason;
            status = UR.status;
            createdAt = UR.createdAt;
            NofDays = UR.NofDays;
            SingleDayLeave = UR.SingleDayLeave;
            ReportingManager = UR.ReportingManager;
            ReportingManagerEmail = UR.ReportingManagerEmail;

        }

        public LeaveMangement(string updateUniqueaID, LeaveMangementViewModel UR)
        {
            //Id = id;
            UnqueID = updateUniqueaID;
            //DateStart = DateTime.Now.ToString("dd-MM-yyyy");  // UR.DateStart;
            DateStart = UR.DateStart;
            name = UR.name;
            description = UR.description;
            BillingDate = UR.BillingDate;
            StaffType = UR.StaffType;
            FirstName = UR.FirstName;
            leaveId = UR.leaveId;
            leaveReason = UR.leaveReason;
            dateFrom = UR.dateFrom;
            dateTo = UR.dateTo;
            approved = UR.approved;
            deniedReason = UR.deniedReason;
            status = UR.status;
            createdAt = UR.createdAt;
            NofDays = UR.NofDays;
            SingleDayLeave = UR.SingleDayLeave;
            ReportingManager = UR.ReportingManager;
            ReportingManagerEmail = UR.ReportingManagerEmail;

        }
        //[BsonId]
        //[BsonIgnoreIfDefault]
        public Guid Id { get; set; }
        public string UnqueID { get; set; }        
        public string DateStart { get; set; } = new DateTime().ToString("dd-MM-yyyy");        
        public string name { get; set; }
        public string description { get; set; }
        public string BillingDate { get; set; } = new DateTime().ToString("dd-MM-yyyy");        
        public string StaffType { get; set; }
        public string FirstName { get; set; }

        public string leaveId { get; set; }
        public string leaveReason { get; set; }
        public string dateFrom { get; set; }
        public string dateTo { get; set; }
        public string approved { get; set; }
        public string deniedReason { get; set; }
        public string status { get; set; }
        public string createdAt { get; set; }
        public string NofDays { get; set; }
        public string SingleDayLeave { get; set; }
        public string ReportingManager { get; set; }
        public string ReportingManagerEmail { get; set; }


    }


}





