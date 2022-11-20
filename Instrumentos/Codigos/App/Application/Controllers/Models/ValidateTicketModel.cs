using System.ComponentModel.DataAnnotations;

namespace Application.Controllers.Models
{
    public class ValidateTicketModel
    {
        [Required]
        public string? ValidationHash { get; set; }
    }
}