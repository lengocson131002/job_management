using Repository.Enums;
using Repository.Utils;

namespace JobApplicationManagement.Models.Company
{
    public class GetAllCompanyModel
    {
        public string? Query { get; set; }
        public int PageSize { get; set; } = Constant.DEFAULT_PAGE_SIZE;
        public int PageNumber { get; set; } = Constant.DEFAULT_PAGE_NUMBER;
    }
}
