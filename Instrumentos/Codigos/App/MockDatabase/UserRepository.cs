using System.Linq;
using System.Threading.Tasks;
using Domain.Exceptions;
using Domain.Models.Users;
using Domain.Repositories;
using MockDatabase.Base;

namespace MockDatabase
{
    internal class UserRepository : MockBaseRepository<User>, IUserRepository<User>
    {
        public Task<User> Save(User user)
        {
            InsertOrUpdate(user.Username, user);
            return Task.FromResult(user);
        }

        public Task<User> GetByUsername(string username)
        {
            var user = Storage.Values.FirstOrDefault(u => u.Username == username);
            if (user == null)
                throw new UserNotFoundException(username);

            return Task.FromResult(user);
        }
    }
}