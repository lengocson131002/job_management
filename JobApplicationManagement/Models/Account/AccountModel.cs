
using Repository.Enums;
using System.ComponentModel.DataAnnotations;

namespace JobApplicationManagement.Models.Auth
{
    public class AccountModel
    {
        public string Id { get; set; }

        public string FullName { get; set; }

        public string Username { get; set; }

        public string? Email { get; set; }

        public string? Phone { get; set; }

        public string? Description { get; set; }

        public AccountStatus Status { get; set; }

        public bool? IsRootAdmin { get; set; } = false;
    }
}
