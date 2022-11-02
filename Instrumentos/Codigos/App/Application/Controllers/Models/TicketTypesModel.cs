using System;
using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Application.Controllers.Models
{
    public class TicketTypesModel
    {
        public string? Code { get; set; }
        [Required]
        public string? TicketName { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        public EnumTicketQualification Qualification { get; set; }
        [Required]
        public decimal PriceBrl { get; set; }
        [Required]
        public long TotalTickets { get; set; }
    }
}