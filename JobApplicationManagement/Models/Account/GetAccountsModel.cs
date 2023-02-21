
using Repository.Enums;
using Repository.Utils;
using System.ComponentModel.DataAnnotations;

namespace JobApplicationManagement.Models.Auth
{
    public class GetAccountsModel
    {
        public string? Query { get; set; }
        public AccountStatus? Status { get; set; }
        public int PageSize { get; set; } = Constant.DEFAULT_PAGE_SIZE;
        public int PageNumber { get; set; } = Constant.DEFAULT_PAGE_NUMBER;

    }
}
