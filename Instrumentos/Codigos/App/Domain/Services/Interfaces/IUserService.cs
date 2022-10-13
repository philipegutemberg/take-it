using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<Ticket>> ListMyTickets(string username);
    }
}