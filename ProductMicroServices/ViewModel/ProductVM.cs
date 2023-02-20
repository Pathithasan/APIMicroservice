using System;
using ProductMicroServices.Models;

namespace ProductMicroServices.ViewModel
{
    public class ProductVM : BaseClass
    {
        public string Description { get; set; } = String.Empty;

        public decimal Price { get; set; }

        //public int CategoryId { get; set; }

        public string CategoryName { get; set; } = String.Empty;


    }
}


