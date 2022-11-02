using System.Threading.Tasks;
using Domain.Models.Users;

namespace Domain.Services.Interfaces
{
    public interface ICustomerService : IGenericUserService<CustomerUser>
    {
        Task<CustomerUser> Create(CustomerUser user);

        Task<string> GetInternalAddress(string username);
    }
}