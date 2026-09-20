using Test_Backend.Dto;
using Test_Backend.Model;
using Test_Backend.Repositories;

namespace Test_Backend.Services
{
    public class CustomerService:ICustomerService
    {
        private readonly ICustomerRepository _repository;
        public CustomerService(ICustomerRepository repository) { _repository = repository; }

        public async Task<IEnumerable<CustomerDto>> GetAllCustomersAsync()
        {
            var customers = await _repository.GetAllCustomersAsync();
            return customers.Select(u => new CustomerDto
            {
                Id = u.Id,
                Name = u.Name,
                Email = u.Email,
                Phone = u.Phone,
                Address = u.Address,
                Status = u.Status
            }).ToList();
        }

        public async Task<CustomerDto> GetCustomerByIdAsync(int id)
        {
            var customer = await _repository.GetCustomerByIdAsync(id);
            if (customer == null) return null;
            return new CustomerDto { Id = customer.Id, Name = customer.Name, Email = customer.Email, Phone = customer.Phone, Address = customer.Address, Status = customer.Status };
        }

        public async Task<CustomerDto> CreateCustomerAsync(CreateUpdateCustomerDto dto)
        {
            var customer = new Customer { Name = dto.Name, Email = dto.Email, Phone = dto.Phone, Address = dto.Address, Status = dto.Status };
            var created = await _repository.CreateCustomerAsync(customer);
            return new CustomerDto { Id = created.Id, Name = created.Name, Email = created.Email, Phone = created.Phone, Address = created.Address, Status = created.Status };
        }

        public async Task<bool> UpdateCustomerAsync(int id, CreateUpdateCustomerDto dto)
        {
            var existing = await _repository.GetCustomerByIdAsync(id);
            if (existing == null) return false;

            existing.Name = dto.Name; existing.Email = dto.Email; existing.Phone = dto.Phone; existing.Address = dto.Address; existing.Status = dto.Status;
            await _repository.UpdateCustomerAsync(existing);
            return true;
        }

        public async Task<bool> DeleteCustomerAsync(int id)
        {
            var existing = await _repository.GetCustomerByIdAsync(id);
            if (existing == null) return false;

            await _repository.DeleteCustomerAsync(existing);
            return true;
        }
    }
}
