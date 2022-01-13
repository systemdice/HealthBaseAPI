
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.GenericRepository.Utility;
using MongoDB.GenericRepository.ViewModel;
using System;

namespace MongoDB.GenericRepository.Model
{
    public class AppointmentDetail
    {
        public AppointmentDetail(AppointmentDetailViewModel UR)
        {
            Id = Guid.NewGuid();
            UnqueID = CommonMethods.GetUniqueIDShorter(); //new Random().Next(10, 500).ToString();
            DateStart = DateTime.Now.ToString("dd-MM-yyyy"); // UR.DateStart;
            PatientDetails = UR.PatientDetails;
            ReferralMaster = UR.ReferralMaster;
            AppointmentStatus = UR.AppointmentStatus;
            AppointmentCharge = UR.AppointmentCharge;
            AppointmentPaymentMode = UR.AppointmentPaymentMode;
            AppointmentPaymentStatus = UR.AppointmentPaymentStatus;
        }

        public AppointmentDetail(string updateUniqueaID, AppointmentDetailViewModel UR)
        {
            //Id = id;
            UnqueID = updateUniqueaID;
            DateStart = DateTime.Now.ToString("dd-MM-yyyy");  // UR.DateStart;
            PatientDetails = UR.PatientDetails;
            ReferralMaster = UR.ReferralMaster;
            AppointmentStatus = UR.AppointmentStatus;
            AppointmentCharge = UR.AppointmentCharge;
            AppointmentPaymentMode = UR.AppointmentPaymentMode;
            AppointmentPaymentStatus = UR.AppointmentPaymentStatus;
        }
        //[BsonId]
        //[BsonIgnoreIfDefault]
        public Guid Id { get; set; }
        public string UnqueID { get; set; }

        public string DateStart { get; set; } = new DateTime().ToShortDateString();
        public SinglePatientDetails PatientDetails { get; set; }
        public SingleReferralMaster ReferralMaster { get; set; }
        public string AppointmentStatus { get; set; }
        public string AppointmentCharge { get; set; }
        public string AppointmentPaymentMode { get; set; }
        public string AppointmentPaymentStatus { get; set; }


    }
}

