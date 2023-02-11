using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Models
{
    [Table("Company")]
    public class Company : BaseEntity
    {
        [Key]
        public long Id { get; set; }

        public string Name { get; set; }    

        public string Email { get; set; }

        public string Phone { get; set; }   

        public string Address { get; set; }

        public float? Longitude { get; set; }

        public float? Latitude { get; set; }

        public string Country { get; set; }

        public int? MinScale { get; set; } // Số lượng nhân viên tối thiểu

        public int? MaxScale { get; set; }   // Số lượng nhân viên tối đa

        public string? Description { get; set; }

        public IList<JobDescription> JobDescriptions { get; set; }

    }
}
