using Test_Backend.Dto;

namespace Test_Backend.Services
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerDto>> GetAllCustomersAsync();
        Task<CustomerDto> GetCustomerByIdAsync(int id);
        Task<CustomerDto> CreateCustomerAsync(CreateUpdateCustomerDto dto);
        Task<bool> UpdateCustomerAsync(int id, CreateUpdateCustomerDto dto);
        Task<bool> DeleteCustomerAsync(int id);
    }
}
