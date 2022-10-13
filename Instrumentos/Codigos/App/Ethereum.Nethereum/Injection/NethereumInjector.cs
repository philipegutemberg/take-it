using Domain.Services.Interfaces;
using Ethereum.Nethereum.Services;
using Ethereum.Nethereum.Services.Interfaces;
using Ethereum.Nethereum.TicketTokenSmartContract.Deployment;
using Microsoft.Extensions.DependencyInjection;

namespace Ethereum.Nethereum.Injection
{
    public static class NethereumInjector
    {
        public static IServiceCollection InjectNethereumServices(
            this IServiceCollection services, 
            string url,
            string mainAccountPrivateKey,
            string mainAccountPublicKey,
            string tokenHolderAccountPrivateKey,
            string tokenHolderAccountPublicKey) => services
            .AddSingleton<IMainAccountFaucet>(p => new Web3Service(url, mainAccountPrivateKey, mainAccountPublicKey))
            .AddSingleton<ITokenHolderAccountFaucet>(p => new Web3Service(url, tokenHolderAccountPrivateKey, tokenHolderAccountPublicKey))
            .AddTransient<TicketTokenDeploymentService>()
            .AddTransient<DeploymentService>()
            .AddTransient<ITokenCreationService, TicketTokenCreationService>()
            .AddTransient<ITokenService, TicketTokenService>();
    }
}