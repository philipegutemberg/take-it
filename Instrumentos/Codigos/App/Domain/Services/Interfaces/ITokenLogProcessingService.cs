using System.Threading.Tasks;

namespace Domain.Services.Interfaces
{
    public interface ITokenLogProcessingService
    {
        Task ProcessEventLog(string fromAddress, string toAddress, long tokenId);
    }
}