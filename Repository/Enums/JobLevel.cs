using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Enums
{
    public enum JobLevel
    {
        [Display(Name = "Internship")]
        INTERNSHIP,
        [Display(Name = "Fresher")]
        FRESHER,
        [Display(Name = "Junior")]
        JUNIOR,
        [Display(Name = "Senior")]
        SENIOR
    }
}
