using Backend.Models;

namespace Backend.Controllers.Post
{
    public class CustomerPost
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Country { get; set; }

        public Customer ToCustomerModel()
        {
            return new Customer
            {
                FirstName = this.FirstName,
                LastName = this.LastName,
                Email = this.Email,
                PhoneNumber = this.PhoneNumber,
                Country = this.Country
            };
        }
    }
}
