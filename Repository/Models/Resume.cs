using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Models
{
    [Table("Resume")]
    public class Resume : BaseEntity
    {
        [Key]
        public long Id { get; set; }    

        public string Name { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public DateTime Birthday { get; set; }

        public string FileUrl { get; set; }

        public string? Description { get; set; }

        public IList<Skill> Skills { get; set; } 

        public IList<JobDescription> JobDescriptions { get; set; }

    }
}
