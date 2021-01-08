using System;
using System.Collections.Generic;

namespace ConsoleApp2
{
    public class Order
    {
        public string OrderCode { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public DateTimeOffset Created { get; set; }
        public List<Product> Products { get; set; }

        public Order()
        {
            Created = DateTimeOffset.Now;
            Products = new List<Product>();
        }
    }
}
