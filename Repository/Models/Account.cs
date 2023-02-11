using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Models
{
    [Table("Account")]
    public class Account : BaseEntity
    {
        [Key]
        public string Id { get; set; }

        public string FullName { get; set; }

        public string Username { get; set; }
    
        public string Password { get; set; }

        public string? Email { get; set; }
        
        public string? Phone { get; set; }

        public string? Description { get; set; }
    }
}
