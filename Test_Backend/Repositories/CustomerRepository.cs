using Microsoft.EntityFrameworkCore;
using Test_Backend.Data;
using Test_Backend.Model;

namespace Test_Backend.Repositories
{
    public class CustomerRepository:ICustomerRepository
    {
        private readonly AppDbContex _context;
        public CustomerRepository(AppDbContex context) { _context = context; }

        public async Task<IEnumerable<Customer>> GetAllCustomersAsync() => await _context.customers.ToListAsync();
        public async Task<Customer> GetCustomerByIdAsync(int id) => await _context.customers.FindAsync(id);
        public async Task<Customer> CreateCustomerAsync(Customer customer)
        {
            _context.customers.Add(customer);
            await _context.SaveChangesAsync();
            return customer;
        }
        public async Task UpdateCustomerAsync(Customer customer)
        {
            _context.customers.Update(customer);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteCustomerAsync(Customer customer)
        {
            _context.customers.Remove(customer);
            await _context.SaveChangesAsync();
        }
    }

}
