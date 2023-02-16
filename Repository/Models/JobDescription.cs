using Repository.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Models
{
    [Table("JobDescription")]
    public class JobDescription : BaseEntity
    {
        [Key]
        public long Id { get; set; }

        public string Title { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string Description { get; set; }

        public int? RequiredNumber { get; set; }

        public long? MinSalary { get; set; }

        public long? MaxSalary { get; set; }

        public JobType Type { get; set; }

        public JobLevel Level { get; set; }

        public DateTime? ClosedAt { get; set; }

        public long CompanyId { get; set; }
        public Company Company { get; set; }

        public IList<Skill> Skills { get; set; }

        public IList<Resume> Resumes { get; set; }

    }
}
