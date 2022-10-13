using System.Threading.Tasks;

namespace Domain.Services.Interfaces
{
    public interface ITokenService
    {
        Task Mint(string contractAddress);

        Task TransferToTicketOwner(string contractAddress, string ticketOwnerAddress, long tokenId);
        
        Task<long> GetBalance(string contractAddress, string ownerAddress);
    }
}