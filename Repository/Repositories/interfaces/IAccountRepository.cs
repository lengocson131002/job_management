using Repository.Enums;
using Repository.Models;
using Repository.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.interfaces
{
    public interface IAccountRepository : IBaseRepository<Account>
    {
        Account? GetByUsername(string username);
        public PageResult<Account> GetAll(
            string? query,
            AccountStatus? status,
            int pageNumber,
            int pageSize
            );
    }
}
