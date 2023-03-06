using Repository.Models;
using System.ComponentModel.DataAnnotations;

namespace JobApplicationManagement.Models.Contract
{
    public class CreateContractModel
    {
        public long CompanyId { get; set; }

        public string CompanyName { get; set; }


        public long ResumeId { get; set; }

        public string ResumeName  { get; set; }

        [Required]
        public DateTime InterviewTime { get; set; }
        
        public string? Interviewer { get; set; }

        [Required]
        [Range(1, Int64.MaxValue)]    
        public long OfferSalary { get; set; }

        [Required]
        [Range(1, Int64.MaxValue)]
        public long RequestSalary { get; set;}

        public long JobDescriptionId { get; set; }
    }
}
