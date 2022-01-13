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
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _uow;

        public ProductController(IProductRepository productRepository, IUnitOfWork uow)
        {
            _productRepository = productRepository;
            _uow = uow;
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> Get()
        {
            var products = await _productRepository.GetAll();
            return Ok(products);
        }
        [HttpGet("{UnqueID}")]
        public async Task<ActionResult<Product>> Get(string UnqueID)
        {
            var product = await _productRepository.GetById(UnqueID);
            return Ok(product);
        }

        [HttpGet("{id}/{UnqueID}")]
        public async Task<string> GetName(int id, string UnqueID)
        {
            var product = await _productRepository.GetById(UnqueID);
            return product.Name;
        }

       

       
       



        [HttpPost, Route("PostSimulatingError")]
        public IActionResult PostSimulatingError([FromBody] ProductViewModel value)
        {
            var product = new Product(value.Description,value.Name,value.UnqueID);
            _productRepository.Add(product);

            // The product will not be added
            return BadRequest();
        }

        [HttpPost]
        public async Task<ActionResult<Product>> Post([FromBody] ProductViewModel value)
        {
            var product = new Product(value.Description, value.Name, value.UnqueID);
            _productRepository.Add(product);

            // it will be null
            var testProduct = await _productRepository.GetById(product.UnqueID);

            // If everything is ok then:
            await _uow.Commit();

            // The product will be added only after commit
            testProduct = await _productRepository.GetById( product.UnqueID);

            return Ok(testProduct);
        }

        [HttpPut("{UnqueID}")]
        public async Task<ActionResult<Product>> Put(string UnqueID, [FromBody] ProductViewModel value)
        {
            var product1 = await _productRepository.GetById(UnqueID);

            string aa = new CommonUtility(_productRepository,_uow).GetID(UnqueID).ToString();
            //var a = _productRepository.GetById(value.UnqueID, value.UnqueID);
            var product = new Product(product1.Id, value.Description,value.Name, value.UnqueID);

            _productRepository.Update(product, UnqueID);

            await _uow.Commit();

            return Ok(await _productRepository.GetById(value.UnqueID));
        }

        [HttpDelete("{UnqueID}")]
        public async Task<ActionResult> Delete(string UnqueID)
        {
            _productRepository.Remove(UnqueID);

            // it won't be null
            var testProduct = await _productRepository.GetById(UnqueID);

            // If everything is ok then:
            await _uow.Commit();

            // not it must by null
            //testProduct = await _productRepository.GetById("153");

            return Ok();
        }
    }
}
