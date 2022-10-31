using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface ITokenEventProcessingRepository
    {
        Task SetLastProcessed(long lastProcessed);

        Task<long?> GetLastProcessed();
    }
}