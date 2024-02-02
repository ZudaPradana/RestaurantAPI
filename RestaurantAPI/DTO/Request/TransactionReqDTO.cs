using System.Text.Json.Serialization;

namespace RestaurantAPI.DTO.Request
{
    public class TransactionReqDto
    {
        public int CustomerId { get; set; }

        public string PaymentMethod { get; set; }

        public float AmountTotal { get; set; }

        public List<TransactionItemReqDto> Items { get; set; }

    }
}
