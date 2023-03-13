using System;
using ProductMicroServices.Models;

namespace ProductMicroServices.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetAllCategories();
        Task<Category> GetByCategoryId(int categorytId);
        Task InsertCategory(Category category );
        Task UpdateCategory(Category category);
        Task DeleteCategory(int categoryID);
    }
}

