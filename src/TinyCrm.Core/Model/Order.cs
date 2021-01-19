using System;
using System.Collections.Generic;

namespace TinyCrm.Core.Model
{
    public class Order
    {
        public Guid OrderId { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public DateTimeOffset Created { get; set; }
        //public List<Product> Products { get; set; }

        public Order()
        {
            OrderId = Guid.NewGuid();
            Created = DateTimeOffset.Now;
            //Products = new List<Product>();
        }
    }
}
