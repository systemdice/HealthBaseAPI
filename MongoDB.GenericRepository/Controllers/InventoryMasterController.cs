using Microsoft.AspNetCore.Mvc;
using MongoDB.GenericRepository.Interfaces;
using MongoDB.GenericRepository.Model;
using MongoDB.GenericRepository.Utility;
using MongoDB.GenericRepository.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoDB.GenericRepository.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class InventoryMasterController : ControllerBase
    {
        private readonly IInventoryMasterRepository _inventoryMasterRepository;
        private readonly IUnitOfWork _uow;

        public InventoryMasterController(IInventoryMasterRepository inventoryMasterRepository, IUnitOfWork uow)
        {
            _inventoryMasterRepository = inventoryMasterRepository;
            _uow = uow;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<InventoryMaster>>> Get()
        {
            var inventoryMasters = await _inventoryMasterRepository.GetAll();

            int currentMonth = DateTime.Now.Month;
            int currentYear = DateTime.Now.Year;

            var lstFutureYears = inventoryMasters.Where(x => (Convert.ToInt16(x.ExpireYear) - currentYear) > 0);
            //var pp = lst.Count();
            var lstSameYear = (inventoryMasters.Where(x => (Convert.ToInt16(x.ExpireYear) - currentYear) == 0));
            //var kk = lst1.Count();
            var lstMonthComparision = lstSameYear.Where(p=> Convert.ToInt16(p.ExpireMonth)- currentMonth >= 0);


            var finalList = lstFutureYears.Union(lstMonthComparision);


           



            return Ok(finalList);
        }

        [HttpGet]
        [Route("GetCategoryWise/{CategoryName}/{entryowner}")]
        public async Task<ActionResult<IEnumerable<InventoryMaster>>> GetCategoryWise(string CategoryName,string entryowner)
        {
            //var inventoryMasters = await _inventoryMasterRepository.GetAll();
            var inventoryMasters = await _inventoryMasterRepository.GetAllPharmaFilter(entryowner,"");
            inventoryMasters = inventoryMasters.Where(a => a.EntryOwner == entryowner)
                .OrderBy(x => x.itemName);
            if (CategoryName != "All") {
                inventoryMasters = inventoryMasters.Where(a => a.Others == CategoryName);
            }
            int currentMonth = DateTime.Now.Month;
            int currentYear = DateTime.Now.Year;

            var lstFutureYears = inventoryMasters.Where(x => (Convert.ToInt16(x.ExpireYear) - currentYear) > 0);
            //var pp = lst.Count();
            var lstSameYear = (inventoryMasters.Where(x => (Convert.ToInt16(x.ExpireYear) - currentYear) == 0));
            //var kk = lst1.Count();
            var lstMonthComparision = lstSameYear.Where(p => Convert.ToInt16(p.ExpireMonth) - currentMonth >= 0);


            var finalList = lstFutureYears.Union(lstMonthComparision);

            var res = from c in finalList
                    select new
                    {
                        UnqueID = c.UnqueID,
                        inventoryID = c.inventoryID,
                        itemName = c.itemName,
                        stockQty = c.stockQty,
                        ExpireMonth = c.ExpireMonth,
                        ExpireYear = c.ExpireYear,
                        BarCode = c.BarCode,
                        ItemCode = c.ItemCode,
                        UnitPrice = c.UnitPrice,
                        SellPrice = c.SellPrice,
                        Discount = c.Discount,
                        SGST = c.SGST,
                        CGST = c.CGST,
                        IGST = c.IGST,
                        Extra = c.Extra,
                        Others = c.Others,
                        HSNCode = c.HSNCode,
                        UOM = c.UOM,
                        BatchNumber = c.BatchNumber,
                        expiryDate = c.expiryDate,
                        EntryOwner = c.EntryOwner,
                        TotalGST = c.TotalGST
                    };




            return Ok(res);
        }

        [HttpGet]
        [Route("GetCategoryWiseQuickLook/{CategoryName}/{entryowner}")]
        public async Task<ActionResult<IEnumerable<InventoryMaster>>> GetCategoryWiseQuickLook(string CategoryName, string entryowner)
        {
            var inventoryMasters = await _inventoryMasterRepository.GetAllPharmaFilter(entryowner,"");
            inventoryMasters = inventoryMasters.Where(a => a.EntryOwner == entryowner)
                .OrderBy(x => x.itemName); 
            if (CategoryName != "All")
            {
                inventoryMasters = inventoryMasters.Where(a => a.Others == CategoryName);
            }
            int currentMonth = DateTime.Now.Month;
            int currentYear = DateTime.Now.Year;

            var lstFutureYears = inventoryMasters.Where(x => (Convert.ToInt16(x.ExpireYear) - currentYear) > 0);
            //var pp = lst.Count();
            var lstSameYear = (inventoryMasters.Where(x => (Convert.ToInt16(x.ExpireYear) - currentYear) == 0));
            //var kk = lst1.Count();
            var lstMonthComparision = lstSameYear.Where(p => Convert.ToInt16(p.ExpireMonth) - currentMonth >= 0);


            var finalList = lstFutureYears.Union(lstMonthComparision);

            var res = from c in finalList
                      select new
                      {
                          Others = c.Others,
                          inventoryID = c.inventoryID,
                          itemName = c.itemName,                         
                          BarCode = c.BarCode,
                          ItemCode = c.ItemCode,                          
                      };




            return Ok(res);
        }

        [HttpGet("{UnqueID}")]
        public async Task<ActionResult<InventoryMaster>> Get(string UnqueID)
        {
            var inventoryMaster = await _inventoryMasterRepository.GetById(UnqueID);
            return Ok(inventoryMaster);
        }
        [Route("[action]/{DateStart}")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InventoryMaster>>> GetByDateStart(string DateStart)
        {
            var inventoryMasters = await _inventoryMasterRepository.GetAll();
            var available = from c in inventoryMasters where c.DateStart == DateStart select c;


            //DateTime frmdt = Convert.ToDateTime("2020-11-27T18:30:00.000Z");
            //DateTime frmdt = Convert.ToDateTime(DateStart);
            //string frmdtString = frmdt.ToString("dd-MM-yyy");
            //Console.WriteLine(frmdtString);
            var availableFilter = inventoryMasters.Where(c => c.DateStart != null);
            //var totalAvailable = availableFilter.Where(c => Convert.ToDateTime(c.ReferralMaster.appointment).ToString("dd-MM-yyy") == frmdtString);
            var totalAvailable = availableFilter.Where(c => c.DateStart.Trim() == DateStart.Trim());
            //var availableCars = inventoryMasters.Where(c => c.IsAvailable).ToArray();
            return Ok(totalAvailable); // inventoryMaster.PatientDetails.;
        }

        //[HttpGet("{DateStart}")]
        //public async Task<ActionResult<InventoryMaster>> Get(string DateStart)
        //{
        //    var inventoryMaster = await _inventoryMasterRepository.GetById(UnqueID);
        //    return Ok(inventoryMaster);
        //}

        [HttpGet("{id}/{UnqueID}")]
        public async Task<string> GetName(int id, string UnqueID)
        {
            var inventoryMaster = await _inventoryMasterRepository.GetById(UnqueID);
            return "PatientName"; // inventoryMaster.PatientDetails.;
        }


        [HttpGet("{UnqueID}/{DateStart}")]
        public async Task<string> GetName(string UnqueID, string DateStart)
        {
            var inventoryMaster = await _inventoryMasterRepository.GetByIdDateStartOnly(DateStart);
            return "PatientName"; // inventoryMaster.PatientDetails.;
        }









        [HttpPost, Route("PostSimulatingError")]
        public IActionResult PostSimulatingError([FromBody] InventoryMasterViewModel value)
        {
            var inventoryMaster = new InventoryMaster(value);
            _inventoryMasterRepository.Add(inventoryMaster);

            // The inventoryMaster will not be added
            return BadRequest();
        }

        [HttpPost]
        public async Task<ActionResult<InventoryMaster>> Post([FromBody] InventoryMasterViewModel value)
        {

            var inventoryMaster = new InventoryMaster(value);
            // var inventoryMaster = new InventoryMaster(value.Notes, value.Name, value.UnqueID);
            _inventoryMasterRepository.Add(inventoryMaster);

            // it will be null
            var testInventoryMaster = await _inventoryMasterRepository.GetById(inventoryMaster.UnqueID);

            // If everything is ok then:
            await _uow.Commit();

            // The inventoryMaster will be added only after commit
            testInventoryMaster = await _inventoryMasterRepository.GetById(inventoryMaster.UnqueID);

            return Ok(testInventoryMaster);
        }

        [HttpPut("{UnqueID}")]
        public async Task<ActionResult<InventoryMaster>> Put(string UnqueID, [FromBody] InventoryMasterViewModel value)
        {
            //var inventoryMaster1 = await _inventoryMasterRepository.GetById(UnqueID);

            ////string aa = new CommonUtility(_inventoryMasterRepository, _uow).GetID(UnqueID).ToString();
            ////var a = _inventoryMasterRepository.GetById(value.UnqueID, value.UnqueID);
            //var inventoryMaster = new InventoryMaster(inventoryMaster1.Id, value.Notes, value.Name, value.UnqueID);

            //_inventoryMasterRepository.Update(inventoryMaster, UnqueID);
            var inventoryMaster = new InventoryMaster(UnqueID, value);

            _inventoryMasterRepository.Update(inventoryMaster, inventoryMaster.UnqueID);

            await _uow.Commit();

            return Ok(await _inventoryMasterRepository.GetById(value.UnqueID));
        }

        [HttpDelete("{UnqueID}")]
        public async Task<ActionResult> Delete(string UnqueID)
        {
            _inventoryMasterRepository.Remove(UnqueID);

            // it won't be null
            var testInventoryMaster = await _inventoryMasterRepository.GetById(UnqueID);

            // If everything is ok then:
            await _uow.Commit();

            // not it must by null
            //testInventoryMaster = await _inventoryMasterRepository.GetById("153");

            return Ok();
        }
    }
}
