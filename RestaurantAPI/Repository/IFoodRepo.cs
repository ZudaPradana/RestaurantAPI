using RestaurantAPI.DTO.Request;
using RestaurantAPI.Models;

namespace RestaurantAPI.Repository
{
    public interface IFoodRepo<T>
    {
        Task<List<T>> GetAllFood();

        Task<T> GetFoodById(int id);

        Task<T> AddFood(Food food);

        Task<bool> UpdateFood(FoodReqDTO reqDto, int id);

        Task<bool> DeleteFood(int id);
    }
}
