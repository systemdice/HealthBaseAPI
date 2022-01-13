
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.GenericRepository.ViewModel;
using System;
using System.Collections.Generic;
using MongoDB.GenericRepository.Utility;

namespace MongoDB.GenericRepository.Model
{
    public class InventoryMaster
    {
        public InventoryMaster(InventoryMasterViewModel UR)
        {
            Id = Guid.NewGuid();
            UnqueID = CommonMethods.GetUniqueID(); ; // DateTime.Now.Second + DateTime.Now.Millisecond + (new Random().Next(10, 5000) + DateTime.Now.Minute).ToString(); //new Random().Next(10, 5000).ToString();
            DateStart = DateTime.Now.ToString("dd-MM-yyyy"); // UR.DateStart;
            inventoryID = CommonMethods.GetUniqueID(); // DateTime.Now.Second+ DateTime.Now.Millisecond+ (new Random().Next(10, 500)+ DateTime.Now.Minute).ToString(); //UR.inventoryID;
            itemName = UR.itemName;
            stockQty = UR.stockQty;
            reorderQty = UR.reorderQty;
            DateReorder = UR.DateReorder;
            priorityStatus = UR.priorityStatus;
            ExpireMonth = UR.ExpireMonth;
            ExpireYear = UR.ExpireYear;
            RackPlaceLocation = UR.RackPlaceLocation;

            BarCode = UR.BarCode;
            ItemCode = UR.ItemCode;
            UnitPrice = UR.UnitPrice;
            SellPrice = UR.SellPrice; 
            Discount = UR.Discount;
            SGST = UR.SGST;
            CGST = UR.CGST;
            IGST = UR.IGST;
            Extra = UR.Extra;
            Others = UR.Others;
            HSNCode = UR.HSNCode;
            UOM = UR.UOM;
            BatchNumber = UR.BatchNumber;
            expiryDate = UR.expiryDate;
            EntryOwner = UR.EntryOwner;
            InventoryOwner = UR.InventoryOwner;
            ProfitPrice = UR.ProfitPrice;
            TotalGST = UR.TotalGST;
            UpdatedBy = UR.UpdatedBy;

        }

        public InventoryMaster(string updateUniqueaID, InventoryMasterViewModel UR)
        {
            //Id = id;
            UnqueID = updateUniqueaID;
            DateStart = DateTime.Now.ToString("dd-MM-yyyy");  // UR.DateStart;
            inventoryID = UR.inventoryID;
            itemName = UR.itemName;
            stockQty = UR.stockQty;
            reorderQty = UR.reorderQty;
            DateReorder = UR.DateReorder;
            priorityStatus = UR.priorityStatus;
            ExpireMonth = UR.ExpireMonth;
            ExpireYear = UR.ExpireYear;
            RackPlaceLocation = UR.RackPlaceLocation;

            BarCode = UR.BarCode;
            ItemCode = UR.ItemCode;
            UnitPrice = UR.UnitPrice;
            SellPrice = UR.SellPrice;
            Discount = UR.Discount;
            SGST = UR.SGST;
            CGST = UR.CGST;
            IGST = UR.IGST;
            Extra = UR.Extra;
            Others = UR.Others;
            HSNCode = UR.HSNCode;
            UOM = UR.UOM;
            BatchNumber = UR.BatchNumber;
            expiryDate = UR.expiryDate;
        EntryOwner = UR.EntryOwner;
            InventoryOwner = UR.InventoryOwner;
            ProfitPrice = UR.ProfitPrice;
            TotalGST = UR.TotalGST;
            UpdatedBy = UR.UpdatedBy;
        }
        //[BsonId]
        //[BsonIgnoreIfDefault]
        public Guid Id { get; set; }
        public string UnqueID { get; set; }


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
    public class InventoryFew
    {
        public string UnqueID { get; set; }


        public string inventoryID { get; set; }
        public string itemName { get; set; }
        public string stockQty { get; set; }
       public string ExpireMonth { get; set; }
        public string ExpireYear { get; set; }
        
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
       
        public string TotalGST { get; set; }
    }
    



}





