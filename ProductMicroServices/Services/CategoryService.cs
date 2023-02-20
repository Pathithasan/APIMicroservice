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

        public void DeleteCategory(int categoryId)
        {
            _repository.Delete(categoryId);
            _repository.Save();
        }

        public IEnumerable<Category> GetAllCategories()
        {
            //return _repository.GetAll();
            return (from cat in _repository.GetAll()
                    orderby cat.Name
                    select cat);
        }

        public Category GetByCategoryId(int categoryId)
        {
            return _repository.GetById(categoryId);
        }

        public void InsertCategory(Category category)
        {
            _repository.Insert(category);
            //_repository.Save();
        }

        public void UpdateCategory(Category category)
        {
            _repository.Update(category);
            _repository.Save();
        }
    }
}

