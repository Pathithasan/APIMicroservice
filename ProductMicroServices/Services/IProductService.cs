using System;
using ProductMicroServices.Models;

namespace ProductMicroServices.Services
{
    public interface IProductService
    {
        IEnumerable<Product> GetAllProducts();
        Product GetByProductyId(int productId);
        void InsertProduct(Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(int productId);
    }
}

