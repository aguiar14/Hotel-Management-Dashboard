using Backend.Models;

namespace Backend.Controllers.Put
{
    public class CustomerPut
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Country { get; set; }

        public Customer ToCustomerModel(int id)
        {
            return new Customer
            {
                Id = id,
                FirstName = this.FirstName,
                LastName = this.LastName,
                Email = this.Email,
                PhoneNumber = this.PhoneNumber,
                Country = this.Country
            };
        }
    }

}
