using System;

namespace Domain.Models
{
    public record Ticket
    {
        public Ticket(string eventId)
        {
            Id = Guid.NewGuid().ToString();
            EventId = eventId;
        }

        public Ticket() { }
 
        public string Id { get; set; }
        public string EventId { get; init; }
        public DateTime? PurchaseDate { get; set; }
        public string OwnerId { get; set; }

        public void Purchase(string customerId)
        {
            PurchaseDate = DateTime.UtcNow;
            OwnerId = customerId;
        }
    }
}