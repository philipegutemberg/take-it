using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Services.Interfaces
{
    public interface ITokenCreationService
    {
        Task<string> Create(Event @event);
    }
}