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
    public class PatientDetailsController : ControllerBase
    {
        private readonly IPatientDetailsRepository _patientDetailsRepository;
        private readonly IUnitOfWork _uow;

        public PatientDetailsController(IPatientDetailsRepository patientDetailsRepository, IUnitOfWork uow)
        {
            _patientDetailsRepository = patientDetailsRepository;
            _uow = uow;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PatientDetails>>> Get()
        {
            var patientDetailss = await _patientDetailsRepository.GetAll();
            var allPatient = from UR in patientDetailss
                             select new
                             {

                                 UnqueID = UR.UnqueID,
                                 Title = UR.Title,
                                 UserName = UR.UserName,
                                 FirstName = UR.FirstName,
                                 LastName = UR.LastName,
                                 Year = UR.Year,
                                 Month = UR.Month,
                                 Days = UR.Days,

                                 Gender = UR.Gender,
                                 Email = UR.Email,
                                 ContactNumber = UR.ContactNumber,
                                 Address = UR.Address,
                                 //Status = UR.Status;
                                 //AppointmentID = UR.AppointmentID;
                                 //CaseID = UR.CaseID,
                                 //BedID = UR.BedID;
                                 //TestID = UR.TestID;
                                 //NewID = UR.NewID;
                                 //OtherID = UR.OtherID;
                                 //Doctorfees = UR.Doctorfees;
                                 //HospitalDiscount = UR.HospitalDiscount;
                                 //GrandTotal = UR.GrandTotal;
                                 //TreatmentContinue = UR.TreatmentContinue;
                                 //AssignDoctor = UR.AssignDoctor;
                                 DOB = UR.DOB,
                                 //Relationship = UR.Relationship;
                                 //Pregnancy = UR.Pregnancy;
                                 //PatientCategory = UR.PatientCategory;
                                 //RefferDoctorName = UR.RefferDoctorName;
                                 //PermananetAddress = UR.PermananetAddress;
                                 //OfficeAddress = UR.OfficeAddress;
                                 //MaritalStatus = UR.MaritalStatus;
                                 //CO = UR.CO;
                                 //Religion = UR.Religion;
                                 //Occupation = UR.Occupation;
                                 //BloodGroup = UR.BloodGroup;
                                 //Allergy = UR.Allergy;
                                 //AssignedPharma = UR.AssignedPharma;
                                 //AssignedDept = UR.AssignedDept;
                                 //Height = UR.Height;
                                 //Weight = UR.Weight;
                                 //Temperature = UR.Temperature;
                                 //RespiratoryRate = UR.RespiratoryRate;
                                 //RhType = UR.RhType;
                                 //BPReading = UR.BPReading;
                                 //FatherName = UR.FatherName;
                                 //MotherName = UR.MotherName;
                                 //AdvPayment = UR.AdvPayment;
                             };
            return Ok(allPatient);
        }
        [HttpGet]
        [Route("getAllPatientUniqueCheck")]
        public async Task<ActionResult<IEnumerable<PatientDetails>>> getAllPatientUniqueCheck()
        {
            var patientDetailss = await _patientDetailsRepository.GetAll();
            var allPatient = from UR in patientDetailss
                             select new
                             {

                                 UnqueID = UR.UnqueID,
                                 //Title = UR.Title,
                                 //UserName = UR.UserName,
                                 FirstName = UR.FirstName,
                                 LastName = UR.LastName,
                                 //Year = UR.Year,
                                 //Month = UR.Month,
                                 //Days = UR.Days,

                                 //Gender = UR.Gender,
                                 //Email = UR.Email,
                                 ContactNumber = UR.ContactNumber,
                                 //Address = UR.Address,
                                 //Status = UR.Status;
                                 //AppointmentID = UR.AppointmentID;
                                 //CaseID = UR.CaseID,
                                 //BedID = UR.BedID;
                                 //TestID = UR.TestID;
                                 //NewID = UR.NewID;
                                 //OtherID = UR.OtherID;
                                 //Doctorfees = UR.Doctorfees;
                                 //HospitalDiscount = UR.HospitalDiscount;
                                 //GrandTotal = UR.GrandTotal;
                                 //TreatmentContinue = UR.TreatmentContinue;
                                 //AssignDoctor = UR.AssignDoctor;
                                 //DOB = UR.DOB,
                                 //Relationship = UR.Relationship;
                                 //Pregnancy = UR.Pregnancy;
                                 //PatientCategory = UR.PatientCategory;
                                 //RefferDoctorName = UR.RefferDoctorName;
                                 //PermananetAddress = UR.PermananetAddress;
                                 //OfficeAddress = UR.OfficeAddress;
                                 //MaritalStatus = UR.MaritalStatus;
                                 //CO = UR.CO;
                                 //Religion = UR.Religion;
                                 //Occupation = UR.Occupation;
                                 //BloodGroup = UR.BloodGroup;
                                 //Allergy = UR.Allergy;
                                 //AssignedPharma = UR.AssignedPharma;
                                 //AssignedDept = UR.AssignedDept;
                                 //Height = UR.Height;
                                 //Weight = UR.Weight;
                                 //Temperature = UR.Temperature;
                                 //RespiratoryRate = UR.RespiratoryRate;
                                 //RhType = UR.RhType;
                                 //BPReading = UR.BPReading;
                                 //FatherName = UR.FatherName;
                                 //MotherName = UR.MotherName;
                                 //AdvPayment = UR.AdvPayment;
                             };
            return Ok(allPatient);
        }

        [HttpGet]
        [Route("getUniquePatient/{username}/{contactNumber}")]
        public async Task<ActionResult<bool>> getUniquePatient(string username,string contactNumber)
        {
            //var farmacyDeliveryToPatient = await _farmacyDeliveryToPatientRepository.GetById(UnqueID);
            //return Ok(farmacyDeliveryToPatient);

            //var filterData = await _patientDetailsRepository.GetAll();
            var filterData = await _patientDetailsRepository.getUniquePatient(username, contactNumber);
            var staffType = from c in filterData where c.FirstName == username && c.ContactNumber == contactNumber
                             select c;
            //int p = staffType.ToList().Count;
            return Ok(staffType.ToList().Count > 0 ?false:true);
        }

        [HttpGet("{UnqueID}")]
        public async Task<ActionResult<PatientDetails>> Get(string UnqueID)
        {
            var patientDetails = await _patientDetailsRepository.GetById(UnqueID);
            return Ok(patientDetails);
        }

        [HttpGet("{id}/{UnqueID}")]
        public async Task<string> GetName(int id, string UnqueID)
        {
            var patientDetails = await _patientDetailsRepository.GetById(UnqueID);
            return patientDetails.UserName;
        }

        [HttpPost, Route("PostSimulatingError")]
        public IActionResult PostSimulatingError([FromBody] PatientDetailsViewModel value)
        {
            var patientDetails = new PatientDetails(value);
            _patientDetailsRepository.Add(patientDetails);

            // The patientDetails will not be added
            return BadRequest();
        }

        [HttpPost]
        public async Task<ActionResult<PatientDetails>> Post([FromBody] PatientDetailsViewModel value)
        {

            var patientDetails = new PatientDetails(value);
            // var patientDetails = new PatientDetails(value.Notes, value.Name, value.UnqueID);
            _patientDetailsRepository.Add(patientDetails);

            // it will be null
            var testPatientDetails = await _patientDetailsRepository.GetById(patientDetails.UnqueID);

            // If everything is ok then:
            await _uow.Commit();

            // The patientDetails will be added only after commit
            testPatientDetails = await _patientDetailsRepository.GetById(patientDetails.UnqueID);

            return Ok(testPatientDetails);
        }

        [HttpPut("{UnqueID}")]
        public async Task<ActionResult<PatientDetails>> Put(string UnqueID, [FromBody] PatientDetailsViewModel value)
        {
            //var patientDetails1 = await _patientDetailsRepository.GetById(UnqueID);

            ////string aa = new CommonUtility(_patientDetailsRepository, _uow).GetID(UnqueID).ToString();
            ////var a = _patientDetailsRepository.GetById(value.UnqueID, value.UnqueID);
            //var patientDetails = new PatientDetails(patientDetails1.Id, value.Notes, value.Name, value.UnqueID);

            //_patientDetailsRepository.Update(patientDetails, UnqueID);
            var patientDetails = new PatientDetails(UnqueID, value);

            _patientDetailsRepository.Update(patientDetails, patientDetails.UnqueID);

            await _uow.Commit();

            return Ok(await _patientDetailsRepository.GetById(value.UnqueID));
        }

        [HttpDelete("{UnqueID}")]
        public async Task<ActionResult> Delete(string UnqueID)
        {
            _patientDetailsRepository.Remove(UnqueID);

            // it won't be null
            var testPatientDetails = await _patientDetailsRepository.GetById(UnqueID);

            // If everything is ok then:
            await _uow.Commit();

            // not it must by null
            //testPatientDetails = await _patientDetailsRepository.GetById("153");

            return Ok();
        }
    }
}
