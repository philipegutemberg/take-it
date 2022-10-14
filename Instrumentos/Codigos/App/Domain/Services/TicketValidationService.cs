using System.Threading.Tasks;
using Domain.Services.Interfaces;

namespace Domain.Services
{
    internal class TicketValidationService : ITicketValidationService
    {
        private readonly IQRCodeService _qrCodeService;

        public TicketValidationService(IQRCodeService qrCodeService)
        {
            _qrCodeService = qrCodeService;
        }
        
        public async Task<byte[]> GetTicketImage(string ticketId)
        {
            return await _qrCodeService.Generate("Esse TCC vai sair!!!!");
        }

        public Task<bool> IsValid(string ticketText)
        {
            throw new System.NotImplementedException();
        }
    }
}