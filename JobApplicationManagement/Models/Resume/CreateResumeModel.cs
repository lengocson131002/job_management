using Repository.Enums;
using System.ComponentModel.DataAnnotations;

namespace JobApplicationManagement.Models.Resume
{
    public class CreateResumeModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email format")]
        public string Email { get; set; }
        [Required]
        [Phone(ErrorMessage = "Invalid Phone Number")]
        public string Phone { get; set; }
        [Required]
        public DateTime Birthday { get; set; }
        [Required]
        public IFormFile File { get; set; }

        public string? FileUrl { get; set; }

        [MaxLength(160, ErrorMessage = "Description is not over 160 characters")]
        public string? Description { get; set; }

        [Required]
        [MinLength(1, ErrorMessage = "Must choosing at least one skill")]
        public List<long> SkillIds { get; set; } = new List<long>();
    }
}
