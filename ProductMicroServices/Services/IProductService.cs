using System;
using ProductMicroServices.Models;

namespace ProductMicroServices.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllProducts();
        Task<Product> GetByProductyId(int productId);
        Task InsertProduct(Product product);
        Task UpdateProduct(Product product);
        Task DeleteProduct(int productId);
    }
}

