using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Enums
{
    public enum AccountStatus
    {
        [Display(Name = "Active")]
        ACTIVE,
        [Display(Name = "Inactive")]
        INACTIVE
    }
}
