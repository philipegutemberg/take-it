using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Repositories
{
    public interface ITicketRepository
    {
        Task<Ticket> Save(Ticket ticket);

        Task<Ticket> GetByCode(string code);

        Task<IEnumerable<Ticket>> GetAllOwnedByCustomer(string customerCode);
    }
}