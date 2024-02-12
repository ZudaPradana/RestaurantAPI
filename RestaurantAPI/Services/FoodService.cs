using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Data;
using RestaurantAPI.DTO.Request;
using RestaurantAPI.Models;
using RestaurantAPI.Services.Repository;

namespace RestaurantAPI.Services
{
    public class FoodService : IFoodRepo<Food>
    {
        private readonly AppDbContext _context;

        public FoodService(AppDbContext context)
        {
            this._context = context;
        }

        public async Task<Food> AddFoodAsync(FoodReqDTO reqDTO)
        {
            var food = new Food()
            {
                Name = reqDTO.Name, 
                Description = reqDTO.Description, 
                FoodType = reqDTO.FoodType, 
                Price = reqDTO.Price
            };
            await _context.Foods.AddAsync(food);
            await _context.SaveChangesAsync();

            return food;
        }

        public async Task<bool> DeleteFoodAsync(int id)
        {
            var deleteById = await GetFoodByIdAsync(id);

            if(deleteById != null)
            {
                _context.Foods.Remove(deleteById);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<List<Food>> GetAllFoodAsync()
        {
           return await _context.Foods.ToListAsync();
        }

        public async Task<Food> GetFoodByIdAsync(int id)
        {
            var findById = await _context.Foods.FindAsync(id);

            if(findById != null)
            {
                return findById;
            }

            throw new DirectoryNotFoundException($"Customer With ID {id} Not Found");
        }

        public async Task<bool> UpdateFoodAsync(FoodReqDTO reqDto, int id)
        {
            var findById = await GetFoodByIdAsync(id);

            var food = new Food()
            {
                Name = reqDto.Name,
                Price = reqDto.Price,
                Description = reqDto.Description,
                FoodType = reqDto.FoodType,
            };

            if(findById != null)
            {
                findById.Name = reqDto.Name;
                findById.Description = reqDto.Description;
                findById.Price = reqDto.Price;
                findById.FoodType = reqDto.FoodType;

                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }
}
