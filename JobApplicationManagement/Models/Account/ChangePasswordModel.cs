
using Repository.Enums;
using System.ComponentModel.DataAnnotations;

namespace JobApplicationManagement.Models.Auth
{
    public class ChangePasswordModel
    {
        [Required]
        public string OldPassword { get; set; }
        [Required]
        public string NewPassword { get; set; }
        [Required]
        public string ConfirmPassword { get; set; }
    }
}
