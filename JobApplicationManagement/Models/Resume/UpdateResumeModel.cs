using Repository.Enums;
using System.ComponentModel.DataAnnotations;

namespace JobApplicationManagement.Models.Resume
{
    public class UpdateResumeModel
    {
        [Required]
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [Phone]
        public string Phone { get; set; }
        [Required]
        public DateTime Birthday { get; set; }
        public string? FileName { get; set; }
        public string? FileUrl { get; set; }
        public IFormFile? UpdatedFile { get; set; }

        [MaxLength(160, ErrorMessage = "Description is not over 160 characters")]
        public string? Description { get; set; }

        [Required]
        [MinLength(1, ErrorMessage = "Must choosing at least one skill")]
        public List<long> SkillIds { get; set; } = new List<long>();
    }
}
