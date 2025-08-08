using Backend.Models;

namespace Backend.Interfaces
{
    public interface ICustomerRepository
    {
        Task<Customer>GetCustomerByIdAsync(int id);
        Task<ApiResponse<Customer>> GestCustomersAsync();
        Task<Customer>CreateCustomerAsync(Customer customer);
        Task<Customer> UpdateCustomerAsync(Customer customer);
        Task<bool> DeleteCustomerAsync(int id);

    }
}
