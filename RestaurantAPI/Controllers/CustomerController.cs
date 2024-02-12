using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.DTO.Request;
using RestaurantAPI.Models;
using RestaurantAPI.Services;

namespace RestaurantAPI.Controllers
{
    [ApiController]
    [Route("api/customers")]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerService _customerService;

        public CustomerController(CustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        //Task merepresentasikan operasi asinkron
        //ActionResult mewakili hasil dari metode aksi dalam kontroler
        public async Task<ActionResult<List<Customer>>> GetAllCustomers()
        {
            var customers = await _customerService.GetAllCustomerAsync();
            return Ok(customers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomerById(int id)
        {
                try
                {
                var customer = await _customerService.GetCustomerByIdAsync(id);

                if (customer == null)
                {
                    return NotFound($"Customer with ID {id} not found");
                }

                return Ok(customer);
                }       
                catch (Exception ex)
                {
                    return StatusCode(500, $"Internal Server Error: {ex.Message}");
                }
        }

        [HttpPost]
        public async Task<ActionResult> AddCustomer([FromBody] CustomerReqDTO reqDTO)
        {
            try
            {
                await _customerService.AddCustomerAsync(reqDTO);
                return new ObjectResult(reqDTO) { StatusCode = 201 };
            }
            catch (Exception ex)
            {
                // Handle other exceptions if needed
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }

        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCustomer(int id, [FromBody] CustomerReqDTO reqDTO)
        {
            try
            {
                await _customerService.UpdateCustomerAsync(reqDTO, id);
                return new ObjectResult($"Edited ID {id} Successfully");
            }
            catch (Exception ex)
            {
                // Handle other exceptions if needed
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
            
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCustomer(int id)
        {
            try
            {
                await _customerService.DeleteCustomerAsync(id);
                return Ok(new OkObjectResult($"Delete ID {id} Successfully"));
            }
            catch (DirectoryNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                // Handle other exceptions if needed
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
    }
}
