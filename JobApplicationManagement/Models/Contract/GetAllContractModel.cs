using Repository.Enums;
using Repository.Utils;

namespace JobApplicationManagement.Models.Contract
{
    public class GetAllContractModel
    {
        public string? Query { get; set; }
        public long? CompanyId { get; set; }
        public int PageSize { get; set; } = Constant.DEFAULT_PAGE_SIZE;
        public int PageNumber { get; set; } = Constant.DEFAULT_PAGE_NUMBER;
    }
}
