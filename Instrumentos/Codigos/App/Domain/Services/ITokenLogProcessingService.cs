using System.Threading.Tasks;

namespace Domain.Services
{
    public interface ITokenLogProcessingService
    {
        Task ProcessEventLog(string fromAddress, string toAddress, long tokenId);
    }
}