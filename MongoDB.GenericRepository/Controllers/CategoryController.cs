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
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _uow;

        public CategoryController(ICategoryRepository categoryRepository, IUnitOfWork uow)
        {
            _categoryRepository = categoryRepository;
            _uow = uow;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> Get()
        {
            var categorys = await _categoryRepository.GetAll();
            return Ok(categorys);
        }
        [HttpGet]
        [Route("getDetailType/{mainCategory}")]
        public async Task<ActionResult<IEnumerable<Category>>> getDetailType(string mainCategory)
        {
            var categorys = await _categoryRepository.GetAllCategoryWithType(mainCategory);
            return Ok(categorys.Where(a=> a.CategoryType == mainCategory));
        }
        [HttpGet("{UnqueID}")]
        public async Task<ActionResult<Category>> Get(string UnqueID)
        {
            var category = await _categoryRepository.GetById(UnqueID);
            return Ok(category);
        }
        [Route("[action]/{DateStart}")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetByDateStart(string DateStart)
        {
            var categorys = await _categoryRepository.GetAll();
            var available = from c in categorys where c.Date == DateStart select c;


            //DateTime frmdt = Convert.ToDateTime("2020-11-27T18:30:00.000Z");
            //DateTime frmdt = Convert.ToDateTime(DateStart);
            //string frmdtString = frmdt.ToString("dd-MM-yyy");
            //Console.WriteLine(frmdtString);
            var availableFilter = categorys.Where(c => c.CategoryName != null);
            //var totalAvailable = availableFilter.Where(c => Convert.ToDateTime(c.ReferralMaster.appointment).ToString("dd-MM-yyy") == frmdtString);
            var totalAvailable = availableFilter.Where(c => c.Date.Trim() == DateStart.Trim());
            //var availableCars = categorys.Where(c => c.IsAvailable).ToArray();
            return Ok(totalAvailable); // category.PatientDetails.;
        }

        //[HttpGet("{DateStart}")]
        //public async Task<ActionResult<Category>> Get(string DateStart)
        //{
        //    var category = await _categoryRepository.GetById(UnqueID);
        //    return Ok(category);
        //}

        [HttpGet("{id}/{UnqueID}")]
        public async Task<string> GetName(int id, string UnqueID)
        {
            var category = await _categoryRepository.GetById(UnqueID);
            return "PatientName"; // category.PatientDetails.;
        }


        [HttpGet("{UnqueID}/{DateStart}")]
        public async Task<string> GetName(string UnqueID, string DateStart)
        {
            var category = await _categoryRepository.GetByIdDateStartOnly(DateStart);
            return "PatientName"; // category.PatientDetails.;
        }









        [HttpPost, Route("PostSimulatingError")]
        public IActionResult PostSimulatingError([FromBody] CategoryViewModel value)
        {
            var category = new Category(value);
            _categoryRepository.Add(category);

            // The category will not be added
            return BadRequest();
        }

        [HttpPost]
        public async Task<ActionResult<Category>> Post([FromBody] CategoryViewModel value)
        {

            var category = new Category(value);
            // var category = new Category(value.Notes, value.Name, value.UnqueID);
            _categoryRepository.Add(category);

            // it will be null
            var testCategory = await _categoryRepository.GetById(category.UnqueID);

            // If everything is ok then:
            await _uow.Commit();

            // The category will be added only after commit
            testCategory = await _categoryRepository.GetById(category.UnqueID);

            return Ok(testCategory);
        }

        [HttpPut("{UnqueID}")]
        public async Task<ActionResult<Category>> Put(string UnqueID, [FromBody] CategoryViewModel value)
        {
            //var category1 = await _categoryRepository.GetById(UnqueID);

            ////string aa = new CommonUtility(_categoryRepository, _uow).GetID(UnqueID).ToString();
            ////var a = _categoryRepository.GetById(value.UnqueID, value.UnqueID);
            //var category = new Category(category1.Id, value.Notes, value.Name, value.UnqueID);

            //_categoryRepository.Update(category, UnqueID);
            var category = new Category(UnqueID, value);

            _categoryRepository.Update(category, category.UnqueID);

            await _uow.Commit();

            return Ok(await _categoryRepository.GetById(value.UnqueID));
        }

        [HttpDelete("{UnqueID}")]
        public async Task<ActionResult> Delete(string UnqueID)
        {
            _categoryRepository.Remove(UnqueID);

            // it won't be null
            var testCategory = await _categoryRepository.GetById(UnqueID);

            // If everything is ok then:
            await _uow.Commit();

            // not it must by null
            //testCategory = await _categoryRepository.GetById("153");

            return Ok();
        }
    }
}
