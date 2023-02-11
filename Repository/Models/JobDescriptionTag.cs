using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Models
{
    [Table("JobDescription_Tag")]
    public class JobDescriptionTag
    {
        [Key]
        public long Id { get; set; }
        public long JobDescriptionId { get; set; }
        public JobDescription JobDescription { get; set; }

        public long TagId { get; set; }
        public Tag Tag { get; set; }
    }
}
