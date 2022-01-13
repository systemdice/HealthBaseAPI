using MongoDB.GenericRepository.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoDB.GenericRepository.ViewModel
{
    public class AppointmentDetailViewModel
    {
        public string UnqueID { get; set; }
        public bool ShouldCommit { get; set; } = true;

        public string DateStart { get; set; } = new DateTime().ToString("dd-MM-yyyy");
        public SinglePatientDetails PatientDetails { get; set; }
        public SingleReferralMaster ReferralMaster { get; set; }
        public string AppointmentStatus { get; set; }
        public string AppointmentCharge { get; set; }
        public string AppointmentPaymentMode { get; set; }
        public string AppointmentPaymentStatus { get; set; }

    }
}
