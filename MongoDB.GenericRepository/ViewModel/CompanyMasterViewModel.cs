using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoDB.GenericRepository.ViewModel
{
    public class CompanyMasterViewModel
    {

        public string UnqueID { get; set; }
        public bool ShouldCommit { get; set; } = true;
        public string DateStart { get; set; }
        public string CompanyName { get; set; }
        public string CompanyID { get; set; }
        public string Version { get; set; }
        public string CompanyCompletAddress { get; set; }
        public string CompanyHeaderName1 { get; set; }
        public string CompanyHeaderName2 { get; set; }
        public string CompanyHeaderName3 { get; set; }
        public string CompanyHeaderName4 { get; set; }
        public string CompanyHeaderName5 { get; set; }
        public string CompanyPhoneNumber1 { get; set; }
        public string CompanyPhoneNumber2 { get; set; }
        public string CompanyEmail1 { get; set; }
        public string CompanyEmail2 { get; set; }
        public string SignOffbyWhom { get; set; }
        public string VDName { get; set; }
        public string DBName { get; set; }
        public string VDURL { get; set; }
        public string DBURL { get; set; }

        public string Technology { get; set; }
        public string SupportProvided { get; set; }
        public string ConcernPersonEmailIDSysDICE { get; set; }
        public string Price { get; set; }
        public string Purchasedate { get; set; }
        public string Renewaldate { get; set; }
        public string ExpireDate { get; set; }
        public string OfferApplied { get; set; }
        public string LicenceKey { get; set; }
        public string LicenceType { get; set; }
        public string CurrentStatus { get; set; }
        public string DataBackup { get; set; }
        //conguration
        public string PathoLAB { get; set; }
        public string Dental { get; set; }
        public string Sacnning { get; set; }
        public string Other { get; set; }
        public string CreatedBy { get; set; }
    }
}
