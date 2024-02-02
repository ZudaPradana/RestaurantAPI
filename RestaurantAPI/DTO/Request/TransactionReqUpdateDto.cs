
namespace RestaurantAPI.DTO.Request
{
    public class TransactionReqUpdateDto
    {

        public float AmountTotal { get; set; }

        public string PaymentMethod { get; set; }


        public List<TransactionItemReqDto> Items { get; set; }
    }


}
