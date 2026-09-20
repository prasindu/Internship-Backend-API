using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Test_Backend.Data;
using Test_Backend.Dto;
using Test_Backend.Model;

namespace Test_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CustomerController : ControllerBase
    {
        private readonly AppDbContex _contex;

        public CustomerController(AppDbContex contex)
        {
            _contex = contex;
        }

        // GET /api/customers
        [HttpGet]
        public async Task<IActionResult> GetCustomers()
        {
            var customers = await _contex.customers.Select(u => new CustomerDto
            {
                Id = u.Id,
                Name = u.Name,
                Email = u.Email,
                Phone = u.Phone,
                Address = u.Address,
                Status = u.Status
            }).ToListAsync();

            return Ok(customers);
        }

        // GET /api/customers/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerID(int id)
        {
            var customer = await _contex.customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound("Customer not found by this id");
            }

            var cust = new CustomerDto
            {
                Id = customer.Id,
                Name = customer.Name,
                Email = customer.Email,
                Phone = customer.Phone,
                Address = customer.Address,
                Status = customer.Status
            };

            return Ok(cust);
        }

        // POST /api/customers
        [HttpPost]
        public async Task<IActionResult> CreateCustomers(CreateUpdateCustomerDto dto)
        {
            var cust = new Customer
            {
                Name = dto.Name,
                Email = dto.Email,
                Phone = dto.Phone,
                Address = dto.Address,
                Status = dto.Status
            };

            _contex.customers.Add(cust);
            await _contex.SaveChangesAsync();

            var returncust = new CustomerDto
            {
                Id = cust.Id,
                Name = cust.Name,
                Email = cust.Email,
                Phone = cust.Phone,
                Address = cust.Address,
                Status = cust.Status
            };

            
            return CreatedAtAction(nameof(GetCustomerID), new { id = cust.Id }, returncust);
        }

        // PUT /api/customers/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer(int id, CreateUpdateCustomerDto dto)
        {
            var custs = await _contex.customers.FindAsync(id);
            if (custs == null)
            {
                return NotFound("Item not found by id");
            }

            custs.Name = dto.Name;
            custs.Email = dto.Email;
            custs.Phone = dto.Phone;
            custs.Address = dto.Address;
            custs.Status = dto.Status;

            await _contex.SaveChangesAsync();
            return NoContent();
        }

        // DELETE /api/customers/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var cusom = await _contex.customers.FindAsync(id);
            if (cusom == null)
            {
                return NotFound("Item not found by this id");
            }

            _contex.customers.Remove(cusom);
            await _contex.SaveChangesAsync();
            return NoContent();
        }
    }
}