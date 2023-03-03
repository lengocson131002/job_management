using Repository.Models;
using Repository.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.interfaces
{
    public interface IResumeRepository : IBaseRepository<Resume>
    {
        public PageResult<Resume> GetAll(
            int pageNumber,
            int pageSize);
    }
}
