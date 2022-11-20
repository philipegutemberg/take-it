using System.Threading.Tasks;

namespace Domain.Services.Interfaces
{
    public interface ITicketValidationService
    {
        Task<string> GetTicketHash(string username, string ticketCode);

        Task<bool> IsValid(string qrCodeText);
    }
}