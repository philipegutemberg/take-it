using System.Threading.Tasks;
using Domain.Models.Users;
using Domain.Repositories;
using Domain.Services.Interfaces;

namespace Domain.Services
{
    internal class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ITokenService _tokenService;

        public CustomerService(ICustomerRepository customerRepository, ITokenService tokenService)
        {
            _customerRepository = customerRepository;
            _tokenService = tokenService;
        }

        public async Task<CustomerUser> Create(CustomerUser user)
        {
            CustomerUser created = await _customerRepository.Insert(user);
            string internalAddress = await _tokenService.GetCustomerInternalAddress(created);
            created.AssignInternalAddress(internalAddress);
            await _customerRepository.UpdateInternalAddress(created);
            return created;
        }

        public async Task<string> GetInternalAddress(string username)
        {
            var customer = await Get(username);
            return customer.InternalAddress;
        }

        public async Task<CustomerUser> Get(string username)
        {
            return await _customerRepository.GetByUsername(username);
        }
    }
}