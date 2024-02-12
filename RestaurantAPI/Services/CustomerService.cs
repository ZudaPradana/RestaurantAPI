using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Data;
using RestaurantAPI.DTO.Request;
using RestaurantAPI.Models;
using RestaurantAPI.Services.Repository;

namespace RestaurantAPI.Services
{
    public class CustomerService : ICustomerRepo<Customer>
    {
        private readonly AppDbContext _context;

        //inject db context
        public CustomerService(AppDbContext context)
        {
            this._context = context;
        }
        public async Task<List<Customer>> GetAllCustomerAsync()
        {
            return await _context.Customers.ToListAsync();
        }

        public async Task<Customer> GetCustomerByIdAsync(int id)
        {
            var findById = await _context.Customers.FindAsync(id);

            if (findById == null)
            {
                throw new DirectoryNotFoundException($"Customer With ID {id} Not Found");
            }

            return findById;
        }

        public async Task<Customer> AddCustomerAsync(CustomerReqDTO reqDTO)
        {
            var customer = new Customer
            {
                Name = reqDTO.Name,
                Email = reqDTO.Email,
                PhoneNumber = reqDTO.PhoneNumber,
            };
            //add data
            await _context.Customers.AddAsync(customer);
            //save after change
            await _context.SaveChangesAsync();

            return customer;
        }

        public async Task<bool> DeleteCustomerAsync(int id)
        {
            var deleteById = await GetCustomerByIdAsync(id);

            if(deleteById != null) {
                //remove
                _context.Customers.Remove(deleteById);
                //save after change
                await _context.SaveChangesAsync();   
                return true;
            }
            return false;
        }


        public async Task<bool> UpdateCustomerAsync(CustomerReqDTO reqDTO, int id)
        {
            var editById = await GetCustomerByIdAsync(id);

            var customer = new Customer
            {
                Name = reqDTO.Name,
                Email = reqDTO.Email,
                PhoneNumber = reqDTO.PhoneNumber,
            };

            if(editById != null)
            {
                editById.Name = customer.Name;
                editById.Email = customer.Email;
                editById.PhoneNumber = customer.PhoneNumber;

                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }
}
