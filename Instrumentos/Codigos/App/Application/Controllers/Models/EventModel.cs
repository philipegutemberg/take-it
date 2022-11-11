using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Application.Controllers.Models
{
    public record EventModel
    {
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        public string? Location { get; set; }
        [Required]
        public string? Title { get; set; }
        [Required]
        public string? Description { get; set; }
        [Required]
        public string? Ticker { get; set; }
        [Required]
        public string? ImageUrl { get; set; }
        [Required]
        public decimal ResaleFeePercentage { get; set; }
        [Required]
        public List<TicketTypesModel>? TicketTypes { get; set; }
    }
}