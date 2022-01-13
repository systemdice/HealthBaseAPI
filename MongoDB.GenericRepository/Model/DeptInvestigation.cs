using MongoDB.GenericRepository.Utility;
using MongoDB.GenericRepository.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoDB.GenericRepository.Model
{
    public class DeptInvestigation
    {

        public DeptInvestigation(DeptInvestigationViewModel UR)
        {
            Id = Guid.NewGuid();
            UnqueID = CommonMethods.GetUniqueIDShorter();// Random().Next(10, 500).ToString();
            TestCode = UR.TestCode;
            Name = UR.Name;
            Fees = UR.Fees;
            RevenueshareAmountFor = UR.RevenueshareAmountFor;
            Gender = UR.Gender;
            TestBookingAllowed = UR.TestBookingAllowed;
            Testslinked = UR.Testslinked;
            Count = UR.Count;
            Panels = UR.Panels;
            Remarks = UR.Remarks;
            ReferenceRange = UR.ReferenceRange;
            Unit = UR.Unit;
            ParameterDetails = UR.ParameterDetails;
            DateStart = UR.DateStart;
            Active = UR.Active;
        }

        public DeptInvestigation(string updateUniqueaID, DeptInvestigationViewModel UR)
        {
            //Id = id;
            UnqueID = updateUniqueaID;
            TestCode = UR.TestCode;
            Name = UR.Name;
            Fees = UR.Fees;
            RevenueshareAmountFor = UR.RevenueshareAmountFor;
            Gender = UR.Gender;
            TestBookingAllowed = UR.TestBookingAllowed;
            Testslinked = UR.Testslinked;
            Count = UR.Count;
            Panels = UR.Panels;
            Remarks = UR.Remarks;
            ReferenceRange = UR.ReferenceRange;
            Unit = UR.Unit;
            ParameterDetails = UR.ParameterDetails;
            DateStart = UR.DateStart;
            Active = UR.Active;
        }

       
        public Guid Id { get; set; }
        public string UnqueID { get; set; }
        public string TestCode { get; set; }
        public string Name { get; set; }
        public string Fees { get; set; }
        public decimal RevenueshareAmountFor { get; set; }
        public string[] Gender { get; set; }
        public string[] TestBookingAllowed { get; set; }
        public string[] Testslinked { get; set; }
        public string Count { get; set; }
        public string Panels { get; set; }
        public string Remarks { get; set; }
        public string ReferenceRange { get; set; }
        public string[] Unit { get; set; }
        public string[] ParameterDetails { get; set; }
        public string DateStart { get; set; } = new DateTime().ToShortDateString();
        public Boolean Active { get; set; }
        public string EnteredBy { get; set; }
    }
}
