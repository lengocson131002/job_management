using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class CompanyRepository : BaseRepository<Company>
    {
        public CompanyRepository(JobManagementDBContext _context) : base(_context)
        {
        }
    }
}
