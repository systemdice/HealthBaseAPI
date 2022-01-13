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
    public class UnitsCategoryController : ControllerBase
    {
        private readonly IUnitsCategoryRepository _unitsCategoryRepository;
        private readonly IUnitOfWork _uow;

        public UnitsCategoryController(IUnitsCategoryRepository unitsCategoryRepository, IUnitOfWork uow)
        {
            _unitsCategoryRepository = unitsCategoryRepository;
            _uow = uow;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UnitsCategory>>> Get()
        {
            var unitsCategorys = await _unitsCategoryRepository.GetAll();
            return Ok(unitsCategorys);
        }
        [HttpGet("{UnqueID}")]
        public async Task<ActionResult<UnitsCategory>> Get(string UnqueID)
        {
            var unitsCategory = await _unitsCategoryRepository.GetById(UnqueID);
            return Ok(unitsCategory);
        }

        [HttpGet("{id}/{UnqueID}")]
        public async Task<string> GetName(int id, string UnqueID)
        {
            var unitsCategory = await _unitsCategoryRepository.GetById(UnqueID);
            return unitsCategory.Name;
        }








        [HttpPost, Route("PostSimulatingError")]
        public IActionResult PostSimulatingError([FromBody] UnitsCategoryViewModel value)
        {
            var unitsCategory = new UnitsCategory(value);
            _unitsCategoryRepository.Add(unitsCategory);

            // The unitsCategory will not be added
            return BadRequest();
        }

        [HttpPost]
        public async Task<ActionResult<UnitsCategory>> Post([FromBody] UnitsCategoryViewModel value)
        {

            var unitsCategory = new UnitsCategory(value);
            // var unitsCategory = new UnitsCategory(value.Notes, value.Name, value.UnqueID);
            _unitsCategoryRepository.Add(unitsCategory);

            // it will be null
            var testUnitsCategory = await _unitsCategoryRepository.GetById(unitsCategory.UnqueID);

            // If everything is ok then:
            await _uow.Commit();

            // The unitsCategory will be added only after commit
            testUnitsCategory = await _unitsCategoryRepository.GetById(unitsCategory.UnqueID);

            return Ok(testUnitsCategory);
        }

        [HttpPut("{UnqueID}")]
        public async Task<ActionResult<UnitsCategory>> Put(string UnqueID, [FromBody] UnitsCategoryViewModel value)
        {
            //var unitsCategory1 = await _unitsCategoryRepository.GetById(UnqueID);

            ////string aa = new CommonUtility(_unitsCategoryRepository, _uow).GetID(UnqueID).ToString();
            ////var a = _unitsCategoryRepository.GetById(value.UnqueID, value.UnqueID);
            //var unitsCategory = new UnitsCategory(unitsCategory1.Id, value.Notes, value.Name, value.UnqueID);

            //_unitsCategoryRepository.Update(unitsCategory, UnqueID);
            var unitsCategory = new UnitsCategory(UnqueID, value);

            _unitsCategoryRepository.Update(unitsCategory, unitsCategory.UnqueID);

            await _uow.Commit();

            return Ok(await _unitsCategoryRepository.GetById(value.UnqueID));
        }

        [HttpDelete("{UnqueID}")]
        public async Task<ActionResult> Delete(string UnqueID)
        {
            _unitsCategoryRepository.Remove(UnqueID);

            // it won't be null
            var testUnitsCategory = await _unitsCategoryRepository.GetById(UnqueID);

            // If everything is ok then:
            await _uow.Commit();

            // not it must by null
            //testUnitsCategory = await _unitsCategoryRepository.GetById("153");

            return Ok();
        }
    }
}
