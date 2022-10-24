using System;
using Domain.Enums;
using Domain.Models;
using Domain.Models.Users;
using Domain.Services.Interfaces;
using Ethereum.Nethereum.Injection;
using Microsoft.Extensions.DependencyInjection;

var provider = new ServiceCollection()
        .InjectNethereumServices(
            $"https://summer-lingering-silence.ethereum-goerli.discover.quiknode.pro/ee2b11130f5ef374d83453df4e8adf8b2f70b840/",
            "ripple scissors kick mammal hire column oak again sun offer wealth tomorrow wagon turn fatal",
            "C@78726fFhd$Bj8")
    .BuildServiceProvider();

Event @event = new Event(DateTime.Now, "dffg", "ATR", "gdfgkd", "ATR2", 50, 10000);

Ticket ticket = new Ticket(@event.Code, 0);
ticket.TicketId = 245;

EventTicketType eventTicketType = new EventTicketType
{
    Access = "Pass (All days)",
    Code = Guid.NewGuid().ToString(),
    Qualification = EnumTicketQualification.Student,
    SingleDay = false,
    StartDate = new DateTime(2023, 3, 14),
    EndDate = new DateTime(2023, 3, 17),
    MetadataFileUrl = "dfsdfsdfdsfsfsdf",
    PriceBrl = 530
};

Customer customer = new Customer("dfsdf", "dfsf", "dsfsdf", "philipe", "dfsdffdf");
customer.Id = 22;
customer.WalletAddress = "0xCa2acA0E413A6cbbC096F03E0896D28867f431b4";

var tokenCreationService = provider.GetRequiredService<ITokenCreationService>();
@event.TokenContractAddress = await tokenCreationService.Create(@event);

var tokenService = provider.GetRequiredService<ITokenService>();
long balance = await tokenService.GetCustomerBalance(@event, customer);

Console.WriteLine($"Balance minted: {balance} tokens");

await tokenService.EmitToken(eventTicketType, ticket, @event, customer);
balance = await tokenService.GetCustomerBalance(@event, customer);

Console.WriteLine($"Balance minted: {balance} tokens");

await tokenService.TransferToCustomer(ticket, @event, customer);
balance = await tokenService.GetCustomerBalance(@event, customer);

Console.WriteLine($"Balance minted: {balance} tokens");