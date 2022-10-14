using System.Threading.Tasks;

namespace Domain.Services.Interfaces
{
    public interface IQRCodeService
    {
        Task<byte[]> Generate(string text);
    }
}