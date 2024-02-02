using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.DTO.Request;
using RestaurantAPI.Models;
using RestaurantAPI.Services;

namespace RestaurantAPI.Controllers
{
    [Route("api/transactions")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly TransactionService _transactionService;

        public TransactionController(TransactionService transactionService)
        {
            this._transactionService = transactionService;
        }

        [HttpPost]
        public async Task<ActionResult<Transaction>> AddTransaction([FromBody] TransactionReqDto reqDto)
        {
            try
            {
                var newTransaction = new Transaction
                {
                    CustomerId = reqDto.CustomerId,
                    AmountTotal = reqDto.AmountTotal,
                    PaymentMethod = reqDto.PaymentMethod,
                    Items = reqDto.Items.Select(item => new TransactionItems
                    {
                        FoodId = item.FoodId,
                        Quantity = item.Quantity
                    
                    }).ToList()
                };

                var addedTransaction = await _transactionService.AddTransaction(newTransaction);
                return Ok(addedTransaction);
            }
            catch (Exception ex)
            {
                // Memberikan respons yang lebih spesifik saat terjadi kesalahan
                return BadRequest(new { ErrorMessage = "Failed to add transaction.", ErrorDetails = ex.Message });
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<Transaction>>> GetAllTransaction()
        {
            try
            {
                var transactions = await _transactionService.GetAllTransactions();
                return Ok(transactions);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Transaction>> GetTransactionById(int id)
        {
            try
            {
                var findById = await _transactionService.GetTransactionById(id);
                if (findById == null)
                {
                    return NotFound($"Customer with ID {id} not found");
                }
                return Ok(findById);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTransaction(int id)
        {
            try
            {
                await _transactionService.DeleteTransaction(id);

                return Ok(new OkObjectResult($"Delete ID {id} Successfully")); ;
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> EditTransaction([FromBody] TransactionReqUpdateDto reqDto, int id)
        {
            try
            {
                var updated = await _transactionService.UpdateTransaction(reqDto, id);
                if (updated)
                {
                    return Ok($"Transaction with ID {id} updated successfully.");
                }

                return NotFound($"Transaction with ID {id} not found.");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
