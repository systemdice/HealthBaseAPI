using MongoDB.GenericRepository.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoDB.GenericRepository.ViewModel
{
    public class PrintBillViewModel
    {
        public string UnqueID { get; set; }
        public bool ShouldCommit { get; set; } = true;
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
