using RestaurantAPI.DTO.Request;
using RestaurantAPI.Models;

namespace RestaurantAPI.Services.Repository
{
    public interface ICustomerRepo<T>
    {

        Task<List<T>> GetAllCustomerAsync();

        Task<T> GetCustomerByIdAsync(int id);

        Task<T> AddCustomerAsync(CustomerReqDTO reqDTO);

        Task<bool> UpdateCustomerAsync(CustomerReqDTO reqDTO, int id);

        Task<bool> DeleteCustomerAsync(int id);

    }
}
