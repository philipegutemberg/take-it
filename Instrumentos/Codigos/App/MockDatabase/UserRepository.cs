using System;
using System.Linq;
using System.Threading.Tasks;
using Domain.Enums;
using Domain.Exceptions;
using Domain.Models.Users;
using Domain.Repositories;
using MockDatabase.Base;

namespace MockDatabase
{
    internal class UserRepository : MockBaseRepository<User>, IUserRepository
    {
        public Task<User> Save(User user)
        {
            InsertOrUpdate(user.Username, user);
            return Task.FromResult(user);
        }

        public Task<User> GetUser(string username)
        {
            var user = Storage.Values.FirstOrDefault(u => u.Username == username);
            if (user == null)
                throw new UserNotFoundException(username);

            return Task.FromResult(user);
        }

        public async Task<Customer> GetCustomer(string username)
        {
            User user = await GetUser(username);
            if (user.Role != EnumUserRole.Customer)
                throw new UserNotFoundException(username);

            return (Customer)user;
        }
    }
}