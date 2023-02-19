using Repository.Enums;
using Repository.Utils;
using Job = Repository.Models.JobDescription;

namespace JobApplicationManagement.Models.JobDescription
{
    public class GetAllJobDescriptionModel
    {
        public string? Query { get; set; }
        public long? CompanyId { get; set; }
        public JobLevel? Level { get; set; }
        public JobType? Type { get; set; }
        public long? SkillId { get; set; }
        public int PageSize { get; set; } = Constant.DEFAULT_PAGE_SIZE;
        public int PageNumber { get; set; } = Constant.DEFAULT_PAGE_NUMBER;
    }
}
