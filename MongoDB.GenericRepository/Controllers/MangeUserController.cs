//using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
//using Microsoft.AspNetCore.Hosting.Internal;
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
    public class MangeUserController : ControllerBase
    {
        private readonly IUserRegistrationRepository _userRegistrationRepository;
        private readonly IUnitOfWork _uow;
        //private readonly IHostingEnvironment _HostEnvironment;
        readonly ILogger<MangeUserController> _log;


        public MangeUserController(IUserRegistrationRepository userRegistrationRepository, IUnitOfWork uow, ILogger<MangeUserController> log)
        {
            _userRegistrationRepository = userRegistrationRepository;
            _uow = uow;
            //_HostEnvironment = HostEnvironment;
            _log = log;
        }

        string fullPath = @"/Resources/Log/LogActivity.txt";
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserRegistration>>> Get()
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
            

            var userRegistrations = await _userRegistrationRepository.GetAll();
            return Ok(userRegistrations); 
        }
        [HttpGet("{UnqueID}/{single}")]
        public async Task<string> GetName(string UnqueID, string colName)
        {
            var userRegistration = await _userRegistrationRepository.GetById(UnqueID);
            return userRegistration.Username;
        }

        [HttpGet("{name}/{filterColumnName}/{finTheUser}")] 
        public async Task<ActionResult<UserRegistration>> GetUserLoginDetails(string name, string filterColumnName, string finTheUser)
        {
            var userRegistration = await _userRegistrationRepository.GetByName(name, filterColumnName, finTheUser);
            return userRegistration;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserRegistration>> Get(string UnqueID)
        {
            var userRegistration = await _userRegistrationRepository.GetById(UnqueID);
            return Ok(userRegistration);
        }






        [HttpPost, Route("PostSimulatingError")]
        public IActionResult PostSimulatingError([FromBody] UserRegistrationViewModel value)
        {
            var userRegistration = new UserRegistration(value);
            _userRegistrationRepository.Add(userRegistration);

            // The userRegistration will not be added
            return BadRequest();
        }

        [HttpPost]
        public async Task<ActionResult<UserRegistration>> Post([FromBody] UserRegistrationViewModel value)
        {
            var userRegistration = new UserRegistration(value);
            _userRegistrationRepository.Add(userRegistration);

            // it will be null
            var testUserRegistration = await _userRegistrationRepository.GetById( userRegistration.UnqueID);

            // If everything is ok then:
            await _uow.Commit();

            // The userRegistration will be added only after commit
            testUserRegistration = await _userRegistrationRepository.GetById( userRegistration.UnqueID);

            return Ok(testUserRegistration);
        }

        [HttpPut("{UnqueID}")]
        public async Task<ActionResult<UserRegistration>> Put(string UnqueID, [FromBody] UserRegistrationViewModel value)
        {
            //var userRegistration1 = await _userRegistrationRepository.GetById(id, UnqueID);

            //string aa = new CommonUtility(_userRegistrationRepository, _uow).GetID(UnqueID).ToString();
            //var a = _userRegistrationRepository.GetById(value.UnqueID, value.UnqueID);
            var userRegistration = new UserRegistration(UnqueID, value);

            _userRegistrationRepository.Update(userRegistration, userRegistration.UnqueID);

            await _uow.Commit();

            return Ok(await _userRegistrationRepository.GetById(userRegistration.UnqueID));
        }

        [HttpDelete("{UnqueID}")]
        public async Task<ActionResult> Delete(string UnqueID)
        {
            _userRegistrationRepository.Remove(UnqueID);

            // it won't be null
            var testUserRegistration = await _userRegistrationRepository.GetById(UnqueID);

            // If everything is ok then:
            await _uow.Commit();

            // not it must by null
            //testUserRegistration = await _userRegistrationRepository.GetById("153");

            return Ok();
        }
    }
}
