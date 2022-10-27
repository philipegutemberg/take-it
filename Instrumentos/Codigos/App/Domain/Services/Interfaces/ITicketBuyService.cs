using System.Threading.Tasks;

namespace Domain.Services.Interfaces
{
    public interface ITicketBuyService
    {
        Task Buy(string username, string eventTicketTypeCode);
    }
}