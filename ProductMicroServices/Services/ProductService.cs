using System;
using Microsoft.EntityFrameworkCore;
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

        
        /// <summary>
        /// Get a collection of all products
        /// </summary>
        /// <returns>A return of all products</returns>
        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            return (from prd in await _repository.GetAll()
                    orderby prd.Name
                    select prd);
        }

        /// <summary>
        /// Gets a prduct by ID
        /// </summary>
        /// <param name="productId">The ID of product to retrive</param>
        /// <returns></returns>
        public async Task<Product> GetByProductyId(int productId)
        {
            return await _repository.GetById(productId);
            
        }

        /// <summary>
        /// Insert the product entity
        /// </summary>
        /// <param name="product">The product to add</param>
        public async Task InsertProduct(Product product)
        {
            if (product != null)
            {
                await _repository.Insert(product);
            }  
        }

        /// <summary>
        /// Modify the product,  If exist
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public async Task UpdateProduct(Product product)
        {
            if (product != null)
            {
                await _repository.Update(product);
            }     
        }

        /// <summary>
        /// Delete the product by ID
        /// </summary>
        /// <param name="productId">The Id of product to delete</param>
        /// <returns></returns>
        public async Task DeleteProduct(int productId)
        {
            await _repository.Delete(productId);
        }

        
    }
}

