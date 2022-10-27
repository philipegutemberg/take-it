using Domain.Models.Users;

namespace Domain.Repositories
{
    public interface ICustomerRepository : IUserRepository<Customer>
    {
    }
}