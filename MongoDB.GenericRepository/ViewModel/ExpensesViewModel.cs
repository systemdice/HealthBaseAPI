using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoDB.GenericRepository.ViewModel
{
    public class ExpensesViewModel
    {
        public string UnqueID { get; set; }
        public bool ShouldCommit { get; set; } = true;
        public decimal ExpenseAmount { get; set; }

        public string Date { get; set; }

        public string ExpenseCategory { get; set; }

        public string Notes { get; set; }

        public string CategoryName { get; set; }
        public string BusinessType { get; set; }
    }
}
