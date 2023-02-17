using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Models
{
    [Table("Skill")]
    public class Skill : BaseEntity
    {
        [Key]
        public long Id { get; set; }

        public string Name { get; set; }    

        public string? Description { get; set; }

        public IList<JobDescription> JobDescriptions { get; set; }  

        public IList<Resume> Resumes { get; set; } 
    }
}
