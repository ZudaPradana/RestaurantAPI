using System.ComponentModel.DataAnnotations;

namespace RestaurantAPI.DTO.Request
{
    public class CustomerReqDTO
    {
        [Required]
        public string Name { get; set; }

        [Required, EmailAddress(ErrorMessage ="Invalid Email Address.")]
        public string Email { get; set; }

        public string PhoneNumber { get; set; }
    }
}
