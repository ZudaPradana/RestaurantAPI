using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Data;
using RestaurantAPI.DTO.Request;
using RestaurantAPI.Models;
using RestaurantAPI.Services.Repository;

namespace RestaurantAPI.Services
{
    public class TransactionService : ITransactionRepo<Transaction>
    {
        private readonly AppDbContext _context;

        private readonly FoodService _foodService;

        private readonly CustomerService _customerService;

        public TransactionService(AppDbContext context, FoodService foodService, CustomerService customerService)
        {
            this._context = context;
            this._foodService = foodService;
            this._customerService = customerService;
        }

        public async Task<Transaction> AddTransaction(Transaction transaction)
        {
            transaction.TransactionDate = DateTime.UtcNow;

            // Hitung TotalPrice menggunakan metode CalculateTotalPrice
            transaction.TotalPrice = await CalculateTotalPrice(transaction.Items);

            // Hitung Quantity
            transaction.Quantity = CalculateTotalQuantity(transaction.Items);

            // Hitung TotalChange
            transaction.TotalChange = transaction.AmountTotal - transaction.TotalPrice;

            transaction.Customer = await _customerService.GetCustomerByIdAsync(transaction.CustomerId);

            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();

            return transaction;
        }

        public int CalculateTotalQuantity(List<TransactionItems> items)
        {
            return items?.Sum(item => item.Quantity) ?? 0;
        }


        public async Task<float> CalculateTotalPrice(List<TransactionItems> items)
        {
            float totalPrice = 0;

            foreach (var item in items)
            {
                // Menggunakan service untuk mendapatkan informasi makanan berdasarkan ID
                var food = await _foodService.GetFoodByIdAsync(item.FoodId);

                if (food != null)
                {
                    // Hitung total harga dari setiap item
                    totalPrice += item.Quantity * food.Price;
                }
                else
                {
                    throw new DirectoryNotFoundException($"Food With ID {item.FoodId} Not Found");
                }
            }

            return totalPrice;
        }


        public async Task<bool> DeleteTransaction(int id)
        {
            var deleteById = await GetTransactionById(id);

            if (deleteById != null)
            {
                _context.Transactions.Remove(deleteById);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<List<Transaction>> GetAllTransactions()
        {
            var transactions = await _context.Transactions
                .Include(t => t.Items) // Include the Items navigation property
                .ThenInclude(i => i.Food) // Include the Food navigation property for each item
                .Include(t => t.Customer) // Include the Customer navigation property
                .ToListAsync();

            return transactions;
        }

        public async Task<Transaction> GetTransactionById(int id)
        {
            var idTransaction = await _context.Transactions
                .Include(t => t.Items)
                .ThenInclude(i => i.Food)
                .Include(t => t.Customer)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (idTransaction != null)
            {
                return idTransaction;
            }

            throw new DirectoryNotFoundException($"Customer With ID {id} Not Found");
        }

        public async Task<bool> UpdateTransaction(TransactionReqUpdateDto reqDto, int id)
        {
            var existingTransaction = await GetTransactionById(id);

            if(existingTransaction != null)
            {
                // Update properties of the existing transaction with the values from the provided transaction
                existingTransaction.AmountTotal = reqDto.AmountTotal;
                existingTransaction.PaymentMethod = reqDto.PaymentMethod;

                // Update items
                foreach (var existingItem in existingTransaction.Items)
                {
                    var newItem = reqDto.Items.Find(i => i.FoodId == existingItem.Food.Id);

                    if (newItem != null)
                    {
                        existingItem.Quantity = newItem.Quantity;
                    }
                }


                // Update Quantity, TotalPrice, TotalChange after updated Items
                existingTransaction.Quantity = CalculateTotalQuantity(existingTransaction.Items);
                existingTransaction.TotalPrice = await CalculateTotalPrice(existingTransaction.Items);
                existingTransaction.TotalChange = existingTransaction.AmountTotal - existingTransaction.TotalPrice;

                // Save changes to the database
                await _context.SaveChangesAsync();

                return true;
            }

            return false;
        }
    }
}
