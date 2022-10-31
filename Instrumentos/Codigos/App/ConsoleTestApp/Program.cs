using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Amazon;
using Application.Injection;
using AWS_S3.Injection;
using Database.SQLServer.Injection;
using Domain.Enums;
using Domain.Injection;
using Domain.Models;
using Domain.Models.Users;
using Domain.Repositories;
using Domain.Services.Interfaces;
using Ethereum.Nethereum.Injection;
using Ethereum.Nethereum.SmartContracts.ERC721Mintable.BlockProcessor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QRCode.BarCode.Injection;

IConfiguration configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

var provider = new ServiceCollection()
    .AddSingleton(configuration)
    .InjectDomainServices()
    .InjectApplicationServices()
    .InjectSqlServerServices()
    .InjectNethereumServices(
        $"https://summer-lingering-silence.ethereum-goerli.discover.quiknode.pro/ee2b11130f5ef374d83453df4e8adf8b2f70b840/",
        "ripple scissors kick mammal hire column oak again sun offer wealth tomorrow wagon turn fatal",
        "C@78726fFhd$Bj8")
    .InjectBarcodeServices()
    .InjectS3Services("AKIAW7G7XFRODL3YN5UO", "n0bSUIdqLQeMm+xIMiKXl0X8mwPM2q4U8DlL8nOD", RegionEndpoint.USEast1)
    .BuildServiceProvider();

var customerService = provider.GetRequiredService<ICustomerService>();
Customer customer = await customerService.Get("lais");

var eventService = provider.GetRequiredService<IEventService>();
var available = (await eventService.ListAllAvailable()).First();
var @event = available.Key;
var eventTicketType = available.Value.First();

var tokenService = provider.GetRequiredService<ITokenService>();
long balance = await tokenService.GetCustomerBalance(@event, customer);

Console.WriteLine($"Balance minted: {balance} tokens");

var ticketBuyService = provider.GetRequiredService<ITicketBuyService>();
await ticketBuyService.Buy(customer.Username, eventTicketType.Code);

balance = await tokenService.GetCustomerBalance(@event, customer);

Console.WriteLine($"Balance minted: {balance} tokens");

var ticketService = provider.GetRequiredService<ITicketService>();
var tickets = await ticketService.ListMyTickets(customer.Username);

var tokenTransferService = provider.GetRequiredService<ITokenTransferService>();
await tokenTransferService.Transfer(customer.Username, tickets.OrderByDescending(t => t.TokenId).First().Code);

balance = await tokenService.GetCustomerBalance(@event, customer);

Console.WriteLine($"Balance minted: {balance} tokens");

var processingService = provider.GetRequiredService<IERC721BlockProcessor>();
await processingService.StartProcessing(@event, CancellationToken.None);