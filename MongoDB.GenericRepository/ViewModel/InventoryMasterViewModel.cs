using MongoDB.GenericRepository.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoDB.GenericRepository.ViewModel
{
    public class InventoryMasterViewModel
    {
        public string UnqueID { get; set; }
        public bool ShouldCommit { get; set; } = true;

        public string DateStart { get; set; } = new DateTime().ToShortDateString();
        public string inventoryID { get; set; }
        public string itemName { get; set; }
        public string stockQty { get; set; }
        public string reorderQty { get; set; }
        public string DateReorder { get; set; }
        public int priorityStatus { get; set; }
        public string ExpireMonth { get; set; }
        public string ExpireYear { get; set; }
        public string RackPlaceLocation { get; set; }
        public string BarCode { get; set; }
        public string ItemCode { get; set; }
        public string UnitPrice { get; set; } 
        public string SellPrice { get; set; }
        public string Discount { get; set; } = "0";
        public string SGST { get; set; } 
        public string CGST { get; set; } 
        public string IGST { get; set; } 
        public string Extra { get; set; } 
        public string Others { get; set; }

        public string HSNCode { get; set; }
        public string UOM { get; set; }
        public string BatchNumber { get; set; }
        public string expiryDate { get; set; }

        public string EntryOwner { get; set; }
        public string InventoryOwner { get; set; }
        public string ProfitPrice { get; set; }
        public string TotalGST { get; set; }
        public string UpdatedBy { get; set; }


    }
}
