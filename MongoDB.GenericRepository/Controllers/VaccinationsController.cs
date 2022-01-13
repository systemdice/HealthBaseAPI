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
    public class VaccinationsController : ControllerBase
    {
        private readonly IVaccinationsRepository _vaccinationsRepository;
        private readonly IUnitOfWork _uow;

        public VaccinationsController(IVaccinationsRepository vaccinationsRepository, IUnitOfWork uow)
        {
            _vaccinationsRepository = vaccinationsRepository;
            _uow = uow;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vaccinations>>> Get()
        {
            var vaccinationss = await _vaccinationsRepository.GetAll();
            var orderByDescendingResult = from s in vaccinationss
                                          orderby s.SlNO descending
                                          select s;
            return Ok(orderByDescendingResult);
        }
        [HttpGet("{UnqueID}")]
        public async Task<ActionResult<Vaccinations>> Get(string UnqueID)
        {
            var vaccinations = await _vaccinationsRepository.GetById(UnqueID);
            return Ok(vaccinations);
        }

        [HttpGet("{id}/{UnqueID}")]
        public async Task<string> GetName(int id, string UnqueID)
        {
            var vaccinations = await _vaccinationsRepository.GetById(UnqueID);
            return vaccinations.Name;
        }








        [HttpPost, Route("PostSimulatingError")]
        public IActionResult PostSimulatingError([FromBody] VaccinationsViewModel value)
        {
            var vaccinations = new Vaccinations(value);
            _vaccinationsRepository.Add(vaccinations);

            // The vaccinations will not be added
            return BadRequest();
        }

        [HttpPost]
        public async Task<ActionResult<Vaccinations>> Post([FromBody] VaccinationsViewModel value)
        {
            var newModifyCases = await _vaccinationsRepository.GetAll();
            string VaccinationCountString = (newModifyCases.Count() == 0 ? 1 : newModifyCases.Count() + 1).ToString();

            value.VSID = "VS" + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString().Substring(DateTime.Now.Year.ToString().Length - 2) + "-" + VaccinationCountString;
            value.SlNO = VaccinationCountString;
            value.Expiry = value.ExpiryMonth+"-"+value.ExpiryYear;


            int CurrentYear = DateTime.Now.Year;
            int CurrentMonth = DateTime.Now.Month;

            int YearDiff = Convert.ToInt32(value.ExpiryYear) - CurrentYear;
            int MonthDiff = Convert.ToInt32(value.ExpiryMonth) - CurrentMonth;

            if (YearDiff >= 0)
            {
                if (YearDiff == 0)
                {
                    if (MonthDiff < 0)
                    {
                        value.Expiry = "expired";
                        value.ExpiryDayRemaining = "0";
                    }
                    else if (MonthDiff >= 0)
                    {
                        value.ExpiryStatus = "not expired";
                        value.ExpiryDayRemaining = (YearDiff * 12 + MonthDiff).ToString();
                    }
                }
                else if (YearDiff > 0 && YearDiff <= 1)
                {

                    value.ExpiryStatus = "not expired";
                    //(((12 - Convert.ToInt32(CurrentMonth))) + 12 * YearDiff).ToString()  04/2021 -- 07-2022
                    value.ExpiryDayRemaining = (((12 - Convert.ToInt32(CurrentMonth))) + Convert.ToInt32(value.ExpiryMonth)).ToString(); // (YearDiff * 12 + Convert.ToInt32(value.ExpiryMonth)).ToString();
                }
                else if (YearDiff > 1)
                {
                    //04/2021 -- 04-2024 8+24+4
                    value.ExpiryStatus = "not expired";
                    value.ExpiryDayRemaining = (((12 - Convert.ToInt32(CurrentMonth))) + (12 * (YearDiff - 1)) + Convert.ToInt32(value.ExpiryMonth)).ToString();
                    //value.ExpiryDayRemaining = (((12 - Convert.ToInt32(value.ExpiryMonth)) * YearDiff) + CurrentMonth).ToString(); // (YearDiff * 12 + Convert.ToInt32(value.ExpiryMonth)).ToString();
                }

            }
            else
            {
                value.Expiry = "expired";
                value.ExpiryDayRemaining = "0";
            }


            var vaccinations = new Vaccinations(value);
            // var vaccinations = new Vaccinations(value.Notes, value.Name, value.UnqueID);
            _vaccinationsRepository.Add(vaccinations);


            // it will be null
            var testVaccinations = await _vaccinationsRepository.GetById(vaccinations.UnqueID);

            // If everything is ok then:
            await _uow.Commit();

            // The vaccinations will be added only after commit
            testVaccinations = await _vaccinationsRepository.GetById(vaccinations.UnqueID);

            return Ok(testVaccinations);
        }

        [HttpPut("{UnqueID}")]
        public async Task<ActionResult<Vaccinations>> Put(string UnqueID, [FromBody] VaccinationsViewModel value)
        {
            //var vaccinations1 = await _vaccinationsRepository.GetById(UnqueID);

            ////string aa = new CommonUtility(_vaccinationsRepository, _uow).GetID(UnqueID).ToString();
            ////var a = _vaccinationsRepository.GetById(value.UnqueID, value.UnqueID);
            //var vaccinations = new Vaccinations(vaccinations1.Id, value.Notes, value.Name, value.UnqueID);

            //_vaccinationsRepository.Update(vaccinations, UnqueID);
            //var vaccinations = new Vaccinations(UnqueID, value);

            value.Expiry = value.ExpiryMonth + "-" + value.ExpiryYear;
            //var vaccinations = new Vaccinations(value);
            // var vaccinations = new Vaccinations(value.Notes, value.Name, value.UnqueID);
            // _vaccinationsRepository.Add(vaccinations);

            int CurrentYear = DateTime.Now.Year;
            int CurrentMonth = DateTime.Now.Month;

            int YearDiff = Convert.ToInt32(value.ExpiryYear) - CurrentYear;
            int MonthDiff = Convert.ToInt32(value.ExpiryMonth) - CurrentMonth;

            if (YearDiff >= 0)
            {
                if (YearDiff ==0)
                {
                    if (MonthDiff < 0)
                    {
                        value.Expiry = "expired";
                        value.ExpiryDayRemaining = "0";
                    }
                    else if (MonthDiff >= 0)
                    {
                        value.ExpiryStatus = "not expired";
                        value.ExpiryDayRemaining = (YearDiff * 12 + MonthDiff).ToString();
                    }
                }
                else if(YearDiff > 0 && YearDiff <=1 )
                {

                    value.ExpiryStatus = "not expired";
                    //(((12 - Convert.ToInt32(CurrentMonth))) + 12 * YearDiff).ToString()  04/2021 -- 07-2022
                    value.ExpiryDayRemaining = (((12 - Convert.ToInt32(CurrentMonth))) + Convert.ToInt32(value.ExpiryMonth)).ToString(); // (YearDiff * 12 + Convert.ToInt32(value.ExpiryMonth)).ToString();
                }
                else if (YearDiff >  1)
                {
                    //04/2021 -- 04-2024 8+24+4
                    value.ExpiryStatus = "not expired";
                    value.ExpiryDayRemaining = (((12 - Convert.ToInt32(CurrentMonth))) + (12 * (YearDiff - 1)) + Convert.ToInt32(value.ExpiryMonth)).ToString();
                    //value.ExpiryDayRemaining = (((12 - Convert.ToInt32(value.ExpiryMonth)) * YearDiff) + CurrentMonth).ToString(); // (YearDiff * 12 + Convert.ToInt32(value.ExpiryMonth)).ToString();
                }

            }
            else
            {
                value.Expiry = "expired";
                value.ExpiryDayRemaining = "0";
            }

            var vaccinations = new Vaccinations(UnqueID, value);

            _vaccinationsRepository.Update(vaccinations, vaccinations.UnqueID);

            await _uow.Commit();

            return Ok(await _vaccinationsRepository.GetById(value.UnqueID));
        }

        [HttpDelete("{UnqueID}")]
        public async Task<ActionResult> Delete(string UnqueID)
        {
            _vaccinationsRepository.Remove(UnqueID);

            // it won't be null
            var testVaccinations = await _vaccinationsRepository.GetById(UnqueID);

            // If everything is ok then:
            await _uow.Commit();

            // not it must by null
            //testVaccinations = await _vaccinationsRepository.GetById("153");

            return Ok();
        }
    }
}
