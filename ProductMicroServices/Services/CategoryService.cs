using System;
using ProductMicroServices.DAL;
using ProductMicroServices.Models;

namespace ProductMicroServices.Services
{
    public class CategoryService : ICategoryService
    {
        private IGenericRepository<Category> _repository;
        
        public CategoryService(IGenericRepository<Category> repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Get a collection of all categories
        /// </summary>
        /// <returns>A return of all categories</returns>
        public async Task<IEnumerable<Category>> GetAllCategories()
        { 
            return (from cat in await _repository.GetAll()
                    orderby cat.Name
                    select cat);
        }

        /// <summary>
        /// Gets a prduct by ID
        /// </summary>
        /// <param name="categoryId">The ID of category to retrive</param>
        /// <returns></returns>
        public async Task<Category> GetByCategoryId(int categoryId)
        {
            return await _repository.GetById(categoryId);
        }

        /// <summary>
        /// Insert the category entity
        /// </summary>
        /// <param name="category">The category to add</param>
        public async Task InsertCategory(Category category)
        {
            await _repository.Insert(category);
            
        }

        /// <summary>
        /// Modify the category,  If exist
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public async Task UpdateCategory(Category category)
        {
            await _repository.Update(category);
           
        }

        /// <summary>
        /// Delete the category by ID
        /// </summary>
        /// <param name="categoryId">The Id of category to delete</param>
        /// <returns></returns>
        public async Task DeleteCategory(int categoryId)
        {
            await _repository.Delete(categoryId);

        }

    }
}

