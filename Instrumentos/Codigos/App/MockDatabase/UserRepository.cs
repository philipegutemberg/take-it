using System.Linq;
using System.Threading.Tasks;
using Domain.Enums;
using Domain.Models;
using Domain.Models.Users;
using Domain.Repositories;
using MockDatabase.Base;

namespace MockDatabase
{
    internal class UserRepository : MockBaseRepository<User>, IUserRepository
    {
        public UserRepository()
        {
            Insert("1",
                new Customer("Philipe Gutemberg", "philipega@live.com", "31994793424", "philipe", "minhasenha"));
            
            Insert("2", new BackOffice("operacional", "minhasenha"));
        }
        
        public Task<User?> GetUser(string username) =>
            Task.FromResult(_storage.Values.FirstOrDefault(u => u.Username == username));
    }
}