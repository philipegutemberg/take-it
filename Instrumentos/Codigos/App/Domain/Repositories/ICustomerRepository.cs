using System.Threading.Tasks;
using Domain.Models.Users;

namespace Domain.Repositories
{
    public interface ICustomerRepository : IUserRepository<Customer>
    {
        Task UpdateInternalAddress(Customer customer);

        Task<Customer?> GetByInternalAddress(string internalAddress);
    }
}