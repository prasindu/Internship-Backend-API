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

        //GET /api/customers

        [HttpGet]

        public async Task<IActionResult> GetCustomers()
        {
            try
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
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        // GET /api/Customer/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerID(int id)
        {
            try
            {
                var customer = await _contex.customers.FindAsync(id);
                if (customer == null)
                {
                    return NotFound("Customer not find by this id");
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
            }catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }   
        }

        // POST /api/Customer
        [HttpPost]
        public async Task<IActionResult> CreateCustomers(CreateUpdateCustomerDto dto)
        {
            try
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

                return CreatedAtAction(nameof(CreateCustomers), new { id = cust.Id }, returncust);

            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // PUT /api/customers/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer(int id ,CreateUpdateCustomerDto dto)
        {
            try
            {
                var custs = await _contex.customers.FindAsync(id);
                if (custs == null)
                {
                    return NotFound("not item by id");
                }

                custs.Name = dto.Name;
                custs.Email = dto.Email;
                custs.Phone = dto.Phone;
                custs.Address = dto.Address;
                custs.Status = dto.Status;

                await _contex.SaveChangesAsync();
                return NoContent();

            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        // DELETE /api/custrome/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeteteCustomer(int id)
        {
            try
            {
                var cusom = await _contex.customers.FindAsync(id);
                if (cusom == null)
                {
                    return NotFound("not found item by this id");
                }

                _contex.customers.Remove(cusom);
                await _contex.SaveChangesAsync();
                return NoContent();
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }
    }
}
