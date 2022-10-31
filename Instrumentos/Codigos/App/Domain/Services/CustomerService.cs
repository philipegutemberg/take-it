using System.Threading.Tasks;
using Domain.Models.Users;
using Domain.Repositories;
using Domain.Services.Interfaces;

namespace Domain.Services
{
    internal class CustomerService : UserService<Customer>, ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ITokenService _tokenService;

        public CustomerService(ICustomerRepository customerRepository, ITokenService tokenService)
            : base(customerRepository)
        {
            _customerRepository = customerRepository;
            _tokenService = tokenService;
        }

        public override async Task<Customer> Create(Customer user)
        {
            Customer created = await base.Create(user);
            string internalAddress = await _tokenService.GetCustomerInternalAddress(created);
            created.AssignInternalAddress(internalAddress);
            await _customerRepository.UpdateInternalAddress(created);
            return created;
        }
    }
}