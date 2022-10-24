using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Services.Interfaces
{
    public interface ITokenMetadataService
    {
        string Build(Event @event, EventTicketType eventType);

        Task<string> Save(string content, Event @event, EventTicketType eventType);
    }
}