namespace ConsumerDemo
{
    public class OrderProcessingRequest
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int CustomerId { get; set; }
        public int Quantity { get; set; }
        public string Status { get; set; }

        public void Display() {
            Console.WriteLine($"OrderId-{OrderId} ProductId-{ProductId} CustomerId-{CustomerId} Quantity-{Quantity} Status-{Status}");
        }
    }
}
