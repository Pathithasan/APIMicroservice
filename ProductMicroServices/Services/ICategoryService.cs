using System;
using ProductMicroServices.Models;

namespace ProductMicroServices.Services
{
    public interface ICategoryService
    {
        IEnumerable<Category> GetAllCategories();
        Category GetByCategoryId(int categorytId);
        void InsertCategory(Category category );
        void UpdateCategory(Category category);
        void DeleteCategory(int categoryID);
    }
}

