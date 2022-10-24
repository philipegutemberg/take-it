using System.ComponentModel.DataAnnotations;

namespace Application.Controllers.Models
{
    public class NewUserModel
    {
        [Required]
        public string? Username { get; set; }
        [Required]
        public string? Password { get; set; }
        [Required]
        public string? FullName { get; set; }
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? Phone { get; set; }
        [Required]
        public string? WalletAddress { get; set; }
    }
}