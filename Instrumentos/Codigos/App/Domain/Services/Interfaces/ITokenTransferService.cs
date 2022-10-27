using System.Threading.Tasks;

namespace Domain.Services.Interfaces
{
    public interface ITokenTransferService
    {
        Task Transfer(string username, string ticketCode);
    }
}