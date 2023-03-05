using Repository.Utils;

namespace JobApplicationManagement.Models.Resume
{
    public class ViewResumesModel
    {
        public int PageSize { get; set; } = Constant.DEFAULT_PAGE_SIZE;
        public int PageNumber { get; set; } = Constant.DEFAULT_PAGE_NUMBER;
    }
}
