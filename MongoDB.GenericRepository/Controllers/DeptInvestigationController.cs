//using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
//using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.GenericRepository.Interfaces;
using MongoDB.GenericRepository.Model;
using MongoDB.GenericRepository.Utility;
using MongoDB.GenericRepository.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace MongoDB.GenericRepository.Controllers
{
    [Route("[controller]")]
    [ApiController]
    //[EnableCors(headers: "*", methods: "*", exposedHeaders: "X-My-Header")]
    public class DeptInvestigationController : ControllerBase
    {
        private readonly IDeptInvestigationRepository _deptInvestigationRepository;
        private readonly IUnitOfWork _uow;
        //private readonly IHostingEnvironment _HostEnvironment;
        readonly ILogger<MangeUserController> _log;


        public DeptInvestigationController(IDeptInvestigationRepository deptInvestigationRepository, IUnitOfWork uow, ILogger<MangeUserController> log)
        {
            _deptInvestigationRepository = deptInvestigationRepository;
            _uow = uow;
            //_HostEnvironment = HostEnvironment;
            _log = log;
        }

        string fullPath = @"/Resources/Log/LogActivity.txt";
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DeptInvestigation>>> Get()
        {
            //_log.LogInformation("Hello, world!");
            //string webRootPath = _HostEnvironment.WebRootPath;
            //string contentRootPath = _HostEnvironment.ContentRootPath;

            //string path = "";
            //path = Path.Combine(contentRootPath, fullPath);
            //try
            //{
            //    System.IO.File.AppendAllText(path, "new-->" + DateTime.Now.ToString() + Environment.NewLine);
            //}
            //catch (Exception ex)
            //{
            //    _log.LogInformation("Liku exception");
            //    _log.LogInformation(ex.Message);

            //}


            var deptInvestigation = await _deptInvestigationRepository.GetAll();
            return Ok(deptInvestigation);
        }
        [HttpGet("{UnqueID}/{single}")]
        public async Task<string> GetName(string UnqueID, string colName)
        {
            var deptInvestigation = await _deptInvestigationRepository.GetById(UnqueID);
            return deptInvestigation.EnteredBy;
        }

        [HttpGet("{name}/{filterColumnName}/{finTheUser}")]
        public async Task<ActionResult<DeptInvestigation>> GetUserLoginDetails(string name, string filterColumnName, string finTheUser)
        {
            var deptInvestigation = await _deptInvestigationRepository.GetByName(name, filterColumnName, finTheUser);
            return deptInvestigation;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DeptInvestigation>> Get(string UnqueID)
        {
            var deptInvestigation = await _deptInvestigationRepository.GetById(UnqueID);
            return Ok(deptInvestigation);
        }






        [HttpPost, Route("PostSimulatingError")]
        public IActionResult PostSimulatingError([FromBody] DeptInvestigationViewModel value)
        {
            var deptInvestigation = new DeptInvestigation(value);
            _deptInvestigationRepository.Add(deptInvestigation);

            // The userRegistration will not be added
            return BadRequest();
        }

        [HttpPost]
        public async Task<ActionResult<DeptInvestigation>> Post([FromBody] DeptInvestigationViewModel value)
        {
            var deptInvestigation = new DeptInvestigation(value);
            _deptInvestigationRepository.Add(deptInvestigation);

            // it will be null
            var testDeptInvestigation = await _deptInvestigationRepository.GetById(deptInvestigation.UnqueID);

            // If everything is ok then:
            await _uow.Commit();

            // The userRegistration will be added only after commit
            testDeptInvestigation = await _deptInvestigationRepository.GetById(deptInvestigation.UnqueID);

            return Ok(testDeptInvestigation);
        }

        [HttpPut("{UnqueID}")]
        public async Task<ActionResult<DeptInvestigation>> Put(string UnqueID, [FromBody] DeptInvestigationViewModel value)
        {
            //var userRegistration1 = await _deptInvestigationRepository.GetById(id, UnqueID);

            //string aa = new CommonUtility(_deptInvestigationRepository, _uow).GetID(UnqueID).ToString();
            //var a = _deptInvestigationRepository.GetById(value.UnqueID, value.UnqueID);
            var deptInvestigation = new DeptInvestigation(UnqueID, value);

            _deptInvestigationRepository.Update(deptInvestigation, deptInvestigation.UnqueID);

            await _uow.Commit();

            return Ok(await _deptInvestigationRepository.GetById(deptInvestigation.UnqueID));
        }

        [HttpDelete("{UnqueID}")]
        public async Task<ActionResult> Delete(string UnqueID)
        {
            _deptInvestigationRepository.Remove(UnqueID);

            // it won't be null
            var testDeptInvestigation = await _deptInvestigationRepository.GetById(UnqueID);

            // If everything is ok then:
            await _uow.Commit();

            // not it must by null
            //testUserRegistration = await _deptInvestigationRepository.GetById("153");

            return Ok();
        }
    }
}
