using System;

namespace Domain.Models
{
    public record Ticket
    {
        public Ticket(string eventId, long tokenId)
        {
            Id = Guid.NewGuid().ToString();
            EventId = eventId;
            TokenId = tokenId;
        }

        public Ticket() { }
 
        public string Id { get; set; }
        public string EventId { get; init; }
        public DateTime? PurchaseDate { get; set; }
        public string OwnerId { get; set; }
        public long TokenId { get; init; }

        public void Purchase(string customerId)
        {
            PurchaseDate = DateTime.UtcNow;
            OwnerId = customerId;
        }
    }
}