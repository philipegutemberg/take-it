using System.Threading.Tasks;

namespace Domain.Services.Interfaces
{
    public interface ITicketValidationService
    {
        Task<byte[]> GetTicketImage(string ticketId);

        Task<bool> IsValid(string ticketText);
    }
}