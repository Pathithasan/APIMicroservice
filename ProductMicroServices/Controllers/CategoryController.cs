using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.AspNetCore.Mvc;
using ProductMicroServices.Models;
using ProductMicroServices.Services;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProductMicroServices.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("[controller]/[action]")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        //public IEnumerable<Category> GetAll()
        public IActionResult GetAll()
        {

            var categories = _categoryService.GetAllCategories();

            return new OkObjectResult(categories);
            //return categories;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult GetByID(int id)
        {
            return new OkObjectResult(_categoryService.GetByCategoryId(id));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Create([FromBody] Category category)
        {
            using (var scope = new TransactionScope())
            {
                _categoryService.InsertCategory(category);
                scope.Complete();
                return CreatedAtAction(nameof(GetByID), new { category.Id });

            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult Update([FromBody] Category category)
        {
            if (category != null)
            {
                using (var scope = new TransactionScope())
                {
                    _categoryService.UpdateCategory(category);
                    scope.Complete();
                    return new OkResult();

                }
            }
            else
            {
                return new NoContentResult();
            }
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _categoryService.DeleteCategory(id);
            return new OkResult();
        }
    }
}

