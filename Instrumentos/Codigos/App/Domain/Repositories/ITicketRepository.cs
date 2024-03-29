using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Repositories
{
    public interface ITicketRepository
    {
        Task<Ticket> Insert(Ticket ticket);

        Task<Ticket> GetByCode(string code);

        Task<IEnumerable<Ticket>> GetAllOwnedByCustomer(string customerCode);

        Task<Ticket> GetByTokenId(string eventCode, long tokenId);

        Task UpdateOwner(Ticket ticket);

        Task UpdateUsedOnEvent(Ticket ticket);
    }
}