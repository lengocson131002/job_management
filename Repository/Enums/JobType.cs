using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Enums
{
    public enum JobType
    {
        [Display(Name = "Part time")]
        PART_TIME,
        [Display(Name = "Full time")]
        FULL_TIME
    }
}
