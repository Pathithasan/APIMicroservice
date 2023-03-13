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

        /// <summary>
        /// Get a collection of all products
        /// </summary>
        /// <returns>A return of all products</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _categoryService.GetAllCategories();
            if (categories.Count() > 0)
            {
                var products = await _productService.GetAllProducts();
                if (products.Count() > 0)
                {
                    var pro = (from product in products
                               join category in categories

                                on product.CategoryId equals category.Id
                               select new ViewModel.ProductVM
                               {
                                   Id = product.Id,
                                   Name = product.Name,
                                   Description = product.Description,
                                   Price = product.Price,
                                   CategoryName = category.Name

                               }).ToList();
                    return new OkObjectResult(pro);
                }
                else
                {
                    //return NotFound();
                    return new OkObjectResult("Product not found");
                }
            }
            else
            {
                return new OkObjectResult("Category not found");
            }   
            
        }

        /// <summary>
        /// Gets a prduct by ID
        /// </summary>
        /// <param name="id">The ID of product to retrive</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByID(int id)
        {
            return new OkObjectResult(await _productService.GetByProductyId(id));
        }

        /// <summary>
        /// Insert the product entity
        /// </summary>
        /// <param name="product">The product to add</param>
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
        /// Modify the product,  If exist
        /// </summary>
        /// <param name="id"></param>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Product product)
        {
            if (product != null)
            {
                using (var scope = new TransactionScope())
                {
                    await _productService.UpdateProduct(product);
                    scope.Complete();
                    return new OkResult();
                }
            }
            return new NoContentResult();
        }

        /// <summary>
        /// Delete the product by ID
        /// </summary>
        /// <param name="id">The Id of product to delete</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _productService.DeleteProduct(id);
            return new OkResult();
        }
    }
}

