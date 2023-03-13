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
    /// API controller for category
    /// </summary>
    [Route("[controller]/[action]")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        /// <summary>
        /// Get a collection of all categories, If exist
        /// </summary>
        /// <returns>A return of all categories</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _categoryService.GetAllCategories();
            if (categories.Count() > 0)
            {
                return new OkObjectResult(categories);
            }
            else
            {
                return new OkObjectResult("Category not found");
            }
        }

        /// <summary>
        /// Gets a prduct by ID, If exist
        /// </summary>
        /// <param name="cid">The ID of category to retrive</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByID(int id)
        {
            var category =await _categoryService.GetByCategoryId(id);
            if(category != null)
            {
                return new OkObjectResult(category);
            }
            else
            {
                return new OkObjectResult("Category not found");
            }
            
        }

        /// <summary>
        /// Insert the category entity
        /// </summary>
        /// <param name="category">The category to add</param>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Category category)
        {
            using (var scope = new TransactionScope())
            {
                await _categoryService.InsertCategory(category);
                scope.Complete();
                return CreatedAtAction(nameof(GetByID), new { category.Id });

            }
        }

        /// <summary>
        /// Modify the category, If exist
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Category category)
        {
            if (category != null)
            {
                using (var scope = new TransactionScope())
                {
                    await _categoryService.UpdateCategory(category);
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
        /// Delete the category by ID
        /// </summary>
        /// <param name="id">The Id of category to delete</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _categoryService.DeleteCategory(id);
            return new OkResult();
        }
    }
}

