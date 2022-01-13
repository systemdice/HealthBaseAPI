using Microsoft.AspNetCore.Mvc;
using MongoDB.GenericRepository.Interfaces;
using MongoDB.GenericRepository.Model;
using MongoDB.GenericRepository.Utility;
using MongoDB.GenericRepository.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MongoDB.GenericRepository.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoryMasterController : ControllerBase
    {
        private readonly ICategoryMasterRepository _categoryMasterRepository;
        private readonly IUnitOfWork _uow;

        public CategoryMasterController(ICategoryMasterRepository categoryMasterRepository, IUnitOfWork uow)
        {
            _categoryMasterRepository = categoryMasterRepository;
            _uow = uow;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryMaster>>> Get()
        {
            var categoryMasters = await _categoryMasterRepository.GetAll();
            return Ok(categoryMasters);
        }
        [HttpGet("{UnqueID}")]
        public async Task<ActionResult<CategoryMaster>> Get(string UnqueID)
        {
            var categoryMaster = await _categoryMasterRepository.GetById(UnqueID);
            return Ok(categoryMaster);
        }

        [HttpGet("{id}/{UnqueID}")]
        public async Task<string> GetName(int id, string UnqueID)
        {
            var categoryMaster = await _categoryMasterRepository.GetById(UnqueID);
            return categoryMaster.Name;
        }








        [HttpPost, Route("PostSimulatingError")]
        public IActionResult PostSimulatingError([FromBody] CategoryMasterViewModel value)
        {
            var categoryMaster = new CategoryMaster(value);
            _categoryMasterRepository.Add(categoryMaster);

            // The categoryMaster will not be added
            return BadRequest();
        }

        [HttpPost]
        public async Task<ActionResult<CategoryMaster>> Post([FromBody] CategoryMasterViewModel value)
        {

            var categoryMaster = new CategoryMaster(value);
            // var categoryMaster = new CategoryMaster(value.Notes, value.Name, value.UnqueID);
            _categoryMasterRepository.Add(categoryMaster);

            // it will be null
            var testCategoryMaster = await _categoryMasterRepository.GetById(categoryMaster.UnqueID);

            // If everything is ok then:
            await _uow.Commit();

            // The categoryMaster will be added only after commit
            testCategoryMaster = await _categoryMasterRepository.GetById(categoryMaster.UnqueID);

            return Ok(testCategoryMaster);
        }

        [HttpPut("{UnqueID}")]
        public async Task<ActionResult<CategoryMaster>> Put(string UnqueID, [FromBody] CategoryMasterViewModel value)
        {
            //var categoryMaster1 = await _categoryMasterRepository.GetById(UnqueID);

            ////string aa = new CommonUtility(_categoryMasterRepository, _uow).GetID(UnqueID).ToString();
            ////var a = _categoryMasterRepository.GetById(value.UnqueID, value.UnqueID);
            //var categoryMaster = new CategoryMaster(categoryMaster1.Id, value.Notes, value.Name, value.UnqueID);

            //_categoryMasterRepository.Update(categoryMaster, UnqueID);
            var categoryMaster = new CategoryMaster(UnqueID, value);

            _categoryMasterRepository.Update(categoryMaster, categoryMaster.UnqueID);

            await _uow.Commit();

            return Ok(await _categoryMasterRepository.GetById(value.UnqueID));
        }

        [HttpDelete("{UnqueID}")]
        public async Task<ActionResult> Delete(string UnqueID)
        {
            _categoryMasterRepository.Remove(UnqueID);

            // it won't be null
            var testCategoryMaster = await _categoryMasterRepository.GetById(UnqueID);

            // If everything is ok then:
            await _uow.Commit();

            // not it must by null
            //testCategoryMaster = await _categoryMasterRepository.GetById("153");

            return Ok();
        }
    }
}
