using System;
namespace ProductMicroServices.Models
{
    public class Product : BaseClass
    {
        public string Description { get; set; } = String.Empty;

        public decimal Price { get; set; }

        public int CategoryId { get; set; }
    }
}

