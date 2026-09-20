using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Test_Backend.Dto;
using Test_Backend.Services;

namespace Test_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        public CustomerController(ICustomerService customerService) 
        { 
            _customerService = customerService; 
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomers() => Ok(await _customerService.GetAllCustomersAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerID(int id)
        {
            var customer = await _customerService.GetCustomerByIdAsync(id);
            if (customer == null) return NotFound("Customer not found");
            return Ok(customer);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomers(CreateUpdateCustomerDto dto)
        {
            var createdCustomer = await _customerService.CreateCustomerAsync(dto);
            return CreatedAtAction(nameof(GetCustomerID), new { id = createdCustomer.Id }, createdCustomer);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer(int id, CreateUpdateCustomerDto dto)
        {
            var isUpdated = await _customerService.UpdateCustomerAsync(id, dto);
            if (!isUpdated) return NotFound("Customer not found");
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var isDeleted = await _customerService.DeleteCustomerAsync(id);
            if (!isDeleted) return NotFound("Customer not found");
            return NoContent();
        }
    }
}