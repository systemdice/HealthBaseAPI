using Microsoft.AspNetCore.Mvc;
using MongoDB.GenericRepository.Interfaces;
using MongoDB.GenericRepository.Model;
using MongoDB.GenericRepository.Utility;
using MongoDB.GenericRepository.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Collections.Specialized;
using System.Web;
using ExportFile.Models;
using System.Text;
using Newtonsoft.Json;

namespace MongoDB.GenericRepository.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserDetailsController : ControllerBase
    {
        private readonly IUserDetailsRepository _userDetailsRepository;
        private readonly IUnitOfWork _uow;

        public UserDetailsController(IUserDetailsRepository userDetailsRepository, IUnitOfWork uow)
        {
            _userDetailsRepository = userDetailsRepository;
            _uow = uow;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDetails>>> Get()
        {
            var userDetailss = await _userDetailsRepository.GetAll();
            JsonResult categoryJson = new JsonResult(userDetailss);

            //List<UserDetails> details = new List<userdetails>();
            //userdetails user = new userdetails();
            //details.Add(new userdetails { userid = 1, username = "suresh", location = "chennai" });
            //JSON Serialization Method
            string strserialize = JsonConvert.SerializeObject(userDetailss);

            //JSON DeSerialization Method
            //string strmsg = "[{\"userid\":1,\"username\":\"suresh\",\"location\":\"chennai\"},{\"userid\":2,\"username\":\"rohini\",\"location\":\"guntur\"}]";
            //var user = JsonConvert.DeserializeObject<List<userdetails>>(strmsg);

            return Ok(userDetailss);
        }
        [HttpGet]
        [Route("GetLoginDetails/{UName}/{pwd}")]
        public async Task<ActionResult<IEnumerable<UserDetails>>> GetLoginDetails(string UName, string pwd)
        {
            //var userDetailss = await _userDetailsRepository.GetAlllogin();
            var userDetailss = await _userDetailsRepository.GetLoginDetails(UName, pwd);
            //sonResult categoryJson = new JsonResult(userDetailss);

            //List<UserDetails> details = new List<userdetails>();
            //userdetails user = new userdetails();
            //details.Add(new userdetails { userid = 1, username = "suresh", location = "chennai" });
            //JSON Serialization Method
            //string strserialize = JsonConvert.SerializeObject(userDetailss);

            //JSON DeSerialization Method
            //string strmsg = "[{\"userid\":1,\"username\":\"suresh\",\"location\":\"chennai\"},{\"userid\":2,\"username\":\"rohini\",\"location\":\"guntur\"}]";
            //var user = JsonConvert.DeserializeObject<List<userdetails>>(strmsg);

            return Ok(userDetailss);
        }
        [HttpGet("{UnqueID}")]
        public async Task<ActionResult<UserDetails>> Get(string UnqueID)
        {
            var userDetails = await _userDetailsRepository.GetById(UnqueID);
            return Ok(userDetails);
        }

        [HttpGet("{id}/{UnqueID}")]
        public async Task<string> GetName(int id, string UnqueID)
        {
            var userDetails = await _userDetailsRepository.GetById(UnqueID);
            return userDetails.UserName;
        }








        [HttpPost, Route("PostSimulatingError")]
        public IActionResult PostSimulatingError([FromBody] UserDetailsViewModel value)
        {
            var userDetails = new UserDetails(value);
            _userDetailsRepository.Add(userDetails);

            // The userDetails will not be added
            return BadRequest();
        }
        //import export
        [HttpPost]
        [Route("Postexcel/{id}")]
        public ActionResult<string> Postexcel(int id = 0)
        {
            List<Record> recordobj = new List<Record>();
            recordobj.Add(new Record { FName = "Smith", LName = "Singh", Address = "Knpur" });
            recordobj.Add(new Record { FName = "John", LName = "Kumar", Address = "Lucknow" });
            recordobj.Add(new Record { FName = "Vikram", LName = "Kapoor", Address = "Delhi" });
            recordobj.Add(new Record { FName = "Tanya", LName = "Shrma", Address = "Banaras" });
            recordobj.Add(new Record { FName = "Malini", LName = "Ahuja", Address = "Gujrat" });
            recordobj.Add(new Record { FName = "Varun", LName = "Katiyar", Address = "Rajasthan" });
            recordobj.Add(new Record { FName = "Arun  ", LName = "Singh", Address = "Jaipur" });
            recordobj.Add(new Record { FName = "Ram", LName = "Kapoor", Address = "Panjab" });
            recordobj.Add(new Record { FName = "Vishakha", LName = "Singh", Address = "Banglor" });
            recordobj.Add(new Record { FName = "Tarun", LName = "Singh", Address = "Kannauj" });
            recordobj.Add(new Record { FName = "Mayank", LName = "Dubey", Address = "Farrukhabad" });
            JsonResult categoryJson = new JsonResult(recordobj);
           
            //return recordobj;
            // some code //
            //var products = // IEnumerable object //
            //string json = new JavaScriptSerializer().Serialize(recordobj);
            // some code //
            //return json;

            List<Record> obj = new List<Record>();
            obj = recordobj; // RecordInfo();
            StringBuilder str = new StringBuilder();
            str.Append("<table border=`" + "1px" + "`b>");
            str.Append("<tr>");
            str.Append("<td><b><font face=Arial Narrow size=3>FName</font></b></td>");
            str.Append("<td><b><font face=Arial Narrow size=3>LName</font></b></td>");
            str.Append("<td><b><font face=Arial Narrow size=3>Address</font></b></td>");
            str.Append("</tr>");
            foreach (Record val in obj)
            {
                str.Append("<tr>");
                str.Append("<td><font face=Arial Narrow size=" + "14px" + ">" + val.FName.ToString() + "</font></td>");
                str.Append("<td><font face=Arial Narrow size=" + "14px" + ">" + val.LName.ToString() + "</font></td>");
                str.Append("<td><font face=Arial Narrow size=" + "14px" + ">" + val.Address.ToString() + "</font></td>");
                str.Append("</tr>");
            }
            str.Append("</table>");
            Response.Headers["content-disposition"] = "attachment; filename=Information" + DateTime.Now.Year.ToString() + ".xls";
            //HttpContext.Response.AddHeader("content-disposition", "attachment; filename=Information" + DateTime.Now.Year.ToString() + ".xls");
            this.Response.ContentType = "application/vnd.ms-excel";
            byte[] temp = System.Text.Encoding.UTF8.GetBytes(str.ToString());
            return File(temp, "application/vnd.ms-excel");
        }
        
        //ended import export

        [HttpPost]
        public async Task<ActionResult<UserDetails>> Post([FromBody] UserDetailsViewModel value)
        {

            var userDetails = new UserDetails(value);
            // var userDetails = new UserDetails(value.Notes, value.Name, value.UnqueID);
            _userDetailsRepository.Add(userDetails);

            // it will be null
            var testUserDetails = await _userDetailsRepository.GetById(userDetails.UnqueID);

            // If everything is ok then:
            await _uow.Commit();

            // The userDetails will be added only after commit
            testUserDetails = await _userDetailsRepository.GetById(userDetails.UnqueID);

            return Ok(testUserDetails);
        }
        //ned to write proper code (nida silani)
        [HttpPost]
        [Route("PostReturnUserModel")]
        public async Task<ActionResult<ReturnUserModel>> PostReturnUserModel([FromBody] LoginAndPasswordResetModel value)
        {
            var Userloginvalues = await _userDetailsRepository.GetAll(); //.GetLoginDetails(value.UserName, value.Password);
            var display = Userloginvalues.Where(m => m.UserName == value.UserName && m.Password == value.Password).FirstOrDefault();
            
            return Ok(display);
        }

        [HttpPost]
        [Route("PostSMS")]
        public async Task<ActionResult<string>> PostSMS([FromBody] UserDetailsViewModel value)
        {
            try
            {





                String message = HttpUtility.UrlEncode("This is your message");
                using (var wb = new WebClient())
                {
                    byte[] response = wb.UploadValues("https://api.textlocal.in/send/", new NameValueCollection()
                {
                {"apikey" , "seYRn8Am4Og-8ytOtSp0YTZz8n8pqTpbV4q4MoAodX"},
                {"numbers" , "8904292294"},
                {"message" , message},
                {"sender" , "919920329044"}
                });
                    string result = System.Text.Encoding.UTF8.GetString(response);
                    return result;
                }
            }
            catch (Exception ex)
            {
            }
           

            var userDetails = new UserDetails(value);
            // var userDetails = new UserDetails(value.Notes, value.Name, value.UnqueID);
            _userDetailsRepository.Add(userDetails);

            // it will be null
            var testUserDetails = await _userDetailsRepository.GetById(userDetails.UnqueID);

            // If everything is ok then:
            await _uow.Commit();

            // The userDetails will be added only after commit
            testUserDetails = await _userDetailsRepository.GetById(userDetails.UnqueID);

            return Ok(testUserDetails);
        }

        [HttpPost]
        [Route("PostResetUserModel")]
        public async Task<ActionResult<string>> PostResetUserModel([FromBody] LoginAndPasswordResetModel value)
        {
            var Userloginvalues = await _userDetailsRepository.GetAll();
            UserDetails display = Userloginvalues.Where(m => m.UserName == value.UserName && m.Password == value.Password && m.SecretCode== value.SecretCode).FirstOrDefault();
            string result;
            if (display != null)
            {
                try
                {
                    UserDetailsViewModel pp = new UserDetailsViewModel();
                    pp.UnqueID = display.UnqueID;
                    pp.UserName = display.UserName;
                    pp.Password = value.NewPassword;
                    pp.SecretCode = value.SecretCode;
                    pp.ProfilePicName = display.UnqueID;
                    var userDetails = new UserDetails(display.UnqueID, pp);

                    _userDetailsRepository.Update(userDetails, userDetails.UnqueID);
                }
                catch (Exception ex)
                {
                    
                }
                

                await _uow.Commit();
                result = "successful";
            }
            else
            {
                result = "Unsuccessful";
            }

                

            //return Ok(await _userDetailsRepository.GetById(value.UnqueID));

            return Ok(result);
        }


        [HttpPut("{UnqueID}")]
        public async Task<ActionResult<UserDetails>> Put(string UnqueID, [FromBody] UserDetailsViewModel value)
        {
            if (UnqueID == "ChangePassword")
            {
                var userDetailss = await _userDetailsRepository.GetAll();
                var usrFind = userDetailss.Where(a => a.UserName == value.UserName && a.Password == value.OldPassword && a.SecretCode == value.SecretCode).ToList();

                if (usrFind.Count > 0)
                {
                    usrFind[0].Password = value.Password;
                    _userDetailsRepository.Update(usrFind[0], usrFind[0].UnqueID);
                    value.UnqueID = usrFind[0].UnqueID;
                }
            }
            else if (UnqueID == "ResetPassword")
            {
                var userDetailss = await _userDetailsRepository.GetAll();
                var usrFind = userDetailss.Where(a => a.UserName == value.UserName).ToList();

                if (usrFind.Count > 0)
                {
                    usrFind[0].Password = "ChangeMe";
                    _userDetailsRepository.Update(usrFind[0], usrFind[0].UnqueID);
                    value.UnqueID = usrFind[0].UnqueID;
                }
            }
            else
            {
                //var userDetails1 = await _userDetailsRepository.GetById(UnqueID);

                ////string aa = new CommonUtility(_userDetailsRepository, _uow).GetID(UnqueID).ToString();
                ////var a = _userDetailsRepository.GetById(value.UnqueID, value.UnqueID);
                //var userDetails = new UserDetails(userDetails1.Id, value.Notes, value.Name, value.UnqueID);

                //_userDetailsRepository.Update(userDetails, UnqueID);
                var userDetails = new UserDetails(UnqueID, value);

                _userDetailsRepository.Update(userDetails, userDetails.UnqueID);
            }
            await _uow.Commit();

            return Ok(await _userDetailsRepository.GetById(value.UnqueID));
        }

        [HttpDelete("{UnqueID}")]
        public async Task<ActionResult> Delete(string UnqueID)
        {
            _userDetailsRepository.Remove(UnqueID);

            // it won't be null
            var testUserDetails = await _userDetailsRepository.GetById(UnqueID);

            // If everything is ok then:
            await _uow.Commit();

            // not it must by null
            //testUserDetails = await _userDetailsRepository.GetById("153");

            return Ok();
        }
    }
}
