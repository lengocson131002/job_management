using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Models
{
    [Table("Contract")]
    public class Contract : BaseEntity
    {
        [Key]
        public long Id { get; set; }

        public DateTime InterviewTime { get; set; }

        public string? Interviewer { get; set; }

        public long CompanyId { get; set; }
        public Company Company { get; set; }

        public long ResumeId { get; set; }
        public Resume Resume { get; set;}

        public Double offerSalary;      // Mức lương công ty offer

        public Double requestSalary;    // Mức lương ứng viên mong muốn
    }
}
