using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Models
{
    [Table("Application")]
    public class Application
    {
        [Key]
        public long Id { get; set; }

        public long JobDescriptionId { get; set; } 
        public JobDescription JobDescription { get; set; }

        public long ResumeId { get; set; }
        public Resume Resume { get; set; }
    }
}
