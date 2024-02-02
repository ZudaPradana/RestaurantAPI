using RestaurantAPI.DTO.Request;
using RestaurantAPI.Models;

namespace RestaurantAPI.Repository
{
    public interface ITransactionRepo<T>
    {
        Task<List<T>> GetAllTransactions();

        Task<T> GetTransactionById(int id);

        Task<T> AddTransaction(Transaction transaction);

        Task<bool> UpdateTransaction(TransactionReqUpdateDto reqDto, int id);

        Task<bool> DeleteTransaction(int id);
    }
}
