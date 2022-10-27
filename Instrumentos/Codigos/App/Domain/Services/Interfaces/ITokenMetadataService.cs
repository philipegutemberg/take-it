using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Services.Interfaces
{
    public interface ITokenMetadataService
    {
        Task<string> BuildAndGenerateLink(Event @event, EventTicketType eventType);
    }
}