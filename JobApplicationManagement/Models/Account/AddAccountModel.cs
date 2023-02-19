using System.ComponentModel.DataAnnotations;

namespace JobApplicationManagement.Models.Auth
{
    public class AddAccountModel
    {
        [Required]
        public string FullName { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        [Required]
        public string Phone { get; set; }

        public string? Description { get; set; }

    }
}
