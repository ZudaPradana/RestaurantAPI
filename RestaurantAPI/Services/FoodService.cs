using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Data;
using RestaurantAPI.DTO.Request;
using RestaurantAPI.Models;
using RestaurantAPI.Repository;

namespace RestaurantAPI.Services
{
    public class FoodService : IFoodRepo<Food>
    {
        private readonly AppDbContext _context;

        public FoodService(AppDbContext context)
        {
            this._context = context;
        }

        public async Task<Food> AddFood(Food food)
        {
            await _context.Foods.AddAsync(food);
            await _context.SaveChangesAsync();

            return food;
        }

        public async Task<bool> DeleteFood(int id)
        {
            var deleteById = await GetFoodById(id);

            if(deleteById != null)
            {
                _context.Foods.Remove(deleteById);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<List<Food>> GetAllFood()
        {
           return await _context.Foods.ToListAsync();
        }

        public async Task<Food> GetFoodById(int id)
        {
            var findById = await _context.Foods.FindAsync(id);

            if(findById != null)
            {
                return findById;
            }

            throw new DirectoryNotFoundException($"Customer With ID {id} Not Found");
        }

        public async Task<bool> UpdateFood(FoodReqDTO reqDto, int id)
        {
            var findById = await GetFoodById(id);

            if(findById != null)
            {
                findById.Name = reqDto.Name;
                findById.Description = reqDto.Description;
                findById.Price = reqDto.Price;
                findById.FoodType = reqDto.FoodType;

                return true;
            }

            return false;
        }
    }
}
