using System.Threading.Tasks;

namespace Domain.Services.Interfaces
{
    public interface ITokenCreationService
    {
        Task<string> Create(string name, string symbol);
    }
}