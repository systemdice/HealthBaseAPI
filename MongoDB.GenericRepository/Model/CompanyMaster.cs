using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.GenericRepository.Utility;
using MongoDB.GenericRepository.ViewModel;

namespace MongoDB.GenericRepository.Model
{
    public class CompanyMaster
    {

        public CompanyMaster(CompanyMasterViewModel UR)
        {
            Id = Guid.NewGuid();
            UnqueID = CommonMethods.GetUniqueIDShorter();//new Random().Next(10, 500).ToString();
            DateStart = DateTime.Now.ToString("dd-MM-yyyy"); // UR.DateStart;
            CompanyName = UR.CompanyName;
            CompanyID = UR.CompanyID;
            Version = UR.Version;
            CompanyCompletAddress = UR.CompanyCompletAddress;
            CompanyHeaderName1 = UR.CompanyHeaderName1;
            CompanyHeaderName2 = UR.CompanyHeaderName2;
            CompanyHeaderName3 = UR.CompanyHeaderName3;
            CompanyHeaderName4 = UR.CompanyHeaderName4;
            CompanyHeaderName5 = UR.CompanyHeaderName5;
            CompanyPhoneNumber1 = UR.CompanyPhoneNumber1;
            CompanyPhoneNumber2 = UR.CompanyPhoneNumber2;
            CompanyEmail1 = UR.CompanyEmail1;
            CompanyEmail2 = UR.CompanyEmail2;
            SignOffbyWhom = UR.SignOffbyWhom;
            VDName = UR.VDName;
            DBName = UR.DBName;
            VDURL = UR.VDURL;
            DBURL = UR.DBURL;
            Technology = UR.Technology;
            SupportProvided = UR.SupportProvided;
            ConcernPersonEmailIDSysDICE = UR.ConcernPersonEmailIDSysDICE;
            Price = UR.Price;
            Purchasedate = UR.Purchasedate;
            Renewaldate = UR.Renewaldate;
            ExpireDate = UR.ExpireDate;
            OfferApplied = UR.OfferApplied;
            LicenceKey = UR.LicenceKey;
            LicenceType = UR.LicenceType;
            CurrentStatus = UR.CurrentStatus;
            DataBackup = UR.DataBackup;
            PathoLAB = UR.PathoLAB;
            Dental = UR.Dental;
            Sacnning = UR.Sacnning;
            Other = UR.Other;
            CreatedBy = UR.CreatedBy;

        }

        public CompanyMaster(string updateUniqueaID, CompanyMasterViewModel UR)
        {
            //Id = id;
            UnqueID = updateUniqueaID;
            DateStart = DateTime.Now.ToString("dd-MM-yyyy"); // UR.DateStart;
            CompanyName = UR.CompanyName;
            CompanyID = UR.CompanyID;
            Version = UR.Version;
            CompanyCompletAddress = UR.CompanyCompletAddress;
            CompanyHeaderName1 = UR.CompanyHeaderName1;
            CompanyHeaderName2 = UR.CompanyHeaderName2;
            CompanyHeaderName3 = UR.CompanyHeaderName3;
            CompanyHeaderName4 = UR.CompanyHeaderName4;
            CompanyHeaderName5 = UR.CompanyHeaderName5;
            CompanyPhoneNumber1 = UR.CompanyPhoneNumber1;
            CompanyPhoneNumber2 = UR.CompanyPhoneNumber2;
            CompanyEmail1 = UR.CompanyEmail1;
            CompanyEmail2 = UR.CompanyEmail2;
            SignOffbyWhom = UR.SignOffbyWhom;
            VDName = UR.VDName;
            DBName = UR.DBName;
            VDURL = UR.VDURL;
            DBURL = UR.DBURL;
        Technology = UR.Technology;
            SupportProvided = UR.SupportProvided;
            ConcernPersonEmailIDSysDICE = UR.ConcernPersonEmailIDSysDICE;
            Price = UR.Price;
            Purchasedate = UR.Purchasedate;
            Renewaldate = UR.Renewaldate;
            ExpireDate = UR.ExpireDate;
            OfferApplied = UR.OfferApplied;
            LicenceKey = UR.LicenceKey;
            LicenceType = UR.LicenceType;
            CurrentStatus = UR.CurrentStatus;
            DataBackup = UR.DataBackup;
            PathoLAB = UR.PathoLAB;
            Dental = UR.Dental;
            Sacnning = UR.Sacnning;
            Other = UR.Other;
            CreatedBy = UR.CreatedBy;

        }
        //[BsonId]
        //[BsonIgnoreIfDefault]
        public Guid Id { get; set; }
        public string UnqueID { get; set; }
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