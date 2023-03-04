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
        [Required]
        public string ConfirmPassword { get; set; }
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        [Required]
        [RegularExpression(@"^(84|0[3|5|7|8|9])+([0-9]{8})$",
            ErrorMessage = "Invalid phone number")]
        public string Phone { get; set; }

        public string? Description { get; set; }

    }
}
