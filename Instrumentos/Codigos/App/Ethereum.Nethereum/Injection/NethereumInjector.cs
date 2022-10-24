using Domain.Services.Interfaces;
using Ethereum.Nethereum.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Ethereum.Nethereum.Injection
{
    public static class NethereumInjector
    {
        public static IServiceCollection InjectNethereumServices(
            this IServiceCollection services,
            string url,
            string walletWords,
            string walletPassword) =>
            services
                .AddTransient<SmartContracts.ERC721Mintable.Deployment.DeploymentService>()
                .AddTransient<DeploymentService>()
                .AddTransient<MnemonicService>()
                .AddSingleton(p => new WalletService(walletWords, walletPassword))
                .AddSingleton(p => new Web3Service(url))
                .AddTransient<AccountService>()
                .AddTransient<OwnerAccountsService>()
                .AddTransient<ITokenCreationService, ERC271MintableCreationService>()
                .AddTransient<ITokenService, TicketTokenService>()
                .AddTransient<ITokenMetadataService, MetadataFileService>();
    }
}