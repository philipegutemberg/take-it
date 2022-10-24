using System.Threading.Tasks;

namespace Ethereum.Nethereum.Services.Interfaces
{
    public interface IFileStorageService
    {
        Task<string> SaveAndGetLink(string key, string content);
    }
}