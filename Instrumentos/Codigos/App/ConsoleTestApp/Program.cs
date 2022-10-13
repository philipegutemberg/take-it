using System;
using Domain.Services.Interfaces;
using Ethereum.Nethereum.Injection;
using Microsoft.Extensions.DependencyInjection;

var provider = new ServiceCollection()
        .InjectNethereumServices(
            "https://summer-lingering-silence.ethereum-goerli.discover.quiknode.pro/ee2b11130f5ef374d83453df4e8adf8b2f70b840/", 
            "0edde832e34196546524378154c0643e387a23865b727395638de7afd6516225",
            "0xCa2acA0E413A6cbbC096F03E0896D28867f431b4",
            "2fe5e94432dbfe9cfc4334d54601f9106d69b330684cc0489ab053dfcbfdbbaf",
            "0xB051AFC251C1d18f7Db5D6E1e2b53dFbC73d7e41")
    .BuildServiceProvider();

var tokenCreationService = provider.GetRequiredService<ITokenCreationService>();
string tokenAddress = await tokenCreationService.Create("ATR", "ATR0");

var tokenService = provider.GetRequiredService<ITokenService>();
long balance = await tokenService.GetBalance(tokenAddress, "0xCa2acA0E413A6cbbC096F03E0896D28867f431b4");

Console.WriteLine($"Balance: {balance} tokens");

await tokenService.Mint(tokenAddress);

balance = await tokenService.GetBalance(tokenAddress, "0xCa2acA0E413A6cbbC096F03E0896D28867f431b4");

Console.WriteLine($"Balance: {balance} tokens");