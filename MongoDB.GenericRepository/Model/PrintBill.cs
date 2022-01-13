using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.GenericRepository.Utility;
using MongoDB.GenericRepository.ViewModel;
using System;

namespace MongoDB.GenericRepository.Model
{
    public class PrintBill
    {
        public PrintBill(PrintBillViewModel UR)
        {
            Id = Guid.NewGuid();
            UnqueID = CommonMethods.GetUniqueIDShorter(); //new Random().Next(10, 500).ToString();            
            Notes = UR.Notes;
            PrintBillName = UR.PrintBillName;
            PrintBillType = UR.PrintBillType;
            PrintBillStatus = UR.PrintBillStatus;
            Date = UR.Date;
            BillNo = UR.BillNo;
            BillID = UR.BillID;
            PaymentHistorySingle = UR.PaymentHistorySingle;
        }

        public PrintBill(string updateUniqueaID, PrintBillViewModel UR)
        {
            //Id = id;
            UnqueID = updateUniqueaID;
            Notes = UR.Notes;
            PrintBillName = UR.PrintBillName;
            PrintBillType = UR.PrintBillType;
            PrintBillStatus = UR.PrintBillStatus;
            Date = UR.Date;
            BillNo = UR.BillNo;
            BillID = UR.BillID;
            PaymentHistorySingle = UR.PaymentHistorySingle;
    }

        //[BsonId]
        //[BsonIgnoreIfDefault]
        public Guid Id { get; set; }
        public string UnqueID { get; set; }

        public string Notes { get; set; }

        public string PrintBillName { get; set; }
        public string PrintBillType { get; set; }
        public string PrintBillStatus { get; set; }

        public string Date { get; set; }
        public string BillNo { get; set; }
        public string BillID { get; set; }
        public PaymentHistorySingle PaymentHistorySingle { get; set; }



    }
}

