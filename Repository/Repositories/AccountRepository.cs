using Microsoft.EntityFrameworkCore;
using Repository.Models;
using Repository.Repositories.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class AccountRepository : BaseRepository<Account>, IAccountRepository
    {
        public AccountRepository(JobManagementDBContext _context) : base(_context)
        {
        }

        public Account? GetByUsername(String username)
        {
            if (username == null)
            {
                return null;
            }

            return table.Where<Account>(a => a.Username == username).FirstOrDefault();
        }

    }
}
