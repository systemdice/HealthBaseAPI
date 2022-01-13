using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.GenericRepository.Utility;
using MongoDB.GenericRepository.ViewModel;
using System;

namespace MongoDB.GenericRepository.Model
{
    public class Expenses
    {
        public Expenses(ExpensesViewModel UR)
        {
            Id = Guid.NewGuid();
            UnqueID = CommonMethods.GetUniqueIDShorter(); //new Random().Next(10, 500).ToString();            
            
             ExpenseAmount = UR.ExpenseAmount;

             Date = UR.Date;

             ExpenseCategory = UR.ExpenseCategory;

             Notes = UR.Notes;

             CategoryName = UR.BusinessType;
            BusinessType = UR.BusinessType;

        }

        public Expenses(string updateUniqueaID, ExpensesViewModel UR)
        {
            //Id = id;
            UnqueID = updateUniqueaID;
            ExpenseAmount = UR.ExpenseAmount;

            Date = UR.Date;

            ExpenseCategory = UR.ExpenseCategory;

            Notes = UR.Notes;

            CategoryName = UR.CategoryName;
            BusinessType = UR.BusinessType;
        }

        //[BsonId]
        //[BsonIgnoreIfDefault]
        public Guid Id { get; set; }
        public string UnqueID { get; set; }
        
        public decimal ExpenseAmount { get; set; }

        public string Date { get; set; }

        public string ExpenseCategory { get; set; }

        public string Notes { get; set; }
        
        public string CategoryName { get; set; }

        public string BusinessType { get; set; }

    }
}
