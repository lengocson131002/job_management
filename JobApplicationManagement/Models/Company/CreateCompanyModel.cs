using Repository.Enums;
using System.ComponentModel.DataAnnotations;

namespace JobApplicationManagement.Models.Company
{
    public class CreateCompanyModel
    {
        [Key]
        public long Id { get; set; }
        [Required(ErrorMessage = "Name is requied!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is requied!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone is requied!")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Address is requied!")]
        public string Address { get; set; }

        public float? Longitude { get; set; }

        public float? Latitude { get; set; }

        [Required(ErrorMessage = "Country is requied!")]
        public string Country { get; set; }


        [Required(ErrorMessage = "Size of company is requied!")]
        [Range(1, Int32.MaxValue, ErrorMessage = "Size of company should be greater than 0")]
        public int? MinScale { get; set; } // Số lượng nhân viên tối thiểu

        public int? MaxScale { get; set; }   // Số lượng nhân viên tối đa

        [Required(ErrorMessage = "Description is requied!")]
        public string? Description { get; set; }

        [Required]
        public List<long> JobDescriptionsId { get; set; } = new List<long>();
    }
}
