using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantAPI.Models
{
    public class TransactionItems
    {
        public int Id { get; set; }

        public int FoodId { get; set; }

        public int TransactionId { get; set; }

        public int Quantity { get; set; }

        [ForeignKey("FoodId")]
        public Food Food { get; set; }

        [ForeignKey("TransactionId")]
        public Transaction Transaction { get; set; }

    }
}
