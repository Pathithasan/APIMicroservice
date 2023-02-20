using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.AspNetCore.Mvc;
using ProductMicroServices.Models;
using ProductMicroServices.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProductMicroServices.Controllers
{
    [Route("[controller]/[action]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public ProductController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }
       
        // GET: api/values
        [HttpGet]
        public IActionResult GetAll()
        //public IEnumerable<Product> GetAll()
        {
           
            var products = _productService.GetAllProducts();
            var pro = (from pr in _productService.GetAllProducts()
                       join cat in _categoryService.GetAllCategories()
                        on pr.CategoryId equals cat.Id
                        select new ViewModel.ProductVM
                        {
                            Id= pr.Id,
                            Name= pr.Name,
                            Description= pr.Description,
                            Price= pr.Price,
                            CategoryName= cat.Name
                            
                        }).ToList();
            return new OkObjectResult(pro);
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult GetByID(int id)
        {
            return new OkObjectResult(_productService.GetByProductyId(id));
        }

        
        [HttpPost]
        public IActionResult Create([FromBody] Product product)
        {

            using (var scope = new TransactionScope())
            {
                _productService.InsertProduct(product);
                scope.Complete();
                return CreatedAtAction(nameof(GetByID), new { product.Id });

            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Product product)
        {
            if (product != null)
            {
                using (var scope = new TransactionScope())
                {
                    _productService.UpdateProduct(product);
                    scope.Complete();
                    return new OkResult();
                }
            }
            return new NoContentResult();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _productService.DeleteProduct(id);
            return new OkResult();
        }
    }
}

