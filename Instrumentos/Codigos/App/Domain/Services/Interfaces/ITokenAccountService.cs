using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Services.Interfaces
{
    public interface ITokenAccountService
    {
        Task<TokenAccount> GetAccount();
    }
}