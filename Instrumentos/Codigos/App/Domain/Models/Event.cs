using System;
using System.Collections.Generic;
using Domain.Models.Users;

namespace Domain.Models
{
    public record Event
    {
        public Event(
            DateTime startDate,
            DateTime endDate,
            string location,
            string title,
            string description,
            string ticker,
            string imageUrl)
        {
            Code = Guid.NewGuid().ToString();
            StartDate = startDate;
            EndDate = endDate;
            Location = location;
            Title = title;
            Description = description;
            Ticker = ticker;
            TokenContractAddress = string.Empty;
            ImageUrl = imageUrl;
            TicketTypesCodes = new List<string>();
            AlreadyIssuedTickets = 0;
        }

        public Event(
            string code,
            DateTime startDate,
            DateTime endDate,
            string location,
            string title,
            string description,
            string ticker,
            string tokenContractAddress,
            string imageUrl,
            List<string> ticketTypesCodes,
            long alreadyIssuedTickets)
        {
            Code = code;
            StartDate = startDate;
            EndDate = endDate;
            Location = location;
            Title = title;
            Description = description;
            Ticker = ticker;
            TokenContractAddress = tokenContractAddress;
            ImageUrl = imageUrl;
            TicketTypesCodes = ticketTypesCodes;
            AlreadyIssuedTickets = alreadyIssuedTickets;
        }

        public string Code { get; }
        public DateTime StartDate { get; }
        public DateTime EndDate { get; }
        public string Location { get; }
        public string Title { get; }
        public string Description { get; }
        public string Ticker { get; }
        public string TokenContractAddress { get; private set; }
        public string ImageUrl { get; }
        public long AlreadyIssuedTickets { get; private set; }
        public List<string> TicketTypesCodes { get; }

        public bool TryIssueTicket(Customer customer, EventTicketType ticketType, out Ticket? ticket)
        {
            return ticketType.TryIssueTicket(customer, ++AlreadyIssuedTickets, out ticket);
        }

        public void AssignTicketType(string ticketTypeCode)
        {
            TicketTypesCodes.Add(ticketTypeCode);
        }

        public void AssignTokenContractAddress(string address)
        {
            if (!string.IsNullOrEmpty(TokenContractAddress))
                throw new Exception("Token contract address already assigned.");

            TokenContractAddress = address;
        }
    }
}