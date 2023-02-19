using Repository.Enums;
using System.ComponentModel.DataAnnotations;

namespace JobApplicationManagement.Models.JobDescription
{
    public class UpdateJobDescriptionModel
    {
        public long Id { get; set; }

        [Required]
        public long CompanyId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Range(1, Int32.MaxValue)]
        public int? RequiredNumber { get; set; }

        [Range(1, Int32.MaxValue)]
        public long? MinSalary { get; set; }

        [Range(1, Int32.MaxValue)]
        public long? MaxSalary { get; set; }

        [Required]
        public JobType Type { get; set; }

        [Required]
        public JobLevel Level { get; set; }

        [DataType(DataType.Date)]
        public DateTime? ClosedAt { get; set; }

        [Required]
        public List<long> SkillIds { get; set; } = new List<long>();
    }
}
