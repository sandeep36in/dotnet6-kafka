using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProducerDemo
{
    internal class Order
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int CustomerId { get; set; }
        public int Quantity { get; set; }
        public string Status { get; set; }

        public static Order GetNewOrder() {
            Random random = new Random();
            return new Order()
            {
                OrderId = random.Next(),
                ProductId = random.Next(),
                CustomerId = random.Next(),
                Quantity = random.Next(),
                Status = $"Status{random.Next()}"
            };
        }

        public string GetDetails()
        {
            return $"OrderId-{OrderId} ProductId-{ProductId} CustomerId-{CustomerId} Quantity-{Quantity} Status-{Status}";
        }
    }
}
