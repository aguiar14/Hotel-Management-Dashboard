using Backend.Data.Entities;
using Backend.Interfaces;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Data.Repositories
{
    public class CustomerSqlReposistory: ICustomerRepository
    {

        private readonly DbContext _context;

        public CustomerSqlReposistory(DbContext context)
        {
            _context = context;
        }

        public async Task<Customer> CreateCustomerAsync(Customer customer)
        {
            var entityCustomer = new CustomerEntity
            {
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email,
                PhoneNumber = customer.PhoneNumber,
                Country = customer.Country,
                CreatedAt = DateTime.UtcNow,
            };

            _context.Customer.Add(entityCustomer);
            await _context.SaveChangesAsync();
            customer.Id = entityCustomer.Id;

            return customer;
        }

        public async Task<Customer> GetCustomerByIdAsync(int id)
        {
            var customer  = await _context.Customer
                .Where(c => c.Id == id)
                .Select(c => new Customer
                {
                    Id = c.Id,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    Email = c.Email,
                    PhoneNumber = c.PhoneNumber,
                    Country = c.Country
                })
                .FirstOrDefaultAsync();

            return customer;
        }

        public async Task<ApiResponse<Customer>> GestCustomersAsync()
        {
            IQueryable<CustomerEntity> query = _context.Customer;

            var count = await query.CountAsync();
            var items = await query
                .Select(c => new Customer
                {
                    Id = c.Id,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    Email = c.Email,
                    PhoneNumber = c.PhoneNumber,
                    Country = c.Country
                })
                .ToListAsync();

            return new ApiResponse<Customer> { items=items, TotalCount = count };
        }

        public async Task<Customer?> UpdateCustomerAsync(Customer customer)
        {
            var existingCustomer = await _context.Customer.FirstOrDefaultAsync(c => c.Id == customer.Id);

            if(existingCustomer == null)
            {
                return null;
            }

            existingCustomer.FirstName = customer.FirstName;
            existingCustomer.LastName = customer.LastName;
            existingCustomer.Email = customer.Email;
            existingCustomer.PhoneNumber = customer.PhoneNumber;
            existingCustomer.Country = customer.Country;

            _context.Customer.Update(existingCustomer);
            await _context.SaveChangesAsync();

            return customer;

        }

        public async Task<bool> DeleteCustomerAsync(int id)
        {
            var existingCustomer = await _context.Customer.FirstOrDefaultAsync(c => c.Id == id);

            if(existingCustomer == null)
            {
                return false;
            }

            _context.Customer.Remove(existingCustomer);
            await _context.SaveChangesAsync();

            return true;

        }
    }
}
