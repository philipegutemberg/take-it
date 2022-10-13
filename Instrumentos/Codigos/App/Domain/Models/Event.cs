using System;
using Domain.Models.Users;

namespace Domain.Models
{
    public record Event
    {
        public Event(DateTime date, string location, string title, string description, string ticker, decimal price, long ticketsCount)
        {
            Id = Guid.NewGuid().ToString();
            Date = date;
            Location = location;
            Title = title;
            Description = description;
            Ticker = ticker;
            PriceBRL = price;
            TicketStock = new EventTicketStock(Id, ticketsCount);
        }

        public Event() { }

        public string Id { get; init; }
        public DateTime Date { get; init; }
        public string Location { get; init; }
        public string Title { get; init; }
        public string Description { get; init; }
        public string Ticker { get; init; }
        public decimal PriceBRL { get; init; }
        public string? TokenContractId { get; set; }
        public EventTicketStock TicketStock { get; init; }
        public bool Available => !TicketStock.OutOfStock && Date >= DateTime.Today;

        public bool TryPurchase(Customer customer, out Ticket? ticket)
        {
            return TicketStock.TryIssueTicket(customer, out ticket);
        }
    }
}