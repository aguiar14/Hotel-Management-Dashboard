using Backend.Controllers.Post;
using Backend.Controllers.Put;
using Backend.Interfaces;
using Backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase // FIX: Inherit from ControllerBase to access Created()
    {
        readonly ILogger<CustomersController> _logger;
        readonly ICustomerRepository _customerRepository;

        public CustomersController(ILogger<CustomersController> logger, ICustomerRepository customerRepository)
        {
            _logger = logger;
            _customerRepository = customerRepository;
        }

        [HttpPost]
        public async Task<ActionResult<Customer>> Add(CustomerPost customer)
        {
            var newCustomer = await _customerRepository.CreateCustomerAsync(customer.ToCustomerModel());
            return Created(newCustomer.Id.ToString(), newCustomer);
        }



        [HttpGet]
        public Task<ApiResponse<Customer>> Get()
        {
            return _customerRepository.GestCustomersAsync();
        }


        [HttpGet("{customerId}")]
        public async Task<ActionResult<Customer>> GetById(int customerId)
        {
            var existingCustomer = await _customerRepository.GetCustomerByIdAsync(customerId);
            return existingCustomer == null ? NotFound() : Ok(existingCustomer);
        }


        [HttpPut("{customerId}")]
        public async Task<ActionResult<Customer?>> Update(CustomerPut customer, int customerId)
        {
            var updated = await _customerRepository.UpdateCustomerAsync(customer.ToCustomerModel(customerId));
            return updated == null ? NotFound() : Ok(updated);
        }



        [HttpDelete("{customerId}")]
        public async Task<ActionResult<IEnumerable<Customer>>> DeleteCustomer(int customerId)
        {
            var deleted = await _customerRepository.DeleteCustomerAsync(customerId);
            return Accepted(deleted);

        }
    }
}
