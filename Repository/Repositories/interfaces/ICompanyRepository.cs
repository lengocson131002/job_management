using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Models;
using Repository.Utils;

namespace Repository.Repositories.interfaces
{
    public interface ICompanyRepository : IBaseRepository<Company>
    {
        Company? GetByName(string name);
        Company? GetByEmail(string email);
        public PageResult<Company> GetAll(
            string? query,
            int pageNumber,
            int pageSize
            );
    }
}
