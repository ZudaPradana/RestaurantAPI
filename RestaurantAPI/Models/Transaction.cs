using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantAPI.Models
{
    public class Transaction
    {
        public int Id { get; set; } 

        public int CustomerId { get; set; }

        public DateTime TransactionDate { get; set; }

        public int Quantity { get; set; }

        public float AmountTotal { get; set; }

        public string PaymentMethod { get; set; }

        public float TotalPrice { get; set; }

        public float TotalChange { get; set; }

        public List<TransactionItems> Items { get; set; }

        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }
    }
}
