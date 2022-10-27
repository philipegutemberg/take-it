using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Application.Controllers.Models
{
    public class NewSpecialUserModel
    {
        [Required]
        public string? Username { get; set; }
        [Required]
        public string? Password { get; set; }
        [Required]
        public EnumUserRole Role { get; set; }
    }
}