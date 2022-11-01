using System.Threading.Tasks;

namespace Domain.Services.Interfaces
{
    public interface ITicketValidationService
    {
        Task<byte[]> GetTicketImage(string username, string ticketCode);

        Task<bool> IsValid(string qrCodeText);
    }
}