using System.Threading.Tasks;
using Domain.Models.Users;

namespace Domain.Repositories
{
    public interface ICustomerRepository : IGenericUserRepository<CustomerUser>
    {
        Task<CustomerUser> Insert(CustomerUser user);

        Task UpdateInternalAddress(CustomerUser customer);

        Task<CustomerUser?> GetByInternalAddress(string internalAddress);

        Task<CustomerUser> GetByCode(string code);
    }
}