using System.Linq;
using System.Threading.Tasks;
using Domain.Exceptions;
using Domain.Models.Users;
using Domain.Repositories;
using MockDatabase.Base;

namespace MockDatabase
{
    internal class CustomerRepository : MockBaseRepository<Customer>, IUserRepository<Customer>
    {
        private int _count = 0;

        public Task<Customer> Save(Customer user)
        {
            InsertOrUpdate(user.Username, user);
            return Task.FromResult(new Customer(
                ++_count,
                user.Code,
                user.Username,
                user.Password,
                user.FullName,
                user.Email,
                user.Phone,
                user.WalletAddress,
                user.TicketsCodes));
        }

        public Task<Customer> GetByUsername(string username)
        {
            var user = Storage.Values.FirstOrDefault(u => u.Username == username);
            if (user == null)
                throw new UserNotFoundException(username);

            return Task.FromResult(user);
        }
    }
}