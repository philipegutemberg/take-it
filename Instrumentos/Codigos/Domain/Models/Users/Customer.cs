using System.Collections.Generic;
using Domain.Enums;

namespace Domain.Models.Users
{
    public record Customer : User
    {
        public Customer(string fullName, string email, string phone, string username, string password) 
            : base(username, password)
        {
            this.FullName = fullName;
            this.Email = email;
            this.Phone = phone;
            TicketsIds = new List<string>();
        }

        public Customer() { }
        
        public string FullName { get; init; }
        public string Email { get; init; }
        public string Phone { get; init; }
        public List<string> TicketsIds { get; init; }

        public void AssignTicket(string ticketId)
        {
            TicketsIds.Add(ticketId);
        }

        public override EnumUserRole Role => EnumUserRole.Customer;
    }
}