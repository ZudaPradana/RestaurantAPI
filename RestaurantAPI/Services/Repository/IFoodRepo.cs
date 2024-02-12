using RestaurantAPI.DTO.Request;
using RestaurantAPI.Models;

namespace RestaurantAPI.Services.Repository
{
    public interface IFoodRepo<T>
    {
        Task<List<T>> GetAllFoodAsync();

        Task<T> GetFoodByIdAsync(int id);

        Task<T> AddFoodAsync(FoodReqDTO reqDto);

        Task<bool> UpdateFoodAsync(FoodReqDTO reqDto, int id);

        Task<bool> DeleteFoodAsync(int id);
    }
}
