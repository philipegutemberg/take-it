using System;
using System.Collections.Generic;
using System.Linq;
using Amazon;
using Application.Injection;
using AWS_S3.Injection;
using Database.SQLServer.Injection;
using Domain.Enums;
using Domain.Injection;
using Domain.Models;
using Domain.Models.Users;
using Domain.Services.Interfaces;
using Ethereum.Nethereum.Injection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MockDatabase.Injection;
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

Event @event = new(
    new DateTime(2022, 3, 24),
    new DateTime(2022, 3, 26),
    "Autódromo de Interlagos / SP",
    "Lolapalooza 2023",
    "Lolapalooza 2023",
    "LOLA23",
    $"https://portalpopline.com.br/wp-content/uploads/2022/08/Lollapalooza-2023-800x800.jpg");

EventTicketType eventTicketType = new(
    @event,
    "Lola Pass (All days)",
    new DateTime(2022, 3, 24),
    new DateTime(2022, 3, 26),
    EnumTicketQualification.Complimentary,
    1250,
    60
);

Customer customer = new(
    "lais",
    "sialmor4es",
    "Laís Felipe de Moraes",
    "sialmoraes@outlook.com.br",
    "+5532",
    "0xCa2acA0E413A6cbbC096F03E0896D28867f431b4"
    );

var customerService = provider.GetRequiredService<IUserService<Customer>>();
customer = await customerService.Create(customer);

var eventService = provider.GetRequiredService<IEventService>();
await eventService.Register(@event, new List<EventTicketType> { eventTicketType });

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
await tokenTransferService.Transfer(customer.Username, tickets.First().Code);

balance = await tokenService.GetCustomerBalance(@event, customer);

Console.WriteLine($"Balance minted: {balance} tokens");