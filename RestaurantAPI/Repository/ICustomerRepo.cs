using RestaurantAPI.Models;

namespace RestaurantAPI.Repository
{
    public interface ICustomerRepo<T>
    {
        
        Task<List<T>> GetAllCustomerAsync();

        Task<T> GetCustomerByIdAsync(int id);

        Task<T> AddCustomerAsync(Customer customer);

        Task<bool> UpdateCustomerAsync(Customer customer, int id);

        Task<bool> DeleteCustomerAsync(int id);
        
    }
}
