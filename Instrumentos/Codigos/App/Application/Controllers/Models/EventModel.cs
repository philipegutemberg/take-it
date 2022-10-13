using System;

namespace Application.Controllers.Models
{
    public record EventModel
    {
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public string Title { get; init; }
        public string Description { get; init; }
        public string Ticker { get; init; }
        public decimal Price { get; init; }
        public long TicketsMaxCount { get; init; }
    }
}