using System;
using NuGet.Protocol.Core.Types;
using ProductMicroServices.DAL;
using ProductMicroServices.Models;

namespace ProductMicroServices.Services
{
    public class ProductService : IProductService
    {
        private IGenericRepository<Product> _repository;
        public ProductService(IGenericRepository <Product> repository)
        {
            _repository = repository;
        }

        public void DeleteProduct(int productId)
        {
            _repository.Delete(productId);
            _repository.Save();
        }

        public IEnumerable<Product> GetAllProducts()
        {
            
            return _repository.GetAll();
        }

        public Product GetByProductyId(int productId)
        {
            return _repository.GetById(productId);
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="product"></param>
        public void InsertProduct(Product product)
        {
            _repository.Insert(product);
            _repository.Save();
        }

        void IProductService.UpdateProduct(Product product)
        {
            _repository.Update(product);
            _repository.Save();
        }
    }
}

