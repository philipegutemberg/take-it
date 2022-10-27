using System.Collections.Generic;
using Domain.Enums;

namespace Domain.Models.Users
{
    public record Customer : User
    {
        public Customer(string username, string password, string fullName, string email, string phone, string walletAddress)
            : base(username, password)
        {
            FullName = fullName;
            Email = email;
            Phone = phone;
            WalletAddress = walletAddress;
            TicketsCodes = new List<string>();
        }

        public Customer(int id, string code, string username, string password, string fullName, string email, string phone, string walletAddress, List<string> ticketsCodes)
            : base(id, code, username, password)
        {
            FullName = fullName;
            Email = email;
            Phone = phone;
            WalletAddress = walletAddress;
            TicketsCodes = ticketsCodes;
        }

        public string FullName { get; }
        public string Email { get; }
        public string Phone { get; }
        public string WalletAddress { get; }
        public List<string> TicketsCodes { get; }

        public void AssignTicket(string ticketCode)
        {
            TicketsCodes.Add(ticketCode);
        }

        public override EnumUserRole Role => EnumUserRole.Customer;
    }
}