using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Models
{
    [Table("Resume_Skill")]
    public class ResumeSkill
    {
        [Key]
        public long Id { get; set; }

        public long ResumeId { get; set; }
        public Resume Resume { get; set; }

        public long SkillId { get; set; }
        public Skill Skill { get; set; }
    }
}
