using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace MongoDB.GenericRepository.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageUploadController : ControllerBase
    {
        //[HttpPost("single-file")]
        //public async Task Upload(IFormFile file)
        //{
        //    // validate the file, scan virus, save to a file storage
        //}

        //[HttpPost("two-files")]
        //public async Task Upload(IFormFile file1, IFormFile file2)
        //{
        //    // validate the files, scan virus, save them to a file storage
        //}
        //[HttpPost("multiple-files")]
        //public async Task Upload(List<IFormFile> files)
        //{
        //    // validate the files, scan virus, save them to a file storage
        //}

        private IHostingEnvironment _hostingEnvironment;
        public ImageUploadController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;

        }

        //[HttpPost, DisableRequestSizeLimit]
        //public ActionResult UploadFile()
        //{
        //    try
        //    {
        //        var file = Request.Form.Files[0];
        //        string folderName = "Upload";
        //        string webRootPath = _hostingEnvironment.WebRootPath;
        //        string newPath = Path.Combine(webRootPath, folderName);
        //        if (!Directory.Exists(newPath))
        //        {
        //            Directory.CreateDirectory(newPath);
        //        }
        //        if (file.Length > 0)
        //        {
        //            string fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
        //            string fullPath = Path.Combine(newPath, fileName);
        //            using (var stream = new FileStream(fullPath, FileMode.Create))
        //            {
        //                file.CopyTo(stream);
        //            }
        //        }
        //        return Ok("Upload Successful.");
        //    }
        //    catch (System.Exception ex)
        //    {
        //        return Ok("Upload Failed: " + ex.Message);
        //    }
        //}

        [HttpPost, DisableRequestSizeLimit]
        public IActionResult Upload(string nameOfFile)
        {
            //string nameOfFile = "lik1";

            try
            {
                //var formCollection = await Request.ReadFormAsync();
                //var file = formCollection.Files.First();
                var file = Request.Form.Files[0];
                //var file = Request.Form.Files[0];
                var folderName = Path.Combine("Resources", "Images");

                var pathToSaveServer = Path.Combine(Directory.GetCurrentDirectory(), folderName);


                string webRootPath = @"C:\Image\"; // _hostingEnvironment.WebRootPath;
                string pathToSave = Path.Combine(webRootPath, folderName);

               

                if (!Directory.Exists(pathToSave))
                {
                    Directory.CreateDirectory(pathToSave);
                }

                if (!Directory.Exists(pathToSaveServer))
                {
                    Directory.CreateDirectory(pathToSaveServer);
                }
                //var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                if (file.Length > 0)
                {

                    var fileNames = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    //string extensionold = Path.GetExtension(fileNames);
                    //fileNames = nameOfFile + extensionold;
                    var fullPaths = Path.Combine(pathToSaveServer, fileNames);
                    var dbPaths = Path.Combine(folderName, fileNames);
                    using (var stream = new FileStream(fullPaths, FileMode.Create))
                    {
                        file.CopyTo(stream);

                    }

                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    //string extension = Path.GetExtension(fileName);
                    //fileName = nameOfFile + extension;

                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    return Ok(new { dbPath });
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }


        [HttpGet("files/{id}")]

        public async Task<ActionResult> DownloadFile(string id)
        {
            // ... code for validation and get the file
            //var filePath = @"C:\Image\Resources\Images\" + id + ".png";
            //var filePathNA = @"C:\Image\Resources\Images\NA.png";
            var folderNameNA = Path.Combine("Resources", "Images");
            var pathToSaveServerNA = Path.Combine(Directory.GetCurrentDirectory(), folderNameNA) + "\\" + "NA.png";

            var folderName = Path.Combine("Resources", "Images");
            //var filePath = Path.Combine("Resources", "Images");
            var pathToSaveServer = Path.Combine(Directory.GetCurrentDirectory(), folderName)+"\\"+id;

            //var pathToSaveServer = Path.Combine(Directory.GetCurrentDirectory(), filePath);


            //string webRootPath = @"C:\Image\"; // _hostingEnvironment.WebRootPath;
            //string pathToSave = Path.Combine(webRootPath, folderName);

            try
            {
                var provider = new FileExtensionContentTypeProvider();
                if (!provider.TryGetContentType(pathToSaveServer, out var contentType))
                {
                    contentType = "application/octet-stream";
                }

                var bytes = await System.IO.File.ReadAllBytesAsync(pathToSaveServer);
                return File(bytes, contentType, Path.GetFileName(pathToSaveServer));
            }
            catch (Exception)
            {
                var provider = new FileExtensionContentTypeProvider();
                if (!provider.TryGetContentType(pathToSaveServerNA, out var contentTypeNA))
                {
                    contentTypeNA = "application/octet-stream";
                }

                var bytesNA = await System.IO.File.ReadAllBytesAsync(pathToSaveServerNA);
                return File(bytesNA, contentTypeNA, Path.GetFileName(pathToSaveServerNA));

                //throw;
            }

            
        }

        //[HttpGet("files/{id}")]
        //public async Task<ActionResult> DownloadFile(string id)
        //{
        //    var filePath = @"C:\Image\Resources\Images\"+id+".png"; ; // Here, you should validate the request and the existance of the file.

        //    var bytes = await System.IO.File.ReadAllBytesAsync(filePath);
        //    return File(bytes, "text/plain", Path.GetFileName(filePath));
        //}
    }

    public class StudentForm
    {
        [Required] public int FormId { get; set; }
        [Required] public IFormFile StudentFile { get; set; }
    }
}
