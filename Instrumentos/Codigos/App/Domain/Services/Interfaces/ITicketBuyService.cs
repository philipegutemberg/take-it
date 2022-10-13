using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Services.Interfaces
{
    public interface ITicketBuyService
    {
        Task Buy(string username, string eventId);
    }
}